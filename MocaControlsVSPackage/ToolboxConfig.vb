Imports System.ComponentModel
Imports System.Runtime.InteropServices
Imports System.Security.Permissions
Imports Microsoft.VisualStudio.Shell

<ProvideAssemblyFilter("MocaControls, Version=*, Culture=neutral, PublicKeyToken=f981690db0db7da4")>
<Guid("0FB901DF-9F9C-4319-A661-1C84479BF7F8")>
Public Class ToolboxConfig
    Implements IConfigureToolboxItem

    <PrincipalPermission(Security.Permissions.SecurityAction.Demand)>
    Public Sub ConfigureToolboxItem(item As Drawing.Design.ToolboxItem) Implements IConfigureToolboxItem.ConfigureToolboxItem
        If item Is Nothing Then
            Return
        End If

        Dim newFilter As ToolboxItemFilterAttribute
        newFilter = New ToolboxItemFilterAttribute("System.Windows.Forms.UserControl", ToolboxItemFilterType.Require)
        ''If item.TypeName.StartsWith("Moca.Win.") Then
        ''    newFilter = New ToolboxItemFilterAttribute("System.Windows.Forms.UserControl", ToolboxItemFilterType.Require)
        ''Else
        ''    Return
        ''End If

        item.Filter = CType(New System.ComponentModel.ToolboxItemFilterAttribute() {newFilter}, ICollection)
    End Sub

End Class
