using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebIamge.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace WebIamge.Areas.Admin.Controllers
{
    public class ManageController : Controller
    {
        private WebImageBDContext db = new WebImageBDContext();

        // GET: Admin/Manage
        public ActionResult Index(int ? page)
        {

            int pageSize = 3;
            int pageNum = (page ?? 1);
            var list = db.Manages.Where(m => m.Status != 0).ToList();
            return View("Index", list.ToPagedList(pageNum, pageSize));
        }
        public ActionResult Trash()
        {
            var list = db.Manages.Where(m => m.Status == 0).ToList();
            return View("Trash", list);
        }
        // GET: Admin/Manage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manage manage = db.Manages.Find(id);
            if (manage == null)
            {
                return HttpNotFound();
            }
            return View(manage);
        }

        // GET: Admin/Manage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Manage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NameImage,Image,Status")] Manage manage)
        {
            if (ModelState.IsValid)
            {
                var Image = Request.Files["img"];
                string[] FileExtension = { ".jpg", ".png", "gif" };
                if (Image.ContentLength != 0)
                {
                    if (FileExtension.Contains(Image.FileName.Substring(Image.FileName.LastIndexOf("."))))
                    {
                        string imgName = Image.FileName.Substring(Image.FileName.LastIndexOf("."));
                        manage.Image = imgName;
                        string PathImage = Path.Combine(Server.MapPath("~/Public/images/"), imgName);
                        Image.SaveAs(PathImage);
                    }
                }
                db.Manages.Add(manage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(manage);
        }

        // GET: Admin/Manage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manage manage = db.Manages.Find(id);
            if (manage == null)
            {
                return HttpNotFound();
            }
            return View(manage);
        }

        // POST: Admin/Manage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NameImage,Image,Status")] Manage manage)
        {
            if (ModelState.IsValid)
            {
                //hinh anh
                var Image = Request.Files["img"];
                string[] FileExtension = { ".jpg", ".png", "gif" };
                if (Image.ContentLength != 0)
                {
                    if (FileExtension.Contains(Image.FileName.Substring(Image.FileName.LastIndexOf("."))))
                    {
                        string imgName = Image.FileName.Substring(Image.FileName.LastIndexOf("."));
                        //Xoa hinh
                        String DelPath = Path.Combine(Server.MapPath("~/Public/images/"), manage.Image);
                        if (System.IO.File.Exists(DelPath))
                        {
                            System.IO.File.Delete(DelPath);
                        }
                        //
                        manage.Image = imgName;
                        string PathImage = Path.Combine(Server.MapPath("~/Public/images/"), imgName);
                        Image.SaveAs(PathImage);
                    }
                }
                db.Entry(manage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(manage);
        }

        // GET: Admin/Manage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Manage manage = db.Manages.Find(id);
            if (manage == null)
            {
                return HttpNotFound();
            }
            return View(manage);
        }

        // POST: Admin/Manage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Manage manage = db.Manages.Find(id);
            db.Manages.Remove(manage);
            db.SaveChanges();
            return RedirectToAction("Trash","Manage");
        }

        public ActionResult Status(int id)
        {
            Manage manage = db.Manages.Find(id);
            int status = (manage.Status == 1) ? 2 : 1;
            manage.Status = status;
            db.Entry(manage).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DelTrash(int id)
        {
            Manage manage = db.Manages.Find(id);
            manage.Status = 0;
            db.Entry(manage).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Manage");
        }
        public ActionResult ReTrash(int id)
        {
            Manage manage = db.Manages.Find(id);
            manage.Status = 2;
            db.Entry(manage).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Trash","Manage");
        }
    }
}
