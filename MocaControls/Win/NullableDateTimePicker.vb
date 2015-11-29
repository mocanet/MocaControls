
Imports System.Globalization
Imports System.ComponentModel

Namespace Win

	''' <summary>
	''' 標準のDateTimePickerでNullを扱えるように拡張したコントロール
	''' </summary>
	''' <remarks>
	''' 標準のDateTimePickerではNullを扱えないので、設定出来るようにし、
	''' DellキーにてNullにする事が可能となっている。<br/>
	''' Null値の場合に扱う値は、ユーザーがNullValueプロパティにて自由に設定出来ます。<br/>
	''' <br/>
	''' 注１）<br/>
	''' 　vbNullは使用できません。<br/>
	''' 　Null値にしたい場合は、vbNullString 又は、DBNull.Valueを使用してください。<br/>
	''' </remarks>
	<Description("標準のDateTimePickerでNullを扱えるように拡張したコントロール"), _
	ToolboxItem(True),
	ToolboxBitmap(GetType(resourceDummy), "NullableDateTimePicker.bmp"), _
	DesignTimeVisible(True)> _
	Public Class NullableDateTimePicker

		''' <summary>現在の値がNull値かどうかを判定</summary>
		Private _isNull As Boolean

		''' <summary>Nullだった場合に表示する文字列</summary>
		Private _nullValue As String

		'''' <summary>日時のフォーマット</summary>
		Private _format As DateTimePickerFormat = DateTimePickerFormat.Long

		'''' <summary>親のカスタムフォーマット文字列</summary>
		Private _formatAsString As String

		'''' <summary>カスタムフォーマット文字列</summary>
		Private _customFormat As String

#Region " コンストラクタ "

		''' <summary>
		''' デフォルトコンストラクタ
		''' </summary>
		''' <remarks>
		''' </remarks>
		Public Sub New()
			MyBase.New()

			' この呼び出しは Windows フォーム デザイナで必要です。
			InitializeComponent()

			' InitializeComponent() 呼び出しの後に初期化を追加します。

			MyBase.Format = DateTimePickerFormat.Custom
			NullValue = String.Empty
			Me.Format = DateTimePickerFormat.Long
		End Sub

		''' <summary>
		''' 標準DateTimePickerを置き換えるコンストラクタ
		''' </summary>
		''' <param name="dtpOriginal">置き換えするDateTimePicker</param>
		''' <remarks>
		''' コピーするプロパティは今の所
		'''     Location、Size、Font、CalendarFont、Format、CustomFormat、TabIndex、Tag
		''' </remarks>
		Public Sub New(ByVal dtpOriginal As DateTimePicker)
			Me.New()

			_replaceControl(dtpOriginal)
		End Sub

#End Region

#Region " プロパティ "

		''' <summary>
		''' カスタムフォーマット文字列プロパティ
		''' </summary>
		''' <value>フォーマット文字列</value>
		''' <remarks>
		''' 基本クラスのシャドウ
		''' </remarks>
		<System.ComponentModel.Description("カスタムフォーマット文字列")> _
		Public Shadows Property CustomFormat() As String
			Get
				Return _customFormat
			End Get
			Set(ByVal Value As String)
				_customFormat = Value
			End Set
		End Property

		''' <summary>
		''' 日時のフォーマットプロパティ
		''' </summary>
		''' <value>日時のフォーマット</value>
		''' <remarks>
		''' 基本クラスのシャドウ
		''' </remarks>
		<System.ComponentModel.Description("日時のフォーマット")> _
		Public Shadows Property Format() As DateTimePickerFormat
			Get
				Return _format
			End Get
			Set(ByVal Value As DateTimePickerFormat)
				_format = Value
				_setFormat()
			End Set
		End Property

		''' <summary>
		''' 親のカスタムフォーマット文字列プロパティ
		''' </summary>
		''' <value>親のカスタムフォーマット文字列</value>
		''' <remarks>
		''' </remarks>
		<System.ComponentModel.Description("親のカスタムフォーマット文字列")> _
		Public Property FormatAsString() As String
			Get
				Return _formatAsString
			End Get
			Set(ByVal Value As String)
				_formatAsString = Value
				MyBase.CustomFormat = Value
			End Set
		End Property

		''' <summary>
		''' Valueプロパティ
		''' </summary>
		''' <value>Value</value>
		''' <remarks>
		''' 基本クラスのシャドウ
		''' </remarks>
		Public Shadows Property Value() As Object
			Get
				If _isNull Then
					Return Nothing
				Else
					Return MyBase.Value
				End If
			End Get
			Set(ByVal Value As Object)
				If IsDBNull(Value) Then
					_setToNullValue()
				ElseIf Value Is Nothing Then
					_setToNullValue()
				Else
					_setToDateTimeValue()
					MyBase.Value = CType(Value, Date)
				End If
			End Set
		End Property

		''' <summary>
		''' Textプロパティ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' Null状態のときに、<see cref="CustomFormat"/>プロパティを” ”とするため、” ”を返してしまう。<br/>
		''' これを回避する為オーバーライドしてTrimしてます。
		''' </remarks>
		Public Overrides Property Text As String
			Get
				Return MyBase.Text.Trim
			End Get
			Set(value As String)
				MyBase.Text = value
			End Set
		End Property

		''' <summary>
		''' Nullだった場合に表示する文字列プロパティ
		''' </summary>
		''' <value>Nullだった場合に表示する文字列</value>
		''' <remarks>
		''' </remarks>
		<System.ComponentModel.Description("Nullだった場合に表示する文字列")> _
		Public Property NullValue() As String
			Get
				Return _nullValue
			End Get
			Set(ByVal Value As String)
				_nullValue = Value
			End Set
		End Property

#End Region

#Region " イベント "

		''' <summary>
		''' ドロップダウン時のポップアップ画面が閉じられたイベントのオーバーライド
		''' </summary>
		''' <param name="eventargs"></param>
		''' <remarks>
		''' </remarks>
		Protected Overrides Sub OnCloseUp(ByVal eventargs As System.EventArgs)
			If (Control.MouseButtons = MouseButtons.None And _isNull) Then
				_setToDateTimeValue()
				_isNull = False
			End If
			MyBase.OnCloseUp(eventargs)
		End Sub

		''' <summary>
		''' キーアップイベントのオーバーライド
		''' </summary>
		''' <param name="e"></param>
		''' <remarks>
		''' Delete キー押下時は空にする。
		''' </remarks>
		Protected Overrides Sub OnKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
			If (e.KeyCode = Keys.Delete) Then
				Me.Value = vbNullString
				OnValueChanged(EventArgs.Empty)
			End If
			MyBase.OnKeyUp(e)
		End Sub

#End Region

		''' <summary>
		''' 標準DateTimePickerを置き換え
		''' </summary>
		''' <param name="dtpOriginal">置き換えするDateTimePicker</param>
		''' <remarks>
		''' </remarks>
		Private Sub _replaceControl(ByVal dtpOriginal As DateTimePicker)
			Dim parent As Control

			Me.Location = dtpOriginal.Location
			Me.Size = dtpOriginal.Size
			Me.Font = dtpOriginal.Font
			Me.CalendarFont = dtpOriginal.CalendarFont
			Me.CustomFormat = dtpOriginal.CustomFormat
			Me.Format = dtpOriginal.Format
			Me.TabIndex = dtpOriginal.TabIndex
			Me.Tag = dtpOriginal.Tag

			parent = dtpOriginal.Parent
			parent.Controls.Add(Me)
			parent.Controls.Remove(dtpOriginal)
			dtpOriginal.Dispose()
			dtpOriginal = Nothing

			Me.Visible = True
		End Sub

		''' <summary>
		''' フォーマットの設定
		''' </summary>
		''' <remarks>
		''' </remarks>
		Private Sub _setFormat()
			Dim myDTFI As DateTimeFormatInfo = New CultureInfo("ja-JP", False).DateTimeFormat

			Select Case _format
				Case DateTimePickerFormat.Long
					FormatAsString = myDTFI.LongDatePattern
				Case DateTimePickerFormat.Short
					FormatAsString = myDTFI.ShortDatePattern
				Case DateTimePickerFormat.Time
					FormatAsString = myDTFI.ShortTimePattern
				Case DateTimePickerFormat.Custom
					FormatAsString = Me.CustomFormat
			End Select
		End Sub

		''' <summary>
		''' 日付入力時のValue設定
		''' </summary>
		''' <remarks>
		''' </remarks>
		Private Sub _setToDateTimeValue()
			If _isNull Then
				_setFormat()
				_isNull = False
				MyBase.OnValueChanged(New EventArgs)
			End If
		End Sub

		''' <summary>
		''' Null入力時のValue設定
		''' </summary>
		''' <remarks>
		''' </remarks>
		Private Sub _setToNullValue()
			_isNull = True
			MyBase.CustomFormat = CStr(IIf(_nullValue Is Nothing Or _nullValue = String.Empty, " ", "’" + NullValue + "’"))
		End Sub

	End Class

End Namespace
