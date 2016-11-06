
Imports System.ComponentModel


Namespace Win

    ''' <summary>
    ''' RichTextBox の拡張版
    ''' </summary>
    <Description("RichTextBox の拡張版")>
    <ToolboxItem(True)>
    <DesignTimeVisible(True)>
    Public Class RichTextBoxEx
        Inherits RichTextBox

#Region " Declare "

        Private _bottomBorderColor As Color
        Private _bottomBorder As Label

#End Region

#Region " コンポーネント デザイナーで必要です "

        'コンポーネント デザイナーで必要です。
        Private components As System.ComponentModel.IContainer

        <System.Diagnostics.DebuggerNonUserCode()>
        Public Sub New()
            MyBase.New()

            'この呼び出しは、コンポーネント デザイナーで必要です。
            InitializeComponent()

            _bottomBorder = New Label() With {
                .AutoSize = False,
                .Height = 1,
                .Dock = DockStyle.Bottom,
                .BackColor = BottomBorderColor,
                .FlatStyle = FlatStyle.Flat,
                .Visible = True
            }
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

        'メモ: 以下のプロシージャはコンポーネント デザイナーで必要です。
        'コンポーネント デザイナーを使って変更できます。
        'コード エディターを使って変更しないでください。
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            components = New System.ComponentModel.Container()
        End Sub

#End Region

#Region " Property "

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

#End Region

#Region " Overrides "

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

#End Region
#Region " Handles "

        Private Sub _sizeChanged(sender As Object, e As EventArgs)
            _bottomBorder.Height = 1
        End Sub

#End Region

#Region " Method "

        Private Sub _changeStyle()
            If BorderStyle <> BorderStyle.None OrElse Not [ReadOnly] OrElse _bottomBorder.Visible Then
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

            If BorderStyle <> BorderStyle.None Then
                Return
            End If

            If Not [ReadOnly] Then
                Return
            End If

            BackColor = Parent.BackColor
        End Sub

#End Region

    End Class

End Namespace
