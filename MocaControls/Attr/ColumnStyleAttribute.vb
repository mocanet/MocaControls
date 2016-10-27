
Namespace Win

    ''' <summary>
    ''' 非表示列属性
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field, AllowMultiple:=False)>
    Public Class ColumnStyleAttribute
        Inherits Attribute

#Region " コンストラクタ "

        ''' <summary>
        ''' コンストラクタ
        ''' </summary>
        ''' <param name="width">表示幅。省略時はFlexGrid標準</param>
        ''' <param name="align">表示位置</param>
        ''' <param name="format">表示フォーマット式</param>
        ''' <param name="cellType">セルの種類</param>
        ''' <param name="imeMode">IME Mode</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal width As Integer = -1,
                   Optional ByVal align As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft,
                   Optional ByVal format As String = "",
                   Optional ByVal cellType As CellType = CellType.TextBox,
                   Optional ByVal imeMode As ImeMode = ImeMode.NoControl,
                   Optional ByVal wordWrap As Boolean = False,
                   Optional ByVal inputFormat As String = "",
                   Optional ByVal nullValue As Object = "",
                   Optional ByVal rightBorderNone As Boolean = False)
            Me.Width = width
            Me.Align = align
            Me.Format = format
            Me.CellType = cellType
            Me.ImeMode = imeMode
            Me.WordWrap = wordWrap
            Me.InputFormat = inputFormat
            Me.NullValue = nullValue
        End Sub

#End Region

#Region " Property "

        ''' <summary>表示幅</summary>
        Public Property Width As Integer

        ''' <summary>表示位置</summary>
        Public Property Align As DataGridViewContentAlignment

        ''' <summary>表示フォーマット式</summary>
        Public Property Format As String

        ''' <summary>セルの種類</summary>
        Public Property CellType As CellType

        Public Property ImeMode As ImeMode

        Public Property WordWrap As Boolean

        Public Property InputFormat As String

        Public Property NullValue As Object

        Public Property RightBorderNone As Boolean

#End Region

    End Class

End Namespace
