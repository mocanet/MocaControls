
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' AOP を利用するためのコントローラ
    ''' </summary>
    ''' <remarks></remarks>
    <DisplayName("MocaDi"),
     Description("AOP を利用するためのコントローラ"),
     ToolboxItem(False),
     ToolboxBitmap(GetType(resourceDummy), "MocaDi.bmp"),
     DesignTimeVisible(True)>
    Public Class MocaDi

		''' <summary>ページに対しての依存性注入</summary>
		Private _injector As Di.MocaInjector

#Region " コンストラクタ "

		Public Sub New()

			' この呼び出しは、Windows フォーム デザイナで必要です。
			InitializeComponent()

			' InitializeComponent() 呼び出しの後で初期化を追加します。

			Me.Size = New Size(16, 16)
			Me.TabStop = False

			If WinUtil.UserControlDesignMode() Then
				Exit Sub
			End If

			Di.MocaContainerFactory.Init()

			' コンテナ 準備
			_injector = New Di.MocaInjector()
		End Sub

#End Region
#Region " Handles "

		Private Sub MocaDi_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
			If WinUtil.UserControlDesignMode() Then
				Exit Sub
			End If

			_injector.Dispose()
		End Sub

		Private Sub MocaDi_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
			If WinUtil.UserControlDesignMode() Then
				Exit Sub
			End If

			Me.Visible = False
		End Sub

		Private Sub MocaDi_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
			'Me.Size = New Size(16, 16)
		End Sub

#End Region
#Region " Overrides "

		Protected Overrides Sub InitLayout()
			MyBase.InitLayout()

			If WinUtil.UserControlDesignMode() Then
				Exit Sub
			End If

			_injector.Inject(Me.ParentForm)
		End Sub

#End Region

	End Class

End Namespace
