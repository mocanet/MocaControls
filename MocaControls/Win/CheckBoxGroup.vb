
Imports System.Text
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' 複数のチェックボックスをグループとして扱うクラス
    ''' </summary>
    ''' <remarks>
    ''' 複数のチェックボックスを一つのグループとして扱えます。<br/>
    ''' パネルコントロールなどでグループ化するには複雑過ぎる画面などでの使用を想定しています。<br/>
    ''' </remarks>
	<Description("複数のチェックボックスをグループとして扱う"), _
	ToolboxItem(True)> _
	Public Class CheckBoxGroup
		Implements IBindableComponent, INotifyPropertyChanged

		''' <summary>チェックボックス化するItemを格納する</summary>
		Private _aryButton As IList(Of CheckBox)

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
		''' コントロールがユーザーとの対話に応答できるかどうかを示す値を設定します。
		''' </summary>
		''' <value></value>
		''' <remarks></remarks>
		Public WriteOnly Property Enabled() As Boolean
			Set(ByVal value As Boolean)
				For Each btn As CheckBox In _aryButton
					btn.Enabled = value
				Next
			End Set
		End Property

#End Region
#Region " Method "

		''' <summary>
		''' チェックボックスを追加する
		''' </summary>
		''' <param name="ctrl">グループにしたいチェックボックス</param>
		''' <remarks>
		''' </remarks>
		Public Sub Add(ByRef ctrl As CheckBox)
			Dim lValue As Object = Nothing
			Add(ctrl, lValue)
		End Sub

		''' <summary>
		''' チェックボックスを追加する
		''' </summary>
		''' <param name="ctrl">グループにしたいチェックボックス</param>
		''' <param name="value">保持したい値</param>
		''' <remarks>
		''' value にて指定された値を Tag プロパティにて保持します。
		''' </remarks>
		Public Sub Add(ByRef ctrl As CheckBox, ByVal value As Object)
			_aryButton.Add(ctrl)
			ctrl.Checked = False

			If value Is Nothing Then
				Exit Sub
			End If

			ctrl.Tag = value
		End Sub

		''' <summary>
		''' 現在選択されているチェックボックスコントロールを返す
		''' </summary>
		''' <returns></returns>
		''' <remarks>
		''' 未選択時は Length=0 を返します。
		''' </remarks>
		Public Function GetSelected() As CheckBox()
			Dim aryLst As ArrayList = New ArrayList

			For Each btn As CheckBox In _aryButton
				If btn.Checked Then
					aryLst.Add(btn)
				End If
			Next

			Return DirectCast(aryLst.ToArray(GetType(CheckBox)), CheckBox())
		End Function

		''' <summary>
		''' 現在選択されているチェックボックスコントロールの値を返す
		''' </summary>
		''' <returns></returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
		''' </remarks>
		Public Function GetSelectedValue() As Object()
			Dim sel() As CheckBox
			Dim aryLst As ArrayList = New ArrayList

			sel = GetSelected()
			For Each chk As CheckBox In sel
				aryLst.Add(chk.Tag)
			Next

			Return aryLst.ToArray()
		End Function

		''' <summary>
		''' 現在選択されているチェックボックスコントロールの文字列を返す
		''' </summary>
		''' <returns></returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
		''' </remarks>
		Public Function GetSelectedText() As String()
			Dim sel() As CheckBox
			Dim aryLst As ArrayList = New ArrayList

			sel = GetSelected()
			For Each chk As CheckBox In sel
				aryLst.Add(chk.Text)
			Next

			Return DirectCast(aryLst.ToArray(GetType(String)), String())
		End Function

		''' <summary>
		''' 指定された値のチェックボックスを選択する
		''' </summary>
		''' <param name="btn">選択したいCheckBoxを設定</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function SetSelected(ByVal btn As CheckBox) As CheckBox
			If Not btn.Checked Then
				btn.Checked = True
			End If

			Return btn
		End Function

		''' <summary>
		''' 指定された値のチェックボックスを選択する
		''' </summary>
		''' <param name="value"></param>
		''' <returns></returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
		''' </remarks>
		Public Function SetSelected(ByVal value As Object) As CheckBox
			Dim btn As CheckBox

			For Each btn In _aryButton
				If Not btn.Tag.Equals(value) Then
					Continue For
				End If

				btn.Checked = True
				Return btn
			Next

			Return Nothing
		End Function

		''' <summary>
		''' 指定された値のチェックボックスを選択する
		''' </summary>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
		''' </remarks>
		Public Sub SetSelected(ByVal values As ICollection)
			Dim btn As CheckBox

			For Each value As Object In values
				For Each btn In _aryButton
					If Not btn.Tag.Equals(value) Then
						Continue For
					End If
					btn.Checked = True
				Next
			Next
		End Sub

		''' <summary>
		''' 指定された値のチェックボックスが選択されているか返す
		''' </summary>
		''' <param name="value"></param>
		''' <returns></returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提ですので、<see cref="Add" /> メソッドは Value 指定してください。
		''' </remarks>
		Public Function IsSelected(ByVal value As Object) As Boolean
			Dim btn As CheckBox

			Dim result As CheckBox = Nothing

			For Each btn In _aryButton
				If btn.Tag.Equals(value) Then
					Return btn.Checked
				End If
			Next

			Return False
		End Function

		''' <summary>
		''' 選択されている項目のタイトルを指定された区切り文字で連結して返す
		''' </summary>
		''' <param name="delimiter"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function ToStringText(Optional ByVal delimiter As String = ", ") As String
			Dim chks() As CheckBox
			Dim sb As StringBuilder = New StringBuilder

			chks = GetSelected()
			For Each chk As CheckBox In chks
				sb.Append(IIf(sb.Length = 0, String.Empty, delimiter))
				sb.Append(chk.Text)
			Next

			Return sb.ToString
		End Function

#End Region

	End Class

End Namespace
