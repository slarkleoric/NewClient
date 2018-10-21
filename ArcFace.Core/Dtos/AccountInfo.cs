using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcFace.Core.Dtos
{
    public class AccountInfo 
    {
        public string Account { get; set; }
        public string PassWord { get; set; }
        public DateTime? LoginTime { get; set; }
    }

    /// <summary>
    /// 账号信息
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary> 账号 </summary>
        public string Account_Name { get; set; }
        /// <summary> 手机号 </summary>
        public string Phone { get; set; }
        /// <summary> 邮箱 </summary>
        public string Email { get; set; }
        /// <summary> 姓名 </summary>
        public string Name { get; set; }
        /// <summary> 状态 1使用中 2停用 3删除 </summary>
        public int State { get; set; }
        /// <summary> 角色名称 </summary>
        public string Role_Name { get; set; }

        /// <summary> 角色类型 </summary>
        public int Role_Type { get; set; }
        /// <summary> 创建时间 </summary>
        public DateTime Create_Date { get; set; }
        /// <summary> 最后一次登录时间 </summary>
        public DateTime? Login_Date { get; set; }
        /// <summary> 用户等级id </summary>
        public int Level_Id { get; set; }
        /// <summary> 优惠券数量 </summary>
        public int Coupon_Count { get; set; }
        /// <summary> 性别 </summary>
        public int Sex { get; set; }
        /// <summary> 行业 </summary>
        public string Indestry { get; set; }
        /// <summary> 公司名称 </summary>
        public string Company { get; set; }
        /// <summary> 公司电话 </summary>
        public string Company_Phone { get; set; }
        /// <summary> qq </summary>
        public string QQ { get; set; }
        /// <summary> 微信 </summary>
        public string Weixin { get; set; }
        /// <summary> 地址 </summary>
        public string Address { get; set; }
        /// <summary> 签到员签到编号 </summary>
        public string Signer_Code { get; set; }

        public DateTime? SignerBeginTime { get; set; }
        public DateTime? SignerEndTime { get; set; }

        private string _groupname;

        public string GroupName
        {
            get
            {
                if (_groupname + "" == "")
                    return "全部组";
                else
                    return _groupname;
            }
            set { _groupname = value; }
        }

        public string Signer_Drop_Name { get; set; }
    }
}
