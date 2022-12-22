﻿#pragma checksum "..\..\..\Preferences.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "031E7786BA1B618E10D653DFD76A967B0815FB37"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ModernWpf;
using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.DesignTime;
using ModernWpf.Markup;
using ModernWpf.Media.Animation;
using Stock_Market_App;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Stock_Market_App {
    
    
    /// <summary>
    /// Preferences
    /// </summary>
    public partial class Preferences : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 119 "..\..\..\Preferences.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox themeColorInputBox;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\Preferences.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox baseColorInputBox;
        
        #line default
        #line hidden
        
        
        #line 125 "..\..\..\Preferences.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox backgroundColorInputBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Stock Market App;component/preferences.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Preferences.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.14.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.themeColorInputBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 119 "..\..\..\Preferences.xaml"
            this.themeColorInputBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.themeColorInputBoxEnterEvent);
            
            #line default
            #line hidden
            return;
            case 2:
            this.baseColorInputBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 122 "..\..\..\Preferences.xaml"
            this.baseColorInputBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.baseColorInputBoxEnterEvent);
            
            #line default
            #line hidden
            return;
            case 3:
            this.backgroundColorInputBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 125 "..\..\..\Preferences.xaml"
            this.backgroundColorInputBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.backgroundColorInputBoxEnterEvent);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

