using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Second.Website.Model;
using Second.Website.DAL;
using Second.Website.IDAL;

namespace Second.Website.WebApp.Controllers
{
    public class HomeController : Controller
    {
        IHomeCorDal iHDal = new HomeCorDal();
        ISubpageProductDal iSDal = new SubpageProductDal();
        IEmployeeDal iEDal = new EmployeeDal();
        IAboutUSDal iADal = new AboutUSDal();

        // GET: Home
        public ActionResult Index()
        {
            //主页特色产品
            List<SubpageProduct> hFeaturesLists = iSDal.LoadEntity(hf => hf.Features == 1).ToList();
            List<SubpageProduct> hFeaturesList = new List<SubpageProduct>();

            if (hFeaturesLists.Count() < 6)
            {
                for (int i = 0; i < hFeaturesLists.Count(); i++)
                {
                    hFeaturesList.Add(hFeaturesLists[i]);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    hFeaturesList.Add(hFeaturesLists[i]);
                }
            }
            ViewBag.HomeFeaturesList = hFeaturesList;

            //主页显示的员工
            List<Employee> empLists = iEDal.LoadEntity(ee => true).ToList();
            List<Employee> empList = new List<Employee>();
            if (empLists.Count() < 6)
            {
                for (int i = 0; i < empLists.Count(); i++)
                {
                    empList.Add(empLists[i]);
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    empList.Add(empLists[i]);
                }
            }
            ViewBag.HomeEmployeeList = empList;

            //主页关于我们
            List<AboutUS> about = iADal.LoadEntity(a => true).ToList();
            ViewBag.About = about;

            //主页基本信息
            HomeCor homeCorInfo = iHDal.LoadEntity(h => true).FirstOrDefault();
            return View(homeCorInfo);
        }
        public ActionResult Product()
        {
            //电脑产品 ClassID=1
            var pcListtInfo = iSDal.LoadEntity(p => p.ClassID == 1);
            ViewBag.PcList = pcListtInfo;

            //手机产品 ClassID=3
            var moListtInfo = iSDal.LoadEntity(p => p.ClassID == 2);
            ViewBag.MoList = moListtInfo;

            //科技商品 ClassID=3
            var tcListtInfo = iSDal.LoadEntity(p => p.ClassID == 3);
            ViewBag.TcList = tcListtInfo;

            return View();
        }
    }
}