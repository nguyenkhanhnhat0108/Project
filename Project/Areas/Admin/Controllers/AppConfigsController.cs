using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project;

namespace Project.Areas.Admin.Controllers
{
    public class AppConfigsController : Controller
    {
        private Project_V0212Entities db = new Project_V0212Entities();

        // GET: Admin/AppConfigs
        public async Task<ActionResult> Index()
        {
            return View(await db.AppConfigs.ToListAsync());
        }

        // GET: Admin/AppConfigs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppConfig appConfig = await db.AppConfigs.FindAsync(id);
            if (appConfig == null)
            {
                return HttpNotFound();
            }
            return View(appConfig);
        }

        // GET: Admin/AppConfigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppConfigs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Key,Value,IsActive")] AppConfig appConfig)
        {
            if (ModelState.IsValid)
            {
                db.AppConfigs.Add(appConfig);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(appConfig);
        }

        // GET: Admin/AppConfigs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppConfig appConfig = await db.AppConfigs.FindAsync(id);
            if (appConfig == null)
            {
                return HttpNotFound();
            }
            return View(appConfig);
        }

        // POST: Admin/AppConfigs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Key,Value,IsActive")] AppConfig appConfig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appConfig).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(appConfig);
        }

        // GET: Admin/AppConfigs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppConfig appConfig = await db.AppConfigs.FindAsync(id);
            if (appConfig == null)
            {
                return HttpNotFound();
            }
            return View(appConfig);
        }

        // POST: Admin/AppConfigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AppConfig appConfig = await db.AppConfigs.FindAsync(id);
            db.AppConfigs.Remove(appConfig);
            await db.SaveChangesAsync();
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
    }
}
