
Namespace Win

	''' <summary>
	''' カレンダーコントロールの日付表示スタイル
	''' </summary>
	''' <remarks></remarks>
	Public Class DayStyle

#Region " Property "

		''' <summary>
		''' スタイルを識別するID
		''' </summary>
		''' <remarks></remarks>
		Public Name As String

		''' <summary>
		''' タイトル
		''' </summary>
		''' <remarks>
		''' ボタンのツールチップとして表示されます。
		''' </remarks>
		Public Title As String

		''' <summary>
		''' ボタンのバックカラー
		''' </summary>
		''' <remarks></remarks>
		Public BackColor As System.Drawing.Color
		''' <summary>
		''' ボタンの文字色
		''' </summary>
		''' <remarks></remarks>
		Public ForeColor As System.Drawing.Color

		''' <summary>
		''' ボタンのフォント
		''' </summary>
		''' <remarks></remarks>
		Public Font As Font

#End Region

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <param name="name"></param>
		''' <remarks></remarks>
		Public Sub New(ByVal name As String)
			Me.Name = name
		End Sub

#End Region

	End Class

End Namespace
