﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.TextBoxEx3 = New Moca.Win.TextBoxEx(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.AlertMessage1 = New Moca.Win.AlertMessage()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.DebugMarker1 = New Moca.Win.DebugMarker()
        Me.MocaDi1 = New Moca.Win.MocaDi()
        Me.Panel1.SuspendLayout()
        Me.ContextMenuPanel1.SuspendLayout()
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanelEx1.SuspendLayout()
        CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(401, 41)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(48, 19)
        Me.TextBox1.TabIndex = 2
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioButtonEx2)
        Me.Panel1.Controls.Add(Me.RadioButtonEx1)
        Me.Panel1.Location = New System.Drawing.Point(98, 341)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(171, 50)
        Me.Panel1.TabIndex = 10
        '
        'RadioButtonEx2
        '
        Me.RadioButtonEx2.ActiveBottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx2.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButtonEx2.BottomBorderColor = System.Drawing.Color.Blue
        Me.RadioButtonEx2.BottomBorderHeight = 2
        Me.RadioButtonEx2.FlatAppearance.BorderSize = 0
        Me.RadioButtonEx2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButtonEx2.Location = New System.Drawing.Point(61, 12)
        Me.RadioButtonEx2.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.RadioButtonEx2.Name = "RadioButtonEx2"
        Me.RadioButtonEx2.Size = New System.Drawing.Size(49, 27)
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
        Me.RadioButtonEx1.BottomBorderHeight = 2
        Me.RadioButtonEx1.FlatAppearance.BorderSize = 0
        Me.RadioButtonEx1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButtonEx1.Location = New System.Drawing.Point(6, 10)
        Me.RadioButtonEx1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.RadioButtonEx1.Name = "RadioButtonEx1"
        Me.RadioButtonEx1.Size = New System.Drawing.Size(49, 27)
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
        Me.ComboBoxEx1.Location = New System.Drawing.Point(261, 10)
        Me.ComboBoxEx1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.ComboBoxEx1.Name = "ComboBoxEx1"
        Me.ComboBoxEx1.Size = New System.Drawing.Size(99, 20)
        Me.ComboBoxEx1.TabIndex = 11
        Me.ComboBoxEx1.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'ContextMenuPanel1
        '
        Me.ContextMenuPanel1.Controls.Add(Me.Button4)
        Me.ContextMenuPanel1.DirectionType = Moca.Win.AnimateWindow.DirectionType.Top
        Me.ContextMenuPanel1.Location = New System.Drawing.Point(24, 13)
        Me.ContextMenuPanel1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.ContextMenuPanel1.Name = "ContextMenuPanel1"
        Me.ContextMenuPanel1.Opener = Me.Button1
        Me.ContextMenuPanel1.Size = New System.Drawing.Size(109, 50)
        Me.ContextMenuPanel1.TabIndex = 7
        Me.ContextMenuPanel1.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(1, 2)
        Me.Button4.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 20)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "btn1"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Location = New System.Drawing.Point(84, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ModelGridView1
        '
        Me.ModelGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.ModelGridView1.Location = New System.Drawing.Point(6, 76)
        Me.ModelGridView1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.ModelGridView1.Name = "ModelGridView1"
        Me.ModelGridView1.RowEditImage = CType(resources.GetObject("ModelGridView1.RowEditImage"), System.Drawing.Image)
        Me.ModelGridView1.RowEntityType = Nothing
        Me.ModelGridView1.RowTemplate.Height = 33
        Me.ModelGridView1.Size = New System.Drawing.Size(672, 235)
        Me.ModelGridView1.Styles = CType(resources.GetObject("ModelGridView1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.ModelGridView1.TabIndex = 8
        Me.ModelGridView1.TransparentRowSelection = False
        '
        'TextBoxEx2
        '
        Me.TextBoxEx2.BackColor = System.Drawing.Color.White
        Me.TextBoxEx2.BorderColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TextBoxEx2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBoxEx2.BottomBorderColor = System.Drawing.Color.Empty
        Me.TextBoxEx2.CustomChars = Nothing
        Me.TextBoxEx2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.TextBoxEx2.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx2.Location = New System.Drawing.Point(497, 29)
        Me.TextBoxEx2.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.TextBoxEx2.Name = "TextBoxEx2"
        Me.TextBoxEx2.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx2.Separator = ""
        Me.TextBoxEx2.Size = New System.Drawing.Size(82, 18)
        Me.TextBoxEx2.TabIndex = 7
        Me.TextBoxEx2.TextChangedCompleteDelay = 1300
        Me.TextBoxEx2.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'FlowLayoutPanelEx1
        '
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button3)
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button1)
        Me.FlowLayoutPanelEx1.Controls.Add(Me.Button2)
        Me.FlowLayoutPanelEx1.Location = New System.Drawing.Point(300, 338)
        Me.FlowLayoutPanelEx1.Name = "FlowLayoutPanelEx1"
        Me.FlowLayoutPanelEx1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.FlowLayoutPanelEx1.Size = New System.Drawing.Size(252, 47)
        Me.FlowLayoutPanelEx1.TabIndex = 5
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(3, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 6
        Me.Button3.Text = "Button3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(165, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
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
        Me.NullableDateTimePicker1.Location = New System.Drawing.Point(408, 6)
        Me.NullableDateTimePicker1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.NullableDateTimePicker1.Name = "NullableDateTimePicker1"
        Me.NullableDateTimePicker1.NullValue = ""
        Me.NullableDateTimePicker1.Size = New System.Drawing.Size(116, 19)
        Me.NullableDateTimePicker1.TabIndex = 4
        Me.NullableDateTimePicker1.UnfocusedBorderColor = System.Drawing.Color.White
        Me.NullableDateTimePicker1.Value = New Date(2016, 10, 6, 23, 50, 18, 238)
        '
        'TextBoxEx1
        '
        Me.TextBoxEx1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEx1.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.TextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEx1.BottomBorderColor = System.Drawing.Color.Blue
        Me.TextBoxEx1.CustomChars = Nothing
        Me.TextBoxEx1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBoxEx1.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx1.Location = New System.Drawing.Point(231, 17)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx1.ReadOnly = True
        Me.TextBoxEx1.Separator = ""
        Me.TextBoxEx1.Size = New System.Drawing.Size(130, 46)
        Me.TextBoxEx1.TabIndex = 3
        Me.TextBoxEx1.TabStop = False
        Me.TextBoxEx1.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'DataBinder1
        '
        Me.DataBinder1.DataMember = ""
        Me.DataBinder1.DataSource = Nothing
        Me.DataBinder1.Position = -1
        '
        'DataGridViewEx1
        '
        Me.DataGridViewEx1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.DataGridViewEx1.Location = New System.Drawing.Point(680, 76)
        Me.DataGridViewEx1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.DataGridViewEx1.Name = "DataGridViewEx1"
        Me.DataGridViewEx1.RowEditImage = CType(resources.GetObject("DataGridViewEx1.RowEditImage"), System.Drawing.Image)
        Me.DataGridViewEx1.RowTemplate.Height = 33
        Me.DataGridViewEx1.Size = New System.Drawing.Size(262, 235)
        Me.DataGridViewEx1.Styles = CType(resources.GetObject("DataGridViewEx1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.DataGridViewEx1.TabIndex = 12
        Me.DataGridViewEx1.VerticalScrollBarAlwaysShow = False
        '
        'TextBoxEx3
        '
        Me.TextBoxEx3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.TextBoxEx3.BottomBorderColor = System.Drawing.Color.Empty
        Me.TextBoxEx3.CustomChars = Nothing
        Me.TextBoxEx3.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBoxEx3.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx3.Location = New System.Drawing.Point(603, 6)
        Me.TextBoxEx3.Name = "TextBoxEx3"
        Me.TextBoxEx3.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx3.Separator = ""
        Me.TextBoxEx3.Size = New System.Drawing.Size(149, 26)
        Me.TextBoxEx3.TabIndex = 13
        Me.TextBoxEx3.Text = "TextBoxEx3"
        Me.TextBoxEx3.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(619, 351)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 12)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Label1"
        '
        'AlertMessage1
        '
        Me.AlertMessage1.BackColor = System.Drawing.Color.FromArgb(CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer), CType(CType(186, Byte), Integer))
        Me.AlertMessage1.DefaultMessageBackColor = System.Drawing.Color.FromArgb(CType(CType(236, Byte), Integer), CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer))
        Me.AlertMessage1.DefaultMessageForeColor = System.Drawing.Color.FromArgb(CType(CType(9, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.AlertMessage1.DirectionType = Moca.Win.AnimateWindow.DirectionType.Bottom
        Me.AlertMessage1.ErrorBackColor = System.Drawing.Color.FromArgb(CType(CType(242, Byte), Integer), CType(CType(222, Byte), Integer), CType(CType(222, Byte), Integer))
        Me.AlertMessage1.ErrorForeColor = System.Drawing.Color.FromArgb(CType(CType(185, Byte), Integer), CType(CType(74, Byte), Integer), CType(CType(72, Byte), Integer))
        Me.AlertMessage1.Location = New System.Drawing.Point(312, 451)
        Me.AlertMessage1.Margin = New System.Windows.Forms.Padding(0)
        Me.AlertMessage1.Name = "AlertMessage1"
        Me.AlertMessage1.Size = New System.Drawing.Size(240, 42)
        Me.AlertMessage1.SuccessBackColor = System.Drawing.Color.FromArgb(CType(CType(223, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(216, Byte), Integer))
        Me.AlertMessage1.SuccessForeColor = System.Drawing.Color.FromArgb(CType(CType(70, Byte), Integer), CType(CType(136, Byte), Integer), CType(CType(71, Byte), Integer))
        Me.AlertMessage1.TabIndex = 16
        Me.AlertMessage1.WarnBackColor = System.Drawing.Color.FromArgb(CType(CType(252, Byte), Integer), CType(CType(248, Byte), Integer), CType(CType(227, Byte), Integer))
        Me.AlertMessage1.WarnForeColor = System.Drawing.Color.FromArgb(CType(CType(145, Byte), Integer), CType(CType(111, Byte), Integer), CType(CType(53, Byte), Integer))
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(751, 346)
        Me.Button5.Name = "Button5"
        Me.Button5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Button5.Size = New System.Drawing.Size(75, 23)
        Me.Button5.TabIndex = 17
        Me.Button5.Text = "Button5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'DebugMarker1
        '
        Me.DebugMarker1.BackColor = System.Drawing.Color.Transparent
        Me.DebugMarker1.BackgroundImage = CType(resources.GetObject("DebugMarker1.BackgroundImage"), System.Drawing.Image)
        Me.DebugMarker1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.DebugMarker1.Location = New System.Drawing.Point(0, 0)
        Me.DebugMarker1.MaximumSize = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.Name = "DebugMarker1"
        Me.DebugMarker1.Size = New System.Drawing.Size(16, 16)
        Me.DebugMarker1.TabIndex = 18
        Me.DebugMarker1.TabStop = False
        Me.DebugMarker1.ToolTipText = Nothing
        '
        'MocaDi1
        '
        Me.MocaDi1.AutoSize = True
        Me.MocaDi1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MocaDi1.BackgroundImage = CType(resources.GetObject("MocaDi1.BackgroundImage"), System.Drawing.Image)
        Me.MocaDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MocaDi1.Location = New System.Drawing.Point(8, 19)
        Me.MocaDi1.Margin = New System.Windows.Forms.Padding(0)
        Me.MocaDi1.MaximumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.MinimumSize = New System.Drawing.Size(16, 16)
        Me.MocaDi1.Name = "MocaDi1"
        Me.MocaDi1.Size = New System.Drawing.Size(16, 16)
        Me.MocaDi1.TabIndex = 19
        Me.MocaDi1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(952, 522)
        Me.Controls.Add(Me.DebugMarker1)
        Me.Controls.Add(Me.MocaDi1)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.AlertMessage1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxEx3)
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
        Me.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
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
    Friend WithEvents TextBoxEx3 As Moca.Win.TextBoxEx
    Friend WithEvents Label1 As Label
    Friend WithEvents AlertMessage1 As Moca.Win.AlertMessage
    Friend WithEvents Button5 As Button
    Friend WithEvents DebugMarker1 As Moca.Win.DebugMarker
    Friend WithEvents MocaDi1 As Moca.Win.MocaDi
End Class
