Imports System.ComponentModel.Design
Imports System.Drawing.Design
Imports System.IO
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports Microsoft.VisualStudio.Shell
Imports Microsoft.VisualStudio.Shell.Interop

''' <summary>
''' This is the class that implements the package exposed by this assembly.
'''
''' The minimum requirement for a class to be considered a valid package for Visual Studio
''' is to implement the IVsPackage interface and register itself with the shell.
''' This package uses the helper classes defined inside the Managed Package Framework (MPF)
''' to do it: it derives from the Package class that provides the implementation of the 
''' IVsPackage interface and uses the registration attributes defined in the framework to 
''' register itself and its components with the shell.
''' </summary>
' The PackageRegistration attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class
' is a package.
'
' The InstalledProductRegistration attribute is used to register the information needed to show this package
' in the Help/About dialog of Visual Studio.
'ProvideToolboxItems(2)> _
'ProvideToolboxItems(2),
'ProvideToolboxItemConfiguration(GetType(ToolboxConfig))> _

'<PackageRegistration(UseManagedResourcesOnly:=True),
'DefaultRegistryRoot("Software\\Microsoft\\VisualStudio\\9.0"),
'InstalledProductRegistration("#110", "#112", "2.0.3", IconResourceID:=400),
'ProvideLoadKey("Standard", "2.0.3", "Moca.NET Windows Forms Controls 2.0", "MiYABiS", 1),
'ProvideMenuResource(1000, 1),
'Guid(GuidList.guidMocaControlsVSPackagePkgString),
'ProvideToolboxItems(2),
'ProvideToolboxItemConfiguration(GetType(ToolboxConfig))>

<PackageRegistration(UseManagedResourcesOnly:=True),
 InstalledProductRegistration("#110", "#112", "2.1.2", IconResourceID:=400),
 ProvideLoadKey("Standard", "2.1.2", "Moca.NET Windows Forms Controls 2.0", "MiYABiS", 1),
 Guid(GuidList.guidMocaControlsVSPackagePkgString),
 ProvideToolboxItems(4)>
Public NotInheritable Class MocaControlsVSPackage
    Inherits Package

    Private ToolboxItemList As IDictionary(Of String, ArrayList)

    Private _outputPane As OutputWindowPane

    ''' <summary>
    ''' Default constructor of the package.
    ''' Inside this method you can place any initialization code that does not require 
    ''' any Visual Studio service because at this point the package object is created but 
    ''' not sited yet inside Visual Studio environment. The place to do all the other 
    ''' initialization is the Initialize method.
    ''' </summary>
    Public Sub New()
    End Sub

    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Overridden Package Implementation
#Region "Package Members"

    ''' <summary>
    ''' Initialization of the package; this method is called right after the package is sited, so this is the place
    ''' where you can put all the initialization code that rely on services provided by VisualStudio.
    ''' </summary>
    Protected Overrides Sub Initialize()
        MyBase.Initialize()

        Dim mcs As OleMenuCommandService = GetService(GetType(IMenuCommandService))
        If mcs IsNot Nothing Then
            Dim menuCommandID As CommandID = New CommandID(GuidList.guidMocaControlsVSPackageCmdSet, CInt(PkgCmdIDList.cmdidInitializeToolbox))
            Dim menuItem As MenuCommand = New MenuCommand(AddressOf MenuItemCallback, menuCommandID)
            mcs.AddCommand(menuItem)
        End If

        Dim dte As EnvDTE.DTE
        dte = GetService(GetType(EnvDTE.DTE))
        _outputPane = New OutputWindowPane(dte)

        ToolboxItemList = New Dictionary(Of String, ArrayList)
        For ii As Integer = 0 To _componentPath.Count - 1
            _initToolboxItems(ii)
        Next
    End Sub

#End Region

    Private Sub MenuItemCallback(ByVal sender As Object, ByVal e As EventArgs)
        Dim pkg As IVsPackage = GetService(GetType(Package))
        pkg.ResetDefaults(__VSPKGRESETFLAGS.PKGRF_TOOLBOXITEMS)
    End Sub

    Private _componentPath() As String = {"net20", "net35", "net40", "net45", "net46"}
    Private _componentName As String = "MocaControls.dll"
    Private _categoryTabsOld() As String = {"Controls 2.0.0.0", "Controls 3.5.0.0", "Controls 4.0.0.0", "Controls 4.5.0.0", "Controls 4.6.0.0"}
    Private _categoryTabsOld2() As String = {"Controls 2.0.1.0", "Controls 3.5.1.0", "Controls 4.0.1.0", "Controls 4.5.1.0", "Controls 4.6.0.0"}
    Private _categoryTabsOld3() As String = {"Controls 2.0", "Controls 3.5", "Controls 4.0", "Controls 4.5", "Controls 4.6"}
    Private _categoryTabs() As String = {"Fw 2.0", "Fw 3.5", "Fw 4.0", "Fw 4.5", "Fw 4.6"}
    Private _tabName As String = "Moca.NET "

    Private Sub OnRefreshToolbox(ByVal sender As Object, ByVal e As EventArgs) Handles Me.ToolboxInitialized, Me.ToolboxUpgraded
        Dim service As IToolboxService = TryCast(GetService(GetType(IToolboxService)), IToolboxService)
        Dim toolbox As IVsToolbox = TryCast(GetService(GetType(IVsToolbox)), IVsToolbox)

        For ii As Integer = 0 To _componentPath.Count - 1
            _OnRefreshToolbox(service, toolbox, ii)
        Next

        service.SelectedCategory = _tabName & _categoryTabs(0)
        service.Refresh()
    End Sub

    Private Sub _OnRefreshToolbox(ByVal service As IToolboxService, ByVal toolbox As IVsToolbox, ByVal index As Integer)
        Dim target As AssemblyName = _getAssemblyName(index)
        Dim categoryTabOld As String = _tabName & _categoryTabsOld(index)
        Dim categoryTabOld2 As String = _tabName & _categoryTabsOld2(index)
        Dim categoryTabOld3 As String = _tabName & _categoryTabsOld3(index)
        Dim categoryTab As String = _tabName & _categoryTabs(index)

        For Each oldItem As ToolboxItem In service.GetToolboxItems(categoryTabOld)
            service.RemoveToolboxItem(oldItem)
        Next
        toolbox.RemoveTab(categoryTabOld)
        For Each oldItem As ToolboxItem In service.GetToolboxItems(categoryTabOld2)
            service.RemoveToolboxItem(oldItem)
        Next
        toolbox.RemoveTab(categoryTabOld2)
        For Each oldItem As ToolboxItem In service.GetToolboxItems(categoryTabOld3)
            service.RemoveToolboxItem(oldItem)
        Next
        toolbox.RemoveTab(categoryTabOld3)

        For Each oldItem As ToolboxItem In service.GetToolboxItems(categoryTab)
            service.RemoveToolboxItem(oldItem)
        Next
        toolbox.RemoveTab(categoryTab)

        toolbox.AddTab(categoryTab)
        _output("Add Tab : " & categoryTab)

        For Each item As ToolboxItem In ToolboxItemList(_componentPath(index))
            _output("Add Toolbox : " & item.DisplayName)
            service.AddToolboxItem(item, categoryTab)
        Next
    End Sub

    Private Sub _initToolboxItems(ByVal index As Integer)
        Dim asm As AssemblyName
        'Dim asm As Assembly

        Try
            'asm = Assembly.LoadFile(_getAssemblyPath(index))
            asm = _getAssemblyName(index)

            _output("Assembly FullName : " & asm.FullName)

            'Dim items As ICollection = ToolboxService.GetToolboxItems(asm, asm.EscapedCodeBase, True)
            Dim items As ICollection = ToolboxService.GetToolboxItems(asm, True)
            If items Is Nothing Then
                Throw New ApplicationException("Unable to generate a toolbox item listing for " & asm.FullName)
            End If

            _output("Get Toolbox Item Count : " & items.Count)
            ToolboxItemList.Add(_componentPath(index), items)
        Catch ex As ReflectionTypeLoadException
            _output(ex.LoaderExceptions.ToString)
            _output(ex.ToString)
        Catch ex As Exception
            _output(ex.ToString)
        End Try
    End Sub

    Private Sub _output(ByVal msg As String)
        If _outputPane Is Nothing Then
            Return
        End If
        _outputPane.BuildOutput(msg)
    End Sub

    Private Function _getAssemblyName(ByVal index As Integer) As AssemblyName
        Dim pathAssembly As String = _getAssemblyPath(index)
        Return AssemblyName.GetAssemblyName(pathAssembly)
    End Function

    Private Function _getAssemblyPath(ByVal index As Integer) As String
        Dim pathAssembly As String = String.Concat(
                    Path.GetDirectoryName(Me.GetType.Assembly.Location),
                    Path.DirectorySeparatorChar,
                    _componentPath(index),
                    Path.DirectorySeparatorChar,
                    _componentName)

        _output("pathAssembly : " & pathAssembly)
        Return pathAssembly
    End Function

End Class
