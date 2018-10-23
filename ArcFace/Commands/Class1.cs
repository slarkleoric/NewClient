﻿using ArcFace.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArcFaceClient.Commands
{
    /// <summary>
    /// 广播命令：基本ICommand实现接口
    /// </summary>
    public class RelayCommand : ICommand
    {
        public Action ExecuteCommand { get; }
        public Func<bool> CanExecuteCommand { get; }

        public RelayCommand(Action executeCommand, Func<bool> canExecuteCommand)
        {
            this.ExecuteCommand = executeCommand;
            this.CanExecuteCommand = canExecuteCommand;
        }

        public RelayCommand(Action executeCommand)
            : this(executeCommand, null) { }

        /// <summary>
        /// 定义在调用此命令时调用的方法。
        /// </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            ExecuteCommand?.Invoke();
        }

        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand == null || CanExecuteCommand();
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
            remove { if (CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary> 广播命令：基本ICommand实现接口，带参数 </summary>
    public class RelayCommand<T> : ICommand
    {
        public Action<T> ExecuteCommand { get; }

        public Predicate<T> CanExecuteCommand { get; }

        public RelayCommand(Action<T> executeCommand, Predicate<T> canExecuteCommand)
        {
            this.ExecuteCommand = executeCommand;
            this.CanExecuteCommand = canExecuteCommand;
        }

        public RelayCommand(Action<T> executeCommand)
            : this(executeCommand, null) { }

        /// <summary> 定义在调用此命令时调用的方法。 </summary>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public void Execute(object parameter)
        {
            ExecuteCommand?.Invoke(parameter.CastTo<T>());
        }

        /// <summary>
        /// 定义用于确定此命令是否可以在其当前状态下执行的方法。
        /// </summary>
        /// <returns>
        /// 如果可以执行此命令，则为 true；否则为 false。
        /// </returns>
        /// <param name="parameter">此命令使用的数据。如果此命令不需要传递数据，则该对象可以设置为 null。</param>
        public bool CanExecute(object parameter)
        {
            return CanExecuteCommand == null || CanExecuteCommand(parameter.CastTo<T>());
        }

        public event EventHandler CanExecuteChanged
        {
            add { if (CanExecuteCommand != null) CommandManager.RequerySuggested += value; }
            remove { if (CanExecuteCommand != null) CommandManager.RequerySuggested -= value; }
        }
    }
}
