using System;
using System.Collections.Generic;
using System.Text;

namespace Datas
{

    [Serializable]                                                                              //Indicateur pour permettre la serialization
    public class Dossier
    {
        public static int nb = 0;                                                               //Attribut static du nombre de dossier crées
        public string Nom;                                                                      //Autres attributs du dossier
        public DateTime DateCreation;
        public DateTime DateModification;
        public List<Dossier> Dossiers;
        public List<Contact> Contacts;



        public Dossier(string nom)                                                              //Constructeur de dossier avec sa liste de contact et de dossier 
        {
            Nom = nom;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
            Dossiers = new List<Dossier>();
            Contacts = new List<Contact>();
        }

        public Dossier()
        {

        }








        public void afficherDossier(int prof)                                                   //Affichage du dossier avec pour paramettre la profondeur de l'arbre
        {
            string tab = "";
            for (int j = 0; j < prof; j++)
            {
                tab += "  ";                                                                    //Ajout d'un espace pour representer les différence de niveau à l'affichage
            }
            Console.WriteLine(tab + "[D] " + Nom + " (creation " + DateCreation.ToString() + ")");
            nb++;
            foreach (Contact con in Contacts)                                                   //Affichage de tous les contacts
            {
                con.afficherContact(nb);
            }

            foreach (Dossier doss in Dossiers)                                                  //Affichage récursif de tous les dossier de la liste de dossier inferieur
            {

                doss.afficherDossier(nb);
                nb--;
            }
        }
    }
}
