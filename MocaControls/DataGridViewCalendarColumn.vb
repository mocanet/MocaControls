
Namespace Win

    ''' <summary>
    ''' 日付入力セル列を表します。
    ''' </summary>
    Public Class DataGridViewCalendarColumn
        Inherits DataGridViewColumn

#Region " Declare "

        ''' <summary>表示形式</summary>
        Private _pickerFormat As DateTimePickerFormat = DateTimePickerFormat.Custom
        ''' <summary>表示用フォーマット</summary>
        Private _pickerCustomFormat As String = ""
        ''' <summary>入力できる最小値</summary>
        Private _minDate As Date = DateTimePicker.MinimumDateTime
        ''' <summary>入力できる最大値</summary>
        Private _maxDate As Date = DateTimePicker.MaximumDateTime

#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            MyBase.New(New DataGridViewCalendarCell())
        End Sub

#End Region

#Region " Overrides "

        '''<summary>
        '''新しいセルの作成に使用するテンプレートを取得または設定します。
        '''</summary>
        '''<returns>
        '''列に含まれる他のすべてのセルがモデルとする <see cref="T:System.Windows.Forms.DataGridViewCell" />。既定値は null です。
        '''</returns>
        '''<filterpriority>1</filterpriority>
        Public Overrides Property CellTemplate As DataGridViewCell
            Get
                Return MyBase.CellTemplate
            End Get
            Set(value As DataGridViewCell)
                If (value IsNot Nothing) AndAlso
                Not value.GetType().IsAssignableFrom(GetType(DataGridViewCalendarCell)) Then
                    Throw New InvalidCastException("Must be a DataGridViewCalendarCell")
                End If
                MyBase.CellTemplate = value
            End Set
        End Property

        '''<returns>複製された <see cref="T:System.Windows.Forms.DataGridViewBand" /> を表す <see cref="T:System.Object" />。</returns>
        '''<filterpriority>1</filterpriority>
        '''<PermissionSet>
        '''<IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        '''<IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        '''</PermissionSet>
        Public Overrides Function Clone() As Object
            Dim col As DataGridViewCalendarColumn = MyBase.Clone()

            col.PickerFormat = Me.PickerFormat
            col.PickerCustomFormat = Me.PickerCustomFormat
            'col.Format = Me.Format
            col.MinDate = Me.MinDate
            col.MaxDate = Me.MaxDate

            Return col
        End Function

#End Region

#Region " Property "

        ''' <summary>
        ''' 表示形式
        ''' </summary>
        ''' <returns></returns>
        Public Property PickerFormat() As DateTimePickerFormat
            Get
                Return _pickerFormat
            End Get
            Set(ByVal value As DateTimePickerFormat)
                _pickerFormat = value
            End Set
        End Property

        ''' <summary>
        ''' 表示用フォーマット
        ''' </summary>
        ''' <returns></returns>
        Public Property PickerCustomFormat() As String
            Get
                Return _pickerCustomFormat
            End Get
            Set(ByVal value As String)
                _pickerCustomFormat = value
            End Set
        End Property

        ''' <summary>
        ''' 入力できる最小値
        ''' </summary>
        ''' <returns></returns>
        Public Property MinDate() As Date
            Get
                Return _minDate
            End Get
            Set(ByVal value As Date)
                _minDate = value
            End Set
        End Property

        ''' <summary>
        ''' 入力できる最大値
        ''' </summary>
        ''' <returns></returns>
        Public Property MaxDate() As Date
            Get
                Return _maxDate
            End Get
            Set(ByVal value As Date)
                _maxDate = value
            End Set
        End Property

#End Region

    End Class

End Namespace
