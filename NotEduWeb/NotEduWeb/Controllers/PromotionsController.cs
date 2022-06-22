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
    public class PromotionsController : Controller
    {
        private NotEduWebEntities db = new NotEduWebEntities();

        // GET: Promotions
        public ActionResult Index()
        {
            return View(db.Promotions.OrderBy(p => p.annee).ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Eleves> elevesPromo = from eleve in db.Eleves
                                              where eleve.fk_eleve_promotion == id
                                              select eleve;
            ViewBag.ElevesPromo = elevesPromo.OrderBy(e => e.nom).ToList();
            List<MoyenneParCours> itemDeLaPromo = DbTools.FetchMoyenneCoursPromo().Where(item => item.PromotionID == id).OrderBy(i => i.Cours).ToList();
            ViewBag.MoyennesPromos = itemDeLaPromo;
            return View(promotions);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Promotions/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_promotion,annee,nom")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotions);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // POST: Promotions/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_promotion,annee,nom")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotions);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotions promotions = db.Promotions.Find(id);
            db.Promotions.Remove(promotions);
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
