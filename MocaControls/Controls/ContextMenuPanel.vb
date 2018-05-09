
Imports System.ComponentModel
Imports Moca.Win

Namespace Win

    ''' <summary>
    ''' コンテキストメニュー的なパネルコントロール
    ''' </summary>
    <Description("コンテキストメニュー的なパネルコントロール"),
     ToolboxItem(True),
     DesignTimeVisible(True)>
    Public Class ContextMenuPanel
        Inherits FlowLayoutPanel

#Region " Declare "

        Private _aw As New AnimateWindow()
        Private _tim As New Timer
        Private _opener As Control

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()

            If DesignMode Then
                Return
            End If

            BringToFront()
            Visible = False
            DoubleBuffered = True
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' 表示する際にクリックされるコントロール
        ''' </summary>
        ''' <returns></returns>
        Public Property Opener As Control
            Get
                Return _opener
            End Get
            Set(value As Control)
                _opener = value
                If _opener Is Nothing Then
                    Return
                End If
                _opener.Cursor = Cursors.Hand
                AddHandler _opener.Click, AddressOf Me.ShowMenu
            End Set
        End Property

        ''' <summary>
        ''' 表示する時のアニメーション種別
        ''' </summary>
        ''' <returns></returns>
        Public Property DirectionType As AnimateWindow.DirectionType = AnimateWindow.DirectionType.Top

#End Region

#Region " Handles "

        ''' <summary>
        ''' フォーカスを受けた
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub ContextMenuPanel_GotFocus(sender As Object, e As EventArgs) Handles Me.GotFocus
            _aw.Slide(Me, DirectionType)
            Me.Focus()

            AddHandler _tim.Tick, New EventHandler(AddressOf _tim_Tick)
            _tim.Interval = 500
            _tim.Enabled = True
        End Sub

        ''' <summary>
        ''' タイマー間隔経過
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Private Sub _tim_Tick(sender As Object, e As EventArgs)
			If Opener.IsDisposed Then
				_tim.Enabled = False
				Return
			End If

			If _getClientContainState(Opener) OrElse
				_getClientContainState(Me) Then
				Return
			End If

			Dim closeDirectionType As AnimateWindow.DirectionType
            If DirectionType = AnimateWindow.DirectionType.Top Then
                closeDirectionType = AnimateWindow.DirectionType.Bottom
            End If
            If DirectionType = AnimateWindow.DirectionType.Bottom Then
                closeDirectionType = AnimateWindow.DirectionType.Top
            End If
            If DirectionType = AnimateWindow.DirectionType.Left Then
                closeDirectionType = AnimateWindow.DirectionType.Right
            End If
            If DirectionType = AnimateWindow.DirectionType.Right Then
                closeDirectionType = AnimateWindow.DirectionType.Left
            End If
            _aw.SlideClose(Me, closeDirectionType)
            Me.Parent.Refresh()
            _tim.Enabled = False
        End Sub

#End Region
#Region " Method "

        ''' <summary>
        ''' 表示
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        Public Sub ShowMenu(sender As Object, e As EventArgs)
            Dim meHeight As Integer
            For Each ctrl As Control In Me.Controls
                ctrl.Width = Me.Width + 1
                If meHeight < (ctrl.Top + ctrl.Height) Then
                    meHeight = ctrl.Top + ctrl.Height
                End If
            Next
            Height = meHeight
            BringToFront()

            Dim aw As New AnimateWindow()
            aw.Slide(Me, AnimateWindow.DirectionType.Top)
            Focus()

            AddHandler _tim.Tick, New EventHandler(AddressOf _tim_Tick)
            _tim.Interval = 500
            _tim.Enabled = True
        End Sub

        ''' <summary>
        ''' ある点がコントロールやフォームのクライアント領域内に
        ''' 含まれるかどうかに関する情報を取得する
        ''' </summary>
        ''' <param name="ctrl"></param>
        ''' <returns></returns>
        Private Function _getClientContainState(ByVal ctrl As Control) As String
            Dim rect As Rectangle = ctrl.ClientRectangle
            Return _getContainState(ctrl, rect)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="ctrl"></param>
        ''' <param name="rect"></param>
        ''' <returns></returns>
        Private Function _getContainState(ByVal ctrl As Control, ByVal rect As Rectangle) As Boolean
            ' マウス座標（スクリーン座標系）の取得
            Dim mouseScreenPos As Point = Control.MousePosition
            ' マウス座標をクライアント座標系へ変換
            Dim mouseClientPos As Point = ctrl.PointToClient(mouseScreenPos)
            ' マウス座標（クライアント座標系）が領域内かどうか
            Dim inside As Boolean = rect.Contains(mouseClientPos)
            Return inside
        End Function

#End Region

    End Class

End Namespace
