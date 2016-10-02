
Namespace Win

    ''' <summary>
    ''' 列キャプション属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=True)>
    Public Class CaptionAttribute
        Inherits Attribute

#Region " Declare "

        Private _colspanOriginal As Integer

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="caption">列キャプション。省略時はTrue</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal caption As String, Optional ByVal colspan As Integer = 1)
            _Caption = caption
            _Colspan = colspan
            _colspanOriginal = colspan
        End Sub

#End Region

#Region " Property "

        ''' <summary>キャプション</summary>
        Public Property Caption() As String

        ''' <summary>Colspan</summary>
        Public Property Colspan() As Integer

        Public ReadOnly Property IsAdded As Boolean
            Get
                Return Not (_colspanOriginal = Colspan)
            End Get
        End Property

#End Region

    End Class

End Namespace
