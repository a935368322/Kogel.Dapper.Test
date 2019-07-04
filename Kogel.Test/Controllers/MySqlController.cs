using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kogel.Dapper.Extension.MySql;
using Microsoft.AspNetCore.Mvc;
using Kogel.Test.Models;
using Kogel.Data.Model;
using Kogel.Dapper.Extension.MsSql.Helper;

namespace Kogel.Test.Controllers
{
    public class MySqlController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                using (var conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;user id=root;password=a5101264a;database=PermissionManage"))
                {
                   
                    //查询
                    var users = conn.QuerySet<users>().Where(x => x.code != "1").Get();
                    //模糊查询
                    var users1 = conn.QuerySet<users>().Where(x => x.name.Contains("Y")).Get();
                    //修改
                    users1.name = Guid.NewGuid().ToString();
                    users1.createDate = DateTime.Now;
                    int result = conn.CommandSet<users>().Where(x => x.id == 4).Update(users1);
                    ViewData["Message2"] = result;
                    //新增
                    int result2 = conn.CommandSet<users>().Insert(new users() { code = Guid.NewGuid().ToString(), name = "test", createWay = 1, createDate = DateTime.Now, roleId = 2 });
                    ViewData["Message4"] = result2;
                    //删除
                    int result3 = conn.CommandSet<users>().Where(x => x.roleId == 0).Delete();
                    ViewData["Message5"] = result3;
                    //不连表查询返回dynamic
                    var list1 = conn.QuerySet<users>().Where(x => x.code != "1").ToList<dynamic>();
                    //连表查询返回dynamic
                    var list2 = conn.QuerySet<users>().Where(x => x.code != "1").Join<users, project_Role>(x => x.roleId, y => y.id).ToList();
                    //翻页查询
                    var list3 = conn.QuerySet<users>().OrderBy(x => x.createDate).PageList(1, 10);
                    //翻页连表查询返回dynamic
                    var list4 = conn.QuerySet<users>()
                        .Join<users, project_Role>(x => x.roleId, y => y.id)
                        .OrderBy(x => x.createDate).PageList<dynamic>(2, 3);
                    //动态化查询
                    var list5 = conn.QuerySet<users>().Where(new users() { id = 2, name = "Y11" }).Get();
                    //连表使用主表和副表条件翻页查询
                    var list6 = conn.QuerySet<users>().Join<users, project_Role>(x => x.roleId, y => y.id).Where(x => x.name == "Y11")
                        .Where<project_Role>(x => x.projectId == 2 && x.enabled == true).OrderBy(x => x.createDate).PageList<dynamic>(1, 10);
                    //In查询
                    var userss = conn.QuerySet<users>().Where(x => x.id.In("1,2,3")).ToList();

                    var user = conn.QuerySet<users>().Join<users, project_Role>((a, b) => a.roleId == b.id && a.id == 3).Get();

                    var users3 = conn.QuerySet<users>()
                        .Join<users, project_Role>((a, b) => a.roleId == b.id)
                        .Where<users, project_Role>((a, b) => a.id == 3 && b.id == 3)
                        .Get<dynamic>();
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}