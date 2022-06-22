using NotEduWeb.Models;
using NotEduWeb.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NotEduWeb.Controllers
{
    public class HistoriqueController : Controller
    {
        // GET: Historique
        public ActionResult Index()
        {
            List<MoyenneParCours> listeItem = DbTools.FetchMoyenneCoursPromo().OrderBy(i => i.Cours).ToList();

            return View("Index", listeItem);
        }
    }
}