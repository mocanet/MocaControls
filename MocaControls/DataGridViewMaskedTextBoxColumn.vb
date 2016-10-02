
Namespace Win

    ''' <summary>
    ''' DataGridViewMaskedTextBoxCellオブジェクトの列を表します。
    ''' </summary>
    Public Class DataGridViewMaskedTextBoxColumn
        Inherits DataGridViewColumn

#Region " コンストラクタ "

        ''' <summary>
        ''' CellTemplateとするDataGridViewMaskedTextBoxCellオブジェクトを指定して
        ''' 基本クラスのコンストラクタを呼び出す
        ''' </summary>
        Public Sub New()
            MyBase.New(New DataGridViewMaskedTextBoxCell())
        End Sub

#End Region

#Region " Property "

        Private _maskValue As String = ""
        ''' <summary>
        ''' MaskedTextBoxのMaskプロパティに適用する値
        ''' </summary>
        Public Property Mask() As String
            Get
                Return Me._maskValue
            End Get
            Set(ByVal value As String)
                Me._maskValue = value
            End Set
        End Property

#End Region

#Region " Overrides "

        ''' <summary>
        ''' 新しいプロパティを追加しているため、
        ''' Cloneメソッドをオーバーライドする必要がある
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function Clone() As Object
            Dim col As DataGridViewMaskedTextBoxColumn = DirectCast(MyBase.Clone(), DataGridViewMaskedTextBoxColumn)
            col.Mask = Me.Mask
            Return col
        End Function

        ''' <summary>
        ''' CellTemplateの取得と設定
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Property CellTemplate() As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(ByVal value As DataGridViewCell)
                'DataGridViewMaskedTextBoxCellしかCellTemplateに設定できないようにする
                If Not (TypeOf value Is DataGridViewMaskedTextBoxCell) Then
                    Throw New InvalidCastException("DataGridViewMaskedTextBoxCellオブジェクトを指定してください。")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property

#End Region

    End Class

End Namespace
