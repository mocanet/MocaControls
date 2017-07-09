
Imports System.IO
Imports System.ComponentModel
Imports System.Text.RegularExpressions

Imports System.Drawing

Imports Moca.Db
Imports Moca.Win

Namespace Win

    ''' <summary>
    ''' データバインダーコンポーネント
    ''' </summary>
    ''' <remarks>
    ''' 画面の入力項目とデータのバインディングを補助するコンポーネントです。<br/>
    ''' 標準でロジックを組むよりは扱いやすくしたつもりです。<br/>
    ''' </remarks>
	<Description("入力項目とデータのバインディングを補助するコンポーネント"), _
	ToolboxItem(True)> _
	Public Class DataBinder
		Implements IDisposable

		''' <summary>フォームのデータ ソースをカプセル化</summary>
		Private _bindSrc As BindingSource

		Private _ds As DataSet

		Private _lst As Object

		'#Region " コンストラクタ "

		'        ''' <summary>
		'        ''' デフォルトコンストラクタ
		'        ''' </summary>
		'        ''' <remarks></remarks>
		'        Public Sub New()
		'            _bindSrc = New BindingSource()
		'        End Sub

		'#End Region
		'#Region " IDisposable Support "

		'        Private disposedValue As Boolean = False        ' 重複する呼び出しを検出するには

		'        ' IDisposable
		'        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		'            If Not Me.disposedValue Then
		'                If disposing Then
		'                    ' TODO: 明示的に呼び出されたときにマネージ リソースを解放します
		'                End If

		'                ' TODO: 共有のアンマネージ リソースを解放します
		'                _bindSrc.Dispose()
		'            End If
		'            Me.disposedValue = True
		'        End Sub

		'        ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
		'        Public Sub Dispose() Implements IDisposable.Dispose
		'            ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(ByVal disposing As Boolean) に記述します。
		'            Dispose(True)
		'            GC.SuppressFinalize(Me)
		'        End Sub
		'#End Region
#Region " プロパティ "

		Public Property BindSrc() As System.Windows.Forms.BindingSource
			Get
				If _bindSrc Is Nothing Then
					_bindSrc = New BindingSource()
				End If
				Return Me._bindSrc
			End Get
			Set(ByVal value As System.Windows.Forms.BindingSource)
				Me._bindSrc = value
			End Set
		End Property

		''' <summary>
		''' バインドするデータソース
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property DataSource As Object
			Get
				Return Me.BindSrc.DataSource
			End Get
			Set(value As Object)
				_lst = value

				Me.BindSrc.DataSource = value
			End Set
		End Property

		''' <summary>
		''' バインドするデータソースのメンバ名
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property DataMember() As String
			Get
				Return Me.BindSrc.DataMember
			End Get
			Set(ByVal value As String)
				Me.BindSrc.DataMember = value
			End Set
		End Property

		''' <summary>
		''' 現在行位置
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Property Position() As Integer
			Get
				Return Me.BindSrc.CurrencyManager.Position
			End Get
			Set(ByVal value As Integer)
				Me.BindSrc.CurrencyManager.Position = value
			End Set
		End Property

		''' <summary>
		''' 現在行データ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Public ReadOnly Property CurrentRow() As DataRow
			Get
				If _current Is Nothing Then
					Return Nothing
				End If
				If Not TypeOf _current Is DataRowView Then
					Throw New Moca.Exceptions.MocaRuntimeException("TypeOf Is Not DataRowView")
				End If
				Return DirectCast(_current, DataRowView).Row
			End Get
		End Property

		''' <summary>
		''' 現在行データ
		''' </summary>
		''' <typeparam name="T"></typeparam>
		''' <returns></returns>
		''' <remarks></remarks>
        Public Function CurrentEntity(Of T)() As T
            If _current Is Nothing Then
                Return Nothing
            End If
            Return DirectCast(_current, T)
        End Function

		''' <summary>
		''' 現在行データ
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Private ReadOnly Property _current As Object
			Get
				If Me.BindSrc.Current Is Nothing Then
					Return Nothing
				End If
				Return Me.BindSrc.Current
			End Get
		End Property

		''' <summary>
		''' 
		''' </summary>
		''' <value></value>
		''' <returns></returns>
		''' <remarks></remarks>
		Private ReadOnly Property _currentEditableObject As System.ComponentModel.IEditableObject
			Get
				Return DirectCast(Me.BindSrc.Current, System.ComponentModel.IEditableObject)
			End Get
		End Property

#End Region
#Region " データ編集状態制御 "

		''' <summary>
		''' 現在行の編集終了
		''' </summary>
		''' <remarks></remarks>
		Public Sub EndCurrentEdit()
			Me.BindSrc.CurrencyManager.EndCurrentEdit()
			Me._currentEditableObject.EndEdit()
		End Sub

		''' <summary>
		''' 現在行の編集終了
		''' </summary>
		''' <remarks></remarks>
		Public Sub EndEdit()
			If _currentEditableObject Is Nothing Then
				Return
			End If
			Me._currentEditableObject.EndEdit()
		End Sub

		''' <summary>
		''' 新しい行、削除された行、変更された行などの変更があるかどうかを示す値を取得します。
		''' </summary>
		''' <returns>変更がある場合は true。それ以外の場合は false。</returns>
		''' <remarks></remarks>
		Public Function HasChanges() As Boolean
			If _ds Is Nothing Then
				Return False
			End If
			Return _ds.HasChanges
		End Function

		''' <summary>
		''' データソース全体の変更をコミットする 
		''' </summary>
		''' <remarks>
		''' </remarks>
		Public Sub AcceptChanges()
			Me.BindSrc.EndEdit()
		End Sub

		''' <summary>
		''' 現在行の変更をコミットする 
		''' </summary>
		''' <remarks>
		''' </remarks>
		Public Sub AcceptChangesRow()
			Me._currentEditableObject.EndEdit()
		End Sub

		''' <summary>
		''' データソース全体で前回 AcceptChanges を呼び出した以降にこのデータに対して行われたすべての変更をロールバックします。
		''' </summary>
		''' <remarks></remarks>
		Public Sub RejectChanges()
			Me.BindSrc.CancelEdit()
		End Sub

		''' <summary>
		''' 現在行で前回 AcceptChanges を呼び出した以降にこのデータに対して行われたすべての変更をロールバックします。
		''' </summary>
		''' <remarks></remarks>
		Public Sub RejectChangesRow()
			Me._currentEditableObject.CancelEdit()
		End Sub

#End Region
#Region " データ移動制御 "

		''' <summary>
		''' リストの次の項目に移動します。
		''' </summary>
		''' <remarks></remarks>
		Public Sub MoveNext()
			Me.BindSrc.MoveNext()
		End Sub

		''' <summary>
		''' リストの前の項目に移動します。
		''' </summary>
		''' <remarks></remarks>
		Public Sub MovePrevious()
			Me.BindSrc.MovePrevious()
		End Sub

		''' <summary>
		''' リストの最初の項目に移動します。
		''' </summary>
		''' <remarks></remarks>
		Public Sub MoveFirst()
			Me.BindSrc.MoveFirst()
		End Sub

		''' <summary>
		''' リストの最後の項目に移動します。
		''' </summary>
		''' <remarks></remarks>
		Public Sub MoveLast()
			Me.BindSrc.MoveLast()
		End Sub

#End Region
#Region " データバインディング "

		''' <summary>
		''' ラベルのデータバインディング
		''' </summary>
		''' <param name="ctrl">ラベル</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As Label, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
			_dataBinding(ctrl, "Text", dataMember, dataSourceNullValue, nullValue)
		End Sub

		Public Sub DataBinding(ByVal ctrl As Label, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Text", propName, dataSourceNullValue, nullValue)
        End Sub

		''' <summary>
		''' テキストボックスのデータバインディング
		''' </summary>
		''' <param name="ctrl">テキストボックス</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As TextBox, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing, Optional ByVal formatString As String = "")
			_dataBinding(ctrl, "Text", dataMember, dataSourceNullValue, nullValue, formatString)
		End Sub

        Public Sub DataBinding(ByVal ctrl As TextBox, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing, Optional ByVal formatString As String = "")
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Text", propName, dataSourceNullValue, nullValue, formatString)
            If dataMember.MaxLength < 0 Then
                ctrl.MaxLength = 0
                Return
            End If
            If dataMember.UniCode Then
                ctrl.MaxLength = dataMember.MaxLengthB
            Else
                ctrl.MaxLength = dataMember.MaxLength
            End If
        End Sub

        ''' <summary>
        ''' コンボボックスのデータバインディング
        ''' </summary>
        ''' <param name="ctrl">コンボボックス</param>
        ''' <param name="dataMember">バインドするデータソースの項目名</param>
        ''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
        ''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
        ''' <remarks></remarks>
        Public Sub DataBinding(ByVal ctrl As ComboBox, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propertyName As String
            If ctrl.DropDownStyle = ComboBoxStyle.DropDownList Then
                If ctrl.DataSource Is Nothing Then
                    propertyName = "SelectedItem"
                Else
                    propertyName = "SelectedValue"
                End If
            Else
                propertyName = "Text"
            End If
            _dataBinding(ctrl, propertyName, dataMember, dataSourceNullValue, nullValue)
        End Sub

        Public Sub DataBinding(ByVal ctrl As ComboBox, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            DataBinding(ctrl, propName.ToString, dataSourceNullValue, nullValue)
            If dataMember.MaxLength < 0 Then
                ctrl.MaxLength = 0
                Return
            End If
            If dataMember.UniCode Then
                ctrl.MaxLength = dataMember.MaxLengthB
            Else
                ctrl.MaxLength = dataMember.MaxLength
			End If
		End Sub

		''' <summary>
		''' チェックボックスのデータバインディング
		''' </summary>
		''' <param name="ctrl">チェックボックス</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As CheckBox, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
			_dataBinding(ctrl, "Checked", dataMember, dataSourceNullValue, nullValue)
		End Sub

		Public Sub DataBinding(ByVal ctrl As CheckBox, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Checked", propName, dataSourceNullValue, nullValue)
        End Sub

		''' <summary>
		''' 標準のDateTimePickerでNullを扱えるように拡張したコントロールのデータバインディング
		''' </summary>
		''' <param name="ctrl">標準のDateTimePickerでNullを扱えるように拡張したコントロール</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As DateTimePicker, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
			_dataBinding(ctrl, "Value", dataMember, dataSourceNullValue, nullValue)
		End Sub

		Public Sub DataBinding(ByVal ctrl As DateTimePicker, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Value", propName, dataSourceNullValue, nullValue)
        End Sub

		''' <summary>
		''' 標準のNullableDateTimePickerでNullを扱えるように拡張したコントロールのデータバインディング
		''' </summary>
		''' <param name="ctrl">標準のNullableDateTimePickerでNullを扱えるように拡張したコントロール</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As NullableDateTimePicker, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
			_dataBinding(ctrl, "Value", dataMember, dataSourceNullValue, nullValue)
		End Sub

		Public Sub DataBinding(ByVal ctrl As NullableDateTimePicker, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Value", propName, dataSourceNullValue, nullValue)
        End Sub

		''' <summary>
		''' 複数のラジオボタンをグループとして扱うコンポーネントのデータバインディング
		''' </summary>
		''' <param name="ctrl">複数のラジオボタンをグループとして扱うコンポーネント</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As RadioButtonGroup, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
			_dataBinding(ctrl, "SelectedValue", dataMember, dataSourceNullValue, nullValue)
		End Sub

		Public Sub DataBinding(ByVal ctrl As RadioButtonGroup, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing)
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "SelectedValue", propName, dataSourceNullValue, nullValue)
        End Sub

		''' <summary>
		''' テキストボックスExのデータバインディング
		''' </summary>
		''' <param name="ctrl">テキストボックス</param>
		''' <param name="dataMember">バインドするデータソースの項目名</param>
		''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
		''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As TextBoxEx, ByVal dataMember As String, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing, Optional ByVal formatString As String = "")
			_dataBinding(ctrl, "Text", dataMember, dataSourceNullValue, nullValue, formatString)
		End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <param name="ctrl"></param>
		''' <param name="dataMember"></param>
		''' <param name="dataSourceNullValue"></param>
		''' <param name="nullValue"></param>
		''' <param name="formatString"></param>
		''' <remarks></remarks>
		Public Sub DataBinding(ByVal ctrl As TextBoxEx, ByVal dataMember As DbInfoColumn, Optional ByVal dataSourceNullValue As Object = Nothing, Optional ByVal nullValue As Object = Nothing, Optional ByVal formatString As String = "")
            Dim propName As Object = Moca.Db.DbUtil.GetColumnNames(_current.GetType)(dataMember.Name)
            _dataBinding(ctrl, "Text", propName, dataSourceNullValue, nullValue, formatString)
            If dataMember.MaxLength < 0 Then
                ctrl.MaxLength = 0
                Return
            End If
            If dataMember.UniCode Then
                ctrl.MaxLength = dataMember.MaxLengthB
            Else
                ctrl.MaxLength = dataMember.MaxLength
			End If
		End Sub


        ''' <summary>
        ''' データバインディングする
        ''' </summary>
        ''' <param name="obj">対象のコントロール</param>
        ''' <param name="propertyName">バインドするコントロールのプロパティ名</param>
        ''' <param name="dataMember">バインドするデータソースの項目名</param>
        ''' <param name="dataSourceNullValue">コントロールの値が null 参照 (Visual Basic では Nothing) または空の場合にデータ ソースに格納される値を取得または設定します。 </param>
        ''' <param name="nullValue">データ ソースに <see cref="DBNull"/> 値が格納されている場合にコントロール プロパティとして設定される <see cref="Object" /> を取得または設定します。</param>
        ''' <remarks>
        ''' コントロール プロパティの値が変更されるたびに、データ ソースが更新されます。
        ''' </remarks>
        Private Sub _dataBinding(ByVal obj As IBindableComponent, ByVal propertyName As String, ByVal dataMember As String, ByVal dataSourceNullValue As Object, ByVal nullValue As Object, Optional ByVal formatString As String = "")
            Dim bind As Binding
            bind = New Binding(propertyName, Me.BindSrc, dataMember)
            bind.DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged
            bind.NullValue = nullValue
            bind.DataSourceNullValue = Nothing
            If dataSourceNullValue IsNot Nothing Then
                bind.DataSourceNullValue = dataSourceNullValue
            End If
            bind.FormattingEnabled = False
            If formatString.Length > 0 Then
                bind.FormattingEnabled = True
                bind.FormatString = formatString
            End If
            obj.DataBindings.Add(bind)
        End Sub

        ''' <summary>
        ''' ピクチャボックスのデータバインディング
        ''' </summary>
        ''' <param name="pbx">ピクチャボックス</param>
        ''' <param name="dataMember">バインドするデータソースの項目名</param>
        ''' <remarks>
        ''' 未使用。ピクチャボックスをバインディングするのはやめたので。
        ''' </remarks>
        Public Sub DataBinding(ByVal pbx As PictureBox, ByVal dataMember As String)
			Dim bind As Binding
			bind = New Binding("Image", Me.BindSrc, dataMember, True, DataSourceUpdateMode.OnPropertyChanged)
			AddHandler bind.Parse, AddressOf ImageBinding_Parse
			AddHandler bind.Format, AddressOf ImageBinding_Format
			pbx.DataBindings.Add(bind)
		End Sub

		Public Sub DataBinding2(ByVal pbx As PictureBox, ByVal dataMember As String)
			Dim bind As Binding
			bind = New Binding("ImageLocation", Me.BindSrc, dataMember, True, DataSourceUpdateMode.OnPropertyChanged)
			AddHandler bind.Parse, AddressOf ImageBinding_Parse2
			AddHandler bind.Format, AddressOf ImageBinding_Format2
			pbx.DataBindings.Add(bind)
		End Sub

		''' <summary>
		''' ピクチャボックスコントロールのプロパティをデータ値にバインドすると発生します。 
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub ImageBinding_Format2(ByVal sender As Object, ByVal e As ConvertEventArgs)
			If IsDBNull(e.Value) OrElse e.Value Is Nothing Then
				e.Value = Nothing
				Exit Sub
			End If

			Dim img As Byte()
			img = CType(e.Value, Byte())
			If (img Is Nothing) Then
				Exit Sub
			End If
			e.Value = WinUtil.CByteArray2Image(img)
		End Sub

		''' <summary>
		''' ピクチャボックスのデータ連結コントロールの値が変更されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub ImageBinding_Parse2(ByVal sender As Object, ByVal e As ConvertEventArgs)
			If IsDBNull(e.Value) Then
				Exit Sub
			End If
			If e.Value Is Nothing Then
				e.Value = DBNull.Value
			Else
				e.Value = WinUtil.CImage2ByteArray(DirectCast(e.Value, Image))
			End If
		End Sub

		''' <summary>
		''' ピクチャボックスコントロールのプロパティをデータ値にバインドすると発生します。 
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub ImageBinding_Format(ByVal sender As Object, ByVal e As ConvertEventArgs)
			If IsDBNull(e.Value) OrElse e.Value Is Nothing Then
				e.Value = Nothing
				Exit Sub
			End If

			Dim img As Byte()
			img = CType(e.Value, Byte())
			If (img Is Nothing) Then
				Exit Sub
			End If
			e.Value = WinUtil.CByteArray2Image(img)
		End Sub

		''' <summary>
		''' ピクチャボックスのデータ連結コントロールの値が変更されると発生します。
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub ImageBinding_Parse(ByVal sender As Object, ByVal e As ConvertEventArgs)
			If IsDBNull(e.Value) Then
				Exit Sub
			End If
			If e.Value Is Nothing Then
				e.Value = DBNull.Value
			Else
				e.Value = WinUtil.CImage2ByteArray(DirectCast(e.Value, Image))
			End If
		End Sub

#End Region
#Region " Event "

		''' <summary>
		''' 値が変更されているときに発生します
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Public Event ColumnChanging(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

#End Region
#Region " Method "

		''' <summary>
		''' Label コントロールからパラメータを作成する
		''' </summary>
		''' <param name="ctrl">コントロール</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function CreateDbParameter(ByVal ctrl As Label, ByVal cmd As IDbCommandSql) As IDbDataParameter
			Dim bind As Binding
			bind = ctrl.DataBindings("Text")
			Return _createDbParameter(bind, cmd)
		End Function

		''' <summary>
		''' TextBoxBase からの派生型コントロールからパラメータを作成する
		''' </summary>
		''' <param name="ctrl">コントロール</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function CreateDbParameter(ByVal ctrl As TextBoxBase, ByVal cmd As IDbCommandSql) As IDbDataParameter
			Dim bind As Binding
			bind = ctrl.DataBindings("Text")
			Return _createDbParameter(bind, cmd)
		End Function

		''' <summary>
		''' ComboBox コントロールからパラメータを作成する
		''' </summary>
		''' <param name="ctrl">コントロール</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function CreateDbParameter(ByVal ctrl As ComboBox, ByVal cmd As IDbCommandSql) As IDbDataParameter
			Dim bind As Binding
			bind = ctrl.DataBindings("SelectedValue")
			Return _createDbParameter(bind, cmd)
		End Function

		''' <summary>
		''' PictureBox コントロールからパラメータを作成する
		''' </summary>
		''' <param name="ctrl">コントロール</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function CreateDbParameter(ByVal ctrl As PictureBox, ByVal cmd As IDbCommandSql) As IDbDataParameter
			Dim bind As Binding
			bind = ctrl.DataBindings("Image")
			Return _createDbParameter(bind, cmd)
		End Function

#End Region
#Region " Private Method "

		''' <summary>
		''' 値が変更されているときに発生します
		''' </summary>
		''' <param name="sender"></param>
		''' <param name="e"></param>
		''' <remarks></remarks>
		Private Sub _rowChanging(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)
			RaiseEvent ColumnChanging(sender, e)
		End Sub

		''' <summary>
		''' バインディングからパラメータを作成する
		''' </summary>
		''' <param name="bind"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _createDbParameter(ByVal bind As Binding, ByVal cmd As IDbCommandSql) As IDbDataParameter
			Dim paramName As String
			Dim value As Object

			' 列名取得
			paramName = bind.BindingMemberInfo.BindingField
			' バインディングされているデータの値取得
			If TypeOf _current Is DataRowView Then
				value = Me.CurrentRow.Item(paramName)
			Else
				value = _current.GetType().InvokeMember(paramName, Reflection.BindingFlags.GetProperty Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.Instance, Nothing, _current, New Object() {})
			End If

			' イメージのときはバイト配列に変換する
			If TypeOf value Is Image Then
				value = WinUtil.CImage2ByteArray(DirectCast(value, Image))
			End If

			Return cmd.SetParameter(paramName, value)
		End Function

#End Region

	End Class

End Namespace
