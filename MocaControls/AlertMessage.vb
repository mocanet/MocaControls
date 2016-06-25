
Imports System.ComponentModel
Imports System.Drawing

Namespace Win

	''' <summary>
	''' アラートメッセージ
	''' </summary>
	''' <remarks></remarks>
	<Description("アラートメッセージ"), _
	  ToolboxItem(True),
	  DesignTimeVisible(True)> _
	Public Class AlertMessage

#Region " Declare"

		''' <summary>ウィンドウアニメーション</summary>
		Private _animateWindow As Moca.Win.AnimateWindow

        Private _timer As Timer

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

			' この呼び出しはデザイナーで必要です。
			InitializeComponent()

			' InitializeComponent() 呼び出しの後で初期化を追加します。

			If Moca.Win.WinUtil.UserControlDesignMode Then
				Return
			End If

            _animateWindow = New Moca.Win.AnimateWindow
            _timer = New Timer
            AddHandler _timer.Tick, AddressOf Timer_TicK
        End Sub

#End Region

#Region " Property "

		''' <summary>
		''' デフォルトの背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("デフォルトの背景色"), Browsable(True)>
		Public Property DefaultMessageBackColor As Color = Color.FromArgb(236, 241, 245)

		''' <summary>
		''' デフォルトの文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("デフォルトの文字色"), Browsable(True)>
		Public Property DefaultMessageForeColor As Color = Color.FromArgb(26, 26, 26)

		''' <summary>
		''' エラー時の背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("エラー時の背景色"), Browsable(True)>
		Public Property ErrorBackColor As Color = Color.FromArgb(242, 222, 222)

		''' <summary>
		''' エラー時の文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("エラー時の文字色"), Browsable(True)>
		Public Property ErrorForeColor As Color = Color.FromArgb(185, 74, 72)

		''' <summary>
		''' 正常時の背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("正常時の背景色"), Browsable(True)>
		Public Property SuccessBackColor As Color = Color.FromArgb(223, 240, 216)

		''' <summary>
		''' 正常時の文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("正常時の文字色"), Browsable(True)>
		Public Property SuccessForeColor As Color = Color.FromArgb(70, 136, 71)

		''' <summary>
		''' 警告時の背景色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("警告時の背景色"), Browsable(True)>
		Public Property WarnBackColor As Color = Color.FromArgb(252, 248, 227)

		''' <summary>
		''' 警告時の文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("警告時の文字色"), Browsable(True)>
		Public Property WarnForeColor As Color = Color.FromArgb(145, 111, 53)

		''' <summary>
		''' メッセージ内容
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		<Description("メッセージ内容"), Browsable(True)>
		Public Overrides Property Text As String
			Get
				Return Me.lblAlert.Text
			End Get
			Set(value As String)
				Me.lblAlert.Text = value
			End Set
		End Property

        ''' <summary>
        ''' フォント
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Description("フォント"), Browsable(True)>
        Public Overrides Property Font As Font
            Get
                Return Me.lblAlert.Font
            End Get
            Set(value As Font)
                Me.lblAlert.Font = value
            End Set
        End Property

        ''' <summary>
        ''' アニメーションの方向
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Description("アニメーションの方向"), Browsable(True)>
        Public Property DirectionType As Moca.Win.AnimateWindow.DirectionType = AnimateWindow.DirectionType.Top

        <Description("全体をクリックして閉じる"), Browsable(True)>
        Public Property FullClickClose As Boolean
            Get
                Return Not btnAlertClose.Visible
            End Get
            Set(value As Boolean)
                btnAlertClose.Visible = Not value
            End Set
        End Property

        <Description("表示して何秒後に閉じるか指定する。０は閉じない。"), Browsable(True)>
        Public Property AutoCloseSecond As Integer

#End Region
#Region " Handles "

        Private Sub AlertMessage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Me.lblAlert.BackColor = Me.DefaultMessageBackColor
            Me.lblAlert.ForeColor = Me.DefaultMessageForeColor

            If Moca.Win.WinUtil.UserControlDesignMode Then
                Return
            End If

            Clear()

            Me.TabStop = False
            Me.btnAlertClose.TabStop = False
        End Sub

        Private Sub btnAlertClose_Click(sender As System.Object, e As System.EventArgs) Handles btnAlertClose.Click
            _timer.Stop()

            Dim dt As Moca.Win.AnimateWindow.DirectionType
            Select Case Me.DirectionType
                Case AnimateWindow.DirectionType.Top
                    dt = AnimateWindow.DirectionType.Bottom
                Case AnimateWindow.DirectionType.Bottom
                    dt = AnimateWindow.DirectionType.Top
                Case AnimateWindow.DirectionType.Left
                    dt = AnimateWindow.DirectionType.Right
                Case AnimateWindow.DirectionType.Right
                    dt = AnimateWindow.DirectionType.Left
            End Select
            _animateWindow.SlideClose(Me, dt)
            Clear()
        End Sub

        Private Sub AlertMessage_Click(sender As Object, e As EventArgs)
            btnAlertClose_Click(sender, e)
        End Sub

        Private Sub Timer_TicK(sender As Object, e As EventArgs)
            btnAlertClose_Click(sender, e)
        End Sub

#End Region
#Region " Method "

        ''' <summary>
        ''' アラートクリア
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Clear()
			Me.lblAlert.BackColor = Me.DefaultMessageBackColor
			Me.lblAlert.ForeColor = Me.DefaultMessageForeColor
			Me.lblAlert.Text = String.Empty
			Me.btnAlertClose.BackColor = Me.lblAlert.BackColor
			Me.BackColor = Me.ForeColor
            Me.Visible = False
            _timer.Stop()
        End Sub

		''' <summary>
		''' 正常アラート
		''' </summary>
		''' <param name="msg"></param>
		''' <param name="args"></param>
		''' <remarks></remarks>
		Public Sub Success(ByVal msg As String, ParamArray args() As String)
            Success(AutoCloseSecond, msg, args)
        End Sub

        ''' <summary>
		''' 正常アラート
        ''' </summary>
        ''' <param name="second"></param>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        Public Sub Success(ByVal second As Integer, ByVal msg As String, ParamArray args() As String)
            _showAlert(second, Me.SuccessBackColor, Me.SuccessForeColor, msg, args)
        End Sub

        ''' <summary>
        ''' エラーアラート
        ''' </summary>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        ''' <remarks></remarks>
        Public Sub [Error](ByVal msg As String, ParamArray args() As String)
            [Error](AutoCloseSecond, msg, args)
        End Sub

        ''' <summary>
        ''' エラーアラート
        ''' </summary>
        ''' <param name="second"></param>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        Public Sub [Error](ByVal second As Integer, ByVal msg As String, ParamArray args() As String)
            _showAlert(second, Me.ErrorBackColor, Me.ErrorForeColor, msg, args)
        End Sub

        ''' <summary>
        ''' ワーニングアラート
        ''' </summary>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        ''' <remarks></remarks>
        Public Sub Warn(ByVal msg As String, ParamArray args() As String)
            Warn(AutoCloseSecond, msg, args)
        End Sub

        ''' <summary>
        ''' ワーニングアラート
        ''' </summary>
        ''' <param name="second"></param>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        Public Sub Warn(ByVal second As Integer, ByVal msg As String, ParamArray args() As String)
            _showAlert(second, Me.WarnBackColor, Me.WarnForeColor, msg, args)
        End Sub

        ''' <summary>
        ''' アラートを表示
        ''' </summary>
        ''' <param name="second"></param>
        ''' <param name="labelBackColor"></param>
        ''' <param name="labelForeColor"></param>
        ''' <param name="msg"></param>
        ''' <param name="args"></param>
        ''' <remarks></remarks>
        Private Sub _showAlert(ByVal second As Integer, ByVal labelBackColor As Color, labelForeColor As Color, ByVal msg As String, ParamArray args() As String)
            Me.lblAlert.BackColor = labelBackColor
            Me.lblAlert.ForeColor = labelForeColor
            Me.lblAlert.Text = String.Format(msg, args)
            Me.btnAlertClose.BackColor = Me.lblAlert.BackColor
            Me.btnAlertClose.ForeColor = labelForeColor
            Me.BackColor = labelForeColor
            If Me.FullClickClose Then
                Me.Cursor = Cursors.Hand
                AddHandler Me.lblAlert.Click, AddressOf AlertMessage_Click
                Me.btnAlertClose.Visible = False
            Else
                Me.Cursor = Cursors.Default
                RemoveHandler Me.lblAlert.Click, AddressOf AlertMessage_Click
                Me.btnAlertClose.Visible = True
            End If
            _animateWindow.Slide(Me, DirectionType)
            Me.btnAlertClose.Refresh()
            Me.Parent.Focus()
            If second.Equals(0) Then
                Return
            End If
            _timer.Interval = second * 1000
            _timer.Stop()
            _timer.Start()
        End Sub

#End Region

    End Class

End Namespace
