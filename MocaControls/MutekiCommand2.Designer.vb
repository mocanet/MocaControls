
Imports System.ComponentModel.Design

Namespace Win

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class MutekiCommand2
        Inherits System.ComponentModel.Component

        '<System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New(ByVal container As System.ComponentModel.IContainer)
            MyClass.New()

            'Windows.Forms クラス作成デザイナーのサポートに必要です
            If (container IsNot Nothing) Then
                container.Add(Me)

                If DesignMode Then
                    Return
                End If

                _getFormInstance()
            End If

        End Sub

        '<System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New()
            MyBase.New()

            'この呼び出しは、コンポーネント デザイナーで必要です。
            InitializeComponent()

            _keys = New Keys() {Keys.Up, Keys.Up, Keys.Down, Keys.Down, Keys.Left, Keys.Right, Keys.Left, Keys.Right, Keys.B, Keys.A}
            _isRun = False
        End Sub

        Private Sub _getFormInstance() ' called from constructor 
            Dim _host As IDesignerHost = Nothing
            If MyBase.Site IsNot Nothing Then
                _host = CType(MyBase.Site.GetService(GetType(IDesignerHost)), IDesignerHost)
            End If
            If _host IsNot Nothing Then
                _frm = CType(_host.RootComponent, Form)
                _frm.KeyPreview = True
                AddHandler _frm.KeyDown, AddressOf _onKeyDown
            End If
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

        'コンポーネント デザイナーで必要です。
        Private components As System.ComponentModel.IContainer

        'メモ: 以下のプロシージャはコンポーネント デザイナーで必要です。
        'コンポーネント デザイナーを使って変更できます。
        'コード エディターを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

    End Class

End Namespace
