using ArcFace.Core;
using ArcFace.Core.AppService;
using ArcFace.Core.Dtos;
using ArcFaceClient.Commands;
using ArcFaceClient.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ArcFaceClient.ViewModel
{
    public class VLogin : VBase
    {
        public ICommand LoginCommand
        {
            get
            {

                return new Commands.RelayCommand(Login, Logincheck);
            }
        }
        
        #region 账号和密码

        private string _account = string.Empty;
        public string Account
        {
            get => _account;
            set
            {
                _account = value;
                OnPropertyChanged(() => Account);
            }
        }
        private string _pzw = string.Empty;
        public string Pzw
        {
            get => _pzw;
            set
            {
                _pzw = value;
                OnPropertyChanged(() => Pzw);
                if (string.IsNullOrEmpty(value))
                    AdminDataService.Instance.InsertOrUpdate(GlobalKeys.LoginAccount, new AccountInfo() { Account = Account });
            }
        }
        private bool _issave;
        public bool IsSave
        {
            get => _issave;
            set
            {
                _issave = value;
                OnPropertyChanged(() => IsSave);
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
                OnPropertyChanged(() => ErrorInfo);
            }
        }

        private Visibility _errorVisible = Visibility.Hidden;

        public Visibility ErrorVisible
        {
            get => _errorVisible;
            set
            {
                _errorVisible = value;
                OnPropertyChanged(() => ErrorVisible);
            }
        }

        #endregion

        public VLogin()
        {
            //登录
            //LoginCommand = new RelayCommand(Login, Logincheck,true);
            var info = AdminDataService.Instance.Query<AccountInfo>(GlobalKeys.LoginAccount) ?? new AccountInfo();
            Account = info.Account;

            if ((DateTime.Now - info.LoginTime)?.Days < 7)
                Pzw = info.PassWord;
            if (!string.IsNullOrEmpty(Pzw))
            {
                IsSave = true;
            }
        }

        #region 登录
        private void Login()
        {
            ResetError();

            //默认账号
            if(Account == "admin" && Pzw == "123456")
            {
                var ac = new AccountInfo
                {
                    Account = Account,
                    LoginTime = DateTime.Now
                };
                App.CurrentUser = ac;

                var win = new ActivityView();
                LocalSysCmds.OpenWindow(win);
            }

            var user = UserAppService.Instance.Login(Account.Trim().ToLower(), Pzw.Trim().ToLower());

            if (user != null)
            {
                var ac = new AccountInfo
                {
                    Account = user.account,
                    LoginTime = DateTime.Now
                };

                //保存登信息
                if (IsSave) ac.PassWord = user.password;
                AdminDataService.Instance.InsertOrUpdate(GlobalKeys.LoginAccount, ac);


                App.CurrentUser = ac;

                var win = new ActivityView();
                LocalSysCmds.OpenWindow(win);
            }
            else
            {
                //没有成功，保存登录名
                AdminDataService.Instance.InsertOrUpdate(GlobalKeys.LoginAccount, new AccountInfo() { Account = Account });
                ErrorVisible = Visibility.Visible;
                ErrorInfo = "用户名密码错误！";
            }
        }

        private bool Logincheck()
        {
            //var ele = Element as LoginView;
            return !string.IsNullOrWhiteSpace(Account)  ;
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
