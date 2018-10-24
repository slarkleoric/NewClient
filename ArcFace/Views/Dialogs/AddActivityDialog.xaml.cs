using ArcFaceClient.Commands;
using ArcFaceClient.ViewModel.Dialogs;
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

namespace ArcFaceClient.Views.Dialogs
{
    /// <summary>
    /// AddActivityDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddActivityDialog 
    {
        public AddActivityDialog(string id = null)
        {
            InitializeComponent();
            this.InitModel(new VAddActivity(id));
        }
    }
}
