﻿#pragma checksum "..\..\ReportKitsMovingWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1084AA23D2739CD1FC777C597F4F6780B3707A49E22EB343F2B0F17FEA5BF65E"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CarCenter;
using CarCenter.utils;
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


namespace CarCenter {
    
    
    /// <summary>
    /// ReportKitsMovingWindow
    /// </summary>
    public partial class ReportKitsMovingWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\ReportKitsMovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerFrom;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ReportKitsMovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DatePickerTo;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\ReportKitsMovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonMakeReport;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ReportKitsMovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSendPdf;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\ReportKitsMovingWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Documents.TableRowGroup TableRowGroup;
        
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
            System.Uri resourceLocater = new System.Uri("/Автоцентр Корыто. Учет комплектаций;component/reportkitsmovingwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ReportKitsMovingWindow.xaml"
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
            this.DatePickerFrom = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 2:
            this.DatePickerTo = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.ButtonMakeReport = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\ReportKitsMovingWindow.xaml"
            this.ButtonMakeReport.Click += new System.Windows.RoutedEventHandler(this.ButtonMakeReport_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonSendPdf = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\ReportKitsMovingWindow.xaml"
            this.ButtonSendPdf.Click += new System.Windows.RoutedEventHandler(this.ButtonSendPdf_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TableRowGroup = ((System.Windows.Documents.TableRowGroup)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
