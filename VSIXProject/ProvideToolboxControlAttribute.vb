'***************************************************************************
'
'    Copyright (c) Microsoft Corporation. All rights reserved.
'    This code is licensed under the Visual Studio SDK license terms.
'    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
'    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
'    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
'    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
'
'***************************************************************************

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Globalization
Imports Microsoft.VisualStudio.Shell

''' <summary>
''' This attribute adds a ToolboxControlsInstaller key for the assembly to install toolbox controls from the assembly
''' </summary>
''' <remarks>
''' For example
'''     [$(Rootkey)\ToolboxControlsInstaller\$FullAssemblyName$]
'''         "Codebase"="$path$"
'''         "WpfControls"="1"
''' </remarks>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False, Inherited:=True)>
<System.Runtime.InteropServices.ComVisibleAttribute(False)>
Public NotInheritable Class ProvideToolboxControlAttribute
    Inherits RegistrationAttribute

    Private Const ToolboxControlsInstallerPath As String = "ToolboxControlsInstaller"

    Private _isWpfControls As Boolean
    Private _name As String

    ''' <summary>
    ''' Creates a new ProvideToolboxControl attribute to register the assembly for toolbox controls installer
    ''' </summary>
    ''' <param name="isWpfControls"></param>
    Public Sub New(ByVal name As String, ByVal isWpfControls As Boolean)
        If (name Is Nothing) Then
            Throw New ArgumentException(NameOf(name))
        End If

        Me.Name = name
        Me.IsWpfControls = isWpfControls
    End Sub

    ''' <summary>
    ''' Gets whether the toolbox controls are for WPF.
    ''' </summary>
    Private Property IsWpfControls As Boolean
        Get
            Return Me._isWpfControls
        End Get
        Set(ByVal value As Boolean)
            Me._isWpfControls = value
        End Set
    End Property

    ''' <summary>
    ''' Gets the name for the controls
    ''' </summary>
    Private Property Name As String
        Get
            Return Me._name
        End Get
        Set(ByVal value As String)
            Me._name = value
        End Set
    End Property

    ''' <summary>
    ''' Called to register this attribute with the given context.  The context
    ''' contains the location where the registration information should be placed.
    ''' It also contains other information such as the type being registered and path information.
    ''' </summary>
    ''' <param name="context">Given context to register in</param>
    Public Overrides Sub Register(ByVal context As RegistrationAttribute.RegistrationContext)
        If (context Is Nothing) Then
            Throw New ArgumentNullException(NameOf(context))
        End If

        Using key As Key = context.CreateKey(String.Format(CultureInfo.InvariantCulture, "{0}\{1}", _
                                                         ToolboxControlsInstallerPath, _
                                                         context.ComponentType.Assembly.FullName))
            key.SetValue(String.Empty, Me.Name)
            key.SetValue("Codebase", context.CodeBase)
            If (Me.IsWpfControls) Then
                key.SetValue("WPFControls", "1")
            End If
        End Using
    End Sub

    Public Overrides Sub Unregister(ByVal context As RegistrationAttribute.RegistrationContext)
        If (context IsNot Nothing) Then
            context.RemoveKey(String.Format(CultureInfo.InvariantCulture, "{0}\{1}",
                                                         ToolboxControlsInstallerPath,
                                                         context.ComponentType.AssemblyQualifiedName))
        End If
    End Sub
End Class