using System;
using System.Collections.Generic;
using System.Text;

namespace Datas
{
    [Serializable]                                              //Indicateur pour permettre la serialization
    public class ExplorateurDeFichier
    {

        public List<Dossier> Dossiers;                          //Liste de dossier de l'arboressance



        public ExplorateurDeFichier()                           //Constructeur de l'explorateur de fichier
        {
            Dossiers = new List<Dossier>();
        }






        public void afficherFichiers()                          //Affichage de l'arboressance de dossier
        {

            foreach (Dossier doss in Dossiers)                  //Foreach sur le nombre de dossier de 1er niveau
            {
                doss.afficherDossier(0);
            }

        }
    }
}
