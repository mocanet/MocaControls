<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class GridTestForm
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
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridDesignSettings7 As Moca.GridDesignSettings = New Moca.GridDesignSettings()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(GridTestForm))
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim GridDesignSettings8 As Moca.GridDesignSettings = New Moca.GridDesignSettings()
        Me.ModelGridView1 = New Moca.Win.ModelGridView()
        Me.DataGridViewEx1 = New Moca.Win.DataGridViewEx()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDel = New System.Windows.Forms.Button()
        Me.btnAdd2 = New System.Windows.Forms.Button()
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ModelGridView1
        '
        Me.ModelGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ModelGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle7.Font = New System.Drawing.Font("Meiryo UI", 9.0!)
        DataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ModelGridView1.DefaultCellStyle = DataGridViewCellStyle7
        GridDesignSettings7.SettingsKey = ""
        Me.ModelGridView1.DesignSettings = GridDesignSettings7
        Me.ModelGridView1.HorizontalScrollBarAlwaysShow = False
        Me.ModelGridView1.Location = New System.Drawing.Point(10, 29)
        Me.ModelGridView1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.ModelGridView1.Name = "ModelGridView1"
        Me.ModelGridView1.RowEditImage = CType(resources.GetObject("ModelGridView1.RowEditImage"), System.Drawing.Image)
        Me.ModelGridView1.RowEntityType = Nothing
        Me.ModelGridView1.RowTemplate.Height = 33
        Me.ModelGridView1.Size = New System.Drawing.Size(245, 205)
        Me.ModelGridView1.Styles = CType(resources.GetObject("ModelGridView1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.ModelGridView1.TabIndex = 9
        Me.ModelGridView1.TransparentRowSelection = False
        Me.ModelGridView1.VerticalScrollBarAlwaysShow = False
        '
        'DataGridViewEx1
        '
        Me.DataGridViewEx1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridViewEx1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridViewEx1.DBInfoColumns = Nothing
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Meiryo UI", 9.0!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(33, Byte), Integer))
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.DataGridViewEx1.DefaultCellStyle = DataGridViewCellStyle8
        GridDesignSettings8.SettingsKey = ""
        Me.DataGridViewEx1.DesignSettings = GridDesignSettings8
        Me.DataGridViewEx1.HorizontalScrollBarAlwaysShow = False
        Me.DataGridViewEx1.Location = New System.Drawing.Point(278, 29)
        Me.DataGridViewEx1.Margin = New System.Windows.Forms.Padding(1, 2, 1, 2)
        Me.DataGridViewEx1.Name = "DataGridViewEx1"
        Me.DataGridViewEx1.RowEditImage = CType(resources.GetObject("DataGridViewEx1.RowEditImage"), System.Drawing.Image)
        Me.DataGridViewEx1.RowTemplate.Height = 33
        Me.DataGridViewEx1.Size = New System.Drawing.Size(241, 205)
        Me.DataGridViewEx1.Styles = CType(resources.GetObject("DataGridViewEx1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.DataGridViewEx1.TabIndex = 13
        Me.DataGridViewEx1.TransparentRowSelection = False
        Me.DataGridViewEx1.VerticalScrollBarAlwaysShow = False
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(278, 239)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(42, 19)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.Text = "add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDel
        '
        Me.btnDel.Location = New System.Drawing.Point(477, 239)
        Me.btnDel.Name = "btnDel"
        Me.btnDel.Size = New System.Drawing.Size(42, 19)
        Me.btnDel.TabIndex = 15
        Me.btnDel.Text = "del"
        Me.btnDel.UseVisualStyleBackColor = True
        '
        'btnAdd2
        '
        Me.btnAdd2.Location = New System.Drawing.Point(326, 239)
        Me.btnAdd2.Name = "btnAdd2"
        Me.btnAdd2.Size = New System.Drawing.Size(42, 19)
        Me.btnAdd2.TabIndex = 16
        Me.btnAdd2.Text = "add2"
        Me.btnAdd2.UseVisualStyleBackColor = True
        '
        'GridTestForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(529, 265)
        Me.Controls.Add(Me.btnAdd2)
        Me.Controls.Add(Me.btnDel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.DataGridViewEx1)
        Me.Controls.Add(Me.ModelGridView1)
        Me.Name = "GridTestForm"
        Me.Text = "GridTestForm"
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridViewEx1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ModelGridView1 As Moca.Win.ModelGridView
    Friend WithEvents DataGridViewEx1 As Moca.Win.DataGridViewEx
    Friend WithEvents btnAdd As Button
    Friend WithEvents btnDel As Button
    Friend WithEvents btnAdd2 As Button
End Class
