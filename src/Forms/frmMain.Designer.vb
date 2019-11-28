<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnProcessCur = New System.Windows.Forms.Button
        Me.txtCurFrom = New System.Windows.Forms.TextBox
        Me.txtCurTo = New System.Windows.Forms.TextBox
        Me.txtCurSpread = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtValMask = New System.Windows.Forms.TextBox
        Me.txtGAINDestination = New System.Windows.Forms.TextBox
        Me.txtGAINSource = New System.Windows.Forms.TextBox
        Me.btnImportGAINData = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'btnProcessCur
        '
        Me.btnProcessCur.Location = New System.Drawing.Point(12, 23)
        Me.btnProcessCur.Name = "btnProcessCur"
        Me.btnProcessCur.Size = New System.Drawing.Size(306, 23)
        Me.btnProcessCur.TabIndex = 0
        Me.btnProcessCur.Text = "Import DiskTrading Currency Data"
        Me.btnProcessCur.UseVisualStyleBackColor = True
        '
        'txtCurFrom
        '
        Me.txtCurFrom.Location = New System.Drawing.Point(13, 59)
        Me.txtCurFrom.Name = "txtCurFrom"
        Me.txtCurFrom.Size = New System.Drawing.Size(750, 20)
        Me.txtCurFrom.TabIndex = 1
        Me.txtCurFrom.Text = "C:\Source\vs2005\MleckUtils.VBNet.v1\bin\Debug\DAT\Cur\raw\disktrading\csv\AUDA0." & _
            "CSV\AUDA0.CSV"
        '
        'txtCurTo
        '
        Me.txtCurTo.Location = New System.Drawing.Point(12, 85)
        Me.txtCurTo.Name = "txtCurTo"
        Me.txtCurTo.Size = New System.Drawing.Size(751, 20)
        Me.txtCurTo.TabIndex = 2
        Me.txtCurTo.Text = "C:\Source\vs2005\MleckUtils.VBNet.v1\bin\Debug\DAT\Cur\Processed\USDAUD"
        '
        'txtCurSpread
        '
        Me.txtCurSpread.Location = New System.Drawing.Point(863, 59)
        Me.txtCurSpread.Name = "txtCurSpread"
        Me.txtCurSpread.Size = New System.Drawing.Size(123, 20)
        Me.txtCurSpread.TabIndex = 3
        Me.txtCurSpread.Text = "0.02"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(860, 43)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Pip Spread"
        '
        'txtValMask
        '
        Me.txtValMask.Location = New System.Drawing.Point(863, 85)
        Me.txtValMask.Name = "txtValMask"
        Me.txtValMask.Size = New System.Drawing.Size(123, 20)
        Me.txtValMask.TabIndex = 5
        Me.txtValMask.Text = "0.00"
        '
        'txtGAINDestination
        '
        Me.txtGAINDestination.Location = New System.Drawing.Point(13, 200)
        Me.txtGAINDestination.Name = "txtGAINDestination"
        Me.txtGAINDestination.Size = New System.Drawing.Size(750, 20)
        Me.txtGAINDestination.TabIndex = 8
        Me.txtGAINDestination.Text = "C:\Source\vs2005\MleckUtils.VBNet.v1\bin\Debug\DAT\Cur\raw\gain\txt\EURJPY"
        '
        'txtGAINSource
        '
        Me.txtGAINSource.Location = New System.Drawing.Point(14, 174)
        Me.txtGAINSource.Name = "txtGAINSource"
        Me.txtGAINSource.Size = New System.Drawing.Size(749, 20)
        Me.txtGAINSource.TabIndex = 7
        Me.txtGAINSource.Text = "C:\Source\vs2005\MleckUtils.VBNet.v1\bin\Debug\DAT\Cur\raw\gain\raw\EURJPY"
        '
        'btnImportGAINData
        '
        Me.btnImportGAINData.Location = New System.Drawing.Point(13, 138)
        Me.btnImportGAINData.Name = "btnImportGAINData"
        Me.btnImportGAINData.Size = New System.Drawing.Size(306, 23)
        Me.btnImportGAINData.TabIndex = 6
        Me.btnImportGAINData.Text = "Import GAIN Currency Data"
        Me.btnImportGAINData.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(998, 301)
        Me.Controls.Add(Me.txtGAINDestination)
        Me.Controls.Add(Me.txtGAINSource)
        Me.Controls.Add(Me.btnImportGAINData)
        Me.Controls.Add(Me.txtValMask)
        Me.Controls.Add(Me.txtCurSpread)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCurTo)
        Me.Controls.Add(Me.txtCurFrom)
        Me.Controls.Add(Me.btnProcessCur)
        Me.Name = "frmMain"
        Me.Text = "frmMain"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnProcessCur As System.Windows.Forms.Button
    Friend WithEvents txtCurFrom As System.Windows.Forms.TextBox
    Friend WithEvents txtCurTo As System.Windows.Forms.TextBox
    Friend WithEvents txtCurSpread As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtValMask As System.Windows.Forms.TextBox
    Friend WithEvents txtGAINDestination As System.Windows.Forms.TextBox
    Friend WithEvents txtGAINSource As System.Windows.Forms.TextBox
    Friend WithEvents btnImportGAINData As System.Windows.Forms.Button
End Class
