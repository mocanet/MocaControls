
Namespace Win

    ''' <summary>
    ''' DataGridView の列情報設定イベント引数
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GridMakeColumnBeforeEventArgs
        Inherits EventArgs

        ''' <summary>列位置</summary>
        Public Property Index As Integer
        ''' <summary>列タイプ</summary>
        Public Property MakeCellType As CellType
        ''' <summary>入力タイプ</summary>
        Public Property InputControl As TextBoxEx.InputFormatType
        ''' <summary>エンティティの該当するプロパティ情報</summary>
        Public Property ModelProperty As System.Reflection.PropertyInfo
    End Class

End Namespace
