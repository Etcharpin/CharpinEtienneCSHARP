using Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public interface ISerialisable                                                      //Interface de serialization
    {
        void serialise(ExplorateurDeFichier o, string path, byte[] key);                //Fonction de serialization d'un objet
        ExplorateurDeFichier deserialise(string path, byte[] key);                      //Fonction de deserialization d'un objet
    }
}
