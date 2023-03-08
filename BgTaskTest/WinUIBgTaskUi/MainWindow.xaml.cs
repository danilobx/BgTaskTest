// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WinUIBgTaskUi
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private string _exampleTaskName = "ToastBgTask";
        
        public MainWindow()
        {
            InitializeComponent();
            LoadTasks();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var builder = new BackgroundTaskBuilder
            {
                Name = _exampleTaskName,
                TaskEntryPoint = "BgTaskComponent.ToastBgTask"
            };
            builder.SetTrigger(new SystemTrigger(SystemTriggerType.TimeZoneChange, false));
            builder.Register();
            LoadTasks();
        }

        private void UnregisterButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var task in BackgroundTaskRegistration.AllTasks)
            {
                if (task.Value.Name == _exampleTaskName)
                {
                    task.Value.Unregister(true);
                }
            }
            LoadTasks();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void LoadTasks()
        {
            var registeredTasks = BackgroundTaskRegistration.AllTasks;

            if (registeredTasks.Count == 0)
            {
                //InfoBGTask.Text = "No BG Tasks Registered";
                RegisterButton.IsEnabled = true;
                UnregisterButton.IsEnabled = false;
                return;
            }
            RegisterButton.IsEnabled = false;
            UnregisterButton.IsEnabled = true;
            //InfoBGTask.Text = "Bg Task Registered: " + _exampleTaskName;
        }
    }
}
