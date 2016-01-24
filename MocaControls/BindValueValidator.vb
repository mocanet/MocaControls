
Imports System.Reflection

Imports Moca.Attr
Imports Moca.Util
Imports Moca.Win

Namespace Win

	''' <summary>
	''' バインドしているコントロールの値を検証する
	''' </summary>
	''' <remarks>
	''' 現在対応しているコントロール<br/>
	''' <see cref="TextBox"></see><br/>
	''' <see cref="ComboBox"></see><br/>
	''' <see cref="NullableDateTimePicker"></see><br/>
	''' <see cref="DateTimePicker"></see><br/>
	''' </remarks>
	Public Class BindValueValidator

#Region " Declare "

#Region " _entityBindInfo "

		''' <summary>
		''' 検証時のデータたち
		''' </summary>
		''' <remarks></remarks>
		Private Class _entityBindInfo

			Private _columnNames As IDictionary(Of String, String)

			Private _errorColumns As IList(Of String)

#Region " コンストラクタ "

			Public Sub New(ByVal bind As Object, ByVal entity As Object)
				Me.BindTarget = bind
				Me.EntityTarget = entity

				Me.EntityPropertyInfos = ClassUtil.GetProperties(Me.EntityTarget)
				Me.UpdateEntityStop = False
				Me.TableDefinition = Nothing
				Me.IsValid = True

				_errorColumns = New List(Of String)
			End Sub

#End Region

#Region " Property "

			Property BindTarget As Object
			Property EntityTarget As Object

			Property EntityPropertyInfos As PropertyInfo()

			Property EntityPropertyInfo As PropertyInfo
			Property ControlPropertyInfo As PropertyInfo
			Property ControlAttribute As BindControlAttribute

			Property Control As Object

			Property Value As Object

			Property ValidateMethod As UpdateEntityValidate

			Property TableDefinition As Object
			Property TableDefinitionFieldInfo As FieldInfo

			Property UpdateEntityStop As Boolean

			Property IsValid As Boolean

			ReadOnly Property IsError(ByVal caption As String) As Boolean
				Get
					Return _errorColumns.Contains(caption)
				End Get
			End Property

			ReadOnly Property ErrorColumns As IList(Of String)
				Get
					Return _errorColumns
				End Get
			End Property

			ReadOnly Property TableDefinitionColumn(ByVal name As String) As Moca.Db.DbInfoColumn
				Get
					If Me.TableDefinition Is Nothing Then
						Return Nothing
					End If

					If _columnNames Is Nothing Then
						_getColumnNames()
					End If

					Dim columnName As String = String.Empty
					If Not _columnNames.TryGetValue(name, columnName) Then
						Return Nothing
					End If

					Dim column As Moca.Db.DbInfoColumn = Nothing
					column = TryCast(Me.TableDefinitionFieldInfo.FieldType.InvokeMember(columnName, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetProperty, Nothing, Me.TableDefinition, New Object() {}), Moca.Db.DbInfoColumn)
					Return column
				End Get
			End Property

#End Region
#Region " Methods "

			Public Sub AddError(ByVal caption As String)
				_errorColumns.Add(caption)
			End Sub

			Private Sub _getColumnNames()
				_columnNames = New Dictionary(Of String, String)

				Dim props() As PropertyInfo = Me.TableDefinitionFieldInfo.FieldType.GetProperties()
				For Each prop As PropertyInfo In props
					Dim attrColumn As Moca.Db.Attr.ColumnAttribute = ClassUtil.GetCustomAttribute(Of Moca.Db.Attr.ColumnAttribute)(prop)
					Dim columnName As String = prop.Name
					If attrColumn IsNot Nothing Then
						columnName = attrColumn.ColumnName
					End If
					_columnNames.Add(columnName, prop.Name)
				Next
			End Sub

#End Region

		End Class

#End Region

#End Region

		''' <summary>検証</summary>
		Public Property Validator As Validator

#Region " コンストラクタ "

		''' <summary>
		''' コンストラクタ
		''' </summary>
		''' <remarks></remarks>
		Public Sub New()
			Validator = New Validator
		End Sub

#End Region

		''' <summary>
		''' 検証開始
		''' </summary>
		''' <param name="bindTarget">バインドしているコントロールがあるフォーム</param>
		''' <param name="entity">エンティティ</param>
		''' <param name="validateMethod">検証した結果を処理するデリゲート</param>
		''' <returns></returns>
		''' <remarks></remarks>
		Public Function DoValidation(ByVal bindTarget As Object, ByVal entity As Object, Optional ByVal validateMethod As UpdateEntityValidate = Nothing) As Boolean
			Dim bindInfo As _entityBindInfo = New _entityBindInfo(bindTarget, entity)
			bindInfo.ValidateMethod = validateMethod

			'テーブル定義を取得
			_getTableDefinition(bindInfo)

			' エンティティのプロパティ数分繰り返す
			For Each entityPropertyInfo As PropertyInfo In bindInfo.EntityPropertyInfos
				bindInfo.EntityPropertyInfo = entityPropertyInfo
				' エンティティのBindControl属性を取得
				bindInfo.ControlAttribute = _getAttrBindControl(bindInfo.EntityPropertyInfo)
				If bindInfo.ControlAttribute Is Nothing Then
					' エンティティの値を取得
					bindInfo.Value = _getEntityValue(bindInfo.EntityTarget, bindInfo.EntityPropertyInfo)
					' バインド先は無くても検証があればやる
					_validate(bindInfo)
					If bindInfo.UpdateEntityStop Then
						Return bindInfo.IsValid
					End If
					' BindControl属性無しは次へ
					Continue For
				End If

				Dim setFlag As Boolean = False

				' エンティティのBindControl属性分繰り返す
				For Each controlName As String In bindInfo.ControlAttribute.ControlName
					' ターゲットのプロパティを取得
					bindInfo.ControlPropertyInfo = ClassUtil.GetProperties(bindInfo.BindTarget.GetType, controlName)
					If bindInfo.ControlPropertyInfo Is Nothing Then
						' コントロールが無いときは次へ
						Continue For
					End If

					' コントロール自体を取得
					bindInfo.Control = _getBindControl(bindInfo.BindTarget, bindInfo.ControlPropertyInfo)
					If bindInfo.Control Is Nothing Then
						Continue For
					End If

					' 値取得
					If TypeOf bindInfo.Control Is TextBox Then
						_getControlValue(bindInfo, "Text")
						setFlag = True
					ElseIf TypeOf bindInfo.Control Is ComboBox Then
						_getControlValue(bindInfo, "SelectedValue")
						setFlag = True
					ElseIf TypeOf bindInfo.Control Is NullableDateTimePicker Then
						_getControlValue(bindInfo, "Value")
						setFlag = True
					ElseIf TypeOf bindInfo.Control Is DateTimePicker Then
						_getControlValue(bindInfo, "Value")
						setFlag = True
					End If

					' 検証
					_validate(bindInfo)
					If Not bindInfo.UpdateEntityStop Then
						Continue For
					End If

					Return bindInfo.IsValid
				Next
				If Not setFlag Then
					' エンティティの値を取得
					bindInfo.Value = _getEntityValue(bindInfo.EntityTarget, bindInfo.EntityPropertyInfo)
					' バインド先は無くても検証があればやる
					_validate(bindInfo)
					If bindInfo.UpdateEntityStop Then
						Return bindInfo.IsValid
					End If
				End If
			Next

			Return bindInfo.IsValid
		End Function

		''' <summary>
		''' コントロールの値を取得
		''' </summary>
		''' <param name="bindInfo"></param>
		''' <param name="name"></param>
		''' <remarks></remarks>
		Private Sub _getControlValue(ByVal bindInfo As _entityBindInfo, ByVal name As String)
			Dim value As Object

			' コントロールの値を取得
			value = bindInfo.Control.GetType.InvokeMember(name, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetProperty, Nothing, bindInfo.Control, New Object() {})

			bindInfo.Value = value
		End Sub

		''' <summary>
		''' 検証
		''' </summary>
		''' <param name="bindInfo"></param>
		''' <remarks></remarks>
		Private Function _validate(ByVal bindInfo As _entityBindInfo) As Boolean
			If bindInfo.ValidateMethod Is Nothing Then
				' 検証省略時は処理終了
				Return True
			End If

			' 検証種別取得
			Dim attrValid As ValidateAttribute = _getAttrValidate(bindInfo.EntityPropertyInfo)
			If attrValid Is Nothing Then
				Return True
			End If

			' キャプション取得
			Dim caption As String = _getCaption(bindInfo.EntityPropertyInfo)
			Dim columnName As String = _getTableColumnName(bindInfo.EntityPropertyInfo)

			Dim args As UpdateEntityValidateArgs = New UpdateEntityValidateArgs(bindInfo.ErrorColumns)
			args.Caption = caption
			args.EntityPropertyName = bindInfo.EntityPropertyInfo.Name
			args.ValidateType = attrValid.ValidateType
			args.Min = attrValid.Min
			args.Max = attrValid.Max
			args.Value = bindInfo.Value
			args.IsValid = True
			args.ValidateStop = False

			Dim columnDefinition As Moca.Db.DbInfoColumn = bindInfo.TableDefinitionColumn(columnName)
			If columnDefinition IsNot Nothing Then
				If args.Max Is Nothing Then
					If Validator.IsValidLenghtMaxB(args.ValidateType) Then
						args.Max = columnDefinition.MaxLength
					Else
						args.Max = columnDefinition.MaxLengthB
					End If
				End If
			End If

			Dim value As Object = args.Value
			If IsArray(value) Then
				value = CType(args.Value, Array).Length
			End If
			Dim verifyValue As String = String.Empty
			If value IsNot Nothing Then
				verifyValue = value.ToString
			End If
			args.ValidateResultType = Validator.Verify(verifyValue, args.ValidateType, args.Min, args.Max)
			If args.ValidateResultType <> ValidateTypes.None Then
				args.IsValid = False
				bindInfo.AddError(caption)
			End If

			bindInfo.ValidateMethod.Invoke(bindInfo.Control, args)
			bindInfo.UpdateEntityStop = args.ValidateStop
			If bindInfo.IsValid Then
				bindInfo.IsValid = args.IsValid
			End If
			Return args.IsValid
		End Function

		Private Sub _getTableDefinition(ByVal bindInfo As _entityBindInfo)
			If bindInfo.ValidateMethod Is Nothing Then
				Return
			End If

			Dim builder As Moca.Db.EntityBuilder = New Moca.Db.EntityBuilder
			builder.SetColumnInfo(bindInfo.EntityTarget)

			'TODO: インタフェースチェックする予定
			Dim info As FieldInfo = Nothing
			Dim fields() As FieldInfo = bindInfo.EntityTarget.GetType.GetFields(BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.NonPublic)
			For Each field As FieldInfo In fields
				Dim attr As Moca.Db.Attr.TableAttribute = ClassUtil.GetCustomAttribute(Of Moca.Db.Attr.TableAttribute)(field.FieldType)
				If attr Is Nothing Then
					Continue For
				End If
				info = field
			Next
			'info = bindInfo.EntityTarget.GetType.GetField("tableDefinition", BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)
			If info Is Nothing Then
				Return
			End If

			Dim obj As Object
			obj = bindInfo.EntityTarget.GetType.InvokeMember(info.Name, BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public Or BindingFlags.GetField, Nothing, bindInfo.EntityTarget, New Object() {})
			bindInfo.TableDefinition = obj
			bindInfo.TableDefinitionFieldInfo = info
		End Sub

		''' <summary>
		''' バインドコントロール属性を取得する
		''' </summary>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getAttrBindControl(ByVal propInfo As PropertyInfo) As BindControlAttribute
			Return ClassUtil.GetCustomAttribute(Of BindControlAttribute)(propInfo)
		End Function

		''' <summary>
		''' エンティティの値を取得する
		''' </summary>
		''' <param name="entity"></param>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getEntityValue(ByVal entity As Object, ByVal propInfo As PropertyInfo) As Object
			Return entity.GetType.InvokeMember(propInfo.Name, BindingFlags.Instance Or BindingFlags.Public Or BindingFlags.GetProperty, Nothing, entity, New Object() {})
		End Function

		''' <summary>
		''' バインド先のコントロールを取得する
		''' </summary>
		''' <param name="bindTarget"></param>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getBindControl(ByVal bindTarget As Object, ByVal propInfo As PropertyInfo) As Object
			Return bindTarget.GetType.InvokeMember(propInfo.Name, BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.GetProperty, Nothing, bindTarget, New Object() {})
		End Function

		''' <summary>
		''' 検証属性を取得する
		''' </summary>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getAttrValidate(ByVal propInfo As PropertyInfo) As ValidateAttribute
			Return ClassUtil.GetCustomAttribute(Of ValidateAttribute)(propInfo)
		End Function

		''' <summary>
		''' エンティティのキャプション属性を取得し文字列を返す
		''' </summary>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getCaption(ByVal propInfo As PropertyInfo) As String
			Dim attr As CaptionAttribute
			attr = ClassUtil.GetCustomAttribute(Of CaptionAttribute)(propInfo)
			If attr Is Nothing Then
				Return String.Empty
			End If
			Return attr.Caption
		End Function

		''' <summary>
		''' エンティティのテーブル列名を返す
		''' </summary>
		''' <param name="propInfo"></param>
		''' <returns></returns>
		''' <remarks></remarks>
		Private Function _getTableColumnName(ByVal propInfo As PropertyInfo) As String
			Dim attr As Moca.Db.Attr.ColumnAttribute
			attr = ClassUtil.GetCustomAttribute(Of Moca.Db.Attr.ColumnAttribute)(propInfo)
			If attr Is Nothing Then
				Return propInfo.Name
			End If
			Return attr.ColumnName
		End Function

	End Class

End Namespace
