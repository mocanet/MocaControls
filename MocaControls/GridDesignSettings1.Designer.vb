﻿'------------------------------------------------------------------------------
' <auto-generated>
'     このコードはツールによって生成されました。
'     ランタイム バージョン:4.0.30319.42000
'
'     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
'     コードが再生成されるときに損失したりします。
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On



<Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
 Global.System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.0.1.0")>  _
Partial Public NotInheritable Class GridDesignSettings
    Inherits Global.System.Configuration.ApplicationSettingsBase
    
    Private Shared defaultInstance As GridDesignSettings = CType(Global.System.Configuration.ApplicationSettingsBase.Synchronized(New GridDesignSettings()),GridDesignSettings)
    
    Public Shared ReadOnly Property [Default]() As GridDesignSettings
        Get
            Return defaultInstance
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("Normal,Alternate,Fixed,Highlight,Focus,Editor,Frozen,NewRow,SelectedColumnHeader,"& _ 
        "SelectedRowHeader,FilterEditor,Error,Disable,ReadOnly,Required,NoEdit,NegativeVa"& _ 
        "lue,Closed,Modify")>  _
    Public ReadOnly Property StyleNames() As String
        Get
            Return CType(Me("StyleNames"),String)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("242, 222, 222")>  _
    Public ReadOnly Property ErrorBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ErrorBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("185, 74, 72")>  _
    Public ReadOnly Property ErrorForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ErrorForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("Info")>  _
    Public ReadOnly Property RequiredBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("RequiredBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("33, 33, 33")>  _
    Public ReadOnly Property RequiredForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("RequiredForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("Meiryo UI, 9pt")>  _
    Public ReadOnly Property NormalFont() As Global.System.Drawing.Font
        Get
            Return CType(Me("NormalFont"),Global.System.Drawing.Font)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("239, 239, 239")>  _
    Public ReadOnly Property ReadOnlyBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ReadOnlyBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("33, 33, 33")>  _
    Public ReadOnly Property ReadOnlyForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ReadOnlyForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("Gainsboro")>  _
    Public ReadOnly Property DisableBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("DisableBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("DarkGray")>  _
    Public ReadOnly Property DisableForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("DisableForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("251, 251, 243")>  _
    Public ReadOnly Property FrozenBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("FrozenBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("LightSlateGray")>  _
    Public ReadOnly Property NoEditForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("NoEditForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("250, 250, 250")>  _
    Public ReadOnly Property ReadOnlyBorderColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ReadOnlyBorderColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("[赤]")>  _
    Public ReadOnly Property NegativeValueFormat() As String
        Get
            Return CType(Me("NegativeValueFormat"),String)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("178, 24, 43")>  _
    Public ReadOnly Property NegativeValueForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("NegativeValueForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("239, 239, 239")>  _
    Public ReadOnly Property ClosedBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ClosedBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("250, 250, 250")>  _
    Public ReadOnly Property ClosedBorderColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ClosedBorderColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("LightSlateGray")>  _
    Public ReadOnly Property ClosedForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("ClosedForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("33, 33, 33")>  _
    Public ReadOnly Property NormalForeColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("NormalForeColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("White")>  _
    Public ReadOnly Property NormalBackColor() As Global.System.Drawing.Color
        Get
            Return CType(Me("NormalBackColor"),Global.System.Drawing.Color)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("Meiryo UI, 9pt, style=Bold")>  _
    Public ReadOnly Property ModifyFont() As Global.System.Drawing.Font
        Get
            Return CType(Me("ModifyFont"),Global.System.Drawing.Font)
        End Get
    End Property
    
    <Global.System.Configuration.ApplicationScopedSettingAttribute(),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Configuration.DefaultSettingValueAttribute("この行は変更されています")>  _
    Public ReadOnly Property ModifyToolTipText() As String
        Get
            Return CType(Me("ModifyToolTipText"),String)
        End Get
    End Property
End Class
