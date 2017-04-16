

Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' 無敵コマンド
    ''' </summary>
    <Description("無敵コマンドのコンポーネント")>
    <ToolboxItem(True)>
    Public Class MutekiCommand2

#Region " Declare "

        Private _frm As Form

        Private _keys() As Keys

        Private _comanndIndex As Integer

        Private _isRun As Boolean

        Public Event Muteki As EventHandler

#End Region

#Region " Property "

        ''' <summary>
        ''' 無敵コマンドに入る為のキー配列
        ''' </summary>
        ''' <returns></returns>
        Public Property MutekiCommandKeys As Keys()
            Get
                Return _keys
            End Get
            Set(value As Keys())
                _keys = value
            End Set
        End Property

        ''' <summary>
        ''' 無敵中かどうか
        ''' </summary>
        ''' <returns></returns>
        Public Property IsRun As Boolean
            Get
                Return _isRun
            End Get
            Set(value As Boolean)
                _isRun = value
            End Set
        End Property

#End Region

#Region " Method "

        ''' <summary>
        ''' 無敵コマンド入力判定
        ''' </summary>
        ''' <param name="keyCode"></param>
        ''' <returns></returns>
        Public Function IsCommand(ByVal keyCode As Keys) As Boolean
            If _comanndIndex > _keys.Length Then
                _comanndIndex = 0
                Return False
            End If
            If Not _keys(_comanndIndex).Equals(keyCode) Then
                _comanndIndex = 0
                Return False
            End If

            If Not _keys.GetUpperBound(0).Equals(_comanndIndex) Then
                _comanndIndex += 1
                Return False
            End If

            Return True
        End Function

        Private Sub _onKeyDown(sender As Object, e As KeyEventArgs)
            If Not IsCommand(e.KeyCode) Then
                Return
            End If

            _isRun = Not _isRun

            RaiseEvent Muteki(Me, New EventArgs())
        End Sub

#End Region


    End Class

End Namespace
