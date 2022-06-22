using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotEduWeb.Models
{
    public class MoyenneParCours
    {
        public decimal Moyenne { get; set; }
        public string Cours { get; set; }
        public int CoursID { get; set; }
        public int Annee { get; set; }
        public string Promotion { get; set; }
        public int PromotionID { get; set; }
    }
}