
Imports System.Text
Imports System.Drawing

Namespace Win

	''' <summary>
	''' WYSIWYG エディター
	''' </summary>
	''' <remarks></remarks>
	Public Class WYSIWYGEditor

		''' <summary>
		''' フォントサイズ
		''' </summary>
		''' <remarks></remarks>
		Public Enum FontSize As Integer
			xsmall = 10	' 75%:12px:10.5px
			small = 12 ' 88%:14px:12px
			medium = 14	' 100%:16px:14px
			large = 17 ' 120%:19px:17.5px
			xlarge = 24	' 150%:24px:24.5px
		End Enum

#Region " Property "

		''' <summary>
		''' フォント
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overrides Property Font As System.Drawing.Font
			Get
				Return MyBase.Font
			End Get
			Set(value As System.Drawing.Font)
				MyBase.Font = value
				Me.rtxtSource.Font = value
			End Set
		End Property

		''' <summary>
		''' 文字色
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overrides Property ForeColor As System.Drawing.Color
			Get
				Return MyBase.ForeColor
			End Get
			Set(value As System.Drawing.Color)
				MyBase.ForeColor = value
				Me.rtxtSource.ForeColor = value
			End Set
		End Property

		''' <summary>
		''' データバインディング
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Shadows ReadOnly Property DataBindings As ControlBindingsCollection
			Get
				Return Me.rtxtSource.DataBindings
			End Get
		End Property

		''' <summary>
		''' IMEモード
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Protected Overrides Property ImeModeBase As System.Windows.Forms.ImeMode
			Get
				Return MyBase.ImeModeBase
			End Get
			Set(value As System.Windows.Forms.ImeMode)
				MyBase.ImeModeBase = value
				Me.rtxtSource.ImeMode = value
			End Set
		End Property

		''' <summary>
		''' 入力内容
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Overrides Property Text As String
			Get
				Return Me.rtxtSource.Text
			End Get
			Set(value As String)
				Me.rtxtSource.Text = value
			End Set
		End Property

		''' <summary>
		''' リッチテキスト内容
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property Rtf As String
			Get
				Return Me.rtxtSource.Rtf
			End Get
			Set(value As String)
				Me.rtxtSource.Rtf = value
			End Set
		End Property

		''' <summary>
		''' 入力内容のHTMLコード
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property ToHTML As String
			Get
				Return _rich2HTML(Me.rtxtSource)
			End Get
		End Property

#End Region

#Region " Handles "

		''' <summary>
		''' 太字かどうかを示す
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnBold_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnBold.Click
			If Me.rtxtSource.SelectedText.Length.Equals(0) Then
				Return
			End If
			Me.rtxtSource.SelectionFont = New Drawing.Font(Me.rtxtSource.SelectionFont, _getFontStyle(Not Me.rtxtSource.SelectionFont.Bold, Me.rtxtSource.SelectionFont.Italic, Me.rtxtSource.SelectionFont.Underline, Me.rtxtSource.SelectionFont.Strikeout))
		End Sub

		''' <summary>
		''' 斜体スタイルが適用されているかどうかを示す
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnItalic_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnItalic.Click
			If Me.rtxtSource.SelectedText.Length.Equals(0) Then
				Return
			End If
			Me.rtxtSource.SelectionFont = New Drawing.Font(Me.rtxtSource.SelectionFont, _getFontStyle(Me.rtxtSource.SelectionFont.Bold, Not Me.rtxtSource.SelectionFont.Italic, Me.rtxtSource.SelectionFont.Underline, Me.rtxtSource.SelectionFont.Strikeout))
		End Sub

		''' <summary>
		''' 下線付きかどうかを示す
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnUnderline_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnUnderline.Click
			If Me.rtxtSource.SelectedText.Length.Equals(0) Then
				Return
			End If
			Me.rtxtSource.SelectionFont = New Drawing.Font(Me.rtxtSource.SelectionFont, _getFontStyle(Me.rtxtSource.SelectionFont.Bold, Me.rtxtSource.SelectionFont.Italic, Not Me.rtxtSource.SelectionFont.Underline, Me.rtxtSource.SelectionFont.Strikeout))
		End Sub

		''' <summary>
		''' フォントを通る水平線を指定するかどうかを示す
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnStrikeout_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnStrikeout.Click
			If Me.rtxtSource.SelectedText.Length.Equals(0) Then
				Return
			End If
			Me.rtxtSource.SelectionFont = New Drawing.Font(Me.rtxtSource.SelectionFont, _getFontStyle(Me.rtxtSource.SelectionFont.Bold, Me.rtxtSource.SelectionFont.Italic, Me.rtxtSource.SelectionFont.Underline, Not Me.rtxtSource.SelectionFont.Strikeout))
		End Sub

		''' <summary>
		''' 文字を大きくする
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnSizeUp_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnSizeUp.Click
			Select Case CInt(Me.rtxtSource.SelectionFont.Size)
				Case Is <= FontSize.xsmall
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.small, Me.rtxtSource.SelectionFont.Style)
				Case Is <= FontSize.small
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.medium, Me.rtxtSource.SelectionFont.Style)
				Case Is <= FontSize.medium
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.large, Me.rtxtSource.SelectionFont.Style)
				Case Is <= FontSize.large
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.xlarge, Me.rtxtSource.SelectionFont.Style)
			End Select
		End Sub

		''' <summary>
		''' 文字を小さくする
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnSizeDown_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnSizeDown.Click
			Select Case CInt(Me.rtxtSource.SelectionFont.Size)
				Case Is >= FontSize.xlarge
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.large, Me.rtxtSource.SelectionFont.Style)
				Case Is >= FontSize.large
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.medium, Me.rtxtSource.SelectionFont.Style)
				Case Is >= FontSize.medium
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.small, Me.rtxtSource.SelectionFont.Style)
				Case Is >= FontSize.small
					Me.rtxtSource.SelectionFont = New Font(Me.rtxtSource.SelectionFont.FontFamily, FontSize.xsmall, Me.rtxtSource.SelectionFont.Style)
			End Select
		End Sub

		''' <summary>
		''' 文字色を変更
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnColor_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnColor.Click
			If Me.rtxtSource.SelectedText.Length.Equals(0) Then
				Return
			End If
			Me.ColorDialog1.ShowDialog(Me)
			Me.rtxtSource.SelectionColor = Me.ColorDialog1.Color
		End Sub

		''' <summary>
		''' ブラウザでの描画確認
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub tsbtnBrowserView_Click(sender As System.Object, e As System.EventArgs) Handles tsbtnBrowserView.Click
			Me.rtxtSource.Visible = Not Me.tsbtnBrowserView.Checked
			Me.ieView.Visible = Me.tsbtnBrowserView.Checked
			If Not Me.tsbtnBrowserView.Checked Then
				Me.rtxtSource.Focus()
				Return
			End If
			Const C_DIV As String = "<div style='font-family: ""{0}"";'>{1}</div>"
			Me.ieView.DocumentText = String.Format(C_DIV, Me.rtxtSource.Font.FontFamily.Name, _rich2HTML(Me.rtxtSource))
		End Sub

#End Region
#Region " Methods "

		''' <summary>
		''' 変換
		''' </summary>
		''' <param name="rtbBox"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _rich2HTML(ByVal rtbBox As RichTextBox) As String
			Dim sb As StringBuilder = New StringBuilder(1024)
			Dim ii As Integer, bold As Boolean, underline As Boolean, italic As Boolean, strikeout As Boolean,
			 foreColor As Boolean, nowForeColor As Drawing.Color,
			fontSize As Boolean, nowFontSize As Single

			For ii = 0 To rtbBox.Text.Length - 1
				rtbBox.SelectionStart = ii
				rtbBox.SelectionLength = 1

				If Not bold AndAlso rtbBox.SelectionFont.Bold Then
					bold = True
					sb.Append("<b>")
				ElseIf bold = True AndAlso Not rtbBox.SelectionFont.Bold Then
					bold = False
					sb.Append("</b>")
				End If

				If Not italic AndAlso rtbBox.SelectionFont.Italic Then
					italic = True
					sb.Append("<i>")
				ElseIf italic AndAlso Not rtbBox.SelectionFont.Italic Then
					italic = False
					sb.Append("</i>")
				End If

				If Not strikeout AndAlso rtbBox.SelectionFont.Strikeout Then
					strikeout = True
					sb.Append("<s>")
				ElseIf strikeout AndAlso Not rtbBox.SelectionFont.Strikeout Then
					strikeout = False
					sb.Append("</s>")
				End If

				If Not underline AndAlso rtbBox.SelectionFont.Underline Then
					underline = True
					sb.Append("<u>")
				ElseIf underline AndAlso Not rtbBox.SelectionFont.Underline Then
					underline = False
					sb.Append("</u>")
				End If

				If foreColor AndAlso rtbBox.SelectionColor <> nowForeColor Then
					foreColor = False
					sb.Append("</font>")
				End If
				If Not foreColor AndAlso rtbBox.SelectionColor <> nowForeColor Then
					foreColor = True
					nowForeColor = rtbBox.SelectionColor
					sb.AppendFormat("<font color='{0}'>", Drawing.ColorTranslator.ToHtml(rtbBox.SelectionColor))
				End If

				If fontSize AndAlso rtbBox.SelectionFont.Size <> nowFontSize Then
					fontSize = False
					sb.Append("</span>")
				End If
				If Not fontSize AndAlso rtbBox.SelectionFont.Size <> nowFontSize Then
					fontSize = True
					nowFontSize = rtbBox.SelectionFont.Size
					sb.AppendFormat("<span style='font-size:{0}pt;'>", nowFontSize)
				End If

				Dim ss As String = rtbBox.Text.Substring(ii, 1)
				Select Case ss
					Case vbTab
						sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;")
					Case vbLf
						sb.AppendLine("<br />")
					Case " "
						sb.Append("&nbsp;")
					Case Else
						sb.Append(ss)
				End Select
			Next

			If foreColor Then
				foreColor = False
				sb.Append("</font>")
			End If
			If fontSize Then
				foreColor = False
				sb.Append("</span>")
			End If

			Return sb.ToString
		End Function

		''' <summary>
		''' フォントスタイルコンバート
		''' </summary>
		''' <param name="Bold"></param>
		''' <param name="Italic"></param>
		''' <param name="Underline"></param>
		''' <param name="Strikeout"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getFontStyle(ByVal Bold As Boolean, ByVal Italic As Boolean, ByVal Underline As Boolean, ByVal Strikeout As Boolean) As Drawing.FontStyle
			Dim style As Drawing.FontStyle = FontStyle.Regular

			If Bold Then
				style = style Or FontStyle.Bold
			End If
			If Italic Then
				style = style Or FontStyle.Italic
			End If
			If Underline Then
				style = style Or FontStyle.Underline
			End If
			If Strikeout Then
				style = style Or FontStyle.Strikeout
			End If

			Return style
		End Function

#End Region

	End Class

End Namespace
