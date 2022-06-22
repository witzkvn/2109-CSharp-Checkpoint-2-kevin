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
    public class ElevesController : Controller
    {
        private NotEduWebEntities db = new NotEduWebEntities();

        // GET: Eleves
        public ActionResult Index()
        {
            var eleves = db.Eleves.Include(e => e.Promotions).OrderBy(e => e.nom);
            return View(eleves.ToList());
        }

        // GET: Eleves/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleves eleves = db.Eleves.Find(id);
            if (eleves == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Notes> notesEleve = from note in db.Notes
                                             join cours in db.Cours
                                             on note.fk_note_cours equals cours.ID_cours
                                             where note.fk_note_eleve == id
                                             select note;
            List<decimal> notesValeurs = notesEleve.Select(note => note.valeur).ToList();
            if(notesValeurs.Count > 0)
            {
                ViewBag.moyenne = Utilities.ArrondirNote(decimal.ToDouble(notesValeurs.Average()));

            } else
            {
                ViewBag.moyenne = -1;
            }

            ViewBag.listeNotes = notesEleve.OrderBy(n => n.Cours.nom).ToList();
            return View(eleves);
        }

        // GET: Eleves/Create
        public ActionResult Create()
        {
            ViewBag.fk_eleve_promotion = new SelectList(db.Promotions, "ID_promotion", "nom");
            Dictionary<int, string> dicoPromos = DbTools.GetListeDesPromotionsFormattee();

            ViewBag.promos = dicoPromos;
            return View();
        }

        // POST: Eleves/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_eleve,nom,prenom,date_naissance,fk_eleve_promotion")] Eleves eleves)
        {
            if (ModelState.IsValid)
            {
                db.Eleves.Add(eleves);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_eleve_promotion = new SelectList(db.Promotions, "ID_promotion", "nom", eleves.fk_eleve_promotion);
            Dictionary<int, string> dicoPromos = DbTools.GetListeDesPromotionsFormattee();

            ViewBag.promos = dicoPromos;
            return View(eleves);
        }

        // GET: Eleves/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleves eleves = db.Eleves.Find(id);
            if (eleves == null)
            {
                return HttpNotFound();
            }
            ViewBag.date = eleves.date_naissance.ToString("yyyy-MM-dd");
            ViewBag.fk_eleve_promotion = new SelectList(db.Promotions, "ID_promotion", "nom", eleves.fk_eleve_promotion);
            Dictionary<int, string> dicoPromos = DbTools.GetListeDesPromotionsFormattee();
            ViewBag.promos = dicoPromos;
            return View(eleves);
        }

        // POST: Eleves/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_eleve,nom,prenom,date_naissance,fk_eleve_promotion")] Eleves eleves)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eleves).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_eleve_promotion = new SelectList(db.Promotions, "ID_promotion", "nom", eleves.fk_eleve_promotion);
            return View(eleves);
        }

        // GET: Eleves/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Eleves eleves = db.Eleves.Find(id);
            if (eleves == null)
            {
                return HttpNotFound();
            }
            return View(eleves);
        }

        // POST: Eleves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Eleves eleves = db.Eleves.Find(id);
            db.Eleves.Remove(eleves);
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
