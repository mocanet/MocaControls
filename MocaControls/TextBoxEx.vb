
Imports System.Text.RegularExpressions

Imports System.ComponentModel
Imports System.Windows.Forms

Namespace Win

    ''' <summary>
    ''' 標準のTextBoxで入力制限等を扱えるように拡張したコントロール
    ''' </summary>
    ''' <remarks>
    ''' ・テキストボックスのIMEは無効とします。<br/>
    ''' ・フォーカスが当たると文字を選択状態にします。<br/>
    ''' ・Enterキーは判定してPressEnterKeyイベントを発行します。<br/>
    ''' ・コピペ処理を考慮しています。<br/>
    ''' <br/>
    ''' ※下記サイトを参考に作成しています。<Br/>
    ''' http://jeanne.wankuma.com/tips/vb.net/textbox/permitchars.html
    ''' </remarks>
    <Description("標準のTextBoxで入力制限等を扱えるように拡張したコントロール"),
    ToolboxItem(True),
    DesignTimeVisible(True)>
    Public Class TextBoxEx

#Region " Declare "

#Region " DllImport "

        <System.Runtime.InteropServices.DllImport("USER32.DLL",
         CharSet:=System.Runtime.InteropServices.CharSet.Auto)>
        Private Shared Function SendMessage(
  ByVal hWnd As IntPtr,
  ByVal wMsg As Integer,
  ByVal wParam As Integer,
  ByVal lParam As String) As Integer
        End Function

#End Region

#Region " Enum "

        ''' <summary>入力制御種別列挙型</summary>
        <Flags()>
        Public Enum InputFormatType As Integer
            ''' <summary>制御無し</summary>
            None = 0
            ''' <summary>数字のみ</summary>
            Number = 1
            ''' <summary>数値</summary>
            Digit = Number * 2
            ''' <summary>英字</summary>
            Alpha = Digit * 2
            ''' <summary>スペース含む</summary>
            WithSpace = Alpha * 2
            ''' <summary>大文字</summary>
            Upper = WithSpace * 2
            ''' <summary>符号</summary>
            ''' <remarks>
            ''' <see cref="InputFormatType.Digit"/>、<see cref="InputFormatType.Number"/>指定時のみ有効
            ''' </remarks>
            Sign = Upper * 2
            ''' <summary>符号（マイナス時は赤文字）</summary>
            ''' <remarks>
            ''' <see cref="InputFormatType.Digit"/>、<see cref="InputFormatType.Number"/>指定時のみ有効
            ''' </remarks>
            SignWithColor = Sign * 2
            ''' <summary>記号</summary>
            Symbol = SignWithColor * 2
            ''' <summary>カスタム</summary>
            Custom = Symbol * 2
        End Enum

#End Region

        ''' <summary>入力制御種別別の許可する文字種</summary>
        Protected Shared inputFormatStrings() As String = New String() {String.Empty, "[0-9]", "[A-Za-z0-9]", "[ A-Za-z0-9]"}

        ''' <summary>Enterキー押下イベント</summary>
        Public Event PressEnterKey()

        ''' <summary>文字メッセージ</summary>
        Private Const WM_CHAR As Integer = &H102
        ''' <summary>貼り付けメッセージ</summary>
        Private Const WM_PASTE As Integer = &H302
        ''' <summary>キー押下メッセージ</summary>
        Private Const WM_KEYDOWN As Integer = &H100

        ''' <summary>貼り付け処理中フラグ</summary>
        Private _nowPaste As Boolean
        ''' <summary>区切り入力中フラグ</summary>
        Private _nowChange As Boolean

        ''' <summary>入力制御種別</summary>
        Private _inputFormat As InputFormatType = InputFormatType.None

        ''' <summary>小数点以下の桁数</summary>
        Private _percision As Integer = 0
        ''' <summary>小数点入力済みフラグ</summary>
        Private _percisionSign As Boolean = False
        ''' <summary>区切り</summary>
        Private _separator As String = String.Empty
        ''' <summary>小数点の右側にある保存できる最大文字</summary>
        Private _scale As Integer = 0
        ''' <summary>元々のテキストボックスの最大入力桁数</summary>
        Private _orgMaxLength As Integer = 0
        ''' <summary>元々のテキストボックスの文字色</summary>
        Private _orgForeColor As Color

        Private _bottomBorderColor As Color
        Private _bottomBorder As Label

        Private _customChars As String

#End Region

#Region " Property "

        ''' <summary>
        ''' 入力制御種別
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property InputFormat() As InputFormatType
            Get
                Return Me._inputFormat
            End Get
            Set(ByVal value As InputFormatType)
                Me._inputFormat = value
                If _isFormat(InputFormatType.Digit) Then
                    If _isFormat(InputFormatType.Alpha) Or _isFormat(InputFormatType.Number) Or _isFormat(InputFormatType.Symbol) Or _isFormat(InputFormatType.Upper) Or _isFormat(InputFormatType.WithSpace) Then
                        Throw New ArgumentException("From being combined with the Digit Only SignWithColor and Sign.")
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' 小数点以下の桁数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Percision() As System.Int32
            Get
                Return Me._percision
            End Get
            Set(ByVal value As System.Int32)
                Me._percision = value
            End Set
        End Property

        ''' <summary>
        ''' 小数点入力済みフラグ
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property PercisionSign() As System.Boolean
            Get
                Return Me._percisionSign
            End Get
            Set(ByVal value As System.Boolean)
                Me._percisionSign = value
            End Set
        End Property

        ''' <summary>
        ''' 区切り
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property Separator() As System.String
            Get
                Return Me._separator
            End Get
            Set(ByVal value As System.String)
                Me._separator = value
            End Set
        End Property

        ''' <summary>
        ''' 小数点の右側にある保存できる最大文字
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NumericScale() As System.Int32
            Get
                Return Me._scale
            End Get
            Set(ByVal value As System.Int32)
                Me._scale = value
            End Set
        End Property

        ''' <summary>
        ''' 負数の表示色
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property NegativeColor() As Color

        ''' <summary>
        ''' 英字指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsAlpha() As Boolean
            Get
                Return _isFormat(InputFormatType.Alpha)
            End Get
        End Property

        ''' <summary>
        ''' 数値指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsDigit() As Boolean
            Get
                Return _isFormat(InputFormatType.Digit)
            End Get
        End Property

        ''' <summary>
        ''' 数字指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsNumber() As Boolean
            Get
                Return _isFormat(InputFormatType.Number)
            End Get
        End Property

        ''' <summary>
        ''' 大文字指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsUpper() As Boolean
            Get
                Return _isFormat(InputFormatType.Upper)
            End Get
        End Property

        ''' <summary>
        ''' スペース許容有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsWithSpace() As Boolean
            Get
                Return _isFormat(InputFormatType.WithSpace)
            End Get
        End Property

        ''' <summary>
        ''' 符号許容有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' <see cref="InputFormatType.Digit"/>、<see cref="InputFormatType.Number"/>指定時のみ有効
        ''' </remarks>
        Public ReadOnly Property IsSign() As Boolean
            Get
                Return _isFormat(InputFormatType.Sign)
            End Get
        End Property

        ''' <summary>
        ''' 符号許容有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' <see cref="InputFormatType.Digit"/>、<see cref="InputFormatType.Number"/>指定時のみ有効
        ''' </remarks>
        Public ReadOnly Property IsSignWithColor() As Boolean
            Get
                Return _isFormat(InputFormatType.SignWithColor)
            End Get
        End Property

        ''' <summary>
        ''' 記号指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsSymbol() As Boolean
            Get
                Return _isFormat(InputFormatType.Symbol)
            End Get
        End Property

        ''' <summary>
        ''' カスタム指定有無
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property IsCustom() As Boolean
            Get
                Return _isFormat(InputFormatType.Custom)
            End Get
        End Property

        <Category("Appearance")>
        <Description("下線の色")>
        Public Property BottomBorderColor As Color
            Get
                Return _bottomBorderColor
            End Get
            Set(value As Color)
                _bottomBorderColor = value
                If _bottomBorder IsNot Nothing Then
                    _bottomBorder.BackColor = _bottomBorderColor
                End If
            End Set
        End Property

        ''' <summary>
        ''' 許可する特定の文字
        ''' </summary>
        ''' <returns></returns>
        <Description("許可する特定の文字を場合の文字。InputFormat プロパティに Custom を追加指定してください。")>
        Public Property CustomChars As String
            Get
                Return _customChars
            End Get
            Set(value As String)
                _customChars = value
            End Set
        End Property

        <Description("必須項目かどうか")>
        Public Property Required As Boolean

#End Region

#Region "　Overrides　"

        ''' <summary>
        ''' 入力文字最大桁数
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Property MaxLength() As Integer
            Get
                Return MyBase.MaxLength
            End Get
            Set(ByVal value As Integer)
                MyBase.MaxLength = value
                _orgMaxLength = value
            End Set
        End Property

        ''' <summary>
        ''' 文字色
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overrides Property ForeColor() As Color
            Get
                Return MyBase.ForeColor
            End Get
            Set(ByVal value As Color)
                MyBase.ForeColor = value
                _orgForeColor = value
            End Set
        End Property

        ''' <summary>
        ''' WndProc イベント
        ''' </summary>
        ''' <param name="m"></param>
        ''' <remarks></remarks>
        Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

            Select Case m.Msg
                Case WM_KEYDOWN
                    Dim keyCode As Keys = CType(m.WParam.ToInt32, Keys) And Keys.KeyCode
                    _onKeyDown(keyCode)

                Case WM_CHAR
                    Dim eKeyPress As New KeyPressEventArgs(Microsoft.VisualBasic.ChrW(m.WParam.ToInt32()))
                    Me.OnChar(eKeyPress)
                    If eKeyPress.Handled Then
                        Return
                    End If

                Case WM_PASTE
                    If Not _nowPaste Then
                        If Me.InputFormat <> InputFormatType.None Then
                            Me.OnPaste(New System.EventArgs())
                            Return
                        End If
                    End If
            End Select

            MyBase.WndProc(m)
        End Sub

        Private Sub _onKeyDown(ByVal keyCode As Keys)
            Select Case keyCode
                Case Keys.Enter
                    RaiseEvent PressEnterKey()

                Case Keys.Delete
                    _OnCharBackSpace()

            End Select
        End Sub

#End Region
#Region "　Overridable　"

        ''' <summary>
        ''' 文字列がクライアント領域に送信された時に呼び出されるメソッド
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnChar(ByVal e As KeyPressEventArgs)
            ' 入力桁数算出
            _scale = _orgMaxLength
            If Me.Percision > 0 Then
                _scale = _orgMaxLength - Me.Percision - 1
            End If

            If Char.IsControl(e.KeyChar) Then
                Select Case e.KeyChar
                    Case ChrW(Keys.Back)
                        ' BackSpaceキー押下
                        _OnCharBackSpace()

                End Select
                Return
            End If

            ' 何も指定なしはここまで
            If Me.InputFormat = InputFormatType.None Then
                Return
            End If

            ' カスタム指定のとき
            If IsCustom() Then
                If Not _exclusionCharCustom(e.KeyChar) Then
                    Return
                End If
            End If

            Select Case e.KeyChar
                Case "0"c To "9"c
                    ' 0 ～ 9 キー押下
                    e.Handled = _exclusionChar09(e.KeyChar)

                Case "A"c To "Z"c
                    ' 大文字の A ～ Z キー押下
                    e.Handled = _exclusionCharAZ(e.KeyChar)

                Case "a"c To "z"c
                    ' 小文字の a ～ z キー押下
                    e.Handled = _exclusionCharAZ2(e.KeyChar)
                    ' 大文字のみなら小文字は大文字コードへ変換！
                    If Me.IsUpper Then
                        e.KeyChar = Char.ToUpper(e.KeyChar)
                    End If

                Case " "c
                    ' Spaceキー押下
                    e.Handled = _exclusionCharSpace(e.KeyChar)

                Case "+"c, "-"c
                    e.Handled = _exclusionCharSign(e.KeyChar)

                Case "."c
                    ' .キー押下
                    e.Handled = _exclusionCharDot(e.KeyChar)

                Case "!"c To "/"c, ":"c To "@"c, "["c To "`"c, "~"c, "|"c, "{"c, "}"c
                    ' 記号
                    e.Handled = _exclusionCharSymbol(e.KeyChar)

                Case ChrW(Keys.Back)
                    ' BackSpaceキー押下

                Case Else
                    ' 上記以外は無効(処理済とする)
                    e.Handled = True
            End Select
        End Sub

        ''' <summary>
        ''' BackSpaceキー押下
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub _OnCharBackSpace()
            If Not Me.IsDigit Then
                Exit Sub
            End If

            ' 数値のとき

            If Not Me.IsSignWithColor Then
                Exit Sub
            End If

            ' 符号色付きのとき

            If Not Me.Text.StartsWith("-") OrElse Me.SelectionStart > 1 Then
                Return
            End If

            ' 先頭が「-」でカーソルが先頭のときは色をデフォルトへ戻す
            MyBase.ForeColor = _orgForeColor
        End Sub

        ''' <summary>
        ''' カスタム指定の時のチェック
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        Private Function _exclusionCharCustom(ByVal KeyChar As Char) As Boolean
            If CustomChars.Contains(KeyChar) Then
                Return False
            End If
            Return True
        End Function

        ''' <summary>
        ''' 0 ～ 9 キー押下
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionChar09(ByVal KeyChar As Char) As Boolean
            ' 数値、数字以外はスルー
            If Not Me.IsDigit AndAlso Not Me.IsNumber Then
                Return True
            End If

            If Me.IsSign Or Me.IsSignWithColor Then
                ' サイン付きのとき
                If Me.SelectionStart = 0 Then
                    ' カーソルが先頭のとき
                    If Me.SelectionLength > 0 Then
                        If Me.SelectionLength = 1 And Me.Text.StartsWith("-") And Me.Text.Length >= Me.MaxLength Then
                            Return True
                        End If
                        ' 選択状態はOKでスルー
                        Return False
                    End If
                    If Me.Text.StartsWith("-") Then
                        ' マイナス記号付きはNGでスルー
                        Return True
                    End If
                End If
            End If

            Dim pos As Integer
            pos = Me.Text.IndexOf("."c)
            Select Case pos

                Case Is > 0
                    ' 小数点あり
                    If Me.SelectionStart <= pos Then
                        ' 小数点より手前
                        If _scale = (pos - 1) Then
                            If Me.SelectionLength = 0 Then
                                Return True
                            End If
                        End If
                    Else
                        ' 小数点より奥
                        If (Me.Text.Length - pos) > Percision Then
                            If Me.SelectionLength = 0 Then
                                Return True
                            End If
                        End If
                    End If

                Case Else
                    ' 小数点なし
                    If _scale <= 0 Then
                        Return False
                    End If
                    ' サイン付きのときはサインを外した桁数を算出
                    Dim len As Integer = Me.Text.Length
                    If Me.IsSign Or Me.IsSignWithColor Then
                        len = Me.Text.Replace("-", String.Empty).Length
                    End If
                    If len >= _scale Then
                        ' 入力最大桁数に達している
                        If Me.SelectionLength = 0 Then
                            ' 文字が未選択時は無効
                            Return True
                        End If
                    End If

                    ' 入力最大桁数に達していない
            End Select

            If Me.IsDigit Then
                ' 数値のとき
                If Me.SelectionStart = 0 AndAlso KeyChar = "0"c Then
                    ' カーソルが先頭で「０」を入力時は無効
                    Return True
                End If
                If Me.SelectionStart = 1 AndAlso KeyChar = "0"c AndAlso Me.Text.StartsWith("-") Then
                    ' カーソルが２桁目で「０」を入力で「-」始まり時は無効
                    Return True
                End If
            End If

            Return False
        End Function

        ''' <summary>
        ''' 大文字の A ～ Z キー押下
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionCharAZ(ByVal KeyChar As Char) As Boolean
            If Not Me.IsAlpha Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' 小文字の a ～ z キー押下
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionCharAZ2(ByVal KeyChar As Char) As Boolean
            If Not Me.IsAlpha Then
                Return True
            End If
            Return False
        End Function

        ''' <summary>
        ''' スペース入力の無効判定
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionCharSpace(ByVal KeyChar As Char) As Boolean
            ' スペース有り指定以外は無効
            Return Not Me.IsWithSpace
        End Function

        ''' <summary>
        ''' 数値サイン付判定
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 入力形式として<see cref="InputFormatType.Digit"/>と<see cref="InputFormatType.Sign"/>が指定されたとき、
        ''' 文字列の先頭へ符号をつけます。<br/>
        ''' 「ー」入力のときはドグル入力で＋－が入れ替わります。<br/>
        ''' 「＋」入力のときはマイナス時は符号を消してプラス時は何もしません。<br/>
        ''' </remarks>
        Private Function _exclusionCharSign(ByVal KeyChar As Char) As Boolean
            If Not Me.IsDigit Then
                If Not Me.IsSymbol Then
                    Return True
                End If
                Return False
            End If
            If Not Me.IsSign AndAlso Not Me.IsSignWithColor Then
                Return True
            End If

            ' 数値または数字で無郷付きのとき

            If Me.SelectionStart > 0 Then
                ' カーソルが先頭でない
                If Me.Text.StartsWith("-") Then
                    ' 「-」記号あり
                    Dim buf As Integer = Me.SelectionStart
                    _changeText(Me.Text.Replace("-", String.Empty))
                    Me.SelectionStart = buf - 1
                Else
                    ' 「-」記号なし
                    If KeyChar = "-"c Then
                        Dim buf As Integer = Me.SelectionStart
                        _changeText("-" & Me.Text)
                        Me.SelectionStart = buf + 1
                    End If
                End If
            Else
                ' カーソルが先頭
                If Me.Text.StartsWith("-") Then
                    ' 「-」記号あり
                    _changeText(Me.Text.Replace("-", String.Empty))
                Else
                    ' 「-」記号なし
                    If KeyChar = "-"c Then
                        Dim buf As Integer = Me.SelectionStart
                        _changeText("-" & Me.Text)
                        Me.SelectionStart = buf + 1
                    End If
                End If
            End If

            _setSignInput(Me.Text)

            Return True
        End Function

        ''' <summary>
        ''' ドット入力の無効判定
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionCharDot(ByVal KeyChar As Char) As Boolean
            ' 数値のとき以外は有効
            If Not Me.IsDigit And Not Me.IsSymbol Then
                Return True
            End If
            ' 小数点無しのときは無効
            If Me.Percision = 0 Then
                Return True
            End If
            ' ドットが既に存在するときは無効
            If Me.Text.Split("."c).Length > 1 Then
                Return True
            End If
            ' 最初の文字としては無効
            If Me.Text.Length = 0 Then
                Return True
            End If
            ' 文字列の途中では無効
            If Me.Text.Length > Me.SelectionStart Then
                Return True
            End If

            Return False
        End Function

        ''' <summary>
        ''' 記号入力の無効判定
        ''' </summary>
        ''' <param name="KeyChar"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _exclusionCharSymbol(ByVal KeyChar As Char) As Boolean
            ' 記号指定されていない時は無効
            Return Not Me.IsSymbol
        End Function

        ''' <summary>
        ''' [貼り付け] した時に呼び出されるメソッド
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Protected Overridable Sub OnPaste(ByVal e As System.EventArgs)
            If Me.InputFormat = InputFormatType.None Then
                Exit Sub
            End If

            If _nowChange Then
                Exit Sub
            End If

            ' フォルダのコピーはNothingになるらしい
            If Not Clipboard.GetDataObject().GetDataPresent(System.Windows.Forms.DataFormats.Text) Then
                Return
            End If

            Dim stString As String = Clipboard.GetDataObject().GetData(System.Windows.Forms.DataFormats.Text).ToString()

            Dim newClip As String

            ' コピペとかのためにここでテキストの内容をチェック！
            If Me.IsDigit Then
                MyBase.MaxLength = _orgMaxLength
                newClip = _cnvDigit(stString)
            Else
                Dim cnv As String = String.Empty

                If Me.IsNumber Then
                    cnv = "0-9"
                End If
                If Me.IsAlpha Then
                    If Me.IsUpper Then
                        cnv = cnv & "A-Za-z"
                    Else
                        cnv = cnv & "a-z"
                    End If
                End If
                If Me.IsSymbol Then
                    cnv = cnv & "!-/:-@\[-`~|{}"
                End If
                If Me.IsWithSpace Then
                    cnv = cnv & " "
                End If

                newClip = _cnvNoDigit(stString, cnv)
            End If
            If newClip.Length = 0 Then
                Exit Sub
            End If

            Clipboard.SetText(newClip)
            _nowPaste = True
            Call SendMessage(Me.Handle, WM_PASTE, 0, String.Empty)
            _nowPaste = False
            Clipboard.SetText(stString)
        End Sub

        Protected Overrides Sub OnBorderStyleChanged(e As EventArgs)
            MyBase.OnBorderStyleChanged(e)

            _changeStyle()
            Invalidate()
        End Sub

        Protected Overrides Sub OnReadOnlyChanged(e As EventArgs)
            MyBase.OnReadOnlyChanged(e)

            _changeStyle()
            Invalidate()
        End Sub

        Protected Overrides Sub OnParentBackColorChanged(e As EventArgs)
            MyBase.OnParentBackColorChanged(e)

            _changeBackColor()
        End Sub

        Protected Overrides Sub OnParentChanged(e As EventArgs)
            MyBase.OnParentChanged(e)

            _changeBackColor()
        End Sub

        Private Sub _changeStyle()
            If BorderStyle <> System.Windows.Forms.BorderStyle.None OrElse Not [ReadOnly] OrElse _bottomBorder.Visible Then
                BackColor = Color.White
                _bottomBorder.Visible = False
                TabStop = True
                Return
            End If

            _changeBackColor()
            Margin = DefaultMargin
            _bottomBorder.Visible = True
            TabStop = False
        End Sub

        Private Sub _changeBackColor()
            If Parent Is Nothing Then
                Return
            End If

            If BorderStyle <> System.Windows.Forms.BorderStyle.None Then
                Return
            End If
            If Not [ReadOnly] Then
                Return
            End If

            BackColor = Parent.BackColor
        End Sub

#End Region
#Region " Handles "

        ''' <summary>
        ''' テキストボックスへフォーカス移動イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TextBoxEx_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
            If Len(Me.Separator) > 0 Then
                Me.MaxLength = _orgMaxLength
                _changeText(Replace(Me.Text, Me.Separator, String.Empty))
            End If
            Me.SelectAll()
        End Sub

        ''' <summary>
        ''' テキストボックスからフォーカス移動イベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TextBoxEx_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LostFocus
            If Me.Text.Length = 0 Then
                Exit Sub
            End If

            If Me.Separator.Length = 0 Then
                Exit Sub
            End If

            Dim idx As Integer

            idx = Me.Text.IndexOf(".")
            If idx = 0 Then
                idx = Me.Text.Length + 1
            End If

            Dim cnv As String

            cnv = Me.Text.Substring(0, idx - 1)
            cnv = String.Format("{0,F" & Me.Percision.ToString & "}", cnv)
            cnv = cnv & Me.Text.Substring(Me.Text.Length - idx + 1)

            Me.MaxLength = cnv.Length
            _changeText(cnv)
        End Sub

        ''' <summary>
        ''' キー押下
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TextBoxEx_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
            _keyPressUpper(e)
        End Sub

        ''' <summary>
        ''' キーアップ時
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub TextBoxEx_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
            _keyUpDigit(e)
        End Sub

#End Region

        Private Sub _sizeChanged(sender As Object, e As EventArgs)
            _bottomBorder.Height = 1
        End Sub

        ''' <summary>
        ''' キー押下時（Upper指定時）
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub _keyPressUpper(ByVal e As KeyPressEventArgs)
            If Not Me.IsUpper Then
                Exit Sub
            End If
            e.KeyChar = Char.ToUpper(e.KeyChar)
        End Sub

        ''' <summary>
        ''' キーアップ時の数値制御
        ''' </summary>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub _keyUpDigit(ByVal e As KeyEventArgs)
            If Not Me.IsDigit Then
                Return
            End If
            Select Case e.KeyCode
                Case Keys.Back, Keys.Delete
                Case Keys.D0 To Keys.D9, Keys.NumPad0 To Keys.NumPad9
                Case Keys.Z, Keys.Y
                    If Not e.Control Then
                        Return
                    End If
                Case Else
                    Return
            End Select
            If Me.Text.Length = 0 Then
                Exit Sub
            End If

            Dim sign As Boolean
            Dim ary() As String

            ary = Me.Text.Split("."c)
            sign = ary(0).StartsWith("-")
            ary(0) = ary(0).Replace("-", String.Empty)

            If ary(0).Length = 0 Then
                ary(0) = "0"
            End If

            Dim val1 As Double = CDbl(ary(0))
            ary(0) = val1.ToString

            Dim buf As Integer = Me.SelectionStart
            _changeText(IIf(sign, "-", String.Empty).ToString & String.Join("."c, ary))
            _setSignInput(Me.Text)
            Me.SelectionStart = buf
        End Sub

        ''' <summary>
        ''' フォーマット種別の判別
        ''' </summary>
        ''' <param name="targetType"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _isFormat(ByVal targetType As InputFormatType) As Boolean
            Return ((_inputFormat And targetType) = targetType)
        End Function

        ''' <summary>
        ''' 数値変換
        ''' </summary>
        ''' <param name="val"></param>
        ''' <returns></returns>
        ''' <remarks>文字列から数値のみを抽出して返す</remarks>
        Private Function _cnvDigit(ByVal val As String) As String
            Dim pattern As String = "\+|\-|([0-9])+|\."

            Dim idx As Integer
            Dim rc As String

            rc = String.Empty
            _percisionSign = False

            ' 文字列の選択範囲を考慮するため選択位置へコピー内容を挿入
            val = Me.Text.Substring(0, Me.SelectionStart) & val & Me.Text.Substring(Me.SelectionStart, Me.SelectionLength) & Me.Text.Substring(Me.SelectionStart + Me.SelectionLength)

            ' 許容する文字のみ抜き出し
            For Each matchVal As Match In Regex.Matches(val, pattern)
                rc = rc & _cnvDigitMatch(idx, matchVal.Value)
                idx = idx + 1
            Next

            ' 小数点を考慮する

            Dim sign As Boolean
            Dim ary() As String

            ' 小数点で分ける
            ary = rc.Split("."c)
            ' 小数点より前の文字列がマイナスか判定し、一旦符号を削除
            sign = ary(0).StartsWith("-")
            ary(0) = ary(0).Replace("-", String.Empty)

            ' もし小数点より前が０ケタなら「０」とする
            If ary(0).Length = 0 Then
                ary(0) = "0"
            End If

            ' 小数点より前を許容桁数でちょん切る
            Dim val1 As Long = CLng(ary(0))
            ary(0) = val1.ToString.Substring(0, CInt(IIf(_scale < val1.ToString.Length, _scale, val1.ToString.Length)))

            ' 小数点で分けたのを元に戻す
            Dim buf As String
            buf = IIf(sign, "-", String.Empty).ToString & String.Join("."c, ary)

            _setSignInput(rc)

            If Me.SelectionLength > 0 Then
                ' 画面上選択状態なら選択部分に当てはまる文字を抽出
                rc = buf.Substring(Me.SelectionStart, Me.SelectionLength)
            Else
                ' 画面上未選択状態なら桁数分そのまま抽出
                rc = buf.Substring(Me.SelectionStart, Me.MaxLength - Me.Text.Length)
            End If

            Return rc
        End Function

        ''' <summary>
        ''' 負号入力モード時の設定
        ''' </summary>
        ''' <param name="val"></param>
        ''' <remarks></remarks>
        Private Sub _setSignInput(ByVal val As String)
            If val.StartsWith("-") Then
                ' マイナス始まりなら最大桁数を１upして文字色変えるなら変える
                MyBase.MaxLength = _orgMaxLength + 1
                If Me.IsSignWithColor Then
                    MyBase.ForeColor = Me.NegativeColor
                End If
            Else
                ' 上以外なら最大桁数と文字色を元に戻す
                MyBase.MaxLength = _orgMaxLength
                If Me.IsSignWithColor Then
                    MyBase.ForeColor = _orgForeColor
                End If
            End If
        End Sub

        ''' <summary>
        ''' 数値指定時のコピー文字列抽出判定
        ''' </summary>
        ''' <param name="idx"></param>
        ''' <param name="Value"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _cnvDigitMatch(ByVal idx As Integer, ByVal Value As String) As String
            Select Case Value
                Case "-"
                    If Me.IsSign Or Me.IsSignWithColor Then
                        If idx <> 0 Then
                            Return String.Empty
                        End If
                    End If
                Case Else
            End Select
            If Me.Percision > 0 Then
                ' 小数点あり
                Select Case Value
                    Case "."
                        If _percisionSign Then
                            Return String.Empty
                        End If
                        _percisionSign = True
                    Case Else
                End Select
            Else
                ' 小数点なし
                Select Case Value
                    Case "."
                        ' ドットは無効
                        Return String.Empty
                    Case Else
                End Select
            End If

            Return Value
        End Function

        ''' <summary>
        ''' 数値以外
        ''' </summary>
        ''' <param name="val"></param>
        ''' <param name="cnv"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function _cnvNoDigit(ByVal val As String, ByVal cnv As String) As String
            Const C_STR As String = "[{0}]"
            Dim ii As Integer
            Dim rc As String
            Dim pattern As String

            pattern = Replace(C_STR, "{0}", cnv)
            rc = String.Empty

            ' 有効な文字のみにする
            For ii = 0 To val.Length - 1
                Dim cc As String
                cc = val.Substring(ii, 1)
                If Regex.IsMatch(cc, pattern) Then
                    rc = rc & cc
                End If
            Next ii

            ' 大文字へ
            If Me.IsUpper Then
                rc = rc.ToUpper
            End If

            Return rc
        End Function

        ''' <summary>
        ''' テキストプロパティ編集
        ''' </summary>
        ''' <param name="val"></param>
        ''' <remarks></remarks>
        Private Sub _changeText(ByVal val As String)
            _nowChange = True
            Me.Text = val
            _nowChange = False
        End Sub

    End Class

End Namespace
