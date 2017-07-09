
Namespace Win

    ''' <summary>
    ''' 継承したコントロールでFlowLayoutPanelのプロパティを変更できるようにする
    ''' </summary>
    <System.ComponentModel.Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design")>
    Public Class FlowLayoutPanelEx
        Inherits FlowLayoutPanel

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New()
            InitializeComponent()
        End Sub

#End Region

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
