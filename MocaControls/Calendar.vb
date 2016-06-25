

Imports System.ComponentModel
Imports System.Drawing
Imports System.Threading

Imports Moca.Win

Namespace Win

	''' <summary>
	''' カレンダーコントロール
	''' </summary>
	''' <remarks></remarks>
	<Description("カレンダー"), _
	ToolboxItem(True),
	DesignTimeVisible(True)> _
	Public Class Calendar

#Region " Declare "

#Region " Event "

		''' <summary>
		''' 日付が変更された時のイベント
		''' </summary>
		''' <param name="day">選択された日付</param>
		''' <remarks></remarks>
		Public Event ChangedDay(ByVal day As Date, ByVal e As EventArgs)

		''' <summary>
		''' 月が変更された時のイベント
		''' </summary>
		''' <param name="month">選択された月</param>
		''' <remarks></remarks>
		Public Event ChangedMonth(ByVal month As Date, ByVal e As EventArgs)

		''' <summary>
		''' 日付でキーが押されたときのイベント
		''' </summary>
		''' <param name="day">押された時にフォーカスがあった日付</param>
		''' <param name="e">キーイベント引数</param>
		''' <remarks></remarks>
		Public Event DayKeyDown(ByVal day As Date, ByVal e As System.Windows.Forms.KeyEventArgs)

#End Region
#Region " Property "

		Private _titleFormat As String
		''' <summary>
		''' タイトルのフォーマット
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("タイトルのフォーマット"), DefaultValue("yyyy 年 MM 月"), Browsable(True)> _
		Public Property TitleFormat As String
			Get
				Return _titleFormat
			End Get
			Set(value As String)
				_titleFormat = value
				Me.lblHeader.Text = _titleFormat
			End Set
		End Property

		''' <summary>
		''' タイトルのフォント
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("タイトルのフォント"), DefaultValue("メイリオ, 18pt")> _
		Public Property TitleFont As Font
			Get
				Return Me.lblHeader.Font
			End Get
            Set(value As Font)
                Me.lblHeader.Font = value
                Me.btnPreMonth.Font = value
                Me.btnNextMonth.Font = value
                Me.lblDayOfWeek0.Font = value
                Me.lblDayOfWeek1.Font = value
                Me.lblDayOfWeek2.Font = value
                Me.lblDayOfWeek3.Font = value
                Me.lblDayOfWeek4.Font = value
                Me.lblDayOfWeek5.Font = value
                Me.lblDayOfWeek6.Font = value
            End Set
        End Property

		''' <summary>
		''' タイトルの文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("タイトルの文字色")> _
		Public Property TitleForeColor As Color
			Get
				Return Me.lblHeader.ForeColor
			End Get
			Set(value As Color)
				Me.lblHeader.ForeColor = value
				Me.btnPreMonth.ForeColor = value
				Me.btnNextMonth.ForeColor = value
			End Set
		End Property

		''' <summary>
		''' 曜日（平日）タイトルの文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("曜日（平日）タイトルの文字色")> _
		Public Property DayOfMonthTitleForeColor As Color
			Get
				Return Me.lblDayOfWeek1.ForeColor
			End Get
			Set(value As Color)
				Me.lblDayOfWeek1.ForeColor = value
				Me.lblDayOfWeek2.ForeColor = value
				Me.lblDayOfWeek3.ForeColor = value
				Me.lblDayOfWeek4.ForeColor = value
				Me.lblDayOfWeek5.ForeColor = value
			End Set
		End Property

		''' <summary>
		''' 曜日（日曜日）タイトルの文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("曜日（日曜日）タイトルの文字色")> _
		Public Property SundayTitleForeColor As Color
			Get
				Return Me.lblDayOfWeek0.ForeColor
			End Get
			Set(value As Color)
				Me.lblDayOfWeek0.ForeColor = value
			End Set
		End Property

		''' <summary>
		''' 曜日（土曜日）タイトルの文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("曜日（土曜日）タイトルの文字色")> _
		Public Property SaturdayTitleForeColor As Color
			Get
				Return Me.lblDayOfWeek6.ForeColor
			End Get
			Set(value As Color)
				Me.lblDayOfWeek6.ForeColor = value
			End Set
		End Property

		Private _valueDate As Date
		Private _value As Object
		''' <summary>
		''' 年月日
		''' </summary>
		''' <remarks></remarks>
		<Description("選択された年月日"), [ReadOnly](True), Browsable(False)> _
		Public Property Value() As Object
			Get
				Return _value
			End Get
			Set(ByVal value As Object)
				_setValue(value)

				If _isDesignMode Then
					Return
				End If

				' カレンダー表示
				_setCalendar(_targetMonth.Year, _targetMonth.Month)
			End Set
		End Property

		Private _preSelectEnable As Boolean
		''' <summary>
		''' 過去の選択
		''' </summary>
		''' <remarks></remarks>
		<Description("過去の選択"), DefaultValue(False)> _
		Public Property PreSelectEnable() As Boolean
			Get
				Return _preSelectEnable
			End Get
			Set(ByVal value As Boolean)
				_preSelectEnable = value
			End Set
		End Property

		''' <summary>
		''' 表示する最小日
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' 省略時は<see cref="Date.MinValue"></see>
		''' </remarks>
		<Description("表示する最小日"), DefaultValue("0001/01/01 00:00:00")> _
		Public Property MinDate As Date = Date.MinValue

		''' <summary>
		''' 表示する最大日
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks>
		''' 省略時は<see cref="Date.MaxValue"></see>
		''' </remarks>
		<Description("表示する最大日"), DefaultValue("9999/12/31 23:59:59")> _
		Public Property MaxDate As Date = Date.MaxValue

		''' <summary>
		''' 選択不可の日付の表示スタイルID
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("選択不可の日付の表示スタイルID")> _
		Public Property TrailStyle As String

		Private _defaultDayBackColor As Color = Color.White
		''' <summary>
		''' デフォルトで使用する日付の背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("デフォルトで使用する日付の背景色")> _
		Public Property DefaultDayBackColor As Color
			Get
				Return _defaultDayBackColor
			End Get
			Set(value As Color)
				_defaultDayBackColor = value
				For ii = 0 To _btnDays.GetUpperBound(0)
					Dim btns() As Control = Me.Controls.Find("btnDay" + (ii + 1).ToString, True)
					btns(0).BackColor = _defaultDayBackColor
				Next
			End Set
		End Property

		Private _defaultDayForeColor As Color = Color.FromArgb(42, 49, 60)
		''' <summary>
		''' デフォルトで使用する日付の文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("デフォルトで使用する日付の文字色")> _
		Public Property DefaultDayForeColor As Color
			Get
				Return _defaultDayForeColor
			End Get
			Set(value As Color)
				_defaultDayForeColor = value
				For ii = 0 To _btnDays.GetUpperBound(0)
					Dim btns() As Control = Me.Controls.Find("btnDay" + (ii + 1).ToString, True)
					btns(0).ForeColor = _defaultDayForeColor
				Next
			End Set
		End Property

		Private _defaultDayFont As Font = New Font("メイリオ", 15.75)
		''' <summary>
		''' デフォルトで使用する日付のフォント
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("デフォルトで使用する日付のフォント"), DefaultValue("メイリオ, 15.75pt, style=Bold")> _
		Public Property DefaultDayFont As Font
			Get
				Return _defaultDayFont
			End Get
			Set(value As Font)
				_defaultDayFont = value
				For ii = 0 To _btnDays.GetUpperBound(0)
					Dim btns() As Control = Me.Controls.Find("btnDay" + (ii + 1).ToString, True)
					btns(0).Font = _defaultDayFont
				Next
                Me.btnMonth0.Font = value
                Me.btnMonth1.Font = value
                Me.btnMonth2.Font = value
                Me.btnMonth3.Font = value
                Me.btnMonth4.Font = value
                Me.btnMonth5.Font = value
                Me.btnMonth6.Font = value
                Me.btnMonth7.Font = value
                Me.btnMonth8.Font = value
                Me.btnMonth9.Font = value
                Me.btnMonth10.Font = value
                Me.btnMonth11.Font = value
            End Set
        End Property

		''' <summary>
		''' 選択された日付の背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("選択された日付の背景色")> _
		Public Property SelectedDayBackColor As Color = Color.FromArgb(51, 255, 153)

		''' <summary>
		''' 選択された日付の文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("選択された日付の文字色")> _
		Public Property SelectedDayForeColor As Color = Color.White

		''' <summary>
		''' 選択された日付のフォント
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("選択された日付のフォント"), DefaultValue("メイリオ, 15.75pt")> _
		Public Property SelectedDayFont As Font = New Font("メイリオ", 15.75, FontStyle.Bold)

		''' <summary>
		''' タイトルのツールチップ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("タイトルのツールチップ")> _
		Public Property HeaderToolTip As String

		''' <summary>
		''' ＞のツールチップ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("＞のツールチップ")> _
		Public Property NextMonthToolTip As String

		''' <summary>
		''' ＜のツールチップ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("＜のツールチップ")> _
		Public Property PreMonthToolTip As String

		''' <summary>
		''' 本日のツールチップ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("本日のツールチップ")> _
		Public Property TodayToolTip As String

		Private _dayStyles As IDictionary(Of String, DayStyle)
		''' <summary>
		''' 休日以外の表示スタイル
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("休日以外の表示スタイル")> _
		Public ReadOnly Property DayStyles As IDictionary(Of String, DayStyle)
			Get
				If _dayStyles Is Nothing Then
					_dayStyles = New Dictionary(Of String, DayStyle)
				End If
				Return _dayStyles
			End Get
		End Property

		Private _holidayStyles As IDictionary(Of String, DayStyle)
		''' <summary>
		''' 休日用の表示スタイル
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("休日用の表示スタイル")> _
		Public ReadOnly Property HolidayStyles As IDictionary(Of String, DayStyle)
			Get
				If _holidayStyles Is Nothing Then
					_holidayStyles = New Dictionary(Of String, DayStyle)
				End If
				Return _holidayStyles
			End Get
		End Property

		Private _holiday As IDictionary(Of Date, String)
		''' <summary>
		''' 休日日付
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("休日日付")> _
		Public ReadOnly Property Holiday As IDictionary(Of Date, String)
			Get
				If _holiday Is Nothing Then
					_holiday = New Dictionary(Of Date, String)
				End If
				Return _holiday
			End Get
		End Property

		''' <summary>
		''' デザインモードか返す
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Private ReadOnly Property _isDesignMode As Boolean
			Get
				If Me.DesignMode Then
					Return True
				End If
				If WinUtil.UserControlDesignMode() Then
					Return True
				End If

				Return False
			End Get
		End Property

#End Region

		Private _targetMonth As Date
		Private _selectedButton As Button
		Private _btnDays(41) As Button
		Private _lblDays(41) As Label

		Private _optYear As Date
		Private _btnMonths(11) As Button

		Private _animate As New AnimateWindow

		Private _tip As ToolTip
		Private _tipDay As ToolTip

#End Region

#Region " コンストラクタ "

		Public Sub New()

			' この呼び出しはデザイナーで必要です。
			InitializeComponent()

			' InitializeComponent() 呼び出しの後で初期化を追加します。

			If String.IsNullOrEmpty(Me.TitleFormat) Then
				Me.TitleFormat = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.YearMonthPattern
			End If

			Me.lblDayOfWeek0.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Sunday)
			Me.lblDayOfWeek1.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Monday)
			Me.lblDayOfWeek2.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Tuesday)
			Me.lblDayOfWeek3.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Wednesday)
			Me.lblDayOfWeek4.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Thursday)
			Me.lblDayOfWeek5.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Friday)
			Me.lblDayOfWeek6.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.AbbreviatedDayNames(DayOfWeek.Saturday)

			Me.btnMonth0.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(0)
			Me.btnMonth1.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(1)
			Me.btnMonth2.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(2)
			Me.btnMonth3.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(3)
			Me.btnMonth4.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(4)
			Me.btnMonth5.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(5)
			Me.btnMonth6.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(6)
			Me.btnMonth7.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(7)
			Me.btnMonth8.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(8)
			Me.btnMonth9.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(9)
			Me.btnMonth10.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(10)
			Me.btnMonth11.Text = Thread.CurrentThread.CurrentUICulture.DateTimeFormat.MonthNames(11)

			If Me.MaxDate.Equals(Date.MinValue) Then
				Me.MaxDate = Date.MaxValue
			End If

			If _isDesignMode Then
				Return
			End If

			For ii = 0 To _btnMonths.GetUpperBound(0)
				Dim btns As Control() = Me.Controls.Find("btnMonth" + ii.ToString, True)
				If btns.Length = 0 Then
					Continue For
				End If

				Dim btn As Button = btns(0)

				AddHandler btn.Click, AddressOf btnMonth_Click

				_btnMonths(ii) = btn
			Next

			For ii = 0 To _btnDays.GetUpperBound(0)
				Dim btns As Control() = Me.Controls.Find("btnDay" + (ii + 1).ToString, True)
				If btns.Length = 0 Then
					Continue For
				End If

				Dim btn As Button = btns(0)
				Dim lbl As Label = New Label()

				_btnDays(ii) = btn
				_lblDays(ii) = lbl

				btn.Tag = Me.pnlTBody.GetCellPosition(btn)
				lbl.Dock = DockStyle.Fill
				lbl.TextAlign = ContentAlignment.MiddleCenter
				lbl.Margin = New Padding(0)
				lbl.Font = Me.DefaultDayFont
				lbl.BackColor = Me.DefaultDayBackColor
				lbl.ForeColor = Color.FromKnownColor(KnownColor.ControlLight)

				AddHandler btn.Click, AddressOf btnDay_Click
				AddHandler btn.KeyDown, AddressOf btnDay_KeyDown
				AddHandler btn.Enter, AddressOf btnDay_Enter
				AddHandler btn.Leave, AddressOf btnDay_Leave
			Next

			_selectedButton = Nothing
			_tip = New ToolTip
			_tipDay = New ToolTip
			_setValue(New Date(Now.Year, Now.Month, Now.Day))

			Me.pnlMonth.Visible = False

			Me.btnToday.Text = Now.ToString("yy/MM/dd")

			Me.btnMonth0.Tag = 1
			Me.btnMonth1.Tag = 2
			Me.btnMonth2.Tag = 3
			Me.btnMonth3.Tag = 4
			Me.btnMonth4.Tag = 5
			Me.btnMonth5.Tag = 6
			Me.btnMonth6.Tag = 7
			Me.btnMonth7.Tag = 8
			Me.btnMonth8.Tag = 9
			Me.btnMonth9.Tag = 10
			Me.btnMonth10.Tag = 11
			Me.btnMonth11.Tag = 12

			AddHandler Me.MouseWheel, AddressOf Calendar_MouseWheel
		End Sub

#End Region

#Region " Handles "

		''' <summary>
		''' ロード
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub Calendar_Load(sender As Object, e As System.EventArgs) Handles Me.Load
			If _isDesignMode Then
				Return
			End If

			_tip.SetToolTip(Me.lblHeader, Me.HeaderToolTip)
			_tip.SetToolTip(Me.btnNextMonth, Me.NextMonthToolTip)
			_tip.SetToolTip(Me.btnPreMonth, Me.PreMonthToolTip)
			_tip.SetToolTip(Me.btnToday, Me.TodayToolTip)
		End Sub

		''' <summary>
		''' このコントロール内でホイール移動イベント
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub Calendar_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs)
			Debug.Print("Calendar_MouseWheel:{0}", e.Delta)
			' ホイールスクロールを処理済みにする
			DirectCast(e, HandledMouseEventArgs).Handled = True

			If e.Delta > 0 Then
				_actPreMonth()
			Else
				_actNextMonth()
			End If
		End Sub

		Private Sub Calendar_KeyDown(keyData As System.Windows.Forms.Keys)
			Dim idx As Integer
			Dim btn As Button
			Dim btns As IList(Of Button) = New List(Of Button)
			For Each item As Button In _btnDays
				If Not item.Focused Then
					Continue For
				End If
				btns.Add(item)
			Next

			If btns.Count.Equals(0) Then
				btn = Nothing
			Else
				btn = btns(0)
			End If

			Select Case keyData
				Case Keys.Left
					If btn Is Nothing Then
						_actPreMonth()
						Return
					End If
					idx = Array.IndexOf(_btnDays, btn) - 1
					If _btnDays.GetLowerBound(0) > idx Then
						_actPreMonth()
						Return
					Else
						If Not _btnDays(idx).Visible Then
							_actPreMonth()
							Return
						End If
						_btnDays(idx).Focus()
					End If

				Case Keys.Right
					If btn Is Nothing Then
						_actNextMonth()
						Return
					End If
					idx = Array.IndexOf(_btnDays, btn) + 1
					If _btnDays.GetUpperBound(0) < idx Then
						_actNextMonth()
						Return
					Else
						If Not _btnDays(idx).Visible Then
							_actNextMonth()
							Return
						End If
						_btnDays(idx).Focus()
					End If

				Case Keys.Up
					If btn Is Nothing Then
						_actPreMonth()
						Return
					End If
					idx = Array.IndexOf(_btnDays, btn) - 7
					If _btnDays.GetLowerBound(0) > idx Then
						_actPreMonth()
						Return
					Else
						If Not _btnDays(idx).Visible Then
							_actPreMonth()
							Return
						End If
						_btnDays(idx).Focus()
					End If

				Case Keys.Down
					If btn Is Nothing Then
						_actNextMonth()
						Return
					End If
					idx = Array.IndexOf(_btnDays, btn) + 7
					If _btnDays.GetUpperBound(0) < idx Then
						_actNextMonth()
						Return
					Else
						If Not _btnDays(idx).Visible Then
							_actNextMonth()
							Return
						End If
						_btnDays(idx).Focus()
					End If

				Case Keys.PageUp
					_actPreMonth()

				Case Keys.PageDown
					_actNextMonth()

			End Select
		End Sub

		''' <summary>
		''' カレンダーの表示・非表示が変わった
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub Calendar_VisibleChanged(sender As Object, e As System.EventArgs) Handles Me.VisibleChanged
			If _isDesignMode Then
				Return
			End If
			If Not Me.Visible Then
				Return
			End If

			' カレンダー表示
			_setCalendar(_targetMonth.Year, _targetMonth.Month)
			If _selectedButton Is Nothing Then
				Dim lst() As Button = _getEnableDayButton()
				If lst IsNot Nothing Then
					lst(0).Focus()
				End If
			End If
		End Sub

		''' <summary>
		''' 前の月ボタンクリック処理
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnPreMon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreMonth.Click
			If Me.pnlOptionTBody.Visible Then
				_actPreYear()
				Return
			End If

			_actPreMonth()
		End Sub

		''' <summary>
		''' 次の月ボタンクリック処理
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnNextMon_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextMonth.Click
			If Me.pnlOptionTBody.Visible Then
				_actNextYear()
				Return
			End If

			_actNextMonth()
		End Sub

		''' <summary>
		''' カレンダーボタンクリック
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnDay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
			selectedChangeDay(CType(sender, Button))
		End Sub

		''' <summary>
		''' カレンダーボタンキー押下
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnDay_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
			Dim btn As Button
			Dim day As Date

			btn = CType(sender, Button)
			day = New Date(_targetMonth.Year, _targetMonth.Month, btn.Text)

			RaiseEvent DayKeyDown(day, e)

			If Me.Value = day Then
				_setDayStyleEnter(day.Year, day.Month, btn)
			Else
				_setDayStyleLeave(day.Year, day.Month, btn)
			End If
		End Sub

		''' <summary>
		''' カレンダーボタンの入力
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnDay_Enter(sender As Object, e As System.EventArgs)
			Dim btn As Button

			btn = CType(sender, Button)
			btn.FlatAppearance.BorderSize = 1
		End Sub

		''' <summary>
		''' カレンダーボタンから離れる
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnDay_Leave(sender As Object, e As System.EventArgs)
			Dim btn As Button

			btn = CType(sender, Button)
			btn.FlatAppearance.BorderSize = 0
		End Sub

		''' <summary>
		''' 月選択のボタンクリック
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnMonth_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
			Dim btn As Button

			btn = CType(sender, Button)

			_targetMonth = New Date(_optYear.Year, btn.Tag, 1)

			' カレンダー表示
			_setCalendar(_targetMonth.Year, _targetMonth.Month)
			If _selectedButton Is Nothing Then
				Dim lst() As Button = _getEnableDayButton()
				If lst IsNot Nothing Then
					lst(0).Focus()
				End If
			End If

			_closeMonthSelect(True)
		End Sub

		''' <summary>
		''' 月選択内でホイール移動イベント
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub pnlMonthList_MouseWheel(sender As Object, e As System.Windows.Forms.MouseEventArgs)
			' ホイールスクロールを処理済みにする
			DirectCast(e, HandledMouseEventArgs).Handled = True

			If e.Delta > 0 Then
				_actPreYear()
			Else
				_actNextYear()
			End If
		End Sub

		''' <summary>
		''' タイトル部のクリック
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub lblHeader_Click(sender As Object, e As System.EventArgs) Handles lblHeader.Click
			Me.pnlMonth.Location = Me.pnlTable.Location
			Me.pnlMonth.Size = Me.pnlTable.Size
			If Me.pnlMonth.Visible Then
				_closeMonthSelect(False)
			Else
				_showMonthSelect()
			End If
		End Sub

		''' <summary>
		''' 当日部のクリック
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub btnToday_Click(sender As System.Object, e As System.EventArgs) Handles btnToday.Click
			_closeMonthSelect(True)
			Me.Value = Now
		End Sub

#End Region
#Region " Overrides "

		Protected Overrides Function ProcessCmdKey(ByRef msg As System.Windows.Forms.Message, keyData As System.Windows.Forms.Keys) As Boolean
			If keyData = Keys.Right Or keyData = Keys.Left Or keyData = Keys.Up Or keyData = Keys.Down Or keyData = Keys.PageUp Or keyData = Keys.PageDown Then
				Calendar_KeyDown(keyData)
				Return True
			End If

			Return MyBase.ProcessCmdKey(msg, keyData)
		End Function

#End Region

		''' <summary>
		''' 選択日付ボタンの移動
		''' </summary>
		''' <param name="btn"></param>
		''' <remarks></remarks>
		Private Sub selectedChangeDay(ByVal btn As Button)
			Dim style As DayStyle = Nothing

			If _selectedButton IsNot Nothing Then
				_setDayStyleLeave(_targetMonth.Year, _targetMonth.Month, _selectedButton)
			End If

			_selectedButton = btn
			_setValue(New Date(_targetMonth.Year, _targetMonth.Month, _selectedButton.Text))

			_setDayStyleEnter(_targetMonth.Year, _targetMonth.Month, _selectedButton)

			RaiseEvent ChangedDay(_valueDate, New EventArgs)
		End Sub

		''' <summary>
		''' 日付が非選択状態のスタイル設定
		''' </summary>
		''' <param name="year"></param>
		''' <param name="month"></param>
		''' <param name="btn"></param>
		''' <remarks></remarks>
		Private Sub _setDayStyleLeave(ByVal year As Integer, ByVal month As Integer, ByVal btn As Button)
			Dim day As Date = New Date(year, month, btn.Text)
			Dim style As DayStyle = Nothing
			Dim wkBackColor As Color = Me.DefaultDayBackColor
			Dim wkForeColor As Color = Me.DefaultDayForeColor
			Dim wkFont As Font = Me.DefaultDayFont
			Dim wkCaption As String = String.Empty

			If Me.Holiday.ContainsKey(day) Then
				If Me.HolidayStyles.TryGetValue(Me.Holiday(day), style) Then
					wkBackColor = style.BackColor
					wkForeColor = style.ForeColor
					wkFont = style.Font
					wkCaption = style.Title
				End If
			End If

			btn.BackColor = wkBackColor
			btn.ForeColor = wkForeColor
			btn.Font = wkFont
			_tipDay.SetToolTip(btn, wkCaption)
		End Sub

		''' <summary>
		''' 日付が選択状態のスタイル設定
		''' </summary>
		''' <param name="year"></param>
		''' <param name="month"></param>
		''' <param name="btn"></param>
		''' <remarks></remarks>
		Private Sub _setDayStyleEnter(ByVal year As Integer, ByVal month As Integer, ByVal btn As Button)
			Dim day As Date = New Date(year, month, btn.Text)
			Dim style As DayStyle = Nothing
			Dim wkBackColor As Color = Me.SelectedDayBackColor
			Dim wkForeColor As Color = Me.SelectedDayForeColor
			Dim wkFont As Font = Me.SelectedDayFont
			Dim wkCaption As String = String.Empty

			If Me.Holiday.ContainsKey(Me.Value) Then
				If Me.HolidayStyles.TryGetValue(Me.Holiday(Me.Value), style) Then
					wkForeColor = style.BackColor
					wkFont = style.Font
					wkCaption = style.Title
				End If
			End If

			btn.BackColor = wkBackColor
			btn.ForeColor = wkForeColor
			btn.Font = wkFont
			_tipDay.SetToolTip(btn, wkCaption)
		End Sub

		''' <summary>
		''' 休日か判定する
		''' </summary>
		''' <param name="value"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function IsHoliday(ByVal value As Date) As Boolean
			Return Me.Holiday.ContainsKey(value)
		End Function

		''' <summary>
		''' 日付設定
		''' </summary>
		''' <param name="val"></param>
		''' <remarks></remarks>
		Private Sub _setValue(ByVal val As Object)
			_value = val
			If _value Is Nothing Then
				Return
			End If
			_valueDate = _value
			_targetMonth = New Date(_valueDate.Year, _valueDate.Month, 1)
		End Sub

		''' <summary>
		''' 前年に移動
		''' </summary>
		''' <remarks></remarks>
		Private Sub _actPreYear()
			If _optYear.Year <= Me.MinDate.Year Then
				Return
			End If

			_optYear = _optYear.AddYears(-1)

			_setMonth(_optYear, AnimateWindow.DirectionType.Left)
		End Sub

		''' <summary>
		''' 次年に移動
		''' </summary>
		''' <remarks></remarks>
		Private Sub _actNextYear()
			If _optYear.Year >= Me.MaxDate.Year Then
				Return
			End If

			_optYear = _optYear.AddYears(1)

			_setMonth(_optYear, AnimateWindow.DirectionType.Right)
		End Sub

		''' <summary>
		''' 前月に移動
		''' </summary>
		''' <remarks></remarks>
		Private Sub _actPreMonth()
			Dim selectedDay As Date = New Date(_targetMonth.Year, _targetMonth.Month, 1)

			If selectedDay <= Me.MinDate Then
				Return
			End If

			_targetMonth = New Date(selectedDay.AddMonths(-1).Year, selectedDay.AddMonths(-1).Month, 1)

			' カレンダー表示
			_setCalendar(_targetMonth.Year, _targetMonth.Month, AnimateWindow.DirectionType.Left)

			If _selectedButton IsNot Nothing Then
				Return
			End If

			Dim lst() As Button = _getEnableDayButton()
			If lst Is Nothing Then
				Return
			End If

			lst(lst.GetUpperBound(0)).Focus()
		End Sub

		''' <summary>
		''' 次月に移動
		''' </summary>
		''' <remarks></remarks>
		Private Sub _actNextMonth()
			Dim selectedDay As Date = New Date(_targetMonth.Year, _targetMonth.Month, Date.DaysInMonth(_targetMonth.Year, _targetMonth.Month))

			If selectedDay >= Me.MaxDate Then
				Return
			End If

			_targetMonth = New Date(selectedDay.AddMonths(1).Year, selectedDay.AddMonths(1).Month, 1)

			' カレンダー表示
			_setCalendar(_targetMonth.Year, _targetMonth.Month, AnimateWindow.DirectionType.Right)

			If _selectedButton IsNot Nothing Then
				Return
			End If

			Dim lst() As Button = _getEnableDayButton()
			If lst Is Nothing Then
				Return
			End If

			lst(0).Focus()
		End Sub

		''' <summary>
		''' 現在有効な日付のボタンを返す
		''' </summary>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getEnableDayButton() As Button()
			'Dim lst = From item In _btns Select item Where item.Visible
			Dim lst As List(Of Button) = New List(Of Button)
			For Each btn As Button In _btnDays
				If Not btn.Visible Then
					Continue For
				End If
				lst.Add(btn)
			Next
			If lst.Count.Equals(0) Then
				Return Nothing
			End If
			Return lst.ToArray
		End Function

		''' <summary>
		''' 月選択画面を表示
		''' </summary>
		''' <remarks></remarks>
		Private Sub _showMonthSelect()
			_setMonth(_targetMonth)

			RemoveHandler Me.MouseWheel, AddressOf Calendar_MouseWheel
			AddHandler Me.pnlMonth.MouseWheel, AddressOf pnlMonthList_MouseWheel

			'_animate.Center(Me.pnlOption, time:=100)
			_animate.Slide(Me.pnlMonth, AnimateWindow.DirectionType.Top, 100)
			Me.pnlMonth.Focus()
		End Sub

		''' <summary>
		''' 月選択の月表示を設定
		''' </summary>
		''' <param name="nowMonth"></param>
		''' <param name="direction"></param>
		''' <remarks></remarks>
		Private Sub _setMonth(ByVal nowMonth As Date, Optional ByVal direction As AnimateWindow.DirectionType = 99)
			Me.lblHeader.Text = nowMonth.ToString("yyyy")
			_optYear = New Date(nowMonth.Year, nowMonth.Month, 1)

			If direction = 99 Then
				Me.pnlOptionTBody.Visible = False
			Else
				_animate.SlideClose(Me.pnlOptionTBody, direction, 100)
			End If

			For ii As Integer = 0 To _btnMonths.GetUpperBound(0)
				Dim btn As Button = _btnMonths(ii)
				Dim dispMonth As Date = New Date(nowMonth.Year, ii + 1, 1)

				' 範囲外は無効
				If CInt(dispMonth.ToString("yyyyMM")) < CInt(CType(Me.MinDate, Date).ToString("yyyyMM")) OrElse
				  CInt(dispMonth.ToString("yyyyMM")) > CInt(CType(Me.MaxDate, Date).ToString("yyyyMM")) Then
					btn.Visible = False
					Continue For
				End If

				' ボタンの有効化,表示等
				btn.Enabled = True
				btn.Visible = True
				btn.BackColor = Me.DefaultDayBackColor
				btn.ForeColor = Me.DefaultDayForeColor
				btn.Font = Me.DefaultDayFont

				' 選択月を指定スタイルに設定
				If dispMonth.Year = nowMonth.Year And dispMonth.Month = nowMonth.Month Then
					btn.BackColor = Me.SelectedDayBackColor
					btn.ForeColor = Me.SelectedDayForeColor
					btn.Font = Me.SelectedDayFont
				End If
			Next

			If direction = 99 Then
				Me.pnlOptionTBody.Visible = True
			Else
				_animate.Slide(Me.pnlOptionTBody, direction, 100)
			End If
			Me.pnlMonth.Focus()
		End Sub

		''' <summary>
		''' 月選択画面を非表示
		''' </summary>
		''' <param name="selected"></param>
		''' <remarks></remarks>
		Private Sub _closeMonthSelect(ByVal selected As Boolean)
			AddHandler Me.MouseWheel, AddressOf Calendar_MouseWheel
			RemoveHandler Me.pnlMonth.MouseWheel, AddressOf pnlMonthList_MouseWheel

			If selected Then
				_animate.CenterClose(Me.pnlMonth, time:=100)
			Else
				_animate.SlideClose(Me.pnlMonth, AnimateWindow.DirectionType.Bottom, 100)
			End If

			Me.lblHeader.Text = _targetMonth.ToString(Me.TitleFormat)
		End Sub

		''' <summary>
		''' カレンダーボタン設定
		''' </summary>
		''' <param name="yy"></param>
		''' <param name="mm"></param>
		''' <remarks></remarks>
		Private Sub _setCalendar(ByVal yy As Integer, ByVal mm As Integer, Optional ByVal direction As AnimateWindow.DirectionType = 99)
			Dim ii As Integer
			Dim dd As Integer = 0
			Dim monthFirst As Date = New Date(yy, mm, 1)
			Dim daysInMonth As Integer

			RaiseEvent ChangedMonth(_targetMonth, New EventArgs)

			Me.lblHeader.Text = _targetMonth.ToString(Me.TitleFormat)
			_tipDay.RemoveAll()

			' 月の日数を求める
			daysInMonth = Date.DaysInMonth(monthFirst.Year, monthFirst.Month)

			If direction = 99 Then
				Me.pnlTBody.Visible = False
			Else
				_animate.SlideClose(Me.pnlTBody, direction, 100)
			End If

			_selectedButton = Nothing

			For ii = 0 To _btnDays.GetUpperBound(0)

				Dim btn As Button = _btnDays(ii)

				Dim pos As TableLayoutPanelCellPosition
				pos = btn.Tag

				Me.pnlTBody.Controls.Remove(btn)
				Me.pnlTBody.Controls.Remove(_lblDays(ii))

				' 月の日数以外のボタンに対して設定を行う
				If ii < monthFirst.DayOfWeek OrElse ii >= (monthFirst.DayOfWeek + daysInMonth) Then
					' ボタンの非表示
					btn.Visible = False
					Me.pnlTBody.Controls.Remove(btn)
					Continue For
				End If

				' 月の日数内のボタンに対して設定を行う
				Me.pnlTBody.Controls.Add(btn, pos.Column, pos.Row)

				' 表示日数カウントアップ
				dd += 1
				Dim day As Date = New Date(yy, mm, dd)

				' ボタンの有効化,表示等
				btn.Enabled = True
				btn.Visible = True
				btn.Text = dd.ToString
				btn.BackColor = Me.DefaultDayBackColor
				btn.ForeColor = Me.DefaultDayForeColor
				btn.Font = Me.DefaultDayFont

				' 選択日のスタイル設定
				If day.Year = _valueDate.Year AndAlso day.Month = _valueDate.Month AndAlso day.Day = _valueDate.Day Then
					btn.BackColor = Me.SelectedDayBackColor
					btn.ForeColor = Me.SelectedDayForeColor
					btn.Font = Me.SelectedDayFont
					btn.Focus()
					_selectedButton = btn
					Continue For
				End If

				' 指定日までのボタンを無効化
				If Date.Parse(yy & "/" & mm & "/" & dd.ToString) < Now.AddDays(-1) Then
					If _preSelectEnable Then
						Continue For
					End If

					_lblDays(ii).BackColor = Color.Transparent
					_lblDays(ii).Font = Me.DefaultDayFont
					_lblDays(ii).Text = btn.Text
					If Not String.IsNullOrEmpty(Me.TrailStyle) AndAlso Me.DayStyles(Me.TrailStyle) IsNot Nothing Then
						_lblDays(ii).BackColor = Me.DayStyles(Me.TrailStyle).BackColor
						_lblDays(ii).ForeColor = Me.DayStyles(Me.TrailStyle).ForeColor
						_lblDays(ii).Font = Me.DayStyles(Me.TrailStyle).Font
					End If
					btn.Visible = False
					Me.pnlTBody.Controls.Remove(btn)
					Me.pnlTBody.Controls.Add(_lblDays(ii), pos.Column, pos.Row)
				End If

				' 休日設定
				If Not Me.Holiday.ContainsKey(day) Then
					Continue For
				End If
				Dim style As DayStyle = Nothing
				If Me.HolidayStyles.TryGetValue(Me.Holiday(day), style) Then
					btn.BackColor = style.BackColor
					btn.ForeColor = style.ForeColor
					btn.Font = style.Font
					_tipDay.SetToolTip(btn, style.Title)
				End If
			Next


			If direction = 99 Then
				Me.pnlTBody.Visible = True
			Else
				_animate.Slide(Me.pnlTBody, direction, 100)
			End If

			If _selectedButton Is Nothing Then
				Return
			End If

			_selectedButton.Focus()
		End Sub

	End Class

End Namespace
