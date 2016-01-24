Imports System

Class GuidList
    Private Sub New()
    End Sub

    Public Const guidMocaControlsVSPackagePkgString As String = "ccb393e3-f0f2-46db-86fa-5ad370408a68"
    Public Const guidMocaControlsVSPackageCmdSetString As String = "077fe583-b8aa-4747-8ca7-8ccba4635d9e"

    Public Shared ReadOnly guidMocaControlsVSPackageCmdSet As New Guid(guidMocaControlsVSPackageCmdSetString)
End Class