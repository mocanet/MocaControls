
Namespace Win

    ''' <summary>
    ''' TextBoxEx で編集できるテキスト情報を DataGridView コントロールに表示します。
    ''' </summary>
    Public Class DataGridViewTextBoxExCell
        Inherits DataGridViewTextBoxCell

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
        End Sub

#End Region

#Region " Overrides "

#Region " Property "

        ''' <summary>
        ''' 編集コントロールの型を指定する
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property EditType() As Type
            Get
                Return GetType(DataGridViewTextBoxExEditingControl)
            End Get
        End Property

#End Region

#Region " Method "

        ''' <summary>
        ''' 編集コントロールを初期化する
        ''' 編集コントロールは別のセルや列でも使いまわされるため、初期化の必要がある
        ''' </summary>
        ''' <param name="rowIndex"></param>
        ''' <param name="initialFormattedValue"></param>
        ''' <param name="dataGridViewCellStyle"></param>
        Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As DataGridViewCellStyle)
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

            '編集コントロールの取得
            Dim txt As DataGridViewTextBoxExEditingControl = TryCast(Me.DataGridView.EditingControl, DataGridViewTextBoxExEditingControl)
            If txt IsNot Nothing Then
                'Textを設定
                Dim text As String = TryCast(initialFormattedValue, String)
                txt.Text = If(text IsNot Nothing, text, "")
                'カスタム列のプロパティを反映させる
                Dim column As DataGridViewTextBoxExColumn = TryCast(Me.OwningColumn, DataGridViewTextBoxExColumn)
                If column IsNot Nothing Then
                    txt.InputFormat = column.InputFormat
                End If
                txt.Size = DataGridView.CurrentCell.Size
                txt.BackColor = DataGridView.CurrentCell.Style.BackColor
            End If
        End Sub

#End Region

#End Region

    End Class

End Namespace
