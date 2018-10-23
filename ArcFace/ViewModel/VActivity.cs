
using ArcFaceClient.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ArcFaceClient.ViewModel
{
   public class VActivity : VBase
    {
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login, () =>{
                    return Account == "1";
                });
            }
        }

        private bool Logincheck()
        {
            return Account=="1";
        }

        private void Login()
        {
            
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
        #endregion
    }
}
