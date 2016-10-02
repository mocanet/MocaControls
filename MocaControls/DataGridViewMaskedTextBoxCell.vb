
Namespace Win

    ''' <summary>
    ''' MaskedTextBoxで編集できるテキスト情報をDataGridViewコントロールに表示します。
    ''' </summary>
    Public Class DataGridViewMaskedTextBoxCell
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
                Return GetType(DataGridViewMaskedTextBoxEditingControl)
            End Get
        End Property

        ''' <summary>
        ''' セルの値のデータ型を指定する
        ''' ここでは、Object型とする
        ''' 基本クラスと同じなので、オーバーライドの必要なし
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property ValueType() As Type
            Get
                Return GetType(Object)
            End Get
        End Property

        ''' <summary>
        ''' 新しいレコード行のセルの既定値を指定する
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property DefaultNewRowValue() As Object
            Get
                Return MyBase.DefaultNewRowValue
            End Get
        End Property

#End Region

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
            Dim maskedBox As DataGridViewMaskedTextBoxEditingControl = TryCast(Me.DataGridView.EditingControl, DataGridViewMaskedTextBoxEditingControl)
            If maskedBox IsNot Nothing Then
                'Textを設定
                Dim maskedText As String = TryCast(initialFormattedValue, String)
                maskedBox.Text = If(maskedText IsNot Nothing, maskedText, "")
                'カスタム列のプロパティを反映させる
                Dim column As DataGridViewMaskedTextBoxColumn = TryCast(Me.OwningColumn, DataGridViewMaskedTextBoxColumn)
                If column IsNot Nothing Then
                    maskedBox.Mask = column.Mask
                End If
                maskedBox.Size = DataGridView.CurrentCell.Size
            End If
        End Sub

#End Region

    End Class

End Namespace
