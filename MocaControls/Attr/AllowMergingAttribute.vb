
Namespace Win

    ''' <summary>
    ''' マージ列属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False)>
    Public Class AllowMergingAttribute
        Inherits Attribute

#Region " Declare "

        ''' <summary>マージ</summary>
        Private _value As Boolean

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="value">マージ列。省略時はTrue</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal value As Boolean = True)
            _value = value
        End Sub

#End Region

#Region " Property "

        ''' <summary>マージ</summary>
        Public ReadOnly Property Merging() As Boolean
            Get
                Return _value
            End Get
        End Property

#End Region

    End Class

End Namespace
