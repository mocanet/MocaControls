
Namespace Win

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class TextBoxEx
        Inherits TextBox

        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()

            'Windows.Forms クラス作成デザイナのサポートに必要です。
            If (container IsNot Nothing) Then
                container.Add(Me)
            End If

        End Sub

        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New()
            MyBase.New()

            'この呼び出しは、コンポーネント デザイナで必要です。
            InitializeComponent()

            ' IME無効
            Me.ImeMode = System.Windows.Forms.ImeMode.Disable
            Me.NegativeColor = Color.Red
            Me._nowPaste = False
            AutoSize = False
            _bottomBorder = New Label() With {
                    .AutoSize = False,
                    .Height = 1,
                    .Dock = DockStyle.Bottom,
                    .BackColor = BottomBorderColor,
                    .FlatStyle = FlatStyle.Flat,
                    .Visible = True}
            Controls.Add(_bottomBorder)
            AddHandler _bottomBorder.SizeChanged, AddressOf _sizeChanged
        End Sub

        'Component は、コンポーネント一覧に後処理を実行するために dispose をオーバーライドします。
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'コンポーネント デザイナで必要です。
        Private components As System.ComponentModel.IContainer

        'メモ: 以下のプロシージャはコンポーネント デザイナで必要です。
        'コンポーネント デザイナを使って変更できます。
        'コード エディタを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

    End Class

End Namespace
