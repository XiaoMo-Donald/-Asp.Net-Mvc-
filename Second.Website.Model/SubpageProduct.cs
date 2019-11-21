using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Second.Website.Model
{
    public partial class SubpageProduct
    {
        /// <summary>
        /// 产品ID
        /// </summary>
        [Key]
        public int ProductID { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        [StringLength(8)]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品信息
        /// </summary>
        [StringLength(30)]
        public string ProductInfo { get; set; }
        /// <summary>
        /// 产品日期
        /// </summary>
        public DateTime ProductTime { get; set; }
        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductPhotoFile { get; set; }
        /// <summary>
        /// 分类ID 1电脑 2手机 3科技
        /// </summary>
        public int ClassID { get; set; }
        /// <summary>
        /// 特色分类 0是 1否
        /// </summary>
        public int Features { get; set; }
        /// <summary>
        /// 下载链接（外链） 电脑
        /// </summary>
        public string PcDownLink { get; set; }
        /// <summary>
        /// 下载链接（外链） 安卓
        /// </summary>
        public string ardDownLink { get; set; }
        /// <summary>
        /// 下载链接（外链） 苹果
        /// </summary>
        public string iosDownLink { get; set; }
        /// <summary>
        /// 购买链接
        /// </summary>
        public string BuyLink { get; set; }
    }
}
