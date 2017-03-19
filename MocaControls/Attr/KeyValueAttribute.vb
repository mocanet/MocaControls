
Namespace Win

    ''' <summary>
    ''' キーと値を指定する属性
    ''' </summary>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Class, AllowMultiple:=True)>
    Public Class KeyValueAttribute
        Inherits Attribute

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="key">キー名称</param>
        ''' <param name="value">値</param>
        Public Sub New(ByVal key As String, ByVal value As Object)
            Me.Key = key
            Me.Value = value
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' キー名称
        ''' </summary>
        ''' <returns></returns>
        Public Property Key As String

        ''' <summary>
        ''' 値
        ''' </summary>
        ''' <returns></returns>
        Public Property Value As Object

#End Region

    End Class

End Namespace
