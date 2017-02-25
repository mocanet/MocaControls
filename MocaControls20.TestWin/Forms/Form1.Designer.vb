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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridDesignSettings1 As Moca.GridDesignSettings = New Moca.GridDesignSettings()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridDesignSettings2 As Moca.GridDesignSettings = New Moca.GridDesignSettings()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.RadioButtonEx2 = New Moca.Win.RadioButtonEx()
        Me.RadioButtonEx1 = New Moca.Win.RadioButtonEx()
        Me.ComboBoxEx1 = New Moca.Win.ComboBoxEx()
        Me.ContextMenuPanel1 = New Moca.Win.ContextMenuPanel()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.ModelGridView1 = New Moca.Win.ModelGridView()
        Me.TextBoxEx2 = New Moca.Win.TextBoxEx(Me.components)
        Me.FlowLayoutPanelEx1 = New Moca.Win.FlowLayoutPanelEx()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.NullableDateTimePicker1 = New Moca.Win.NullableDateTimePicker()
        Me.TextBoxEx1 = New Moca.Win.TextBoxEx(Me.components)
        Me.DataBinder1 = New Moca.Win.DataBinder(Me.components)
        Me.DataGridViewEx1 = New Moca.Win.DataGridViewEx()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuPanel1.SuspendLayout()
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanelEx1.SuspendLayout()
        CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioButtonEx2)
        Me.Panel1.Controls.Add(Me.RadioButtonEx1)
        Me.Panel1.Location = New System.Drawing.Point(213, 682)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(370, 100)
        Me.Panel1.TabIndex = 10
        '
        'RadioButtonEx2
        '
        Me.RadioButtonEx2.ActiveBottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx2.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonEx2.BottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx2.BottomBorderHeight = 3
        Me.RadioButtonEx2.FlatAppearance.BorderSize = 0
        Me.RadioButtonEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButtonEx2.Location = New System.Drawing.Point(132, 23)
        Me.RadioButtonEx2.Name = "RadioButtonEx2"
        Me.RadioButtonEx2.Size = New System.Drawing.Size(106, 54)
        Me.RadioButtonEx2.TabIndex = 10
        Me.RadioButtonEx2.TabStop = True
        Me.RadioButtonEx2.Text = "Radio 1"
        Me.RadioButtonEx2.UseVisualStyleBackColor = True
        '
        'RadioButtonEx1
        '
        Me.RadioButtonEx1.ActiveBottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx1.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonEx1.BottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx1.BottomBorderHeight = 3
        Me.RadioButtonEx1.FlatAppearance.BorderSize = 0
        Me.RadioButtonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButtonEx1.Location = New System.Drawing.Point(13, 20)
        Me.RadioButtonEx1.Name = "RadioButtonEx1"
        Me.RadioButtonEx1.Size = New System.Drawing.Size(106, 54)
        Me.RadioButtonEx1.TabIndex = 9
        Me.RadioButtonEx1.TabStop = True
        Me.RadioButtonEx1.Text = "Radio 1"
        Me.RadioButtonEx1.UseVisualStyleBackColor = True
        '
        'ComboBoxEx1
        '
        Me.ComboBoxEx1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.ComboBoxEx1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.Solid
        Me.ComboBoxEx1.FormattingEnabled = True
        Me.ComboBoxEx1.Items.AddRange(New Object() {"", "Hoge1", "Hoge2"})
        Me.ComboBoxEx1.Location = New System.Drawing.Point(565, 21)
        Me.ComboBoxEx1.Name = "ComboBoxEx1"
        Me.ComboBoxEx1.Size = New System.Drawing.Size(209, 32)
        Me.ComboBoxEx1.TabIndex = 11
        Me.ComboBoxEx1.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'ContextMenuPanel1
        '
        Me.ContextMenuPanel1.Controls.Add(Me.Button4)
        Me.ContextMenuPanel1.DirectionType = Moca.Win.AnimateWindow.DirectionType.Top
        Me.ContextMenuPanel1.Location = New System.Drawing.Point(53, 26)
        Me.ContextMenuPanel1.Name = "ContextMenuPanel1"
        Me.ContextMenuPanel1.Opener = Me.Button1
        Me.ContextMenuPanel1.Size = New System.Drawing.Size(236, 100)
        Me.ContextMenuPanel1.TabIndex = 7
        Me.ContextMenuPanel1.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(3, 3)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(163, 41)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "btn1"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Location = New System.Drawing.Point(184, 6)
        Me.Button1.Margin = New System.Windows.Forms.Padding(7, 6, 7, 6)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(163, 46)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ModelGridView1
        '
        Me.ModelGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Meiryo UI", 9.0!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ModelGridView1.DefaultCellStyle = DataGridViewCellStyle1
        GridDesignSettings1.SettingsKey = ""
        Me.ModelGridView1.DesignSettings = GridDesignSettings1
        Me.ModelGridView1.Location = New System.Drawing.Point(13, 152)
        Me.ModelGridView1.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.ModelGridView1.Name = "ModelGridView1"
        Me.ModelGridView1.RowEditImage = CType(resources.GetObject("ModelGridView1.RowEditImage"), System.Drawing.Image)
        Me.ModelGridView1.RowEntityType = Nothing
        Me.ModelGridView1.RowTemplate.Height = 33
        Me.ModelGridView1.Size = New System.Drawing.Size(984, 516)
        Me.ModelGridView1.Styles = CType(resources.GetObject("ModelGridView1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.ModelGridView1.TabIndex = 8
        Me.ModelGridView1.TransparentRowSelection = False
        '
        'TextBoxEx2
        '
        Me.TextBoxEx2.BottomBorderColor = System.Drawing.Color.Empty
        Me.TextBoxEx2.CustomChars = Nothing
        Me.TextBoxEx2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextBoxEx2.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx2.Location = New System.Drawing.Point(1051, 68)
        Me.TextBoxEx2.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.TextBoxEx2.Name = "TextBoxEx2"
        Me.TextBoxEx2.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx2.NumericScale = 0
        Me.TextBoxEx2.Precision = 0
        Me.TextBoxEx2.PrecisionSign = False
        Me.TextBoxEx2.Required = False
        Me.TextBoxEx2.Separator = ""
        Me.TextBoxEx2.Size = New System.Drawing.Size(173, 32)
        Me.TextBoxEx2.TabIndex = 7
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
        Me.TextBoxEx1.CustomChars = Nothing
        Me.TextBoxEx1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBoxEx1.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx1.Location = New System.Drawing.Point(500, 34)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx1.NumericScale = 0
        Me.TextBoxEx1.Precision = 0
        Me.TextBoxEx1.PrecisionSign = False
        Me.TextBoxEx1.ReadOnly = True
        Me.TextBoxEx1.Required = False
        Me.TextBoxEx1.Separator = ""
        Me.TextBoxEx1.Size = New System.Drawing.Size(282, 92)
        Me.TextBoxEx1.TabIndex = 3
        Me.TextBoxEx1.TabStop = False
        '
        'DataBinder1
        '
        Me.DataBinder1.DataMember = ""
        Me.DataBinder1.DataSource = Nothing
        Me.DataBinder1.Position = -1
        '
        'DataGridViewEx1
        '
        Me.DataGridViewEx1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEx1.DBInfoColumns = Nothing
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Meiryo UI", 9.0!)
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewEx1.DefaultCellStyle = DataGridViewCellStyle2
        GridDesignSettings2.SettingsKey = ""
        Me.DataGridViewEx1.DesignSettings = GridDesignSettings2
        Me.DataGridViewEx1.HorizontalScrollBarAlwaysShow = False
        Me.DataGridViewEx1.Location = New System.Drawing.Point(1011, 152)
        Me.DataGridViewEx1.Name = "DataGridViewEx1"
        Me.DataGridViewEx1.RowEditImage = CType(resources.GetObject("DataGridViewEx1.RowEditImage"), System.Drawing.Image)
        Me.DataGridViewEx1.RowTemplate.Height = 33
        Me.DataGridViewEx1.Size = New System.Drawing.Size(774, 516)
        Me.DataGridViewEx1.Styles = CType(resources.GetObject("DataGridViewEx1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.DataGridViewEx1.TabIndex = 12
        Me.DataGridViewEx1.VerticalScrollBarAlwaysShow = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1797, 824)
        Me.Controls.Add(Me.DataGridViewEx1)
        Me.Controls.Add(Me.ComboBoxEx1)
        Me.Controls.Add(Me.ContextMenuPanel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ModelGridView1)
        Me.Controls.Add(Me.TextBoxEx2)
        Me.Controls.Add(Me.FlowLayoutPanelEx1)
        Me.Controls.Add(Me.NullableDateTimePicker1)
        Me.Controls.Add(Me.TextBoxEx1)
        Me.Controls.Add(Me.TextBox1)
        Me.Margin = New System.Windows.Forms.Padding(2, 4, 2, 4)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.Panel1.ResumeLayout(False)
        Me.ContextMenuPanel1.ResumeLayout(False)
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanelEx1.ResumeLayout(False)
        CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents TextBoxEx2 As Moca.Win.TextBoxEx
    Friend WithEvents ModelGridView1 As Moca.Win.ModelGridView
    Friend WithEvents RadioButtonEx1 As Moca.Win.RadioButtonEx
    Friend WithEvents Panel1 As Panel
    Friend WithEvents RadioButtonEx2 As Moca.Win.RadioButtonEx
    Friend WithEvents ContextMenuPanel1 As Moca.Win.ContextMenuPanel
    Friend WithEvents Button4 As Button
    Friend WithEvents ComboBoxEx1 As Moca.Win.ComboBoxEx
    Friend WithEvents DataBinder1 As Moca.Win.DataBinder
    Friend WithEvents DataGridViewEx1 As Moca.Win.DataGridViewEx
End Class
