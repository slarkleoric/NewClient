using ArcFaceClient.Commands;
using ArcFaceClient.Controls;
using ArcFaceClient.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace ArcFaceClient.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView 
    {
        private VLogin _model;


        public LoginView()
        {
            InitializeComponent();

            _model = new VLogin();
            this.InitModel(_model);
        }

        private void AccountChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            _model.ResetError();

            if (!(sender is PasswordBox box))
                return;
            var pwd = AttachProperty.GetPassword(box);
            if (pwd != box.Password)
                AttachProperty.SetPassword(box, box.Password);
        }
    }
}
