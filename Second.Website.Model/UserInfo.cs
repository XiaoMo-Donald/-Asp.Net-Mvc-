using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Second.Website.Model
{
    public partial class UserInfo
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        [Key, Column(Order = 0)]
        public string UserID { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Key, Column(Order = 1)]
        [StringLength(11)]
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [StringLength(16)]
        public string UserPassword { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime SubTime { get; set; }
        /// <summary>
        /// 管理员邮箱
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string ImageData { get; set; }
       
        /// <summary>
        /// 是否为超级管理员 1 是 0否
        /// </summary>
        public int isAdmin {get; set;  }
    }
}
