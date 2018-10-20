using ArcFaceClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ArcFaceClient.Controls
{
    public class XPage : Page
    {
        public XPage()
        {
            Unloaded += (sender, e) =>
            {
                (DataContext as VBase)?.Cleanup();
            };
        }
    }
}
