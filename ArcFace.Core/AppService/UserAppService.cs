using ArcFace.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.AppService
{
    public class UserAppService : BaseService
    {
        public static AccountInfo GetAccountInfo()
        {
            var info = AdminDataService.Instance.Query<AccountInfo>(GlobalKeys.LoginAccount) ?? new AccountInfo();
            return info;
        }

        public static void UpdateAccountInfo(AccountInfo accountInfo)
        {
            AdminDataService.Instance.InsertOrUpdate(GlobalKeys.LoginAccount, accountInfo);
        }
    }
}
