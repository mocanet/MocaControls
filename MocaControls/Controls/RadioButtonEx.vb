Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' ラジオボタンの拡張版
    ''' </summary>
    <Description("ラジオボタンの拡張版"),
     ToolboxItem(True)>
    Public Class RadioButtonEx
        Inherits RadioButton

#Region " Declare "

        Const _C_BORDER_HEIGHT As Integer = 3
        Private _bottomBorderColor As Color
        Private _activeBottomBorderColor As Color
        Private _bottomBorder As Label

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()

            _bottomBorder = New Label() With {
                        .AutoSize = False,
                        .Height = _C_BORDER_HEIGHT,
                        .Dock = DockStyle.Bottom,
                        .BackColor = BottomBorderColor,
                        .FlatStyle = FlatStyle.Flat,
                        .Visible = True}
            Controls.Add(_bottomBorder)

            If DesignMode Then
                Return
            End If

            AddHandler _bottomBorder.SizeChanged, AddressOf _sizeChanged
        End Sub

#End Region

#Region " Overrides  "

        Protected Overrides Sub OnCheckedChanged(e As EventArgs)
            If Checked Then
                _bottomBorder.BackColor = _activeBottomBorderColor
            Else
                _bottomBorder.BackColor = _bottomBorderColor
            End If
            MyBase.OnCheckedChanged(e)
        End Sub

        'Protected Overrides Sub OnMouseLeave(eventargs As EventArgs)
        '    If Checked Then
        '        Return
        '    End If
        '    _bottomBorder.BackColor = _bottomBorderColor

        '    MyBase.OnMouseLeave(eventargs)
        'End Sub

        'Protected Overrides Sub OnMouseHover(e As EventArgs)
        '    _bottomBorder.BackColor = _activeBottomBorderColor

        '    MyBase.OnMouseHover(e)
        'End Sub

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

        <Category("Appearance")>
        <Description("下線の色")>
        Public Property ActiveBottomBorderColor As Color
            Get
                Return _activeBottomBorderColor
            End Get
            Set(value As Color)
                _activeBottomBorderColor = value
                'If _bottomBorder IsNot Nothing Then
                '    _bottomBorder.BackColor = _activeBottomBorderColor
                'End If
            End Set
        End Property

        <Category("Appearance")>
        <Description("下線の高さ")>
        Public Property BottomBorderHeight As Integer
            Get
                Return _bottomBorder.Height
            End Get
            Set(value As Integer)
                _bottomBorder.Height = value
            End Set
        End Property

        <Category("Appearance")>
        <Description("外観")>
        Public Shadows Property Appearance As Appearance
            Get
                Return MyBase.Appearance
            End Get
            Set(value As Appearance)
                MyBase.Appearance = value
                _styleChanged()
            End Set
        End Property

        <Category("Appearance")>
        <Description("フラットスタイルの外観")>
        Public Shadows Property FlatStyle As FlatStyle
            Get
                Return MyBase.FlatStyle
            End Get
            Set(value As FlatStyle)
                MyBase.FlatStyle = value
                _styleChanged()
            End Set
        End Property

#End Region

#Region " Method "

        Private Sub _sizeChanged(sender As Object, e As EventArgs)
            '_bottomBorder.Height = _C_BORDER_HEIGHT
        End Sub

        Private Sub _styleChanged()
            If Appearance <> Appearance.Button Then
                _bottomBorder.Visible = False
                Return
            End If
            If FlatStyle <> FlatStyle.Flat Then
                _bottomBorder.Visible = False
                FlatAppearance.BorderSize = 1
                Return
            End If

            _bottomBorder.Visible = True
            If _bottomBorder.Visible Then
                FlatAppearance.BorderSize = 0
            End If
        End Sub

#End Region

    End Class

End Namespace
