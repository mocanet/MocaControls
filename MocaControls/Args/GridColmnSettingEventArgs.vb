
Namespace Win

    ''' <summary>
    ''' DataGridView の列情報設定イベント引数
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GridColmnSettingEventArgs
        Inherits EventArgs

        ''' <summary>列位置</summary>
        Public Property Index As Integer
        ''' <summary>列情報</summary>
        Public Property Column As DataGridViewColumn
        ''' <summary>エンティティの該当するプロパティ情報</summary>
        Public Property ModelProperty As System.Reflection.PropertyInfo
    End Class

End Namespace
