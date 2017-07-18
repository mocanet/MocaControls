
Imports System.IO

Imports Moca.Util

Namespace Sys

    ''' <summary>
    ''' システム共通設定値
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class SysSettings

#Region " Declare "

        ''' <summary>シングルトン用インスタンス</summary>
        Private Shared _instance As SysSettings

#Region " Logging For Log4net "
        ''' <summary>Logging For Log4net</summary>
        Private Shared ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)
#End Region
#End Region

#Region " Property "

#End Region
#Region " Methods "

        ''' <summary>
        ''' 当クラスのインスタンスを返す。
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function Instance() As SysSettings
            If _instance Is Nothing Then
                _instance = New SysSettings()
            End If
            Return _instance
        End Function

#End Region

    End Class

End Namespace
