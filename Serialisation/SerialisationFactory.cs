using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public static class SerialisationFactory                                                        //Factory de creation du serializer
    {

        public static ISerialisable GetSerialisor(string serialiseType)                             //Fonction factory
        {

            ISerialisable serialisator = null;

            if (serialiseType == "bin")                                                             //Choix de type de serialisation binaire
            {
                Console.WriteLine("ici");
                serialisator = new BinSerialisation();                                              //Creation de serialisation binaire
            }
            else if (serialiseType == "xml")                                                        //Choix de type de serialisation xml
            {
                serialisator = new XMLSerialisation();                                              //Creation de serialisation xml
            }   

            return serialisator;                                                                    //Retour du serializer
        }
    }
}
