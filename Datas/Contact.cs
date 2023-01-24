using System;
using System.Collections.Generic;
using System.Text;

namespace Datas
{
    [Serializable]                                                                              //Indicateur pour permettre la serialization
    public class Contact
    {

        public string Nom;                                                                      //Attribut de l'objet contact
        public string Prenom;
        public string Courriel;
        public string Societe;
        public Lien Link;
        public DateTime DateCreation;
        public DateTime DateModification;


        public Contact(string nom, string prenom, string courriel, string societe, Lien link)  //Constructeur de contact 
        {
            Nom = nom;
            Prenom = prenom;
            Courriel = courriel;
            Societe = societe;
            Link = link;
            DateCreation = DateTime.Now;
            DateModification = DateTime.Now;
        }

        public Contact()
        {

        }





        public void afficherContact(int nb)                                                 //Affichage du contact avec ses attributs 
        {
            string tab = "";
            for (int j = 0; j < nb; j++)
            {
                tab += "  ";                                                                //Ajout d'un espace pour representer les différence de niveau à l'affichage
            }
            Console.WriteLine(tab + "  |   [C]  " + Prenom + " " + Nom + " (" + Societe + "), " + Courriel + ", " + Link);
        }


    }
}
