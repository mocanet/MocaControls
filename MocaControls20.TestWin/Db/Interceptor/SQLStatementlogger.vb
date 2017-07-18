
Imports Moca.Db.Interceptor

Namespace Db

    ''' <summary>
    ''' DBアクセス実行時のインターセプター
    ''' </summary>
    ''' <remarks>
    ''' SQLステートメント実行前、後、例外発生時へ割込み処理を実行できます。
    ''' </remarks>
    Public Class SQLStatementlogger
        Inherits AbstractDaoInterceptor

#Region " Declare "

#Region " Logging For Log4net "
        ''' <summary>Logging For Log4net</summary>
        Private Shared ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)
#End Region
#End Region

        ''' <summary>
        ''' SQLステートメント実行前
        ''' </summary>
        ''' <param name="dao">DBアクセスオブジェクト</param>
        ''' <remarks></remarks>
        Protected Overrides Sub executeBegin(dao As Moca.Db.IDao)
            ' SQLステートメントの履歴取得ON (全てのステートメント)
            dao.ExecuteHistory = True
            ' Insert, Update, Deleteステートメントのみの時
            'dao.ExecuteUpdateHistory = True
        End Sub

        ''' <summary>
        ''' SQLステートメント実行後
        ''' </summary>
        ''' <param name="dao">DBアクセスオブジェクト</param>
        ''' <remarks></remarks>
        Protected Overrides Sub executeEnd(dao As Moca.Db.IDao)
            For Each sql As String In dao.ExecuteHistories
                _mylog.DebugFormat("executeEnd {0}", sql)
            Next
        End Sub

        ''' <summary>
        ''' SQLステートメント実行エラー
        ''' </summary>
        ''' <param name="dao">DBアクセスオブジェクト</param>
        ''' <param name="ex">例外</param>
        ''' <remarks></remarks>
        Protected Overrides Sub executeError(dao As Moca.Db.IDao, ex As System.Exception)
            _mylog.DebugFormat("executeError {0} : {1}", dao.CommandWrapper.CommandText, ex.Message)
        End Sub

    End Class

End Namespace
