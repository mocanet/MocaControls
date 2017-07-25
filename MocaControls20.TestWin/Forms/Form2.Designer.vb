<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
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

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form2))
        Me.DebugMarker1 = New Moca.Win.DebugMarker()
        Me.MocaDi1 = New Moca.Win.MocaDi()
        Me.AlertMessage1 = New Moca.Win.AlertMessage()
        Me.RadioButtonGroup1 = New Moca.Win.RadioButtonGroup(Me.components)
        Me.RichTextBoxEx1 = New Moca.Win.RichTextBoxEx()
        Me.RadioButtonEx1 = New Moca.Win.RadioButtonEx()
        Me.SuspendLayout()
        '
        'DebugMarker1
        '
        Me.DebugMarker1.BackColor = System.Drawing.Color.Transparent
        Me.DebugMarker1.BackgroundImage = CType(resources.GetObject("DebugMarker1.BackgroundImage"), System.Drawing.Image)
        Me.DebugMarker1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.DebugMarker1.Location = New System.Drawing.Point(27, 8)
        Me.DebugMarker1.MaximumSize = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.Name = "DebugMarker1"
        Me.DebugMarker1.Size = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.TabIndex = 0
        Me.DebugMarker1.TabStop = False
        Me.DebugMarker1.ToolTipText = Nothing
        '
        'MocaDi1
        '
        Me.MocaDi1.AutoSize = True
        Me.MocaDi1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MocaDi1.BackgroundImage = CType(resources.GetObject("MocaDi1.BackgroundImage"), System.Drawing.Image)
        Me.MocaDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MocaDi1.Location = New System.Drawing.Point(8, 8)
        Me.MocaDi1.Margin = New System.Windows.Forms.Padding(0)
        Me.MocaDi1.MaximumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.Name = "MocaDi1"
        Me.MocaDi1.Size = New System.Drawing.Size(16, 16)
        Me.MocaDi1.TabIndex = 1
        Me.MocaDi1.TabStop = False
        '
        'AlertMessage1
        '
        Me.AlertMessage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.AlertMessage1.DefaultMessageBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.AlertMessage1.DefaultMessageForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.AlertMessage1.ErrorBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.AlertMessage1.ErrorForeColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.AlertMessage1.Location = New System.Drawing.Point(9, 48)
        Me.AlertMessage1.Margin = New System.Windows.Forms.Padding(0)
        Me.AlertMessage1.Name = "AlertMessage1"
        Me.AlertMessage1.Size = New System.Drawing.Size(258, 79)
        Me.AlertMessage1.SuccessBackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.AlertMessage1.SuccessForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.AlertMessage1.TabIndex = 2
        Me.AlertMessage1.WarnBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.AlertMessage1.WarnForeColor = System.Drawing.Color.FromArgb(CType(CType(145, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(53, Byte), Integer))
        '
        'RadioButtonGroup1
        '
        Me.RadioButtonGroup1.SelectedButton = Nothing
        Me.RadioButtonGroup1.SelectedValue = Nothing
        '
        'RichTextBoxEx1
        '
        Me.RichTextBoxEx1.BottomBorderColor = System.Drawing.Color.Empty
        Me.RichTextBoxEx1.Location = New System.Drawing.Point(147, 153)
        Me.RichTextBoxEx1.Name = "RichTextBoxEx1"
        Me.RichTextBoxEx1.Size = New System.Drawing.Size(120, 96)
        Me.RichTextBoxEx1.TabIndex = 4
        Me.RichTextBoxEx1.Text = ""
        '
        'RadioButtonEx1
        '
        Me.RadioButtonEx1.ActiveBottomBorderColor = System.Drawing.Color.Empty
        Me.RadioButtonEx1.BottomBorderColor = System.Drawing.Color.Empty
        Me.RadioButtonEx1.BottomBorderHeight = 3
        Me.RadioButtonEx1.Location = New System.Drawing.Point(9, 162)
        Me.RadioButtonEx1.Name = "RadioButtonEx1"
        Me.RadioButtonEx1.Size = New System.Drawing.Size(114, 24)
        Me.RadioButtonEx1.TabIndex = 5
        Me.RadioButtonEx1.TabStop = True
        Me.RadioButtonEx1.Text = "RadioButtonEx1"
        Me.RadioButtonEx1.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.DebugMarker1)
        Me.Controls.Add(Me.RadioButtonEx1)
        Me.Controls.Add(Me.RichTextBoxEx1)
        Me.Controls.Add(Me.AlertMessage1)
        Me.Controls.Add(Me.MocaDi1)
        Me.Name = "Form2"
        Me.Text = "Form2"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DebugMarker1 As Moca.Win.DebugMarker
    Friend WithEvents MocaDi1 As Moca.Win.MocaDi
    Friend WithEvents AlertMessage1 As Moca.Win.AlertMessage
    Friend WithEvents RadioButtonGroup1 As Moca.Win.RadioButtonGroup
    Friend WithEvents RichTextBoxEx1 As Moca.Win.RichTextBoxEx
    Friend WithEvents RadioButtonEx1 As Moca.Win.RadioButtonEx
End Class
