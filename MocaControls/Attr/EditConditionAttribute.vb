
Namespace Win

    ''' <summary>
    ''' 編集状態によって列の編集可否を設定する
    ''' </summary>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=True)>
    Public Class EditConditionAttribute
        Inherits Attribute

#Region " コンストラクタ "

        ''' <summary>
        ''' デフォルトコンストラクタ
        ''' </summary>
        Public Sub New(ByVal status As DataRowState)
            _Status = status
        End Sub

#End Region

#Region " Property "

        ''' <summary>
        ''' オブジェクトの状態
        ''' </summary>
        ''' <returns></returns>
        Public Property Status As DataRowState

#End Region

    End Class

End Namespace
