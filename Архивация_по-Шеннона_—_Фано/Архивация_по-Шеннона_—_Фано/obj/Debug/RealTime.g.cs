﻿#pragma checksum "..\..\RealTime.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C0EC84136C2297C5A46925EEEF9D27CD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
using Архивация_по_Шеннона___Фано;


namespace Архивация_по_Шеннона___Фано {
    
    
    /// <summary>
    /// RealTime
    /// </summary>
    public partial class RealTime : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 93 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainGrid_RealTime;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgClose;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgMinimized;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_input;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txt_output;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\RealTime.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTime;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Архивация_по-Шеннона_—_Фано;component/realtime.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RealTime.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.MainGrid_RealTime = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 101 "..\..\RealTime.xaml"
            ((System.Windows.Shapes.Rectangle)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Rectangle_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.imgClose = ((System.Windows.Controls.Image)(target));
            
            #line 105 "..\..\RealTime.xaml"
            this.imgClose.MouseMove += new System.Windows.Input.MouseEventHandler(this.imgClose_MouseMove);
            
            #line default
            #line hidden
            
            #line 106 "..\..\RealTime.xaml"
            this.imgClose.MouseLeave += new System.Windows.Input.MouseEventHandler(this.imgClose_MouseLeave);
            
            #line default
            #line hidden
            
            #line 107 "..\..\RealTime.xaml"
            this.imgClose.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgClose_MouseUp);
            
            #line default
            #line hidden
            return;
            case 4:
            this.imgMinimized = ((System.Windows.Controls.Image)(target));
            
            #line 114 "..\..\RealTime.xaml"
            this.imgMinimized.MouseMove += new System.Windows.Input.MouseEventHandler(this.imgMinimize_MouseMove);
            
            #line default
            #line hidden
            
            #line 115 "..\..\RealTime.xaml"
            this.imgMinimized.MouseLeave += new System.Windows.Input.MouseEventHandler(this.imgMinimize_MouseLeave);
            
            #line default
            #line hidden
            
            #line 116 "..\..\RealTime.xaml"
            this.imgMinimized.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.imgMinimize_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txt_input = ((System.Windows.Controls.TextBox)(target));
            
            #line 128 "..\..\RealTime.xaml"
            this.txt_input.KeyDown += new System.Windows.Input.KeyEventHandler(this.txt_input_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.txt_output = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            
            #line 141 "..\..\RealTime.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MenuItem_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lblTime = ((System.Windows.Controls.Label)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

