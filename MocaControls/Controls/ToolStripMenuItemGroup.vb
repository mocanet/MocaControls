
Imports System.ComponentModel

Namespace Win

	''' <summary>
	''' 複数のメニューアイテムをグループとして扱うクラス
	''' </summary>
	''' <remarks>
	''' 複数のメニューアイテムを一つのグループとして扱えます。<br/>
	''' </remarks>
	<Description("複数のメニューアイテムをグループとして扱う"),
	ToolboxItem(True),
	ToolboxBitmap(GetType(resourceDummy), "ToolStripMenuItemGroup.bmp")>
	Public Class ToolStripMenuItemGroup

		''' <summary>メニューアイテム化するItemを格納する</summary>
		Private _aryRadButton As IList(Of ToolStripMenuItem)

		''' <summary>親のDropDownButton</summary>
		Protected WithEvents parentDropDownButton As ToolStripDropDownButton

		'#Region " コンストラクタ "

		'		''' <summary>
		'		''' コンストラクタ
		'		''' </summary>
		'		''' <param name="parent">親のDropDownButton</param>
		'		''' <remarks>親のDropDownButtonを使用しているとき用</remarks>
		'		Public Sub New(ByVal parent As ToolStripItem)
		'			parentDropDownButton = DirectCast(parent, ToolStripDropDownButton)
		'			_aryRadButton = New ArrayList
		'		End Sub

		'#End Region

#Region " プロパティ "

		''' <summary>
		''' 親のDropDownButton
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property ParentItem() As ToolStripItem
			Get
				Return parentDropDownButton
			End Get
			Set(ByVal value As ToolStripItem)
				parentDropDownButton = DirectCast(value, ToolStripDropDownButton)
			End Set
		End Property

#End Region

#Region " イベント "

		''' <summary>
		''' 親のDropDownButtonが指定されているときは、DropDownItemClickedイベントをハンドルする
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub parentDropDownButton_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles parentDropDownButton.DropDownItemClicked
			SetSelected(DirectCast(e.ClickedItem, ToolStripMenuItem))
		End Sub

#End Region

		''' <summary>
		''' メニューアイテムを追加する
		''' </summary>
		''' <param name="ctrl">メニューアイテム</param>
		''' <remarks>
		''' </remarks>
		Public Sub Add(ByRef ctrl As ToolStripMenuItem)
			Dim lValue As Object = Nothing
			Add(ctrl, lValue)
		End Sub

		''' <summary>
		''' メニューアイテムを追加する
		''' </summary>
		''' <param name="ctrl">メニューアイテム</param>
		''' <param name="value">値</param>
		''' <remarks>
		''' タグプロパティへ指定された値が保存されます。<br/>
		''' </remarks>
		Public Sub Add(ByRef ctrl As ToolStripMenuItem, ByVal value As Object)
			_aryRadButton.Add(ctrl)
			ctrl.CheckOnClick = False

			If value Is Nothing Then
				Exit Sub
			End If

			ctrl.Tag = value
		End Sub

		''' <summary>
		''' 現在選択されているメニューアイテムコントロールを返す
		''' </summary>
		''' <returns>現在選択されているメニューアイテムコントロール。未選択時は Nothing を返す。</returns>
		''' <remarks>
		''' </remarks>
		Public Function GetSelected() As ToolStripMenuItem
			For Each radButton As ToolStripMenuItem In _aryRadButton
				If radButton.Checked Then
					Return radButton
				End If
			Next

			Return Nothing
		End Function

		''' <summary>
		''' 現在選択されているメニューアイテムコントロールの値を返す
		''' </summary>
		''' <returns>現在選択されているメニューアイテムコントロール。未選択時は Nothing を返す。</returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提です。<br/>
		''' 当メソッドを使用する場合は、<see cref="Add"/> に <c>value</c> を指定してください。
		''' </remarks>
		Public Function GetSelectedValue() As Object
			Dim sel As ToolStripMenuItem

			sel = GetSelected()
			If sel Is Nothing Then
				Return Nothing
			End If

			Return sel.Tag
		End Function

		''' <summary>
		''' 指定された値のメニューアイテムを選択する
		''' </summary>
		''' <param name="mnu">選択したいToolStripMenuItem</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function SetSelected(ByVal mnu As ToolStripMenuItem) As ToolStripMenuItem
			Dim radButton As ToolStripMenuItem

			If Not mnu.Checked Then
				mnu.Checked = True
			End If

			For Each radButton In _aryRadButton
				If Not radButton.Equals(mnu) Then
					radButton.Checked = False
				End If
			Next

			If Not parentDropDownButton Is Nothing Then
				parentDropDownButton.Text = mnu.Text
				parentDropDownButton.ToolTipText = mnu.ToolTipText
				parentDropDownButton.Image = mnu.Image
			End If

			Return mnu
		End Function

		''' <summary>
		''' 指定された値のメニューアイテムを選択する
		''' </summary>
		''' <param name="value">選択したい値</param>
		''' <returns></returns>
		''' <remarks>
		''' タグプロパティに値が設定されている事が前提です。<br/>
		''' 当メソッドを使用する場合は、<see cref="Add"/> に <c>value</c> を指定してください。
		''' </remarks>
		Public Function SetSelected(ByVal value As Object) As ToolStripMenuItem
			Dim radButton As ToolStripMenuItem

			Dim result As ToolStripMenuItem = Nothing

			For Each radButton In _aryRadButton
				If radButton.Tag.Equals(value) Then
					radButton.Checked = True

					If Not parentDropDownButton Is Nothing Then
						parentDropDownButton.Text = radButton.Text
						parentDropDownButton.ToolTipText = radButton.ToolTipText
						parentDropDownButton.Image = radButton.Image
					End If

					result = radButton
				Else
					radButton.Checked = False
				End If
			Next

			Return result
		End Function

	End Class

End Namespace
