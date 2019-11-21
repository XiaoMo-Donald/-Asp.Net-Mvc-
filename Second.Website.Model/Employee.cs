using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Second.Website.Model
{
    public partial class Employee
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        [Key]
        public int EmpoloyeeID { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpoloyeeName { get; set; }
        /// <summary>
        /// 员工职称
        /// </summary>
        public string EmpoloyeePost { get; set; }
        /// <summary>
        /// 员工信息
        /// </summary>
        public string EmpoloyeeInfo { get; set; }
        /// <summary>
        /// 员工照片
        /// </summary>
        public string EmpoloyeePhoto { get; set; }
    }
}
