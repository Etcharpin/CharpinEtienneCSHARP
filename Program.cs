using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Linq;
using Datas;
using Serialisation;

namespace Charpin
{
    class Program
    {

        static void Main(string[] args)
        {

            string Endpath = "\\Documents\\ContactManager" + Environment.UserName + ".db";      //Création du path de l'enregistrement des la database
            string path = @"C:\Users\" + Environment.UserName + Endpath;

            byte[] keybase = new byte[8];                                                       //cle de chiffrement
            var sid = WindowsIdentity.GetCurrent().User;                                        //Utilisateur actuelle de session
            var keysid = new byte[sid.BinaryLength];                                            //Cle a partir du sid de la session
            sid.GetBinaryForm(keysid, 0);                                                       //Transformation en binaire
            keybase = keysid.Take(8).ToArray();                                                 //Raccourssissement dans une chaine de 8 byte

            byte[] key = new byte[8];
            string psswd = "";
            string test = "mdpetienne";                                                         //Mot de passe de la base
            int nbessai = 0;                                                                    //Nombre d'essaie de saisie mot de passe


            ExplorateurDeFichier fic = new ExplorateurDeFichier();
            ExplorateurDeFichier div = new ExplorateurDeFichier();
            ISerialisable serialisable = SerialisationFactory.GetSerialisor("xml");             //Factory de création pour la serialization (xml)
            //ISerialisable serialisable = SerialisationFactory.GetSerialisor("bin");           //Factory de création pour la serialization (binaire)
            if (serialisable != null)
            {
                Console.WriteLine("serialisation ok");                                          //Vérification de la creation du serializable
            }
            else
            {
                Console.WriteLine("Invalide serialisation mode");
            }


            List<Dossier> ordre = new List<Dossier>();                                          //Ordre de la hierarchie des dossier dans l'arbre

            Dossier current = null;                                                             //Dossier courant dans l'insertion
            int nbdossier = 0;

            string userchoose = "";                                                             //Choix du menu dans le switch
            int end = 0;                                                                        //Variable de fin de boucle de menu




            while (end != 1)                                                                    //Boucle de menu
            {
                key = keybase;
                Console.ForegroundColor = ConsoleColor.Green;                                   //Terminal en couleur verte
                Console.Write(">");
                userchoose = Console.ReadLine();                                                //Lecture de l'input
                Console.ForegroundColor = ConsoleColor.White;

                string[] split = userchoose.Split();                                            //Recuperation des arguments
                switch (split[0])
                {


                    case "sortir":                                                              //Cas fin de menu
                        end = 1;
                        break;
                    case "afficher":                                                            //Cas affichage de l'arboressance
                        fic.afficherFichiers();
                        break;
                    case "charger":                                                             //Cas chargement de database 
                        Console.WriteLine("Saisir le mot de passe de la base de donnée");       
                        psswd = ReadPassword();                                                 //Lecture caché du mot de passe
                        nbessai++;
                        while (psswd != test && nbessai < 3)                                    //Test du nombre d'essai de mot de passe
                        {
                            Console.WriteLine("Saisir le mot de passe de la base de donnée");
                            psswd = ReadPassword();
                            nbessai++;
                        }
                        if (psswd != test)
                        {
                            Console.WriteLine("Mot de passe incorrect 3 fois consecutives, suppression de la base de donnée");
                            try
                            {
                                File.Delete(path);                                              //Suppressionde la base si mot de passe incorrect 3 fois
                            }
                            catch (System.IO.FileNotFoundException e)
                            {
                            }

                        }
                        else
                        {
                            Console.WriteLine("Mot de passe correct , voulez vous sair une clé personnelle ? (y/n)");
                            string y = Console.ReadLine();                                      //Lecture de la clé personnelle
                            if (y == "y")
                            {
                                Console.WriteLine("Saissisez une chaine de 8 caractères");
                                string clef = Console.ReadLine();
                                if (clef.Length != 8)                                           //Verification cle
                                {
                                    Console.WriteLine("Erreur de saisie de clé");
                                }
                                else
                                {

                                    try
                                    {
                                        System.Buffer.BlockCopy(clef.ToCharArray(), 0, key, 0, key.Length);
                                        fic = serialisable.deserialise(path, key);              //Deserialization du fichier database
                                        Console.WriteLine("Chargement éffectuer");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Problème de chargement du fichier ( fichier inexistant ou clé érroné)");
                                    }

                                }
                            }
                            else
                            {
                                try
                                {
                                    fic = serialisable.deserialise(path, key);
                                    Console.WriteLine("Chargement éffectuer");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("Problème de chargement du fichier ( fichier inexistant ou clé érroné)");
                                }
                            }

                        }
                        break;
                    case "enregistrer":                                                         //Cas enregistrement du fichier database
                        Console.WriteLine("Voulez vous choisir une clé de chiffrage ? (y/n)");
                        string x = Console.ReadLine();                                          //Lecture de la cle personnelle
                        if (x == "y")
                        {
                            Console.WriteLine("Saissisez une chaine de 8 caractères");
                            string clef = Console.ReadLine();
                            if (clef.Length != 8)                                               //Verification cle
                            {
                                Console.WriteLine("Erreur de saisie de clé");
                            }
                            else
                            {
                                System.Buffer.BlockCopy(clef.ToCharArray(), 0, key, 0, key.Length);
                                serialisable.serialise(fic, path, key);                         //Serialization de l'objet si cle correcte avec cle perso
                                Console.WriteLine("Enregistrement effectué");
                            }
                        }
                        else
                        {
                            serialisable.serialise(fic, path, key);                             //Serialization de l'objet si cle correcte sans cle perso
                            Console.WriteLine("Enregistrement effectué");
                        }


                        break;
                    case "ajouterdossier":                                                     //Cas d'ajout de dossier
                        if (split.Length != 2 && split.Length != 3)
                        {
                            Console.WriteLine("Erreur de syntaxe d'ajout de dossier : ajouterdossier Nomdossier (NomDossierParent)");
                        }
                        else if (split.Length == 2)                                            //Verification du nombre d'arguments
                        {
                            if (current == null)
                            {
                                Dossier d = new Dossier(split[1]);                             //Creation d'un nouveau dossier
                                fic.Dossiers.Add(d);                                           //Ajout du dossier dans l'arboressance
                                Console.WriteLine("Dossier '" + d.Nom + "' ajouté à la racine");
                                current = d;
                                ordre.Add(d);                                                  //Changement de dossier courant
                                nbdossier++;
                            }
                            else
                            {
                                Dossier d = new Dossier(split[1]);
                                current.Dossiers.Add(d);
                                Console.WriteLine("Dossier '" + d.Nom + "' ajouté sous le dossier " + current.Nom);
                                current = d;
                                ordre.Add(d);
                                nbdossier++;

                            }
                        }
                        else
                        {
                            if (Int16.Parse(split[2]) <= nbdossier)                             //Cas de choix de dossier racine
                            {
                                current = ordre[Int16.Parse(split[2])];
                                Dossier d = new Dossier(split[1]);
                                current.Dossiers.Add(d);
                                Console.WriteLine("Dossier '" + d.Nom + "' ajouté sous le dossier " + current.Nom);
                                current = d;
                                ordre.Add(d);
                                nbdossier++;
                            }
                            else
                            {
                                Console.WriteLine("Numéro de dossier inexistant");
                            }

                        }

                        break;
                    case "ajoutercontact":                                                      //Cas ajout de contact dans un dossier
                        if (split.Length != 6)
                        {
                            Console.WriteLine("Erreur de syntaxe d'ajout de Contact : ajoutercontact Prenom Nom Email Société Lien(ami/collegue/relation/reseaux)");
                        }
                        else
                        {
                            if (current == null)
                            {
                                Console.WriteLine("Veuillez creer un dossier avant d'inserer un contact");
                            }
                            else
                            {
                                if (IsValid(split[3]))                                          //Verification d'une addresse mail valide
                                {
                                    try
                                    {
                                        Contact c = new Contact(split[2], split[1], split[3], split[4], (Lien)Enum.Parse(typeof(Lien), split[5]));
                                        current.Contacts.Add(c);                                //Creation du nouveau contact
                                        Console.WriteLine("Contact '" + c.Prenom + " " + c.Nom + "' ajouté sous le dossier " + current.Nom);
                                    }                                                           //Ajout dans la liste de contact
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Erreur de syntaxe d'ajout de Contact : ajoutercontact Prenom Nom Email Société Lien(ami/collegue/relation/reseaux)");

                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Format d'adresse email invalide");
                                }


                            }
                        }

                        break;
                    default:
                        Console.WriteLine("Instruction Inconnue");                              //Affichage des choix du menu
                        Console.WriteLine("La liste des instructions connues est :");
                        Console.WriteLine(" - enregistrer");
                        Console.WriteLine(" - charger");
                        Console.WriteLine(" - ajouterdossier");
                        Console.WriteLine(" - ajoutercontact");
                        Console.WriteLine(" - sortir");
                        break;
                }

            }


            Console.WriteLine(userchoose);
        }



        private static bool IsValid(string email)                                               //Fonction de verification d'addresse mail valide
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }





        public static string ReadPassword()                                                     //Fonction de cachage de mot de passe lors de l'ecriture
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        // Supprimer le dernier caractère du mot de passe
                        password = password.Substring(0, password.Length - 1);

                        // Déplacer le curseur de l'utilisateur en arrière et effacer le caractère affiché
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }
            // Ajouter un saut de ligne
            Console.WriteLine();
            return password;
        }
    }

}
