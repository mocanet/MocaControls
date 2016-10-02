
Namespace Win

    ''' <summary>
    ''' 固定列
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)>
    Public Class FrozenAttribute
        Inherits Attribute

#Region " Declare "

        ''' <summary>固定列</summary>
        Private _value As Integer

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="value">固定列。省略時は-1(指定列)</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal value As Integer = -1)
            _value = value
        End Sub

#End Region

#Region " Property "

        ''' <summary>固定列</summary>
        Public ReadOnly Property Column() As Integer
            Get
                Return _value
            End Get
        End Property

#End Region

    End Class

End Namespace
