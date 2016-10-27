<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.FlowLayoutPanelEx1 = New Moca.Win.FlowLayoutPanelEx()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.NullableDateTimePicker1 = New Moca.Win.NullableDateTimePicker()
        Me.TextBoxEx1 = New Moca.Win.TextBoxEx(Me.components)
        Me.ModelGridView1 = New Moca.Win.ModelGridView()
        Me.FlowLayoutPanelEx1.SuspendLayout()
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(869, 82)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(99, 31)
        Me.TextBox1.TabIndex = 2
        '
        'FlowLayoutPanelEx1
        '
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanelEx1.Location = New System.Drawing.Point(650, 676)
        Me.FlowLayoutPanelEx1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.FlowLayoutPanelEx1.Name = "FlowLayoutPanelEx1"
        Me.FlowLayoutPanelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FlowLayoutPanelEx1.Size = New System.Drawing.Size(624, 94)
        Me.FlowLayoutPanelEx1.TabIndex = 5
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(7, 6)
        Me.Button3.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Button3.Name = "Button3"
        Me.Button3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button3.Size = New System.Drawing.Size(163, 46)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(184, 6)
        Me.Button1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(163, 46)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(361, 6)
        Me.Button2.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(163, 46)
        Me.Button2.TabIndex = 6
        Me.Button2.Text = "Button2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'NullableDateTimePicker1
        '
        Me.NullableDateTimePicker1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.NullableDateTimePicker1.BorderColor = System.Drawing.Color.White
        Me.NullableDateTimePicker1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.NullableDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Long]
        Me.NullableDateTimePicker1.FormatAsString = "yyyy'年'M'月'd'日'"
        Me.NullableDateTimePicker1.Location = New System.Drawing.Point(884, 12)
        Me.NullableDateTimePicker1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.NullableDateTimePicker1.Name = "NullableDateTimePicker1"
        Me.NullableDateTimePicker1.NullValue = ""
        Me.NullableDateTimePicker1.Size = New System.Drawing.Size(247, 31)
        Me.NullableDateTimePicker1.TabIndex = 4
        Me.NullableDateTimePicker1.UnfocusedBorderColor = System.Drawing.Color.White
        Me.NullableDateTimePicker1.Value = New Date(2016, 10, 6, 23, 50, 18, 238)
        '
        'TextBoxEx1
        '
        Me.TextBoxEx1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEx1.BottomBorderColor = System.Drawing.Color.Blue
        Me.TextBoxEx1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBoxEx1.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx1.Location = New System.Drawing.Point(156, 34)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx1.NumericScale = 0
        Me.TextBoxEx1.Percision = 0
        Me.TextBoxEx1.PercisionSign = False
        Me.TextBoxEx1.ReadOnly = True
        Me.TextBoxEx1.Separator = ""
        Me.TextBoxEx1.Size = New System.Drawing.Size(626, 92)
        Me.TextBoxEx1.TabIndex = 3
        Me.TextBoxEx1.TabStop = False
        '
        'ModelGridView1
        '
        Me.ModelGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ModelGridView1.DesignSettings = Nothing
        Me.ModelGridView1.Location = New System.Drawing.Point(12, 181)
        Me.ModelGridView1.Name = "ModelGridView1"
        Me.ModelGridView1.RowEntityType = Nothing
        Me.ModelGridView1.RowTemplate.Height = 33
        Me.ModelGridView1.Size = New System.Drawing.Size(1317, 486)
        Me.ModelGridView1.Styles = CType(resources.GetObject("ModelGridView1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.ModelGridView1.TabIndex = 6
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1341, 824)
        Me.Controls.Add(Me.ModelGridView1)
        Me.Controls.Add(Me.FlowLayoutPanelEx1)
        Me.Controls.Add(Me.NullableDateTimePicker1)
        Me.Controls.Add(Me.TextBoxEx1)
        Me.Controls.Add(Me.TextBox1)
        Me.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.FlowLayoutPanelEx1.ResumeLayout(False)
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBoxEx1 As Moca.Win.TextBoxEx
    Friend WithEvents NullableDateTimePicker1 As Moca.Win.NullableDateTimePicker
    Friend WithEvents FlowLayoutPanelEx1 As Moca.Win.FlowLayoutPanelEx
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents ModelGridView1 As Moca.Win.ModelGridView
End Class
