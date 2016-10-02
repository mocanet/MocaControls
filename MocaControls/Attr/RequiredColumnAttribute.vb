
Namespace Win

    ''' <summary>
    ''' 必須列属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class RequiredColumnAttribute
        Inherits Attribute

#Region " Declare "

        ''' <summary>必須</summary>
        Private _required As Boolean

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="required">必須列。省略時は True</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal required As Boolean = True)
            _required = required
        End Sub

#End Region

#Region " Property "

        ''' <summary>必須</summary>
        Public ReadOnly Property IsRequired() As Boolean
            Get
                Return _required
            End Get
        End Property

#End Region

    End Class

End Namespace
