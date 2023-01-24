using Datas;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    class BinSerialisation : ISerialisable                                                                  //Implemente l'interface 
    {
        public IFormatter formatter = new BinaryFormatter();                                                //Creation du formateur

        public BinSerialisation()                                                                           //Constructeur
        {

        }

        public void serialise(ExplorateurDeFichier o, string path, byte[] key)                              //Fonction de serialization d'un objet dans un fichier avec un cle
        {
            FileStream stream = new FileStream(path,                                                        //Fichier d'ecriture
            FileMode.OpenOrCreate, FileAccess.Write);

            CryptoStream crStream = new CryptoStream(stream, new DESCryptoServiceProvider().CreateEncryptor(key, key), CryptoStreamMode.Write);
                                                                                                            //Flux chiffré pour serializer

            formatter.Serialize(crStream, o);                                                               //Serialisation Binaire de l'objet dans le flux chiffré
            crStream.Close();                                                                               //Fermeture de stream ouverts
            stream.Close();

        }

        public ExplorateurDeFichier deserialise(string path, byte[] key)                                    //Fonction de serialization d'un objet depuis un fichier avec un cle
        {

            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);                       //Fichier de lecture

            CryptoStream crStream = new CryptoStream(stream,
                new DESCryptoServiceProvider().CreateDecryptor(key, key), CryptoStreamMode.Read);           //Flux chiffré pour deserializer

            return (ExplorateurDeFichier)formatter.Deserialize(crStream);                                   //Retour de l'objet deseraliser dans le flux chiffré
        }
    }
}
