using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core
{
    public class GlobalKeys
    {
        /// <summary> 数据库版本 </summary>
        public const string DbVersion = "db_version";
      
        public const string Host = "rest_host";

        /// <summary> 数据模式：0,在线;1,离线 </summary>
        public const string DataMode = "data_mode";
        /// <summary> 登录凭证 </summary>
        public const string AccessToken = "access_token";

        public const string ClientSecret = "client_secret";
        public const string Menus = "huiwu_menus";
        public const string LoginUser = "login_user";
        public const string MeetingData = "meeting_data";
        /// <summary> 手动开启 </summary>
        public const string ManualOpening = "manual_opening_{0}";
        /// <summary> 机器码 </summary>
        public const string MachineCode = "machine_code";
        /// <summary> 会议签到码前缀 </summary>
        public const string MeetingSignPrefix = "meeting_sign_prefix";

        public const string VersionUpdateSql = "version_update_sql";

        public const string ApiUserInfo = "api/v1/user/info";
        public const string ApiManifest = "api/v1/common/manifest";
        public const string ApiMeetings = "api/v1/user/meeting/list";
        /// <summary> 拉取数据接口 </summary>
        public const string ApiSynchPull = "api/v1/meeting/{0}/sync/pull";
        /// <summary> 上传数据接口 </summary>
        public const string ApiSynchPush = "api/v1/meeting/{0}/sync/push";
        /// <summary> 注销登录 </summary>
        public const string ApiLogout = "api/v1/account/logout";
        /// <summary> 报名二维码api/v1/meeting/{meetingId}/enrollfrom/qrs?name={name}</summary>
        public const string ApiQrCode = "api/v1/meeting/{0}/qr_code/list";
        /// <summary> 签到统计 </summary>
        public const string ApiSignInStatis = "api/v1/user/meeting/{0}/statistics";
        /// <summary> 签到账号分配 </summary>
        public const string ApiMeetingSigner = "api/v1/meeting/{0}/signer/list";
        /// <summary> 签到前缀 </summary>
        public const string ApiMeetingSignPrefix = "api/v1/meeting/{0}/signer/code/{1}";
        /// <summary> 签到接口 </summary>
        public const string ApiEnrollSign = "api/v1/meeting/{0}/enroll/{1}/sign";

        /// <summary> 刷新列表消息Token </summary>
        public const string MtRefreshList = "refresh_list";
        /// <summary> 关闭扩展屏Token </summary>
        public const string MtCloseExpansion = "close_expansion";
        /// <summary> 关闭弹窗 </summary>
        public const string MtCloseDialog = "close_dialog";
        /// <summary> 签到Token </summary>
        public const string MtSignIn = "signIn";
        /// <summary> 打印并签到Token </summary>
        public const string MtPrintAndSignIn = "printAndSignIn";

        public const string MtApply = "apply";
        /// <summary>  高级搜索 </summary>
        public const string MtAdvancedSearch = "advanced_search";
        ///// <summary> 刷新同步数量 </summary>
        //public const string MtRefreshSynchCount = "refresh_synch_count";
     
    }
}
