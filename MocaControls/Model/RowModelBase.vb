
Imports Moca.Db.Attr

Namespace Win

    ''' <summary>
    ''' 行モデルの抽象クラス
    ''' </summary>
    Public MustInherit Class RowModelBase
        Inherits Moca.Entity.EntityBase(Of RowModelBase)

#Region " Declare "

        Private _Status As Data.DataRowState

        Private _subTotal As Boolean


#End Region

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New()
            _Status = DataRowState.Unchanged
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' オブジェクトの状態
        ''' </summary>
        ''' <returns></returns>
        <ColumnIgnore(True)>
        <HiddenColumn()>
        Public ReadOnly Property Status As Data.DataRowState
            Get
                Return _Status
            End Get
        End Property

        ''' <summary>
        ''' IsBlank
        ''' </summary>
        ''' <returns></returns>
        <ColumnIgnore(True)>
        <HiddenColumn()>
        Public Overridable ReadOnly Property IsBlank As Boolean
            Get
                Return False
            End Get
        End Property

#End Region

#Region " Method "

        ''' <summary>
        ''' 追加
        ''' </summary>
        Public Sub Add()
            _Status = DataRowState.Added
        End Sub

        ''' <summary>
        ''' 変更
        ''' </summary>
        Public Sub Modify()
            If _Status = DataRowState.Added Then
                Return
            End If
            _Status = DataRowState.Modified
        End Sub

        ''' <summary>
        ''' 変更
        ''' </summary>
        Public Sub Delete()
            If _Status = DataRowState.Added Then
                Return
            End If
            _Status = DataRowState.Deleted
        End Sub

        ''' <summary>
        ''' ステータスセット
        ''' </summary>
        Protected Sub setStatus(ByVal status As DataRowState)
            _Status = status
        End Sub

        ''' <summary>
        ''' コピー
        ''' </summary>
        ''' <param name="value"></param>
        Public Overridable Sub Copy(ByVal value As Object)
        End Sub

        ''' <summary>
        ''' DBNull, String.Empty の時はNothingに変換する
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Protected Function CNothing(ByVal value As Object) As Object
            If DBNull.Value.Equals(value) Then
                Return Nothing
            End If
            If TypeOf value Is String Then
                If String.IsNullOrEmpty(value) Then
                    Return Nothing
                End If
            End If
            Return value
        End Function

        ''' <summary>
        ''' DBNull, String.Empty の時は 0 に変換する
        ''' </summary>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Protected Function CZero(ByVal value As Object) As Object
            If DBNull.Value.Equals(value) Then
                Return 0
            End If
            If value Is Nothing Then
                Return 0
            End If
            Return value
        End Function

#End Region

    End Class

End Namespace
