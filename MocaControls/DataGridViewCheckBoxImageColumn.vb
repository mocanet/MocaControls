
Namespace Win

    ''' <summary>
    ''' DataGridViewCheckBoxImageCell オブジェクトの列を表します。
    ''' </summary>
    Public Class DataGridViewCheckBoxImageColumn
        Inherits DataGridViewColumn

#Region " コンストラクタ "

        ''' <summary>
        ''' CellTemplateとするDataGridViewCheckBoxImageCellオブジェクトを指定して
        ''' 基本クラスのコンストラクタを呼び出す
        ''' </summary>
        Public Sub New()
            MyBase.New(New DataGridViewCheckBoxImageCell())
        End Sub

#End Region

#Region " Property "

        Public Property CheckedImage As Image
        Public Property UnCheckedImage As Image

#End Region

#Region " Overrides "

        ''' <summary>
        ''' 新しいプロパティを追加しているため、
        ''' Cloneメソッドをオーバーライドする必要がある
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function Clone() As Object
            Dim col As DataGridViewCheckBoxImageColumn = DirectCast(MyBase.Clone(), DataGridViewCheckBoxImageColumn)
            col.CheckedImage = Me.CheckedImage
            col.UnCheckedImage = Me.UnCheckedImage
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
                If Not (TypeOf value Is DataGridViewCheckBoxImageCell) Then
                    Throw New InvalidCastException("DataGridViewCheckBoxImageCell オブジェクトを指定してください。")
                End If
                Dim cell As DataGridViewCheckBoxImageCell = value
                cell.ImageLayout = DataGridViewImageCellLayout.Zoom
                MyBase.CellTemplate = cell
            End Set
        End Property

#End Region

    End Class

End Namespace
