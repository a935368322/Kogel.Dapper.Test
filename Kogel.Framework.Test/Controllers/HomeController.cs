using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using Kogel.Utility.Tool;
using Kogel.Data.Model;
//Dapper扩展
using Kogel.Dapper.Extension.MsSql;
using Kogel.Dapper.Extension.MsSql.Extension;
using Kogel.Dapper.Extension.MsSql.Helper;


namespace Kogel.Framework.Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var conn = new DapperHelper("server=localhost;user id=sa;password=sa;database=PermissionManage"))
            {
               var list= conn.QuerySet<userssss>().ToList();
            }
            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}