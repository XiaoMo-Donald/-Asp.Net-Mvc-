using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Second.Website.Model
{
    public partial class HomeCor
    {
        /// <summary>
        /// 编号ID
        /// </summary>
        [Key]
        public int IndexID { get; set; }
        /// <summary>
        /// 网站标题
        /// </summary>
        [StringLength(20)]
        public string IndexTitle { get; set; }
        /// <summary>
        /// 公司Logo
        /// </summary>
        public string Companylogo { get; set; }
        /// <summary>
        /// 公司文化
        /// </summary>
        [StringLength(200)]
        public string CompanyCulture { get; set; }
        /// <summary>
        /// 公司地址
        /// </summary>
        [StringLength(50)]
        public string CompanyAddress { get; set; }
        /// <summary>
        /// 公司路线
        /// </summary>
        [StringLength(30)]
        public string CompanyRoute { get; set; }
        /// <summary>
        /// 网站版权
        /// </summary>
        [StringLength(30)]
        public string CompanyCopyright { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [StringLength(12)]
        public string Telephone { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [StringLength(50)]
        public string Email { get; set; }

    }
}
