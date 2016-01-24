
Public Class OutputWindowPane

    Const C_PANE_NAME As String = "Moca.NET"
    Const BUILD_OUTPUT_PANE_GUID As String = "{1BD8A850-02D1-11D1-BEE7-00A0C913D1F8}"
    Private _dte As EnvDTE.DTE
    Private _buildOutputWindowPane As EnvDTE.OutputWindowPane

    Public Sub New(ByVal dte As EnvDTE.DTE)
        _dte = dte
        _addOutputWindowPane()
        '_getBuildOutputWindowPane()
    End Sub

    ''' <summary>
    ''' 出力のビルドへメッセージ追加
    ''' </summary>
    ''' <param name="msg"></param>
    ''' <remarks></remarks>
    Public Sub BuildOutput(ByVal msg As String)
        If _buildOutputWindowPane Is Nothing Then
            Return
        End If
        _buildOutputWindowPane.OutputString(msg)
        _buildOutputWindowPane.OutputString(vbCrLf)
    End Sub

    Private Sub _addOutputWindowPane()
        Dim win As EnvDTE.Window
        win = _dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput)
        Dim outputWindow As EnvDTE.OutputWindow
        outputWindow = DirectCast(win.Object, EnvDTE.OutputWindow)
        _buildOutputWindowPane = outputWindow.OutputWindowPanes.Add(C_PANE_NAME)
    End Sub

    Private Sub _getBuildOutputWindowPane()
        Dim win As EnvDTE.Window
        Dim outputWindow As EnvDTE.OutputWindow
        win = _dte.Windows.Item(EnvDTE.Constants.vsWindowKindOutput)
        outputWindow = DirectCast(win.Object, EnvDTE.OutputWindow)
        For Each objOutputWindowPane In outputWindow.OutputWindowPanes
            If objOutputWindowPane.Guid.ToUpper = BUILD_OUTPUT_PANE_GUID Then
                _buildOutputWindowPane = objOutputWindowPane
                Exit For
            End If
        Next
        If _buildOutputWindowPane Is Nothing Then
            _addOutputWindowPane()
            Return
        End If
        _buildOutputWindowPane.Activate()
    End Sub

End Class
