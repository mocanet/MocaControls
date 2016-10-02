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
        Me.ModelGridView1 = New Moca.Win.ModelGridView()
        Me.TextBoxEx1 = New Moca.Win.TextBoxEx(Me.components)
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(869, 82)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 31)
        Me.TextBox1.TabIndex = 2
        '
        'ModelGridView1
        '
        Me.ModelGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ModelGridView1.DesignSettings = Nothing
        Me.ModelGridView1.Location = New System.Drawing.Point(12, 143)
        Me.ModelGridView1.Name = "ModelGridView1"
        Me.ModelGridView1.RowEntityType = Nothing
        Me.ModelGridView1.RowTemplate.Height = 33
        Me.ModelGridView1.Size = New System.Drawing.Size(1318, 670)
        Me.ModelGridView1.Styles = CType(resources.GetObject("ModelGridView1.Styles"), System.Collections.Generic.IDictionary(Of String, System.Windows.Forms.DataGridViewCellStyle))
        Me.ModelGridView1.TabIndex = 0
        '
        'TextBoxEx1
        '
        Me.TextBoxEx1.BackColor = System.Drawing.SystemColors.Control
        Me.TextBoxEx1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBoxEx1.BottomBorderColor = System.Drawing.Color.Blue
        Me.TextBoxEx1.ImeMode = System.Windows.Forms.ImeMode.Disable
        Me.TextBoxEx1.InputFormat = Moca.Win.TextBoxEx.InputFormatType.None
        Me.TextBoxEx1.Location = New System.Drawing.Point(157, 34)
        Me.TextBoxEx1.Name = "TextBoxEx1"
        Me.TextBoxEx1.NegativeColor = System.Drawing.Color.Red
        Me.TextBoxEx1.NumericScale = 0
        Me.TextBoxEx1.Percision = 0
        Me.TextBoxEx1.PercisionSign = False
        Me.TextBoxEx1.ReadOnly = True
        Me.TextBoxEx1.Separator = ""
        Me.TextBoxEx1.Size = New System.Drawing.Size(627, 91)
        Me.TextBoxEx1.TabIndex = 3
        Me.TextBoxEx1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 24.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1342, 825)
        Me.Controls.Add(Me.TextBoxEx1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.ModelGridView1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.ModelGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ModelGridView1 As Moca.Win.ModelGridView
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBoxEx1 As Moca.Win.TextBoxEx
End Class
