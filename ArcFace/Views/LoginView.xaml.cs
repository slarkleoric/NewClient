﻿using ArcFaceClient.Commands;
using ArcFaceClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ArcFaceClient.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView 
    {
        public LoginView()
        {
            InitializeComponent();
            this.InitModel(new VLogin());
        }

        private void AccountChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
