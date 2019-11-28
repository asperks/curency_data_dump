Imports System
Imports System.IO
Imports System.Collections
Imports OddSoft.Cmec.Client


Public Class clsAARuntime


    Public ProcForm As New frmMain()


    Public Sub Initialize()
        '   set up the root relationships.
        '        CFG.objParent = Me.Clone
        '        World.objParent = Me.Clone
        '       FuncGroup.objParent = Me.Clone
        '       Mlecks.objParent = Me.Clone



        ProcForm.Show()

        While ProcForm.Visible = True
            System.Windows.Forms.Application.DoEvents()       '   move this after the process loop when ready to compile
        End While

        End



    End Sub

    Public Sub Terminate()

    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
