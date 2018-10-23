using ArcFace.Core.Messaging;
using ArcFaceClient.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArcFaceClient.Controls
{
    public class XDialog : XWindow
    {
        private bool _dialogResult;
        public XDialog() : base(true, "X-Dialog")
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            MaxHeight = SystemParameters.WorkArea.Height - 10;
            
            Messenger.Default.Register<bool>(this, result =>
            {
                _dialogResult = result;
                Close();
            }, LocalKeys.MtCloseDialog);
        }

        public new bool? Show(Window owner = null)
        {
            try
            {
                var win = (owner ?? Application.Current.MainWindow) as XWindow;
                if (win == null || Equals(win, this))
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    return ShowDialog();
                }
                WindowStartupLocation = WindowStartupLocation.CenterOwner;
                win.DialogCount++;
                Owner = win;
                win.ShowMask = true;

                return ShowDialog();
            }
            catch
            {
                return null;
            }
        }

        public static void CloseDialog(bool result = false)
        {
            Messenger.Default.Notify<bool, XDialog>(result, LocalKeys.MtCloseDialog);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (IsActive)
                DialogResult = _dialogResult;
            base.OnClosing(e);
        }

        protected override void OnClosed(EventArgs e)
        {
            var win = Owner as XWindow;
            if (win != null)
            {
                win.DialogCount--;
                if (win.DialogCount <= 0)
                    win.ShowMask = false;
            }
            Messenger.Default.Unregister(this);
            base.OnClosed(e);
        }
    }
}
