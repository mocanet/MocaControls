<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Index
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridDesignSettings1 As Moca.GridDesignSettings = New Moca.GridDesignSettings()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Index))
        Me.grdFindResults = New Moca.Win.ModelGridView()
        Me.MocaDi1 = New Moca.Win.MocaDi()
        Me.DebugMarker1 = New Moca.Win.DebugMarker()
        Me.txtFindID = New Moca.Win.TextBoxEx(Me.components)
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtID = New Moca.Win.TextBoxEx(Me.components)
        Me.txtName = New Moca.Win.TextBoxEx(Me.components)
        Me.txtNote = New Moca.Win.TextBoxEx(Me.components)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.DataBinder1 = New Moca.Win.DataBinder(Me.components)
        Me.btnClear = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        CType(Me.grdFindResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdFindResults
        '
        Me.grdFindResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdFindResults.DefaultCellStyle = DataGridViewCellStyle1
        GridDesignSettings1.SettingsKey = ""
        Me.grdFindResults.DesignSettings = GridDesignSettings1
        Me.grdFindResults.Location = New System.Drawing.Point(14, 70)
        Me.grdFindResults.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.grdFindResults.Name = "grdFindResults"
        Me.grdFindResults.RowEditImage = CType(resources.GetObject("grdFindResults.RowEditImage"), System.Drawing.Image)
        Me.grdFindResults.RowEntityType = Nothing
        Me.grdFindResults.RowTemplate.Height = 21
        Me.grdFindResults.Size = New System.Drawing.Size(329, 188)
        Me.grdFindResults.Styles = CType(resources.GetObject("grdFindResults.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.grdFindResults.TabIndex = 2
        Me.grdFindResults.TransparentRowSelection = False
        '
        'MocaDi1
        '
        Me.MocaDi1.AutoSize = True
        Me.MocaDi1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.MocaDi1.BackgroundImage = CType(resources.GetObject("MocaDi1.BackgroundImage"), System.Drawing.Image)
        Me.MocaDi1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.MocaDi1.Location = New System.Drawing.Point(23, 1)
        Me.MocaDi1.Margin = New System.Windows.Forms.Padding(0)
        Me.MocaDi1.MaximumSize = New System.Drawing.Size(19, 20)
        Me.MocaDi1.MinimumSize = New System.Drawing.Size(19, 20)
        Me.MocaDi1.Name = "MocaDi1"
        Me.MocaDi1.Size = New System.Drawing.Size(19, 20)
        Me.MocaDi1.TabIndex = 1
        Me.MocaDi1.TabStop = False
        '
        'DebugMarker1
        '
        Me.DebugMarker1.BackColor = System.Drawing.Color.Transparent
        Me.DebugMarker1.BackgroundImage = CType(resources.GetObject("DebugMarker1.BackgroundImage"), System.Drawing.Image)
        Me.DebugMarker1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.DebugMarker1.Location = New System.Drawing.Point(1, 1)
        Me.DebugMarker1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.DebugMarker1.MaximumSize = New System.Drawing.Size(19, 20)
        Me.DebugMarker1.MinimumSize = New System.Drawing.Size(19, 20)
        Me.DebugMarker1.Name = "DebugMarker1"
        Me.DebugMarker1.Size = New System.Drawing.Size(19, 20)
        Me.DebugMarker1.TabIndex = 2
        Me.DebugMarker1.TabStop = False
        Me.DebugMarker1.ToolTipText = Nothing
        '
        'txtFindID
        '
        Me.txtFindID.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtFindID.BottomBorderColor = System.Drawing.Color.Empty
        Me.txtFindID.CustomChars = Nothing
        Me.txtFindID.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.txtFindID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtFindID.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.txtFindID.Location = New System.Drawing.Point(49, 34)
        Me.txtFindID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtFindID.Name = "txtFindID"
        Me.txtFindID.NegativeColor = System.Drawing.Color.Red
        Me.txtFindID.Separator = ""
        Me.txtFindID.Size = New System.Drawing.Size(201, 28)
        Me.txtFindID.TabIndex = 0
        Me.txtFindID.Text = "TextBoxEx1"
        Me.txtFindID.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'btnSearch
        '
        Me.btnSearch.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnSearch.Location = New System.Drawing.Point(256, 34)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(87, 29)
        Me.btnSearch.TabIndex = 1
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 27)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "ID : "
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(9, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 29)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "ID : "
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(9, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 29)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Name : "
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(9, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 29)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Note : "
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtID
        '
        Me.txtID.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtID.BottomBorderColor = System.Drawing.Color.Empty
        Me.txtID.CustomChars = Nothing
        Me.txtID.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.txtID.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.txtID.Location = New System.Drawing.Point(65, 30)
        Me.txtID.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtID.Name = "txtID"
        Me.txtID.NegativeColor = System.Drawing.Color.Red
        Me.txtID.Separator = ""
        Me.txtID.Size = New System.Drawing.Size(175, 28)
        Me.txtID.TabIndex = 3
        Me.txtID.Text = "TextBoxEx2"
        Me.txtID.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'txtName
        '
        Me.txtName.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtName.BottomBorderColor = System.Drawing.Color.Empty
        Me.txtName.CustomChars = Nothing
        Me.txtName.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtName.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.txtName.Location = New System.Drawing.Point(65, 66)
        Me.txtName.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtName.Name = "txtName"
        Me.txtName.NegativeColor = System.Drawing.Color.Red
        Me.txtName.Separator = ""
        Me.txtName.Size = New System.Drawing.Size(175, 28)
        Me.txtName.TabIndex = 4
        Me.txtName.Text = "TextBoxEx3"
        Me.txtName.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'txtNote
        '
        Me.txtNote.BorderColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.txtNote.BottomBorderColor = System.Drawing.Color.Empty
        Me.txtNote.CustomChars = Nothing
        Me.txtNote.ImeMode = System.Windows.Forms.ImeMode.Hiragana
        Me.txtNote.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.txtNote.Location = New System.Drawing.Point(65, 102)
        Me.txtNote.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtNote.Name = "txtNote"
        Me.txtNote.NegativeColor = System.Drawing.Color.Red
        Me.txtNote.Separator = ""
        Me.txtNote.Size = New System.Drawing.Size(175, 28)
        Me.txtNote.TabIndex = 5
        Me.txtNote.Text = "TextBoxEx4"
        Me.txtNote.UnfocusedBorderColor = System.Drawing.SystemColors.ControlDark
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(65, 139)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(87, 29)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'DataBinder1
        '
        Me.DataBinder1.DataMember = ""
        Me.DataBinder1.DataSource = Nothing
        Me.DataBinder1.Position = -1
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(154, 139)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(87, 29)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Meiryo UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(349, 11)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(257, 45)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Moca.NET Sample"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtID)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.btnClear)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.txtName)
        Me.GroupBox1.Controls.Add(Me.txtNote)
        Me.GroupBox1.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(354, 70)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(252, 188)
        Me.GroupBox1.TabIndex = 11
        Me.GroupBox1.TabStop = False
        '
        'Index
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(618, 282)
        Me.Controls.Add(Me.DebugMarker1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.txtFindID)
        Me.Controls.Add(Me.MocaDi1)
        Me.Controls.Add(Me.grdFindResults)
        Me.Font = New System.Drawing.Font("Meiryo UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Index"
        Me.Text = "Moca.NET Demo"
        CType(Me.grdFindResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdFindResults As Moca.Win.ModelGridView
    Friend WithEvents MocaDi1 As Moca.Win.MocaDi
    Friend WithEvents DebugMarker1 As Moca.Win.DebugMarker
    Friend WithEvents txtFindID As Moca.Win.TextBoxEx
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtID As Moca.Win.TextBoxEx
    Friend WithEvents txtName As Moca.Win.TextBoxEx
    Friend WithEvents txtNote As Moca.Win.TextBoxEx
	Friend WithEvents btnAdd As System.Windows.Forms.Button
	Friend WithEvents DataBinder1 As Moca.Win.DataBinder
	Friend WithEvents btnClear As System.Windows.Forms.Button
	Friend WithEvents Label5 As System.Windows.Forms.Label
	Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
