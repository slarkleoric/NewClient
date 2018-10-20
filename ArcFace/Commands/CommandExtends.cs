using ArcFaceClient.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace ArcFaceClient.Commands
{
    public static class CommandExtends
    {
        /// <summary>
        /// 绑定命令和命令事件到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            ui.CommandBindings.Add(bind);
        }

        /// <summary> 设置ViewModel </summary>
        /// <typeparam name="T"></typeparam>
        public static void InitModel<T>(this FrameworkElement control, T model)
            where T : VBase
        {
            model.Element = control;
            control.DataContext = model;
        }
    }
}
