using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIs
{
    public class MongoBDConnector
    {
        public bool connect()
        {
            var settings = MongoClientSettings.FromConnectionString(
                "mongodb://admin:xq3eKKtRrs2CApC@g3cluster-shard-00-00.ro39n.mongodb.net:27017," +
                "g3cluster-shard-00-01.ro39n.mongodb.net:27017," +
                "g3cluster-shard-00-02.ro39n.mongodb.net:27017/myFirstDatabase?" +
                "ssl=true&replicaSet=atlas-g88vwx-shard-0&authSource=admin&retryWrites=true&w=majority");

            var client = new MongoClient(settings);
            var database = client.GetDatabase("test");

            return true;
        }
    }
}