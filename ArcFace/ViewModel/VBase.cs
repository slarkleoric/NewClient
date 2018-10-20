using ArcFaceClient.Commands;
using ArcFaceClient.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;

namespace ArcFaceClient.ViewModel
{
    public abstract class VBase : ViewModelBase
    {
        

        public FrameworkElement Element { get; set; }

     

        protected VBase() { }

        protected VBase(IMessenger messenger)
        {
            MessengerInstance = messenger;
        }

        protected void Jump(string uri, int menudId = 0)
        {
            LocalSysCmds.JumpPage(uri, menudId);
        }

        protected void Jump(XPage page, int menudId = 0)
        {
            LocalSysCmds.JumpPage(page, menudId);
        }

        protected void Refresh()
        {
            LocalSysCmds.Refresh();
        }

        public void UiInvoke(Action action)
        {
            Element.Dispatcher.Invoke(action);
        }

        protected void ShowDialog<T>()
            where T : XDialog, new()
        {
            new T().Show();
        }
    }
}
