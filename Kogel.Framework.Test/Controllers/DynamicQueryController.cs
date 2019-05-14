using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kogel.Data.Model;

//Dapper扩展
using Kogel.Dapper.Extension.MsSql;
using Kogel.Dapper.Extension.MsSql.Extension;
//动态化查询
using Kogel.Dapper.Extension.Expressions;
using Kogel.Dapper.Extension.Model;
using System.Data.SqlClient;


namespace Kogel.Framework.Test.Controllers
{
    public class DynamicQueryController : Controller
    {
        // GET: DynamicQuery
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Query(int pageNumber = 1, int pageSize = 20, string sortName = "createDate", string sortOrder = "desc", Dictionary<string, DynamicTree> dynamicWhere = null)
        {
            using (var conn = new SqlConnection("server=localhost;user id=sa;password=sa;database=PermissionManage"))
            {
                var result = conn.QuerySet<users>().Where(dynamicWhere).OrderBy(x => x.createDate).OrderByDescing(x => x.createDate).PageList(pageNumber, pageSize);
                return Json(new
                {
                    total = result.Total,
                    rows = result.Items
                });
            }
        }
    }
}