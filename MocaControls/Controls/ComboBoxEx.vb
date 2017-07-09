Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' ComboBox の拡張版
    ''' </summary>
    ''' <remarks>
    ''' ・フラットにしたときに枠線を引く<br/>
    ''' </remarks>
    <Description("標準の ComboBox を拡張したコントロール"),
     ToolboxItem(True),
     DesignTimeVisible(True)>
    Public Class ComboBoxEx

#Region " Declare "

        ''' <summary>境界線</summary>
        Private _borderColor As Color = Color.FromArgb(100, 100, 100)
        ''' <summary>境界線のスタイル</summary>
        Private _borderStyle As ButtonBorderStyle = ButtonBorderStyle.Solid

        Private Shared WM_PAINT As Integer = &HF

#End Region
#Region " コンストラクタ "

        Public Sub New()

            ' この呼び出しはデザイナーで必要です。
            InitializeComponent()

            ' InitializeComponent() 呼び出しの後で初期化を追加します。

            Me.SetStyle(ControlStyles.DoubleBuffer, True)
            Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        End Sub

#End Region
#Region " Property "

        ''' <summary>
        ''' 境界線
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("コントロールの境界線の色を指定します")>
        Public Property BorderColor() As Color
            Get
                Return _borderColor
            End Get
            Set
                _borderColor = Value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' 境界線のスタイル
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("コントロールに境界線を付けるかどうかを指定します")>
        Public Property BorderStyle() As ButtonBorderStyle
            Get
                Return _borderStyle
            End Get
            Set
                _borderStyle = Value
                Invalidate()
            End Set
        End Property

        ''' <summary>
        ''' 境界線
        ''' </summary>
        ''' <returns></returns>
        <Category("Appearance")>
        <Description("コントロールにフォーカスがないときの境界線の色を指定します")>
        Public Property UnfocusedBorderColor() As Color = SystemColors.ControlDark

#End Region

#Region " Overrides "

        Protected Overrides Sub WndProc(ByRef m As Message)
            MyBase.WndProc(m)

            If m.Msg = WM_PAINT Then
                If FlatStyle <> FlatStyle.Flat Then
                    Return
                End If
                Dim g As Graphics = Graphics.FromHwnd(Handle)
                Dim bounds As New Rectangle(0, 0, Width, Height)
                If Not Focused OrElse Not Enabled Then
                    ControlPaint.DrawBorder(g, bounds, UnfocusedBorderColor, _borderStyle)
                Else
                    ControlPaint.DrawBorder(g, bounds, _borderColor, _borderStyle)
                End If
            End If
        End Sub

#End Region
#Region " Handles "

        Private Sub ComboBoxEx_Resize(sender As Object, e As EventArgs) Handles Me.Resize
            Me.Refresh()
        End Sub

#End Region

    End Class

End Namespace
