
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' デバッグ印
    ''' </summary>
    ''' <remarks></remarks>
    <Description("デバッグ印"),
    ToolboxItem(True),
    ToolboxBitmap(GetType(resourceDummy), "Resources.DebugMarker.bmp"),
    DesignTimeVisible(True)>
    Public Class DebugMarker

		''' <summary>ツールチップテキスト</summary>
		Private _toolTipText As String

#Region " コンストラクタ "

		''' <summary>
		''' デフォルトコンストラクタ
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()

			' この呼び出しは、Windows フォーム デザイナで必要です。
			InitializeComponent()

			' InitializeComponent() 呼び出しの後で初期化を追加します。
			Me.Size = New Size(16, 16)
			Me.TabStop = False
		End Sub

#End Region

#Region " プロパティ "

		''' <summary>ツールチップテキスト</summary>
		<Description("ツールチップテキスト")> _
		Public Property ToolTipText() As String
			Get
				Return _toolTipText
			End Get
			Set(ByVal value As String)
				_toolTipText = value
				Me.toolTip.SetToolTip(Me, _toolTipText)
			End Set
		End Property

#End Region

#Region " イベント "

		''' <summary>
		''' コントロールロード
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub DebugMarker_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
			Me.BringToFront()
			Me.BackColor = Color.Transparent
		End Sub

		''' <summary>
		''' コントロールサイズ変更
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub DebugMarker_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
			'Me.Size = New Size(16, 16)
		End Sub

#End Region

	End Class

End Namespace
