using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Parade.Models;

namespace Parade.Controllers
{
    public class PeopleController : Controller
    {
        private ParadeEntities db = new ParadeEntities();

        // GET: People
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,First_Name,Middle_Name,Last_Name,Taluko,District,Date_of_Birth,Medical_History")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,First_Name,Middle_Name,Last_Name,Taluko,District,Date_of_Birth,Medical_History")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Person person = db.People.Find(id);
            db.People.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult Generate(string Format, int SortBy, int Size = 0)
        {
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            int paradeSize = Convert.ToInt32(webConfigApp.AppSettings.Settings["ParadeSize"].Value.ToString());
            int paradeRecords = Convert.ToInt32(webConfigApp.AppSettings.Settings["ParadeRecords"].Value.ToString());
            if(paradeSize != Size && Size > 0)
            {
                paradeSize = Size;
                webConfigApp.AppSettings.Settings["ParadeSize"].Value = paradeSize.ToString();
            }
            List<Person> people = new List<Person>();
            var query = db.People.AsQueryable();
            if(SortBy == 0)
            {
                query = query.OrderBy(x => x.id);
            }
            if (SortBy == 1)
            {
                query = query.OrderBy(x => x.First_Name);
            }
            if (SortBy == 2)
            {
                query = query.OrderByDescending(x => x.First_Name);
            }
            if (SortBy == 3)
            {
                query = query.OrderBy(x => x.First_Name).ThenBy(x => x.Last_Name).ThenBy(x => x.Middle_Name);
            }
            if (SortBy == 4)
            {
                query = query.OrderByDescending(x => x.First_Name).ThenBy(x => x.Last_Name).ThenBy(x => x.Middle_Name);
            }
            people = query.Skip(paradeRecords).Take(paradeSize).ToList();
            int count = people.Count;
            webConfigApp.AppSettings.Settings["ParadeRecords"].Value = (count + paradeRecords).ToString();
            webConfigApp.Save();
            if (Format == "xlsx")
            {
                string[] columns = { "First_Name", "Middle_Name", "Last_Name", "Taluko", "District", "Date_of_Birth", "Medical_History" };
                byte[] filecontent = Helpers.ExcelExportHelper.ExportExcel(people, "Parade", true, columns);
                return File(filecontent, Helpers.ExcelExportHelper.ExcelContentType, "Parade_" + DateTime.Now.Date + ".xlsx");
            }
            if (Format == "pdf")
            {
                DataTable dataTable = Helpers.ExcelExportHelper.ListToDataTable(people);
                MemoryStream PDFData = Helpers.PDFExportHelper.CreatedPdf(dataTable);
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.Charset = string.Empty;
                Response.Cache.SetCacheability(HttpCacheability.Public);
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Parade_" + DateTime.Now + ".pdf", DateTime.Now.Ticks.ToString().Substring(0, 6)));
                Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.End();
            }
            return RedirectToAction("Index");
        }
    }
}
