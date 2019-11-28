Imports System
Imports System.IO
Imports System.Windows.forms
Imports System.Text

Public Class frmMain
    Public Structure CurRecord
        Dim intDate As Integer
        Dim dateVal As DateTime
        Dim dblCurBid As Double
        Dim dblCurAsk As Double

        Dim dblValSource As Double
    End Structure


    Private Sub btnProcessCur_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcessCur.Click
        Dim Num1 As Integer = 0
        Dim boolFound1 As Boolean
        Dim intInterpolatedNum As Integer = 5

        Dim sr As StreamReader = New StreamReader(Me.txtCurFrom.Text.ToString)
        Dim lstSourceFileData As New List(Of CurRecord)
        Dim strLine As String

        Dim lngSourceFileLineCounter As Long = 0
        Dim boolEndSourceFile As Boolean = False

        Dim dblSpread As Double = CDbl(Me.txtCurSpread.Text.ToString)


        Dim dtInterpolatedMinLast As Date = CDate("1/1/1900")
        Dim curRecordInterpolated As CurRecord
        Dim curRecordInterpolatedNew As CurRecord
        Dim lstCurRecordInterpolated As New List(Of CurRecord)

        Dim dblVal1 As Double
        Dim dblVal2 As Double
        Dim dtVal1 As Date
        Dim strVal1 As String
        Dim intVal1 As Integer = 0


        Dim dt1995 As Date = CDate("1/1/1995")


        Dim lngMinVal As Long = 0
        Dim lngSecVal As Long = 0
        Dim lngDataFileName As Long = 0
        Dim lngDataFilenameOpen As Long = 0
        Dim strOutFile As String = ""

        Dim dblLastAddedBidVal As Double = 0

        Call sr.ReadLine.ToString()
        While boolEndSourceFile = False
            If lstSourceFileData.Count > 10 Then
                lstSourceFileData.RemoveAt(0)
            End If

            System.Windows.Forms.Application.DoEvents()       '   move this after the process loop when ready to compile
            lngSourceFileLineCounter = lngSourceFileLineCounter + 1
            strLine = sr.ReadLine.ToString

            Call ConvertSourceLineToData(strLine, lstSourceFileData, dblSpread, boolFound1)



            If boolFound1 = True Then
                If lstSourceFileData.Count >= intInterpolatedNum Then
                    '   this means it has at least five lines
                    curRecordInterpolated = lstSourceFileData(lstSourceFileData.Count - 3)
                    curRecordInterpolated.dblValSource = lstSourceFileData(lstSourceFileData.Count - 3).dblCurBid

                    If DateDiff("n", lstSourceFileData(lstSourceFileData.Count - 5).dateVal, lstSourceFileData(lstSourceFileData.Count - 1).dateVal) < 2 Then
                        dblVal2 = CDbl(Format(((lstSourceFileData(lstSourceFileData.Count - 1).dblCurBid + lstSourceFileData(lstSourceFileData.Count - 2).dblCurBid + lstSourceFileData(lstSourceFileData.Count - 4).dblCurBid + lstSourceFileData(lstSourceFileData.Count - 5).dblCurBid) / (intInterpolatedNum - 1)), Me.txtValMask.Text))
                        If Math.Abs(dblVal2 - lstSourceFileData(lstSourceFileData.Count - 3).dblCurBid) >= (3.5 * dblSpread) Then
                            curRecordInterpolated.dblCurBid = dblVal2
                        End If
                    End If


                    If dtInterpolatedMinLast = curRecordInterpolated.dateVal Then
                        lstCurRecordInterpolated.Add(curRecordInterpolated)
                    Else

                        dtInterpolatedMinLast = curRecordInterpolated.dateVal

                        If lstCurRecordInterpolated.Count = 0 Then
                            '   this means there are no records in the previous minute data.
                        Else
                            dblVal1 = 60 / (lstCurRecordInterpolated.Count + 1)

                            For Num1 = 0 To lstCurRecordInterpolated.Count - 1
                                curRecordInterpolatedNew = lstCurRecordInterpolated(Num1)
                                dtVal1 = DateAdd(DateInterval.Second, ((Num1 + 1) * dblVal1), lstCurRecordInterpolated(Num1).dateVal)
                                curRecordInterpolatedNew.dateVal = dtVal1
                                curRecordInterpolatedNew.dblCurAsk = curRecordInterpolatedNew.dblCurBid + dblSpread
                                lstCurRecordInterpolated(Num1) = curRecordInterpolatedNew
                            Next

                            dtVal1 = dt1995
                            For Num1 = lstCurRecordInterpolated.Count - 1 To 0 Step -1
                                If dtVal1 = lstCurRecordInterpolated(Num1).dateVal Then
                                    lstCurRecordInterpolated.RemoveAt(Num1)
                                End If
                                dtVal1 = lstCurRecordInterpolated(Num1).dateVal
                            Next

                            For Num1 = 0 To lstCurRecordInterpolated.Count - 1

                                If lstCurRecordInterpolated(Num1).dblCurBid <> dblLastAddedBidVal Then

                                    lngMinVal = DateDiff(DateInterval.Second, dt1995, lstCurRecordInterpolated(Num1).dateVal)
                                    lngDataFileName = lngMinVal - (lngMinVal Mod 100000)
                                    strOutFile = Me.txtCurTo.Text.ToString & "\" & lngDataFileName & ".txt"

                                    '   create the string out of the record
                                    Dim sbOut As New StringBuilder

                                    lngSecVal = DateDiff(DateInterval.Second, dt1995, lstCurRecordInterpolated(Num1).dateVal)

                                    sbOut.Append(lngSecVal.ToString)
                                    sbOut.Append("|")
                                    sbOut.Append(Format(lstCurRecordInterpolated(Num1).dblCurBid, Me.txtValMask.Text.ToString))
                                    sbOut.Append("|")
                                    sbOut.Append(Format(lstCurRecordInterpolated(Num1).dblCurAsk, Me.txtValMask.Text.ToString))

                                    If lstCurRecordInterpolated(Num1).dblValSource = lstCurRecordInterpolated(Num1).dblCurBid Then
                                        sbOut.Append("|   |")
                                        sbOut.Append(Format(lstCurRecordInterpolated(Num1).dblValSource, Me.txtValMask.Text.ToString))
                                    Else
                                        sbOut.Append("| * |")
                                        sbOut.Append(Format(lstCurRecordInterpolated(Num1).dblValSource, Me.txtValMask.Text.ToString))
                                    End If
                                    sbOut.Append("|")

                                    If lngDataFilenameOpen <> lngDataFileName Then
                                        '   put the raw information into the file.
                                        sbOut.Append(Format(lstCurRecordInterpolated(Num1).dateVal, "T"))
                                        sbOut.Append(" ")
                                        sbOut.Append(Format(lstCurRecordInterpolated(Num1).dateVal, "D"))
                                    Else
                                        sbOut.Append("0")
                                    End If
                                    '                                sbOut.Append(vbCrLf)

                                    strVal1 = sbOut.ToString
                                    '   now add them to the file.

                                    If lngDataFilenameOpen <> lngDataFileName Then
                                        '   open a new file
                                        Debug.Print(strVal1)

                                        Dim sw As StreamWriter = New StreamWriter(strOutFile, False)
                                        sw.WriteLine(strVal1)
                                        sw.Close()
                                        sw.Dispose()
                                        sw = Nothing

                                    Else
                                        '   append to the existing file
                                        Dim sw As StreamWriter = New StreamWriter(strOutFile, True)
                                        sw.WriteLine(strVal1)
                                        sw.Close()
                                        sw.Dispose()
                                        sw = Nothing
                                    End If

                                    lngDataFilenameOpen = lngDataFileName

                                End If

                                dblLastAddedBidVal = lstCurRecordInterpolated(Num1).dblCurBid

                            Next

                        End If



                        '   now clear the last minute data.
                        lstCurRecordInterpolated.Clear()

                        '   now add the current record to a new minute data list
                        lstCurRecordInterpolated.Add(curRecordInterpolated)

                    End If

                End If

            End If

            If sr.EndOfStream = True Then boolEndSourceFile = True
        End While





    End Sub


    Private Sub ConvertSourceLineToData(ByRef strSourceLine As String, ByVal lstSourceFileData As List(Of CurRecord), ByVal dblSpread As Double, ByRef boolFound As Boolean)
        Dim CurRecord As CurRecord
        Dim strVal1 As String
        Dim strVal2 As String
        Dim strVal3 As String
        Dim strVal4 As String
        Dim strVal5 As String

        Dim strLine As String
        Dim strDate As String
        Dim dtVal1 As Date

        Dim aryData1() As String

        Dim dblVal1 As Double
        boolFound = False

        strVal1 = Mid$(strSourceLine, 4, 2)
        strVal2 = Mid$(strSourceLine, 1, 2)
        strVal3 = Mid$(strSourceLine, 7, 4)
        If IsNumeric(strVal2) = False Then
            strLine = Mid$(strSourceLine, 2)
            strVal1 = Mid$(strSourceLine, 4, 2)
            strVal2 = Mid$(strSourceLine, 1, 2)
            strVal3 = Mid$(strSourceLine, 7, 4)
        End If

        dtVal1 = CDate(strVal1 & "/" & strVal2 & "/" & strVal3 & " 00:00:00")
        CurRecord.dateVal = DateAdd("s", (CLng(Mid$(strSourceLine, 12, 2)) * 3600) + (CLng(Mid$(strSourceLine, 14, 2)) * 60), dtVal1)

        aryData1 = Split(strSourceLine, ",")
        CurRecord.dblCurBid = aryData1(2)    '  the currency value

        lstSourceFileData.Add(CurRecord)
        boolFound = True
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub



    'performs a bubblesort on an object collection for the specified property
    Public Sub SortObjectList(ByRef List As List(Of CurRecord), ByVal min As Integer, _
        ByVal max As Integer, ByVal propName As String)

        Dim last_swap As Integer
        Dim i As Integer
        Dim j As Integer
        Dim tmp As Object

        ' Repeat until we are done.
        Do While min < max
            ' Bubble up.
            last_swap = min - 1
            ' For i = min + 1 To max
            i = min + 1
            Do While i <= max
                ' Find a bubble.
                If CallByName(List(i - 1), propName, CallType.Get) > CallByName(List(i), propName, CallType.Get) Then
                    ' See where to drop the bubble.
                    tmp = List(i - 1)
                    j = i
                    Do
                        List(j - 1) = List(j)
                        j = j + 1
                        If j > max Then Exit Do
                    Loop While CallByName(List(j), propName, CallType.Get) < CallByName(tmp, propName, CallType.Get)
                    List(j - 1) = tmp
                    last_swap = j - 1
                    i = j + 1
                Else
                    i = i + 1
                End If
            Loop
            ' Update max.
            max = last_swap - 1

            ' Bubble down.
            last_swap = max + 1
            ' For i = max - 1 To min Step -1
            i = max - 1
            Do While i >= min
                ' Find a bubble.
                If CallByName(List(i + 1), propName, CallType.Get) < CallByName(List(i), propName, CallType.Get) Then
                    ' See where to drop the bubble.
                    tmp = List(i + 1)
                    j = i
                    Do
                        List(j + 1) = List(j)
                        j = j - 1
                        If j < min Then Exit Do
                    Loop While CallByName(List(j), propName, CallType.Get) > CallByName(tmp, propName, CallType.Get)
                    List(j + 1) = tmp
                    last_swap = j + 1
                    i = j - 1
                Else
                    i = i - 1
                End If
            Loop
            ' Update min.
            min = last_swap + 1
        Loop
    End Sub







    Private Sub btnImportGAINData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportGAINData.Click
        Dim Num1 As Integer
        Dim Num2 As Integer
        Dim Num3 As Integer
        Dim boolFirst As Boolean = False
        Dim boolFirst1 As Boolean = False
        Dim boolDo As Boolean = False
        Dim boolEnd1 As Boolean = False
        Dim Counter1 As Integer = 0

        Dim lstFiles As New List(Of String)
        Dim lstSourceFileData As New List(Of CurRecord)
        Dim boolEndSourceFile As Boolean = False

        Dim dt1995 As Date = CDate("1/1/1995")

        Dim strLine As String
        Dim aryData() As String
        Dim dtLine As DateTime


        Dim lngSecVal As Long = 0
        Dim lngDataFilename As Long = 0
        Dim lngDataFilenameNew As Long = 0

        Dim lngDataFileValLast As Long = 0
        Dim lngDataFilenameWrite As Long = 0

        Dim strOutFile As String = ""

        Dim swWrite As StreamWriter
        Dim strVal1 As String = ""


        Dim fd1 As New DirectoryInfo(txtGAINSource.Text)
        If fd1.Exists = False Then
            Call MsgBox("No Directory!", MsgBoxStyle.Critical)
        Else
            Dim DirList1 As DirectoryInfo()
            DirList1 = fd1.GetDirectories()
            For Num1 = LBound(DirList1) To UBound(DirList1)
                Dim DirList2 As DirectoryInfo()
                DirList2 = DirList1(Num1).GetDirectories()
                For Num2 = LBound(DirList2) To UBound(DirList2)
                    Dim fileList As FileInfo()
                    fileList = DirList2(Num2).GetFiles()
                    For Num3 = LBound(fileList) To UBound(fileList)
                        If InStr(UCase(fileList(Num3).FullName), ".CSV") > 0 Then
                            lstFiles.Add(fileList(Num3).FullName)
                        End If

                    Next
                Next
            Next
        End If

        If lstFiles.Count > 0 Then
            lstFiles.Sort()

            For Num1 = 0 To lstFiles.Count - 1
                Dim sr As StreamReader = New StreamReader(lstFiles(Num1))

                '                If lstFiles(Num1) = "C:\Source\vs2005\MleckUtils.VBNet.v1\bin\Debug\DAT\Cur\raw\gain\EURUSD\2004\04\EUR_USD_Week4.csv" Then Stop
                '                Call sr.ReadLine.ToString()
                Debug.Print(lstFiles(Num1).ToString)
                boolEndSourceFile = False
                boolFirst = True
                While boolEndSourceFile = False
ReDo1:

                    Dim curRecord As CurRecord
                    strLine = sr.ReadLine.ToString

                    If Trim(strLine) = "" Then

                    Else
                        On Error GoTo ReDo1
                        aryData = Split(strLine, ",")
                        dtLine = CDate(aryData(2))

                        curRecord.dateVal = dtLine
                        curRecord.intDate = DateDiff(DateInterval.Second, dt1995, dtLine)
                        '                    If IsNumeric(aryData(3)) = True And IsNumeric(aryData(4)) = True Then
                        curRecord.dblCurBid = CDbl(Trim(Replace(aryData(3), """", "")))
                        curRecord.dblCurAsk = CDbl(Trim(Replace(aryData(4), """", "")))

                        If boolFirst = True Then
                            boolDo = True
                        Else
                            boolFirst = False
                            boolDo = True
                            If lstSourceFileData.Count > 0 Then
                                If lstSourceFileData(lstSourceFileData.Count - 1).dblCurBid = curRecord.dblCurBid And lstSourceFileData(lstSourceFileData.Count - 1).dblCurAsk = curRecord.dblCurAsk Then
                                    boolDo = False
                                End If
                            End If
                        End If


                        If boolDo = True Then
                            If lstSourceFileData.Count = 0 Then
                                lstSourceFileData.Add(curRecord)
                            Else
                                If curRecord.intDate > lstSourceFileData(lstSourceFileData.Count - 1).intDate Then
                                    lstSourceFileData.Add(curRecord)
                                Else
                                    If curRecord.intDate = lstSourceFileData(lstSourceFileData.Count - 1).intDate Then
                                        lstSourceFileData(lstSourceFileData.Count - 1) = curRecord
                                    Else
                                        Counter1 = lstSourceFileData.Count - 2
                                        boolEnd1 = False
                                        '                                        Debug.Print(lstSourceFileData(Counter1).intDate)
                                        While boolEnd1 = False
                                            If curRecord.intDate = lstSourceFileData(Counter1).intDate Then
                                                boolEnd1 = True

                                            ElseIf curRecord.intDate > lstSourceFileData(Counter1).intDate Then
                                                lstSourceFileData.Insert(Counter1 + 1, curRecord)
                                                boolEnd1 = True
                                            End If

                                            '                                            lstSourceFileData.Reverse()
                                            '                                            Stop
                                            '                                            Stop

                                            If boolEnd1 = False Then
                                                If Counter1 = 0 Then
                                                    '                                                    lstSourceFileData.Insert(0, curRecord)
                                                    boolEnd1 = True
                                                End If
                                            End If
                                            Counter1 = Counter1 - 1
                                        End While

                                    End If

                                End If

                            End If






                            boolFirst = False
                            lngSecVal = DateDiff(DateInterval.Second, dt1995, curRecord.dateVal)
                            If lngDataFileValLast = 0 Then
                                lngDataFileValLast = lngSecVal
                            End If


                            If lstSourceFileData.Count > 250000 Then


                                Counter1 = 0
                                boolEnd1 = False
                                While boolEnd1 = False
                                    On Error GoTo Redo2

                                    boolFirst1 = False
                                    If Counter1 > 200000 Then
                                        '                                        Debug.Print(((curRecord.intDate - lstSourceFileData(0).intDate) / 100000).ToString)
                                        lstSourceFileData.RemoveRange(0, Counter1 + 1)
                                        lstSourceFileData.TrimExcess()
                                        boolEnd1 = True
                                    Else
                                        lngDataFilenameNew = lstSourceFileData(Counter1).intDate - (lstSourceFileData(Counter1).intDate Mod 100000)
                                        If lngDataFilenameNew <> lngDataFilename Then
                                            boolFirst1 = True
                                            strOutFile = Me.txtGAINDestination.Text.ToString & "\" & lngDataFilenameNew & ".txt"

                                            If lngDataFilename <> 0 Then
                                                swWrite.Close()
                                                swWrite.Dispose()
                                                swWrite = Nothing
                                            End If

                                            Dim sw As StreamWriter = New StreamWriter(strOutFile, False)
                                            swWrite = sw
                                            lngDataFilename = lngDataFilenameNew
                                        End If

                                        '   create the string out of the record
                                        Dim sbOut As New StringBuilder

                                        sbOut.Append(lstSourceFileData(Counter1).intDate.ToString)
                                        sbOut.Append("|")
                                        sbOut.Append(Format(lstSourceFileData(Counter1).dblCurBid, Me.txtValMask.Text.ToString))
                                        sbOut.Append("|")
                                        sbOut.Append(Format(lstSourceFileData(Counter1).dblCurAsk, Me.txtValMask.Text.ToString))

                                        sbOut.Append("|   |")
                                        sbOut.Append("|")

                                        If boolFirst1 = True Then
                                            '   put the raw information into the file.
                                            sbOut.Append(Format(lstSourceFileData(Counter1).dateVal, "T"))
                                            sbOut.Append(" ")
                                            sbOut.Append(Format(lstSourceFileData(Counter1).dateVal, "D"))
                                        Else
                                            sbOut.Append("0")
                                        End If

                                        strVal1 = sbOut.ToString

                                        If boolFirst1 = True Then
                                            Debug.Print(strVal1)
                                        End If

                                        swWrite.WriteLine(strVal1)

                                    End If

Redo2:


                                    Counter1 = Counter1 + 1
                                End While

                            End If
                            'End If

                        End If

                    End If

                    If sr.EndOfStream = True Then boolEndSourceFile = True
                End While






            Next

            Stop

        End If

    End Sub
End Class