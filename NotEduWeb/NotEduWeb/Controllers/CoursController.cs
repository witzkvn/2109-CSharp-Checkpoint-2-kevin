using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NotEduWeb.Models;
using NotEduWeb.Tools;

namespace NotEduWeb.Controllers
{
    public class CoursController : Controller
    {
        private NotEduWebEntities db = new NotEduWebEntities();

        // GET: Cours
        public ActionResult Index()
        {
            return View(db.Cours.OrderBy(c => c.nom).ToList());
        }

        // GET: Cours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }

            List<MoyenneParCours> itemDuCours = DbTools.FetchMoyenneCoursPromo().Where(item => item.CoursID == id).OrderByDescending(i => i.Annee).ToList();
            ViewBag.MoyennesPromos = itemDuCours;
            return View(cours);
        }

        // GET: Cours/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cours/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_cours,nom")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                Cours coursExistant = db.Cours.Where(c => c.nom.ToLower().Trim() == cours.nom.ToLower().Trim()).FirstOrDefault();
                if(coursExistant != null)
                {
                    ViewBag.Error = "Un cours avec ce nom existe déjà.";
                    return View("Create");
                }
                db.Cours.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cours);
        }

        // GET: Cours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_cours,nom")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                Cours coursExistant = db.Cours.Where(c => c.nom.ToLower().Trim() == cours.nom.ToLower().Trim()).FirstOrDefault();
                if (coursExistant != null)
                {
                    ViewBag.Error = "Un cours avec ce nom existe déjà.";
                    return View("Edit");
                }
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cours);
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Cours.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Cours.Find(id);
            db.Cours.Remove(cours);
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
    }
}
