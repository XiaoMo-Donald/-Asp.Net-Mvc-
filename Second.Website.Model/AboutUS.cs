using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Second.Website.Model
{
   public class AboutUS
    {
        [Key]
        public int AboutID { get; set; }
        /// <summary>
        /// 关于我们标题
        /// </summary>
        public string AboutTitle { get; set; }
        /// <summary>
        /// 关于我们内容
        /// </summary>
        public string AboutContent { get; set; }
        /// <summary>
        /// 关于我们更多内容（超链接）
        /// </summary>
        public string AboutUsMoreLink { get; set; }
    }
}
