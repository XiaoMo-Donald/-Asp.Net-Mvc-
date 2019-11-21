using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Second.Website.Model;
using Second.Website.Common;

namespace Second.Website.WebApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test        
        //通过业务逻辑接口层访问 使用方法 userInfoBLL.相应的方法（增删改查、分页）
        //IUserInfoService userInfoBLL = new UserInfoService();

        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}