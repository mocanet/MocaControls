
Imports System.ComponentModel

Namespace Win

    ''' <summary>
    ''' 編集できる日付情報を <see cref="DataGridView"/> コントロールに表示します。
    ''' </summary>
    Public Class DataGridViewCalendarCell
        Inherits DataGridViewTextBoxCell

#Region " Overrides "

        '''<summary>ホストされる編集コントロールを追加して初期化します。</summary>
        '''<param name="rowIndex">編集される行のインデックス。</param>
        '''<param name="initialFormattedValue">コントロールに表示される初期値。</param>
        '''<param name="dataGridViewCellStyle">ホストされるコントロールの外観を決定するために使用するセル スタイル。</param>
        '''<filterpriority>1</filterpriority>
        '''<PermissionSet>
        '''<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        '''<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''</PermissionSet>
        Public Overrides Sub InitializeEditingControl(rowIndex As Integer, initialFormattedValue As Object, dataGridViewCellStyle As DataGridViewCellStyle)
            MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

            Dim ctl As DataGridViewCalendarEditingControl = CType(DataGridView.EditingControl, DataGridViewCalendarEditingControl)
            Dim col As DataGridViewCalendarColumn = CType(OwningColumn, DataGridViewCalendarColumn)

            ctl.Format = col.PickerFormat
            ctl.CustomFormat = col.PickerCustomFormat
            ctl.MinDate = col.MinDate
            ctl.MaxDate = col.MaxDate


            If IsDBNull(Value) Then
                ctl.Value = Me.DefaultNewRowValue
            ElseIf (Me.Value Is Nothing) Then
                ctl.Value = Me.DefaultNewRowValue
            Else
                Dim val As DateTime = CType(Me.Value, DateTime)
                If ctl.MinDate <= val AndAlso val <= ctl.MaxDate Then
                    ctl.Value = val
                Else
                    If val < ctl.MinDate Then
                        ctl.Value = ctl.MinDate
                    ElseIf ctl.MaxDate < val Then
                        ctl.Value = ctl.MaxDate
                    End If
                End If
            End If
        End Sub

        '''<summary>表示用に書式指定済みのセル値を取得します。 </summary>
        '''<returns>書式指定済みのセル値。または、セルが <see cref="T:System.Windows.Forms.DataGridView" /> コントロールに属していない場合は null。</returns>
        '''<param name="value">書式設定される値。 </param>
        '''<param name="rowIndex">セルの親行のインデックス。 </param>
        '''<param name="cellStyle">セルに反映される <see cref="T:System.Windows.Forms.DataGridViewCellStyle" />。</param>
        '''<param name="valueTypeConverter">書式指定済みの値の型へカスタムの変換を実行する、元の値の型に関連付けられた <see cref="T:System.ComponentModel.TypeConverter" />。カスタムの変換が不要な場合は null。</param>
        '''<param name="formattedValueTypeConverter">書式指定済みの値の型からカスタムの変換を実行する、その値の型に関連付けられた <see cref="T:System.ComponentModel.TypeConverter" />。カスタムの変換が不要な場合は null。</param>
        '''<param name="context">書式指定済みの値が必要とされているコンテキストを示す <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> 値のビットごとの組み合わせ。</param>
        '''<exception cref="T:System.Exception">書式指定が失敗し、<see cref="T:System.Windows.Forms.DataGridView" /> コントロールの <see cref="E:System.Windows.Forms.DataGridView.DataError" /> イベントのハンドラが定義されていないか、ハンドラで <see cref="P:System.Windows.Forms.DataGridViewDataErrorEventArgs.ThrowException" /> プロパティが true に設定されました。例外オブジェクトは通常、<see cref="T:System.FormatException" /> 型にキャストできます。</exception>
        Protected Overrides Function GetFormattedValue(value As Object, rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, valueTypeConverter As TypeConverter, formattedValueTypeConverter As TypeConverter, context As DataGridViewDataErrorContexts) As Object
            If IsDBNull(value) OrElse value Is Nothing OrElse String.IsNullOrEmpty(cellStyle.Format) Then
                Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
            End If

            Try
                Dim val As DateTime = CDate(value)
                Return val.ToString(cellStyle.Format)
            Catch ex As Exception
                Return MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
            End Try
        End Function

        Public Overrides Function ParseFormattedValue(formattedValue As Object, cellStyle As DataGridViewCellStyle, formattedValueTypeConverter As TypeConverter, valueTypeConverter As TypeConverter) As Object
            If String.IsNullOrEmpty(formattedValue) Then
                'Return cellStyle.NullValue
                Return cellStyle.DataSourceNullValue
            End If
            Return MyBase.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter)
        End Function

#Region " Property "

        Public Overrides ReadOnly Property EditType As Type
            Get
                Return GetType(DataGridViewCalendarEditingControl)
            End Get
        End Property

        Public Overrides ReadOnly Property ValueType As Type
            Get
                Return GetType(DateTime?)
            End Get
        End Property

        Public Overrides ReadOnly Property FormattedValueType As Type
            Get
                Return GetType(String)
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultNewRowValue As Object
            Get
                Return Me.Style.DataSourceNullValue
            End Get
        End Property

#End Region

#End Region

    End Class

End Namespace
