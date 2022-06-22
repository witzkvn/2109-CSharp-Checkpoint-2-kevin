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
    public class NotesController : Controller
    {
        private NotEduWebEntities db = new NotEduWebEntities();

        // GET: Notes
        public ActionResult Index()
        {
            var notes = db.Notes.Include(n => n.Cours).Include(n => n.Eleves);
            return View(notes.ToList());
        }

        // GET: Notes/Details/5
        public ActionResult Details(int? idEleve, int? idCours)
        {
            if (idEleve == null || idCours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(idEleve, idCours);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // GET: Notes/Create
        public ActionResult Create()
        {
            ViewBag.fk_note_cours = new SelectList(db.Cours, "ID_cours", "nom");
            ViewBag.fk_note_eleve = new SelectList(db.Eleves, "ID_eleve", "nom");
            Dictionary<int, string> dicoEleves = DbTools.GetListeDesElevesFormattee();

            ViewBag.eleves = dicoEleves;
            return View();
        }

        // POST: Notes/Create
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "valeur,appreciation,fk_note_eleve,fk_note_cours")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                Notes existingNote = db.Notes.Find(notes.fk_note_eleve, notes.fk_note_cours);
                if(existingNote != null)
                {
                    ViewBag.Error = "Cet élève possède déjà une note pour cette matière.";
                    Dictionary<int, string> dicoEleves = DbTools.GetListeDesElevesFormattee();
                    ViewBag.eleves = dicoEleves;
                    ViewBag.fk_note_cours = new SelectList(db.Cours, "ID_cours", "nom");
                    ViewBag.fk_note_eleve = new SelectList(db.Eleves, "ID_eleve", "nom");
                    return View("Create");
                }
                db.Notes.Add(notes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Dictionary<int, string> dicoElevesElse = DbTools.GetListeDesElevesFormattee();

            ViewBag.eleves = dicoElevesElse;
            ViewBag.fk_note_cours = new SelectList(db.Cours, "ID_cours", "nom", notes.fk_note_cours);
            ViewBag.fk_note_eleve = new SelectList(db.Eleves, "ID_eleve", "nom", notes.fk_note_eleve);
            return View(notes);
        }

        // GET: Notes/Edit/5
        public ActionResult Edit(int? idEleve, int? idCours)
        {
            if (idEleve == null || idCours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(idEleve, idCours);
            if (notes == null)
            {
                return HttpNotFound();
            }
            ViewBag.fk_note_cours = new SelectList(db.Cours, "ID_cours", "nom", notes.fk_note_cours);
            ViewBag.fk_note_eleve = new SelectList(db.Eleves, "ID_eleve", "nom", notes.fk_note_eleve);

            Dictionary<int, string> dicoEleves = DbTools.GetListeDesElevesFormattee();
            ViewBag.eleves = dicoEleves;
            return View(notes);
        }

        // POST: Notes/Edit/5
        // Afin de déjouer les attaques par survalidation, activez les propriétés spécifiques auxquelles vous voulez établir une liaison. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "valeur,appreciation,fk_note_eleve,fk_note_cours")] Notes notes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.fk_note_cours = new SelectList(db.Cours, "ID_cours", "nom", notes.fk_note_cours);
            ViewBag.fk_note_eleve = new SelectList(db.Eleves, "ID_eleve", "nom", notes.fk_note_eleve);
            return View(notes);
        }

        // GET: Notes/Delete/5
        public ActionResult Delete(int? idEleve, int? idCours)
        {
            if (idEleve == null || idCours == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notes notes = db.Notes.Find(idEleve, idCours);
            if (notes == null)
            {
                return HttpNotFound();
            }
            return View(notes);
        }

        // POST: Notes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idEleve, int? idCours)
        {
            Notes notes = db.Notes.Find(idEleve, idCours);
            db.Notes.Remove(notes);
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
