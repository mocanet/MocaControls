Namespace Win

    ''' <summary>
    ''' プロパティの順序指定の属性
    ''' </summary>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)>
    Public Class PropertyOrderAttribute
        Inherits Attribute

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="index">値（０オリジン）</param>
        Public Sub New(ByVal index As Integer)
            Me.Index = index
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' 値（０オリジン）
        ''' </summary>
        ''' <returns></returns>
        Public Property Index As Integer

#End Region

    End Class

End Namespace
