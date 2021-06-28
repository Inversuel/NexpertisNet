using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrudWebList.Controllers.API;
using CrudWebList.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Aspose.Pdf;
using MoreLinq;
using System.Data;
using System.IO;

namespace CrudWebList.Controllers
{
    public class HomeController : Controller
    {
        //Get ALL
        public ActionResult Index()
        {
            try
            {
                var JsonRaw = ApiHandler.GetAllTask();
                var dynamic = JsonConvert.DeserializeObject<dynamic>(JsonRaw.Result.ResponseMessage);
                var dynamicChildren = dynamic["result"].Children();
                List<Models.Task> Modeltests = new List<Models.Task>();
                foreach (JToken result in dynamicChildren)
                {
                    Models.Task Modeltest = result.ToObject<Models.Task>();
                    Modeltests.Add(Modeltest);
                }
                return View(Modeltests);
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Title = "Create";
            return View();
        }
        [HttpPost]
        public ActionResult Create(Models.Task task)
        {
            var jsonData = JsonConvert.SerializeObject(new { User = task.User, AssignedUser = task.AssignedUser, Title = task.Title, Describe = task.Describe, EndDate = task.EndDate, });

            var result = ApiHandler.PostTask(jsonData);

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult EditTask(string id)
        {
            ViewBag.Title = "Edit";
            try
            {
                var JsonRaw = ApiHandler.GetAllTask();
                var dynamic = JsonConvert.DeserializeObject<dynamic>(JsonRaw.Result.ResponseMessage);
                var dynamicChildren = dynamic["result"].Children();
                Models.Task Modeltests = new Models.Task();
                foreach (JToken result in dynamicChildren)
                {
                    if (result["_id"].ToString() == id)
                    {
                        Modeltests = result.ToObject<Models.Task>();
                    }
                }
                return View("Create", Modeltests);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public ActionResult EditTask(Models.Task task)
        {
            var jsonData = JsonConvert.SerializeObject(new { User = task.User, AssignedUser = task.AssignedUser, Title = task.Title, Describe = task.Describe, EndDate = task.EndDate, });

            var result = ApiHandler.PatchTask(jsonData, task._id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(string id)
        {
            ViewBag.Title = "Edit";
            try
            {
                var JsonRaw = ApiHandler.GetAllTask();
                var dynamic = JsonConvert.DeserializeObject<dynamic>(JsonRaw.Result.ResponseMessage);
                var dynamicChildren = dynamic["result"].Children();
                Models.Task Modeltests = new Models.Task();
                foreach (JToken result in dynamicChildren)
                {
                    if (result["_id"].ToString() == id)
                    {
                        Modeltests = result.ToObject<Models.Task>();
                    }
                }
                return View("Details", Modeltests);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public ActionResult Delete(string id)
        {

            var result = ApiHandler.GetDeleted(id);

            return RedirectToAction("Index");
        }
        private static readonly string _dataDir = "Ścieżka do obrazka w kpatalogu api express!!";
        public ActionResult NetToPdf(string ids)
        {
            var listids = Array.ConvertAll(ids.Split(','), p => p.Trim());

            var JsonRaw = ApiHandler.GetAllTask();
            var dynamic = JsonConvert.DeserializeObject<dynamic>(JsonRaw.Result.ResponseMessage);
            var dynamicChildren = dynamic["result"].Children();

            List<Models.Task> Modeltests = new List<Models.Task>();
            foreach (JToken result in dynamicChildren)
            {
                foreach (string id in listids)
                {
                    if (result["_id"].ToString() == id)
                    {
                        Models.Task ModelTask = result.ToObject<Models.Task>();
                        Modeltests.Add(ModelTask);

                    }

                }
            }

            Document document = new Document();

            Page page = document.Pages.Add();

            Table table = new Table
            {
                ColumnWidths = "14% 14% 14% 14% 14% 14% 14%"

            };

            DataTable tableModel = Modeltests.ToDataTable();

            table.ImportDataTable(tableModel, true, 0, 0);
            document.Pages[1].Paragraphs.Add(table);

            var imageFileName = Path.Combine(_dataDir, "nexpertis.jpg");
            page.AddImage(imageFileName, new Rectangle(10, 0, 120, 830));

            using (var streamout = new MemoryStream())
            {
                document.Save(streamout);
                return new FileContentResult(streamout.ToArray(), "application/pdf")
                {
                    FileDownloadName = "TasksPdfKarolMaj.pdf"
                };



            }



        }

    }
}