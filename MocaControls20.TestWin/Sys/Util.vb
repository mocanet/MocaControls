
Namespace Sys

    ''' <summary>
    ''' ユーティリティクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Util

        ''' <summary>
        ''' メッセージを返す
        ''' </summary>
        ''' <param name="id">メッセージID</param>
        ''' <param name="val">置換える内容</param>
        ''' <returns>該当するメッセージ</returns>
        ''' <remarks></remarks>
        Public Shared Function GetMessage(ByVal id As MessageIDs, ByVal ParamArray val() As String) As String
            Return String.Format(My.Resources.Messages.ResourceManager.GetString(id.ToString), val)
        End Function

    End Class

End Namespace
