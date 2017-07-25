Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' 複数のラジオボタンをグループとして扱うコンポーネント
    ''' </summary>
    ''' <remarks>
    ''' 複数のラジオボタンを一つのグループとして扱えます。<br/>
    ''' パネルコントロールなどでグループ化するには複雑過ぎる画面などでの使用を想定しています。<br/>
    ''' データバインディングは SelectedValue が対応しています。<br/>
    ''' </remarks>
    <System.ComponentModel.Description("複数のラジオボタンをグループとして扱う"),
    ToolboxItem(True)>
    Public Class RadioButtonGroup
        Implements IBindableComponent, INotifyPropertyChanged

        ''' <summary>
        ''' 矢印ボタン押下時のボタン移動方法
        ''' </summary>
        ''' <remarks></remarks>
        Private Enum MoveType
            forward = 0
            [Next]
        End Enum

        ''' <summary>ラジオボタン化するItemを格納する</summary>
        Private _dicButton As IDictionary(Of RadioButton, Object)

#Region " Implements "

#Region " IBindableComponent "

        Private _bindingContext As BindingContext

        Public Property BindingContext() As System.Windows.Forms.BindingContext Implements System.Windows.Forms.IBindableComponent.BindingContext
            Get
                If _bindingContext Is Nothing Then
                    _bindingContext = New BindingContext
                End If
                Return _bindingContext
            End Get
            Set(ByVal value As System.Windows.Forms.BindingContext)
                _bindingContext = value
            End Set
        End Property

        Private _dataBindings As ControlBindingsCollection

        Public ReadOnly Property DataBindings() As System.Windows.Forms.ControlBindingsCollection Implements System.Windows.Forms.IBindableComponent.DataBindings
            Get
                If _dataBindings Is Nothing Then
                    _dataBindings = New ControlBindingsCollection(Me)
                End If
                Return _dataBindings
            End Get
        End Property

#End Region
#Region " INotifyPropertyChanged "

        Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged

        Private Sub _firePropertyChanged(ByVal value As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(value))
        End Sub

#End Region

#End Region
#Region " Property "

        ''' <summary>
        ''' グループ内のラジオボタンを値で選択又は選択されているラジオボタンの値を取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
        ''' </remarks>
        <Browsable(False)>
        Public Property SelectedValue() As Object
            Get
                Return _getSelectedValue()
            End Get
            Set(ByVal value As Object)
                _setSelected(value)
            End Set
        End Property

        ''' <summary>
        ''' グループ内のラジオボタンをラジオボタンオブジェクトで選択又は選択されているラジオボタンのラジオボタンオブジェクトを取得
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks>
        ''' 未選択時は Nothing を返します。
        ''' </remarks>
        <Browsable(False)>
        Public Property SelectedButton() As RadioButton
            Get
                Return _getSelected()
            End Get
            Set(ByVal value As RadioButton)
                _setSelected(value)
            End Set
        End Property

        ''' <summary>
        ''' コントロールがユーザーとの対話に応答できるかどうかを示す値を設定します。
        ''' </summary>
        ''' <value></value>
        ''' <remarks></remarks>
        Public WriteOnly Property Enabled() As Boolean
            Set(ByVal value As Boolean)
                For Each btn As RadioButton In _dicButton.Keys
                    btn.Enabled = value
                Next
            End Set
        End Property

        ''' <summary>
        ''' ラジオボタンたち
        ''' </summary>
        ''' <returns></returns>
        <Browsable(False)>
        Public ReadOnly Property Buttons As ICollection(Of RadioButton)
            Get
                Return _dicButton.Keys
            End Get
        End Property

#End Region
#Region " イベント "

        '''' <summary>
        '''' ラジオボタンクリックイベント
        '''' </summary>
        '''' <param name="sender"></param>
        '''' <param name="e"></param>
        '''' <remarks></remarks>
        'Private Sub _radioButton_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        '    Me.SelectedButton = DirectCast(sender, RadioButton)
        'End Sub

        ''' <summary>
        ''' ラジオボタンの Checked プロパティの値が変更されたときイベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub _radioButton_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim btn As RadioButton

            btn = DirectCast(sender, RadioButton)

            If Not btn.Checked Then
                Exit Sub
            End If

            For Each btn2 As RadioButton In _dicButton.Keys
                If btn.Equals(btn2) Then
                    Continue For
                End If
                If btn2.Checked Then
                    btn2.Checked = False
                End If
            Next

            _firePropertyChanged("SelectedValue")
        End Sub

        ''' <summary>
        ''' ラジオボタンキーダウンイベント
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="e"></param>
        ''' <remarks></remarks>
        Private Sub _radioButton_PreviewKeyDown(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs)
            Select Case e.KeyData
                Case Keys.Left, Keys.Up
                    _setForcus(sender, MoveType.forward)
                Case Keys.Right, Keys.Down
                    _setForcus(sender, MoveType.Next)
            End Select

            If Me.SelectedButton.Focused Then
                Return
            End If
            Me.SelectedButton.Focus()
            Me.SelectedButton.Checked = True
            e.IsInputKey = True
        End Sub

#End Region
#Region " Method "

        ''' <summary>
        ''' ラジオボタンを追加する
        ''' </summary>
        ''' <param name="ctrl">グループにしたいラジオボタン</param>
        ''' <remarks>
        ''' </remarks>
        Public Sub Add(ByRef ctrl As RadioButton)
            Dim lValue As Object = Nothing
            Add(ctrl, lValue)
        End Sub

        ''' <summary>
        ''' ラジオボタンを追加する
        ''' </summary>
        ''' <param name="ctrl">グループにしたいラジオボタン</param>
        ''' <param name="value">保持したい値</param>
        ''' <remarks>
        ''' value にて指定された値を Tag プロパティにて保持します。
        ''' </remarks>
        Public Sub Add(ByRef ctrl As RadioButton, ByVal value As Object)
            _dicButton.Add(ctrl, value)
            ctrl.Checked = False
            'AddHandler ctrl.Click, AddressOf _radioButton_Click
            AddHandler ctrl.CheckedChanged, AddressOf _radioButton_CheckedChanged
            AddHandler ctrl.PreviewKeyDown, AddressOf _radioButton_PreviewKeyDown
        End Sub

        ''' <summary>
        ''' 現在選択されているラジオボタンコントロールを返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 未選択時は Nothing を返します。
        ''' </remarks>
        <Obsolete("use RadioButtonGroup.SelectedButton Property")>
        Public Function GetSelected() As RadioButton
            Return _getSelected()
        End Function

        Private Function _getSelected() As RadioButton
            For Each btn As RadioButton In _dicButton.Keys
                If btn.Checked Then
                    Return btn
                End If
            Next

            Return Nothing
        End Function

        ''' <summary>
        ''' 現在選択されているラジオボタンコントロールの値を返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
        ''' </remarks>
        <Obsolete("use RadioButtonGroup.SelectedValue Property")>
        Public Function GetSelectedValue() As Object
            Return _getSelectedValue()
        End Function

        Private Function _getSelectedValue() As Object
            Dim sel As RadioButton

            sel = _getSelected()
            If sel Is Nothing Then
                Return Nothing
            End If

            Return _dicButton(sel)
        End Function

        ''' <summary>
        ''' 指定された値のラジオボタンを選択する
        ''' </summary>
        ''' <param name="btn">選択したいRadioButtonを設定</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        <Obsolete("use RadioButtonGroup.SelectedButton Property")>
        Public Function SetSelected(ByVal btn As RadioButton) As RadioButton
            Me.SelectedButton = btn
            Return Me.SelectedButton
        End Function

        Private Function _setSelected(ByVal btn As RadioButton) As RadioButton
            Dim radButton As RadioButton

            For Each radButton In _dicButton.Keys
                If Not radButton.Equals(btn) Then
                    radButton.Checked = False
                Else
                    radButton.Checked = True
                End If
            Next

            Return btn
        End Function

        ''' <summary>
        ''' 指定された値のラジオボタンを選択する
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
        ''' </remarks>
        <Obsolete("use RadioButtonGroup.SelectedValue Property")>
        Public Function SetSelected(ByVal value As Object) As RadioButton
            Me.SelectedValue = value
            Return Me.SelectedButton
        End Function

        Private Function _setSelected(ByVal value As Object) As RadioButton
            Dim btn As RadioButton

            Dim result As RadioButton = Nothing

            For Each btn In _dicButton.Keys
                If _dicButton(btn).Equals(value) Then
                    btn.Checked = True
                    result = btn
                Else
                    btn.Checked = False
                End If
            Next

            Return result
        End Function

#End Region
#Region " Private Method "

        ''' <summary>
        ''' 指定されたラジオボタンから指定された方向へカーソルを移動する。
        ''' </summary>
        ''' <param name="sender"></param>
        ''' <param name="type"></param>
        ''' <remarks></remarks>
        Private Sub _setForcus(ByVal sender As Object, ByVal type As MoveType)
            Dim btn As RadioButton
            Dim idx As Integer

            btn = DirectCast(sender, RadioButton)
            idx = 0

            Dim ary As New ArrayList(_dicButton.Keys)

            For ii As Integer = 0 To ary.Count - 1
                btn = ary(ii)
                If sender.Equals(btn) Then
                    If type = MoveType.forward Then
                        idx = ii - 1
                    Else
                        idx = ii + 1
                    End If
                    Exit For
                End If
            Next

            If idx < 0 Then
                idx = _dicButton.Keys.Count - 1
            End If
            If idx >= _dicButton.Keys.Count Then
                idx = 0
            End If

            Me.SelectedButton = ary(idx)
        End Sub

#End Region

    End Class

End Namespace
