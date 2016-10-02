
Namespace Win

    ''' <summary>
    ''' 非表示列属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class HiddenColumnAttribute
        Inherits Attribute

#Region " Declare "

        ''' <summary>非表示</summary>
        Private _hidden As Boolean

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="hidden">非表示列。省略時はTrue</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal hidden As Boolean = True)
            _hidden = hidden
        End Sub

#End Region

#Region " Property "

        ''' <summary>非表示</summary>
        Public ReadOnly Property IsHidden() As Boolean
            Get
                Return _hidden
            End Get
        End Property

#End Region

    End Class

End Namespace
