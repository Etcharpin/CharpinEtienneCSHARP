using Datas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisation
{
    class XMLSerialisation : ISerialisable                                                              //Implemente l'interface 
    {
        public XmlSerializer serializer = new XmlSerializer(typeof(ExplorateurDeFichier));              //Creation du serializeur
        public XMLSerialisation()                                                                       //Constructeur
        {


        }


        public void serialise(ExplorateurDeFichier o, string path, byte[] key)                          //Fonction de serialization d'un objet dans un fichier avec un cle
        {

            FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);          //Fichier d'ecriture

            CryptoStream crStream = new CryptoStream(stream,                                            //Flux chiffré pour serializer
               new DESCryptoServiceProvider().CreateEncryptor(key, key), CryptoStreamMode.Write);

            //Serialisation XML
            serializer.Serialize(crStream, o);                                                          //Seralisation dans le flux chiffré
            crStream.Close();                                                                           //Fermeture de stream ouverts
            stream.Close();
        }

        public ExplorateurDeFichier deserialise(string path, byte[] key)                                 //Fonction de serialization d'un objet depuis un fichier avec un cle
        {



            FileStream stream = new FileStream(path,                                                     //Fichier de lecture
                               FileMode.Open, FileAccess.Read);


            CryptoStream crStream = new CryptoStream(stream,                                            //Flux chiffré pour deserializer
                new DESCryptoServiceProvider().CreateDecryptor(key, key), CryptoStreamMode.Read);

            ExplorateurDeFichier ex = (ExplorateurDeFichier)serializer.Deserialize(crStream);           //Deseralisation dans le flux chiffré
            stream.Close();                                                                             //Fermeture de stream ouverts
            return ex;                                                                                  //Retour de l'objet
        }
    }
}

