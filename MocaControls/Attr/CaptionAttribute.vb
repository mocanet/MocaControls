
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
        Private _rowspanOriginal As Integer

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="caption">列キャプション</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal caption As String, Optional ByVal colspan As Integer = 1, Optional ByVal rowspan As Integer = 1)
            _Caption = caption
            _Colspan = colspan
            _colspanOriginal = colspan
            _Rowspan = rowspan
            _rowspanOriginal = rowspan
        End Sub

#End Region

#Region " Property "

        ''' <summary>キャプション</summary>
        Public Property Caption() As String

        ''' <summary>Colspan</summary>
        Public Property Colspan() As Integer

        ''' <summary>Rowspan</summary>
        Public Property Rowspan() As Integer

        Public ReadOnly Property IsAdded As Boolean
            Get
                Return Not (_colspanOriginal = Colspan)
            End Get
        End Property

#End Region

    End Class

End Namespace
