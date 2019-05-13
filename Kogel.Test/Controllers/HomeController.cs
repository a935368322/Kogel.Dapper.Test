using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kogel.Test.Models;
using Kogel.Utility.Tool;
using Kogel.Data.Model;
//Dapper扩展
using Kogel.Dapper.Extension.MsSql;
using Kogel.Dapper.Extension.MsSql.Extension;
using Kogel.Dapper.Extension.MsSql.Helper;

using Kogel.Dapper.Extension;
using Dapper;


namespace Kogel.Test.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var conn = new System.Data.SqlClient.SqlConnection(Resource.connectionString))
            {
                //模型类属性不能出现可空类型,例如int?
                var list = conn.QuerySet<users>().Where(x => x.name.Contains("Y")).ToList();
                //查询
                var users = conn.QuerySet<users>().Where(x => x.code != "1").Get();
                ViewData["Message"] = "Hellow " + users.name;
                //模糊查询
                var users1 = conn.QuerySet<users>().Where(x => x.name.Contains("Y")).Get();
                ViewData["Message1"] = "Hellow " + users1.name;
                //修改
                users1.name = Guid.NewGuid().ToString();
                users1.createDate = DateTime.Now;
                int result = conn.CommandSet<users>().Where(x => x.id == 4).Update(users1);
                ViewData["Message2"] = result;
                //修改查询
                var users2 = conn.QuerySet<users>().Where(x => x.name.Contains("Y")).UpdateSelect(x => new users { name = "Y11" }).FirstOrDefault();
                ViewData["Message3"] = "Hellow " + users2.name;
                //新增
                int result2 = conn.CommandSet<users>().Insert(new users() { code = Guid.NewGuid().ToString(), name = "test", createWay = 1, createDate = DateTime.Now, roleId = 2 });
                ViewData["Message4"] = result2;
                //删除
                int result3 = conn.CommandSet<users>().Where(x => x.roleId == 2 && x.name == users2.name).Delete();
                ViewData["Message5"] = result3;
                //不连表查询返回dynamic
                var list1 = conn.QuerySet<users>().Where(x => x.code != "1").ToList(true);
                //连表查询返回dynamic
                var list2 = conn.QuerySet<users>().Where(x => x.code != "1").Join<users, project_Role>(x => x.roleId, y => y.id).ToList(true);
                //SQL连表查询
                var users3 = conn.Query<users>(@"SELECT * FROM USERS 
                                                 LEFT JOIN PROJECT_ROLE ON PROJECT_ROLE.ID=USERS.ROLEID").ToList();
                //翻页查询
                var list3 = conn.QuerySet<users>().OrderBy(x => x.createDate).PageList(1, 10);
                //翻页连表查询返回dynamic
                var list4 = conn.QuerySet<users>().Join<users, project_Role>(x => x.roleId, y => y.id).OrderBy(x => x.createDate).PageList(1, 10, true);
                //动态化查询
                var list5 = conn.QuerySet<users>().Where(new users() { id = 2, name = "Y11" }).Get();
                //连表使用主表和副表条件翻页查询
                var list6 = conn.QuerySet<users>().Join<users, project_Role>(x => x.roleId, y => y.id).Where(x => x.name == "adonis")
                    .Where<project_Role>(x => x.projectId == 2 && x.enabled == true).OrderBy(x => x.createDate).PageList(1, 10, true);
                //In查询
                var userss = conn.QuerySet<users>().Where(x => x.id.In("1,2,3")).ToList();
                //var users = conn.QuerySet<users>().Where(x => x.id >= 2).ToList();
            }
            stopwatch.Stop(); //  停止监视
            TimeSpan timeSpan = stopwatch.Elapsed; //  获取总时间
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
