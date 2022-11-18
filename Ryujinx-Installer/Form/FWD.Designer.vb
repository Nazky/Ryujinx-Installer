<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FWD
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FWD))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.HopeProgressBar1 = New ReaLTaiizor.Controls.HopeProgressBar()
        Me.HopeButton2 = New ReaLTaiizor.Controls.HopeButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.HopeButton1 = New ReaLTaiizor.Controls.HopeButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ParrotFormEllipse1 = New ReaLTaiizor.Controls.ParrotFormEllipse()
        Me.ParrotControlEllipse1 = New ReaLTaiizor.Controls.ParrotControlEllipse()
        Me.ForeverComboBox1 = New ReaLTaiizor.Controls.ForeverComboBox()
        Me.ParrotControlEllipse2 = New ReaLTaiizor.Controls.ParrotControlEllipse()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ForeverComboBox1)
        Me.Panel1.Controls.Add(Me.HopeProgressBar1)
        Me.Panel1.Controls.Add(Me.HopeButton2)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 180)
        Me.Panel1.TabIndex = 0
        '
        'HopeProgressBar1
        '
        Me.HopeProgressBar1.BarColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeProgressBar1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeProgressBar1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeProgressBar1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.HopeProgressBar1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(252, Byte), Integer))
        Me.HopeProgressBar1.FullBallonColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeProgressBar1.FullBallonText = "Ok!"
        Me.HopeProgressBar1.FullBarColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeProgressBar1.IsError = False
        Me.HopeProgressBar1.Location = New System.Drawing.Point(12, 136)
        Me.HopeProgressBar1.Name = "HopeProgressBar1"
        Me.HopeProgressBar1.ProgressBarStyle = ReaLTaiizor.Controls.HopeProgressBar.Style.ToolTip
        Me.HopeProgressBar1.Size = New System.Drawing.Size(476, 32)
        Me.HopeProgressBar1.TabIndex = 6
        Me.HopeProgressBar1.Text = "HopeProgressBar1"
        Me.HopeProgressBar1.ValueNumber = 0
        Me.HopeProgressBar1.Visible = False
        '
        'HopeButton2
        '
        Me.HopeButton2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton2.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.HopeButton2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton2.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton2.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton2.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.HopeButton2.HoverTextColor = System.Drawing.Color.White
        Me.HopeButton2.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton2.Location = New System.Drawing.Point(146, 89)
        Me.HopeButton2.Name = "HopeButton2"
        Me.HopeButton2.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.HopeButton2.Size = New System.Drawing.Size(209, 41)
        Me.HopeButton2.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton2.TabIndex = 5
        Me.HopeButton2.Text = "Download"
        Me.HopeButton2.TextColor = System.Drawing.Color.White
        Me.HopeButton2.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(16, Byte), Integer), CType(CType(16, Byte), Integer), CType(CType(16, Byte), Integer))
        Me.Panel2.Controls.Add(Me.HopeButton1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(500, 37)
        Me.Panel2.TabIndex = 0
        '
        'HopeButton1
        '
        Me.HopeButton1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(220, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.HopeButton1.ButtonType = ReaLTaiizor.Util.HopeButtonType.Primary
        Me.HopeButton1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.HopeButton1.DangerColor = System.Drawing.Color.FromArgb(CType(CType(245, Byte), Integer), CType(CType(108, Byte), Integer), CType(CType(108, Byte), Integer))
        Me.HopeButton1.DefaultColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.HopeButton1.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.HopeButton1.HoverTextColor = System.Drawing.Color.White
        Me.HopeButton1.InfoColor = System.Drawing.Color.FromArgb(CType(CType(144, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(153, Byte), Integer))
        Me.HopeButton1.Location = New System.Drawing.Point(452, 0)
        Me.HopeButton1.Name = "HopeButton1"
        Me.HopeButton1.PrimaryColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.HopeButton1.Size = New System.Drawing.Size(48, 37)
        Me.HopeButton1.SuccessColor = System.Drawing.Color.FromArgb(CType(CType(103, Byte), Integer), CType(CType(194, Byte), Integer), CType(CType(58, Byte), Integer))
        Me.HopeButton1.TabIndex = 4
        Me.HopeButton1.Text = "X"
        Me.HopeButton1.TextColor = System.Drawing.Color.White
        Me.HopeButton1.WarningColor = System.Drawing.Color.FromArgb(CType(CType(230, Byte), Integer), CType(CType(162, Byte), Integer), CType(CType(60, Byte), Integer))
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(183, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Firmware-Downloader"
        '
        'ParrotFormEllipse1
        '
        Me.ParrotFormEllipse1.CornerRadius = 20
        Me.ParrotFormEllipse1.EffectedForm = Me
        '
        'ParrotControlEllipse1
        '
        Me.ParrotControlEllipse1.CornerRadius = 20
        Me.ParrotControlEllipse1.EffectedControl = Me.ForeverComboBox1
        '
        'ForeverComboBox1
        '
        Me.ForeverComboBox1.BaseColor = System.Drawing.Color.FromArgb(CType(CType(25, Byte), Integer), CType(CType(27, Byte), Integer), CType(CType(29, Byte), Integer))
        Me.ForeverComboBox1.BGColor = System.Drawing.Color.FromArgb(CType(CType(45, Byte), Integer), CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer))
        Me.ForeverComboBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ForeverComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.ForeverComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ForeverComboBox1.Font = New System.Drawing.Font("Segoe UI", 8.0!)
        Me.ForeverComboBox1.ForeColor = System.Drawing.Color.White
        Me.ForeverComboBox1.FormattingEnabled = True
        Me.ForeverComboBox1.HoverColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(158, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ForeverComboBox1.HoverFontColor = System.Drawing.Color.White
        Me.ForeverComboBox1.ItemHeight = 18
        Me.ForeverComboBox1.Location = New System.Drawing.Point(146, 59)
        Me.ForeverComboBox1.Name = "ForeverComboBox1"
        Me.ForeverComboBox1.Size = New System.Drawing.Size(209, 24)
        Me.ForeverComboBox1.TabIndex = 7
        '
        'ParrotControlEllipse2
        '
        Me.ParrotControlEllipse2.CornerRadius = 20
        Me.ParrotControlEllipse2.EffectedControl = Me.HopeButton2
        '
        'FWD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(500, 180)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "FWD"
        Me.Opacity = 0.99R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Firmware-Downloader"
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Label1 As Label
    Friend WithEvents HopeProgressBar1 As ReaLTaiizor.Controls.HopeProgressBar
    Friend WithEvents HopeButton2 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents HopeButton1 As ReaLTaiizor.Controls.HopeButton
    Friend WithEvents ParrotFormEllipse1 As ReaLTaiizor.Controls.ParrotFormEllipse
    Friend WithEvents ParrotControlEllipse1 As ReaLTaiizor.Controls.ParrotControlEllipse
    Friend WithEvents ForeverComboBox1 As ReaLTaiizor.Controls.ForeverComboBox
    Friend WithEvents ParrotControlEllipse2 As ReaLTaiizor.Controls.ParrotControlEllipse
End Class
