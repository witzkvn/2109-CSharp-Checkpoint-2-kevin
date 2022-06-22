using NotEduWeb.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NotEduWeb.Tools
{
    public class Utilities
    {
        private Utilities() { }
        private static Utilities _instance;
        public static Utilities GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Utilities();
            }
            return _instance;
        }

        public static double ArrondirNote(double note)
        {
            int integerPart = (int)Math.Round(note, 1);
            double restToRound = note - integerPart;
            if (restToRound < 0.3)
            {
                restToRound = 0;
            }
            else if (restToRound <= 0.5)
            {
                restToRound = 0.5;
            }
            else
            {
                restToRound = 1;
            }
            double noteArrondie = integerPart + restToRound;
            return noteArrondie;
        }

        public static string GetPremiereLettreMajuscule(string mot)
        {
            if (mot.Length == 0)
            {
                return mot;
            }
            else if (mot.Length == 1)
            {
                return "" + char.ToUpper(mot[0]);
            }
            else
            {
                return char.ToUpper(mot[0]) + mot.Substring(1);
            }
        }

        public static string GetNomCompletUser(Eleves eleve)
        {
            if (eleve != null)
                return $"{eleve.nom.ToUpper()} {GetPremiereLettreMajuscule(eleve.prenom)}";
            else return "Eleve";
        }

        public static string ConvertInShortDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy"); ;
        }
    }
}