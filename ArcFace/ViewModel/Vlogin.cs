using ArcFace.Core;
using ArcFace.Core.AppService;
using ArcFace.Core.Dtos;
using ArcFaceClient.Commands;
using ArcFaceClient.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArcFaceClient.ViewModel
{
    public class VLogin : VBase
    {
        public ICommand LoginCommand { get; }

        #region 账号和密码

        private string _account = string.Empty;
        public string Account
        {
            get => _account;
            set
            {
                _account = value;
                RaisePropertyChanged(() => Account);
            }
        }
        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
                if (string.IsNullOrEmpty(value))
                    UserAppService.UpdateAccountInfo(new AccountInfo() { Account = Account }); //密码更改时重新保存账号
            }
        }
        private bool _issave;
        public bool IsSave
        {
            get => _issave;
            set
            {
                _issave = value;
                RaisePropertyChanged(() => IsSave);
            }
        }
        #endregion

        #region 错误信息

        private string _errorinfo = string.Empty;
        public string ErrorInfo
        {
            get => _errorinfo;
            set
            {
                _errorinfo = value;
                RaisePropertyChanged(() => ErrorInfo);
            }
        }

        private Visibility _errorVisible = Visibility.Hidden;

        public Visibility ErrorVisible
        {
            get => _errorVisible;
            set
            {
                _errorVisible = value;
                RaisePropertyChanged(() => ErrorVisible);
            }
        }

        #endregion

        public VLogin()
        {
            //登录
            LoginCommand = new RelayCommand(Login, Logincheck);
            var info = UserAppService.GetAccountInfo();
            Account = info.Account;

            if ((DateTime.Now - info.LoginTime)?.Days < 7)
                Password = info.PassWord;
            if (!string.IsNullOrEmpty(Password))
            {
                IsSave = true;
            }
        }

        #region 登录
        private void Login()
        {
            ResetError();
            

            //NewRestHelper.Instance.Login(Account, Password, ref cookie);
            if (true)
            {
                var ac = new AccountInfo
                {
                    Account = Account,
                    LoginTime = DateTime.Now
                };

                //保存登信息
                if (IsSave) ac.PassWord = Password;
                UserAppService.UpdateAccountInfo(ac);

                //获取UserInfo
                var user = new UserInfoDto();  //NewRestHelper.Instance.UserInfo();

                if (user == null)
                {
                    ErrorVisible = Visibility.Visible;
                    ErrorInfo = "";// result.Message;

                    Const.DefaultLogger.Error("Login成功，获取用户信息失败！");
                }
                else
                {
                    user.Login_Date = DateTime.Now;
                    App.CurrentUser = user;
                    var win = new ActivityView();
                    LocalSysCmds.OpenWindow(win);
                }
            }
            else
            {
                //没有成功，保存登录名
                UserAppService.UpdateAccountInfo(new AccountInfo() { Account = Account });
                ErrorVisible = Visibility.Visible;
                //ErrorInfo = result.Message;
            }
        }

        private bool Logincheck()
        {
            var ele = Element as LoginView;
            return !string.IsNullOrWhiteSpace(ele?.Account.Text) && !Validation.GetHasError(ele.Account) &&
                   !string.IsNullOrWhiteSpace(ele.Password.Password) && !Validation.GetHasError(ele.Password);
        }

        #endregion

        #region 清空错误提示
        public void ResetError()
        {
            ErrorVisible = Visibility.Hidden;
            ErrorInfo = string.Empty;
        }
        #endregion
    }
}
