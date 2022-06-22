using NotEduWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NotEduWeb.Tools
{
    public static class DbTools
    {
        public static Dictionary<int, string> GetListeDesPromotionsFormattee()
        {
            using (NotEduWebEntities ctx = new NotEduWebEntities()) {
                List<Promotions> promos = ctx.Promotions.ToList();
                Dictionary<int, string> promosFormattee = new Dictionary<int, string>();
                if(promos.Count > 0)
                {
                    foreach (var promo in promos)
                    {
                        promosFormattee.Add(promo.ID_promotion, $"{promo.annee} - {promo.nom}");
                    }
                }
                return promosFormattee;
            }

        }

        public static Dictionary<int, string> GetListeDesElevesFormattee()
        {
            using (NotEduWebEntities ctx = new NotEduWebEntities())
            {
                List<Eleves> eleves = ctx.Eleves.ToList();
                Dictionary<int, string> elevesFormattee = new Dictionary<int, string>();
                if (eleves.Count > 0)
                {
                    foreach (var eleve in eleves)
                    {
                        elevesFormattee.Add(eleve.ID_eleve, Utilities.GetNomCompletUser(eleve));
                    }
                }
                return elevesFormattee;
            }
        }

        public static List<MoyenneParCours> FetchMoyenneCoursPromo()
        {
            List<MoyenneParCours> listeItem = new List<MoyenneParCours>();
            using (var ctx = new NotEduWebEntities())
            {
                listeItem = ctx.Database.SqlQuery<MoyenneParCours>("SELECT AVG(Notes.valeur) AS Moyenne, Cours.nom AS Cours, Cours.ID_cours AS CoursID, Promotions.annee AS Annee, Promotions.nom AS Promotion, Promotions.ID_promotion AS PromotionID FROM Notes INNER JOIN Cours ON  Notes.fk_note_cours = Cours.ID_cours INNER JOIN Eleves ON  Notes.fk_note_eleve = Eleves.ID_eleve INNER JOIN Promotions ON  Eleves.fk_eleve_promotion = Promotions.ID_promotion GROUP BY Cours.nom, Cours.ID_cours, Promotions.nom, Promotions.annee, Promotions.ID_promotion ORDER BY Cours.nom ASC, Promotions.annee ASC;").ToList();
            }

            return listeItem;
        }
    }
}