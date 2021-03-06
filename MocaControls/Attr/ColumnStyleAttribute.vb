﻿
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
		''' <param name="wordWrap">改行の有無</param>
		''' <param name="inputFormat">フォーマット</param>
		''' <param name="nullValue">Nullのときの値</param>
		''' <param name="rightBorderNone">右線の有無</param>
		''' <param name="InputControl">入力文字制御指定</param>
		''' <param name="InputControlCustomChars">入力文字制御指定に Custom が指定された時の許可する文字</param>
		''' <param name="SortMode">ソートモード</param>
		''' <param name="Precision">小数点以下の桁数</param>
		Public Sub New(Optional ByVal width As Integer = -1,
					   Optional ByVal align As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleLeft,
					   Optional ByVal format As String = "",
					   Optional ByVal cellType As CellType = CellType.TextBox,
					   Optional ByVal imeMode As ImeMode = ImeMode.NoControl,
					   Optional ByVal wordWrap As Boolean = False,
					   Optional ByVal inputFormat As String = "",
					   Optional ByVal nullValue As Object = "",
					   Optional ByVal rightBorderNone As Boolean = False,
					   Optional ByVal InputControl As TextBoxEx.InputFormatType = TextBoxEx.InputFormatType.None,
					   Optional ByVal InputControlCustomChars As String = "",
					   Optional ByVal SortMode As DataGridViewColumnSortMode = DataGridViewColumnSortMode.NotSortable,
					   Optional ByVal Precision As System.Int32 = 0)
			Me.Width = width
			Me.Align = align
			Me.Format = format
			Me.CellType = cellType
			Me.ImeMode = imeMode
			Me.WordWrap = wordWrap
			Me.InputFormat = inputFormat
			Me.NullValue = IIf(nullValue Is Nothing, String.Empty, nullValue)
			Me.RightBorderNone = rightBorderNone
			Me.InputControl = InputControl
			Me.InputControlCustomChars = InputControlCustomChars
			Me.SortMode = SortMode
			Me.Precision = Precision
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

        ''' <summary>IME Mode</summary>
        Public Property ImeMode As ImeMode

        ''' <summary>改行の有無</summary>
        Public Property WordWrap As Boolean

        ''' <summary>フォーマット</summary>
        Public Property InputFormat As String

        ''' <summary>Nullのときの値</summary>
        Public Property NullValue As Object

        ''' <summary>右線の有無</summary>
        Public Property RightBorderNone As Boolean

        ''' <summary>入力文字制御指定</summary>
        Public Property InputControl As TextBoxEx.InputFormatType

        ''' <summary>入力文字制御指定に Custom が指定された時の許可する文字</summary>
        Public Property InputControlCustomChars As String

        ''' <summary>ソート</summary>
        Public Property SortMode As DataGridViewColumnSortMode

		''' <summary>小数点以下の桁数</summary>
		Public Property Precision As System.Int32

#End Region

	End Class

End Namespace
