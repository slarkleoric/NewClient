using ArcFace.Core;
using ArcFace.Core.Messaging;
using ArcFaceClient.Commands;
using ArcFaceClient.Controls;
using System;
using System.Windows;

namespace ArcFaceClient.ViewModel
{
    public abstract class VBase : KNotifyPropertyChanged
    {
        public FrameworkElement Element { get; set; }

        private IMessenger _messengerInstance;
        public IMessenger MessengerInstance
        {
            get => _messengerInstance ?? Messenger.Default;
            set => _messengerInstance = value;
        }
        
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

        public virtual void Cleanup()
        {
            MessengerInstance.Unregister(this);
        }
    }
}
