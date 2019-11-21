using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Second.Website.Model;
using Second.Website.Common;
using Second.Website.IDAL;
using Second.Website.DAL;
using System.IO;

namespace Second.Website.WebApp.Controllers
{
    public class AdminController : Controller
    {
        /// <summary>
        /// 数据库操作访问类
        /// </summary>
        DBDal db = new DBDal();
        /// <summary>
        /// 后台管理员用户的数据访问层
        /// </summary>
        IUserInfoDal iUserDal = new UserInfoDal();
        IHomeCorDal iHomeDal = new HomeCorDal();
        IAboutUSDal iAboutDal = new AboutUSDal();
        IEmployeeDal iEmpDal = new EmployeeDal();
        ISubpageProductDal iSubDal = new SubpageProductDal();

        // GET: Admin

        public ActionResult Index()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        #region 文件上传方法
        /// <summary>
        /// 上传文件到服务器
        /// </summary>
        /// <param name="fileUp"></param>
        /// <returns></returns>
        public string UpFlie(HttpPostedFileBase fileUp)
        {
            if (fileUp == null || fileUp.ContentLength <= 0)
            {
                return null;
            }
            string extName = Path.GetExtension(fileUp.FileName);
            string[] extfile = { ".jpg", ".png" };
            if (!extfile.Contains(extName))
            {
                return null;
            }
            string fileSavaPath = Server.MapPath("/Content/UpImagesFile");
            string newName = Guid.NewGuid().ToString() + extName;
            string newSave = "/Content/UpImagesFile/" + newName;
            fileUp.SaveAs(Path.Combine(fileSavaPath, newName));
            return newSave;
        }

        /// <summary>
        /// 上传文件到服务器(上传LOGO)
        /// </summary>
        /// <param name="fileUp"></param>
        /// <returns></returns>
        public string UpLogo(HttpPostedFileBase logoUp)
        {
            if (logoUp == null || logoUp.ContentLength <= 0)
            {
                return null;
            }
            string extName = Path.GetExtension(logoUp.FileName);
            string[] extfile = { ".jpg", ".png" };
            if (!extfile.Contains(extName))
            {
                return null;
            }
            string fileSavaPath = Server.MapPath("/Content/UpImagesFile");
            string newName = "logo" + extName;
            string newSave = "/Content/UpImagesFile/" + newName;
            logoUp.SaveAs(Path.Combine(fileSavaPath, newName));
            return newSave;
        }
        #endregion

        #region 给网站添加默认数据  

        /// <summary>
        /// 给网站添加默认的演示数据
        /// </summary>
        /// <param name="homeCor"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public ActionResult AddDefaultDemoData()
        {
            HomeCor boolHomeCor = iHomeDal.LoadEntity(h => true).FirstOrDefault();
            Employee boolEmp = iEmpDal.LoadEntity(e => true).FirstOrDefault();
            SubpageProduct boolProduct = iSubDal.LoadEntity(e => true).FirstOrDefault();
            if (boolHomeCor != null && boolEmp != null && boolProduct != null)
            {
                return Content("<script >alert('网站默认演示数据已经存在，请不要重复添加！');history.go(-1);</script >", "text/html");
            }
            HomeCor homeCorDemo = new HomeCor();
            homeCorDemo.IndexTitle = "首页标题 - 基于Asp.Net 三层架构官网";
            homeCorDemo.Companylogo = "/Content/Second/images/logo.png";
            homeCorDemo.CompanyCulture = "——合作，团结 · 发展，交流，共赢——轻改变·大体验";
            homeCorDemo.CompanyAddress = "广西柳州职业技术学院图书馆";
            homeCorDemo.CompanyRoute = "火车站乘坐快9、12路等均可抵达";
            homeCorDemo.Telephone = "07716666666";
            homeCorDemo.Email = "Studio@925i.cn";
            homeCorDemo.CompanyCopyright = "基于Asp.Net 三层架构官网";
            iHomeDal.AddEntity(homeCorDemo);

            AboutUS aboutUs = new AboutUS();
            aboutUs.AboutTitle = "基于Asp.Net 三层架构官网 - 柳州职业技术学院";
            aboutUs.AboutContent = "基于Asp.Net 三层架构官网由柳州职业技术学院电子信息工程学院软件技术2班学生Evan同学开发， 于2016年11月是以Web前端为导向，以学习资源为依托，以合作为基本工作形式，以效益最大化和服务于全盟经济社会发展为根本目的，集没有投资、没有融资、 没有资产管理、没有资本运营、只有学习资源和项目开发于一体的综合型平台。个人经过一个月的实践探索，加强监管力度……";
            aboutUs.AboutUsMoreLink = "http://www.925i.cn/AboutUsMore";
            iAboutDal.AddEntity(aboutUs);

            for (int i = 1; i < 7; i++)
            {
                Employee employeeDemo = new Employee();
                employeeDemo.EmpoloyeeName = "我是员工姓名" + i.ToString();
                employeeDemo.EmpoloyeePost = "我是员工岗位" + i.ToString();
                if (i >= 4)
                {
                    employeeDemo.EmpoloyeePhoto = "/Content/Second/images/ICON/default5.png";
                }
                else
                {
                    employeeDemo.EmpoloyeePhoto = "/Content/Second/images/ICON/default" + i + ".png";
                }

                employeeDemo.EmpoloyeeInfo = "这里显示的是员工的简介（演示）" + i.ToString();
                iEmpDal.AddEntity(employeeDemo);
            }
            for (int i = 1; i < 4; i++)
            {
                SubpageProduct subProutDemo = new SubpageProduct();
                for (int j = 1; j < 7; j++)
                {
                    if (i == 1)
                    {
                        subProutDemo.ProductName = "电脑产品名称" + j.ToString();
                        subProutDemo.ProductInfo = "电脑产品信息(演示)" + j.ToString();
                        subProutDemo.ProductPhotoFile = "/Content/Second/images/Icon/PcSoftware" + j + ".png";
                        subProutDemo.ProductTime = DateTime.Now;
                        subProutDemo.ClassID = i;
                        if (j < 3)
                        {
                            subProutDemo.Features = 1;
                        }
                        else
                        {
                            subProutDemo.Features = 0;
                        }
                        subProutDemo.PcDownLink = "http://www.925i.cn/downlod/" + j + ".zip";
                        subProutDemo.ardDownLink = "#";
                        subProutDemo.iosDownLink = "#";
                        subProutDemo.BuyLink = "http://www.925i.cn/buy/" + j + ".html";
                        iSubDal.AddEntity(subProutDemo);
                    }
                    else if (i == 2)
                    {
                        subProutDemo.ProductName = "手机产品名称" + j.ToString();
                        subProutDemo.ProductInfo = "手机产品信息(演示)" + j.ToString();
                        subProutDemo.ProductPhotoFile = "/Content/Second/images/Icon/PhoneSoftware" + j + ".png";
                        subProutDemo.ProductTime = DateTime.Now;
                        subProutDemo.ClassID = i;
                        if (j < 3)
                        {
                            subProutDemo.Features = 1;
                        }
                        else
                        {
                            subProutDemo.Features = 0;
                        }
                        subProutDemo.PcDownLink = "http://www.925i.cn/downlod/" + j + ".zip";
                        subProutDemo.ardDownLink = "http://www.925i.cn/downlod/" + j + ".apk";
                        subProutDemo.iosDownLink = "http://www.925i.cn/downlod/" + j + ".ipa";
                        subProutDemo.BuyLink = "http://www.925i.cn/buy/" + j + ".html";
                        iSubDal.AddEntity(subProutDemo);
                    }

                    else if (i == 3)
                    {
                        subProutDemo.ProductName = "科技商品名称" + j.ToString();
                        subProutDemo.ProductInfo = "科技商品信息(演示)" + j.ToString();
                        subProutDemo.ProductPhotoFile = "/Content/Second/images/Icon/productDemo" + j + ".gif";
                        subProutDemo.ProductTime = DateTime.Now;
                        subProutDemo.ClassID = i;
                        if (j < 3)
                        {
                            subProutDemo.Features = 1;
                        }
                        else
                        {
                            subProutDemo.Features = 0;
                        }
                        subProutDemo.PcDownLink = "http://www.925i.cn/downlod/" + j + ".zip";
                        subProutDemo.ardDownLink = "http://www.925i.cn/downlod/" + j + ".apk";
                        subProutDemo.iosDownLink = "http://www.925i.cn/downlod/" + j + ".ipa";
                        subProutDemo.BuyLink = "http://www.925i.cn/buy/" + j + ".html";
                        iSubDal.AddEntity(subProutDemo);

                    }
                }
            }

            return Content("<script >alert('网站默认演示数据添加成功，现在去登录后台进行管理吧！');window.location.href = 'Login';</script >", "text/html");

        }

        #endregion

        #region 后台管理员的增删改查创建数据库  

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Session["UserLogin"] != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string userName, string passWord)
        {
            if (userName == string.Empty || passWord == string.Empty)
            {
                return Content("<script>alert('用户名和密码不可以为空');history.go(-1);</script>");
            }
            var booLogin = iUserDal.LoadEntity(u => u.UserName == userName).FirstOrDefault();
            if (booLogin != null)
            {
                if (booLogin.UserPassword == passWord)
                {
                    //登录状态
                    Session["UserLogin"] = booLogin;
                    //设置超时
                    Session.Timeout = 10;
                    return Content("<script >alert('登录成功，正在转到主页...');window.location.href = 'Index';</script >", "text/html");
                }
                else
                {
                    return Content("<script>alert('请检查您的账号密码是否正确');history.go(-1);</script>");
                }
            }
            return Content("<script>alert('请检查您的账号密码是否正确');history.go(-1);</script>");
        }
        public ActionResult Logout()
        {
            Session["UserLogin"] = null;
            return Content("<script >alert('注销成功！');window.location.href = 'Login';</script >", "text/html");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserInfo uInfo, HttpPostedFileBase image)
        {
            uInfo.SubTime = DateTime.Now;
            uInfo.UserID = Guid.NewGuid().ToString();
            uInfo.isAdmin = 0;
            uInfo.ImageData = UpFlie(image);

            if (iUserDal.AddEntity(uInfo))
            {
                return Content("<script >alert('注册成功，正在转到登录界面');window.location.href = 'Login';</script >", "text/html");
            }
            return View();
        }



        public ActionResult CreateDatabase()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDatabase(FormCollection collection)
        {
            if (db.CreateDatabase())
            {
                return RedirectToAction("AddDefaultAdminInfo");
            }
            else
            {
                ViewBag.CreateDBEorr = "啊偶~数据库已经存在了喵~丨<a href='Login'>返回登录页面</a>丨<a href='AddDefaultAdminInfo'>或点此处创建超级管理员账户</a>";
                return View();
            }
        }

        #region 创建数据库和添加管理员
        public ActionResult AddDefaultAdminInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDefaultAdminInfo(UserInfo uInfo, HttpPostedFileBase image)
        {
            var hasAdmin = iUserDal.LoadEntity(a => a.isAdmin == 1).FirstOrDefault();
            if (hasAdmin != null)
            {
                return Content("<script>alert('超级管理员账户已经存在，请勿重复创建！');history.go(-1);</script>");
            }
            uInfo.SubTime = DateTime.Now;
            uInfo.UserID = Guid.NewGuid().ToString();
            uInfo.isAdmin = 1;
            uInfo.ImageData = UpFlie(image);

            if (iUserDal.AddEntity(uInfo))
            {
                ViewBag.CreateBaseAndAdminMessag = "<br/><br/><p style='margin-left:-50px;'><h2>第2步已经完成，数据库和管理员账户已经成功创建<h2><br/><a href='AddDefaultDemoData'>第3步，请点击此处导入网站默认的演示数据</a></p>";
            }
            return View();
        }
        #endregion

        public ActionResult ShowUserInfo()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }

            //将查询到的数据记录分页显示代码
            int totalCount;
            int pageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);

            var userInfoList = iUserDal.LoadPageEntity<string>(pageIndex, 10, out totalCount, s => true, ss => ss.UserID, true);
            ViewBag.PageList = PageBarHelper.GetPagaBar(pageIndex, totalCount);
            return View(userInfoList);
        }

        #region 添加管理员（后台管理）
        public ActionResult CreateUserInfo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserInfo(UserInfo uInfo, HttpPostedFileBase image)
        {
            if (uInfo == null)
            {
                return Content("<script >alert('输入的内容不能为空！');history.go(-1);</script >", "text/html");
            }
            #region 将图片存到数据库
            //if (image != null)
            //{
            //    //取得文件的扩展名,并转换成小写 
            //    string fileExtension = System.IO.Path.GetExtension(image.FileName).ToLower();
            //    //限定只能上传jpg和gif图片 
            //    string[] allowExtension = { ".jpg", ".gif" };
            //    //判断上传的文件类型是否正确
            //    if (allowExtension.Contains(fileExtension))
            //    {
            //        uInfo.ImageType = image.ContentType;//获取图片类型
            //        uInfo.ImageData = new byte[image.ContentLength];//新建一个长度等于图片大小的二进制地址
            //        image.InputStream.Read(uInfo.ImageData, 0, image.ContentLength);//将image读取到ImageData中
            //    }
            //    else
            //    {
            //        ViewBag.UpIamgeEorr = "上传的类型错误，当前上传不是图片，请重新上传！";
            //        return View(); ;
            //    }
            //}
            #endregion

            uInfo.SubTime = DateTime.Now;
            uInfo.UserID = Guid.NewGuid().ToString();
            uInfo.ImageData = UpFlie(image);

            if (iUserDal.AddEntity(uInfo))
            {
                return RedirectToAction("ShowUserInfo");
            }
            else
            {
                return Content("添加失败，请重试！");
            }
        }
        #endregion

        /// <summary>
        /// 用户编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EditUserInfo(string id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            UserInfo uInfo = iUserDal.LoadEntity(u => u.UserID == id).FirstOrDefault();
            if (uInfo.isAdmin == 1 && ((UserInfo)Session["UserLogin"]).UserID != uInfo.UserID)
            {
                return Content("<script >alert('对不起，您不能编辑超级管理员的信息！');history.go(-1);</script >", "text/html");
            }
            return View(uInfo);

        }
        [HttpPost]
        public ActionResult EditUserInfo(string id, UserInfo uInfo, HttpPostedFileBase image)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
           
            if (image != null)
            {
                uInfo.ImageData = UpFlie(image);
            }
            if (iUserDal.EditEntity(uInfo))
            {
                return RedirectToAction("ShowUserInfo");
            }
            else
            {
                return Content("编辑出错，请重试！");
            }

        }
        /// <summary>
        /// 用户删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DeleteUserInfo(string id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            UserInfo uInfo = iUserDal.LoadEntity(u => u.UserID == id).FirstOrDefault();
            if (uInfo.UserID == ((UserInfo)Session["UserLogin"]).UserID)
            {
                return Content("<script >alert('对不起，您不能删除自己的账号信息！');history.go(-1);</script >", "text/html");
            }
            if (uInfo.isAdmin == 1)
            {
                return Content("<script >alert('对不起，该用户为超级管理员，您不能够删除！');history.go(-1);</script >", "text/html");
            }
            return View(uInfo);
        }

        [HttpPost]
        public ActionResult DeleteUserInfo(UserInfo uInfo)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
          
            if (iUserDal.DeleteEntity(uInfo))
            {
                return RedirectToAction("ShowUserInfo");
            }
            else
            {
                return Content("删除出错，请重试！");
            }
        }
        #endregion

        #region 网站首页信息  

        ///// <summary>
        ///// 网站首页信息的创建（其实网站完成以后）
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult CreateHomeCor()
        //{
        //    if (Session["UserLogin"] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    return View();

        //}
        //[HttpPost]
        //public ActionResult CreateHomeCor(HomeCor homeCor, HttpPostedFileBase image)
        //{
        //    if (Session["UserLogin"] == null)
        //    {
        //        return RedirectToAction("Login");
        //    }
        //    homeCor.Companylogo = UpLogo(image);
        //    if (iHomeDal.AddEntity(homeCor))
        //    {
        //        return RedirectToAction("HomeCor");
        //    }
        //    else
        //    {
        //        return Content("添加失败，请重试！");
        //    }
        //}



        /// <summary>
        /// 网站首页信息的管理页（数据查询）
        /// </summary>
        /// <returns></returns>
        public ActionResult HomeCor()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            //将查询到的数据记录分页显示代码
            int totalCount;
            int pageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);

            var homeInfoList = iHomeDal.LoadPageEntity<int>(pageIndex, 10, out totalCount, s => true, ss => ss.IndexID, true);
            if (homeInfoList == null)
            {
                return RedirectToAction("CreateHomeCor");
            }

            ViewBag.PageList = PageBarHelper.GetPagaBar(pageIndex, totalCount);
            return View(homeInfoList);
        }

        public ActionResult EditHomeCor(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }

            HomeCor homeInfo = iHomeDal.LoadEntity(h => h.IndexID == id).FirstOrDefault();
            if (homeInfo == null)
            {
                return RedirectToAction("CreateHomeCor");
            }
            return View(homeInfo);
        }
        [HttpPost]
        public ActionResult EditHomeCor(HomeCor homeCor, HttpPostedFileBase image)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (image != null)
            {
                homeCor.Companylogo = UpLogo(image);
            }
            else
            {
                homeCor.Companylogo = homeCor.Companylogo;
            }
            if (iHomeDal.EditEntity(homeCor))
            {
                ViewBag.EditHomeCorMessage = "站点信息更新成功！";
            }
            else
            {
                ViewBag.EditHomeCorMessage = "站点信息更新失败，请检查输入每项内容是否正确！";
            }
            return View(homeCor);
        }
        #endregion


        #region 员工管理 
        public ActionResult ShowEmployee()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            var boolEmployee = iEmpDal.LoadEntity(u => true);
            if (boolEmployee == null)
            {
                return RedirectToAction("Index");
            }
            int Count;
            int pageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);
            var userInfoList = iEmpDal.LoadPageEntity<int>(pageIndex, 10, out Count, s => true, sb => sb.EmpoloyeeID, true);
            ViewBag.PageList = PageBarHelper.GetPagaBar(pageIndex, Count);
            return View(userInfoList);
        }
        public ActionResult CreateEmployee()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee el, HttpPostedFileBase image)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (el == null)
            {
                return Content("<script >alert('输入的内容不能为空！');history.go(-1);</script >", "text/html");
            }
            el.EmpoloyeePhoto = UpFlie(image);
            iEmpDal.AddEntity(el);
            return RedirectToAction("ShowEmployee");
        }
        public ActionResult DeleteEmployee(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            Employee el = iEmpDal.LoadEntity(s => s.EmpoloyeeID == id).FirstOrDefault();
            return View(el);
        }
        [HttpPost]
        public ActionResult DeleteEmployee(Employee el)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (iEmpDal.DeleteEntity(el))
            {
                return RedirectToAction("ShowEmployee");
            }
            else
            {
                return Content("删除失败！");
            }
        }
        public ActionResult UpdateEmployee(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            Employee el = iEmpDal.LoadEntity(s => s.EmpoloyeeID == id).FirstOrDefault();
            return View(el);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employee el, HttpPostedFileBase image)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (image == null)
            {
                el.EmpoloyeePhoto = el.EmpoloyeePhoto;
            }
            else
            {
                el.EmpoloyeePhoto = UpFlie(image);
            }
            if (iEmpDal.EditEntity(el))
            {
                return RedirectToAction("ShowEmployee");
            }
            else
            {
                return Content("更新失败！");
            }
        }
        #endregion
  
        #region 公司简介 
        public ActionResult ShowAboutUS()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            var ab = iAboutDal.LoadEntity(a => true);
            return View(ab);
        }
        public ActionResult CreateAboutUS()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateAboutUS(AboutUS au)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            iAboutDal.AddEntity(au);
            return RedirectToAction("ShowAboutUS");
        }
        public ActionResult DeleteAboutUS(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            var aboutus = iAboutDal.LoadEntity(a => a.AboutID == id).FirstOrDefault(); ;
            return View(aboutus);
        }
        [HttpPost]
        public ActionResult DeleteAboutUS(AboutUS au)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            iAboutDal.DeleteEntity(au);
            return RedirectToAction("ShowAboutUS");
        }
        public ActionResult UpdateAboutUS(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            AboutUS ab = iAboutDal.LoadEntity(a => a.AboutID == id).FirstOrDefault();
            return View(ab);
        }
        [HttpPost]
        public ActionResult UpdateAboutUS(AboutUS au)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            iAboutDal.EditEntity(au);
            return RedirectToAction("ShowAboutUS");
        }
        #endregion

        #region 产品管理  
        public ActionResult ShowSubpageProduct()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            var boolProduct = iSubDal.LoadEntity(u => true);
            if (boolProduct == null)
            {
                return RedirectToAction("Index");
            }
            //将查询到的数据记录分页显示代码
            int totalCount;
            int pageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);

            var userInfoList = iSubDal.LoadPageEntity<string>(pageIndex, 10, out totalCount, s => true, sb => sb.ProductID.ToString(), true);
            ViewBag.PageList = PageBarHelper.GetPagaBar(pageIndex, totalCount);
            return View(userInfoList);
        }
        public ActionResult CreateSubpageProduct()
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateSubpageProduct(SubpageProduct su, HttpPostedFileBase images)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (su.ProductName == null)
            {
                return Content("<script >alert('输入产品名不能为空！');history.go(-1);</script >", "text/html");
            }
            if (su.PcDownLink == null)
            {
                su.PcDownLink = "#";
            }
            if (su.ardDownLink == null)
            {
                su.ardDownLink = "#";
            }
            if (su.iosDownLink == null)
            {
                su.iosDownLink = "#";
            }
            if (su.BuyLink == null)
            {
                su.BuyLink = "#";
            }
            //su.ClassID = Convert.ToInt32(Request.Form["classid"]);
            //su.Features = Convert.ToInt32(Request.Form["features"]);
            su.ProductTime = DateTime.Now;
            su.ProductPhotoFile = UpFlie(images);
            iSubDal.AddEntity(su);
            return RedirectToAction("ShowSubpageProduct");
        }
        public ActionResult DeleteSubpageProduct(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            SubpageProduct su = iSubDal.LoadEntity(s => s.ProductID == id).FirstOrDefault();
            return View(su);
        }
        [HttpPost]
        public ActionResult DeleteSubpageProduct(int id, SubpageProduct su)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (su.Features == 1)
            {
                return Content("<script >alert('当前产品为特色产品，请勿删除！');history.go(-1);</script >", "text/html");
            }
            if (iSubDal.DeleteEntity(su))
            {
                return RedirectToAction("ShowSubpageProduct");
            }
            else
            {
                return Content("删除失败！");
            }
        }
        public ActionResult UpdateSubpageProduct(int id)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            SubpageProduct su = iSubDal.LoadEntity(s => s.ProductID == id).FirstOrDefault();
            return View(su);
        }
        [HttpPost]
        public ActionResult UpdateSubpageProduct(SubpageProduct su, HttpPostedFileBase images)
        {
            if (Session["UserLogin"] == null)
            {
                return RedirectToAction("Login");
            }
            if (su.PcDownLink == null)
            {
                su.PcDownLink = "#";
            }
            if (su.ardDownLink == null)
            {
                su.ardDownLink = "#";
            }
            if (su.iosDownLink == null)
            {
                su.iosDownLink = "#";
            }
            if (su.BuyLink == null)
            {
                su.BuyLink = "#";
            }
            //su.ClassID = Convert.ToInt32(Request.Form["classid"]);
            //su.Features = Convert.ToInt32(Request.Form["features"]);
            if (images == null)
            {
                su.ProductPhotoFile = su.ProductPhotoFile;
            }
            else
            {
                su.ProductPhotoFile = UpFlie(images);
            }
            if (iSubDal.EditEntity(su))
            {
                return RedirectToAction("ShowSubpageProduct");
            }
            else
            {
                return Content("更新失败！");
            }
        }
        #endregion
    }
}