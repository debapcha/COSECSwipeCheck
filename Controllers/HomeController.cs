using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using COSECSwipeCheck.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using PagedList;

namespace COSECSwipeCheck.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        db dbop = new db();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([Bind] Ad_login ad)
        {
            int res = dbop.LoginCheck(ad);
            int pageSize = 5;
            int pageIndex = 1;

            if (res == 0)
            {
                String connectionString = "Data Source=DESKTOP-ILIIGRJ\\SQLEXPRESS;Initial Catalog=COSEC;User id=sa;Password=SQL@15@dc;";
                String sql = "SELECT top 5 * FROM Employee_Entry";

                var model = new List<Approver>();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        var approvers = new Approver();
                        approvers.USERID = Convert.ToString(rdr["USERID"]);
                        approvers.EVENTDATETIME = Convert.ToDateTime(rdr["EVENTDATETIME"]);
                        model.Add(approvers);
                    }
                }
                //model.ToPagedList(pageIndex, pageSize);


                return View("Swipe",model);
                //TempData["msg"] = "You are welcome to Admin Section";
            }
            else
            {
                TempData["msg"] = "Admin id or Password is wrong.!";
            }
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
