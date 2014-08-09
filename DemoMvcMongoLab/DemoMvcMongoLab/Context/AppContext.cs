using MongoDB.Driver;

namespace DemoMvcMongoLab.Context
{
    public class AppContext
    {
        public MongoDatabase Database;
        readonly string connnectionstring = @"mongodb://DemoMvcMongoLab:z25DOUq0grml4Wy6uLwN3Wlg.ZZWdyGXkxIc0A5Hh0s-@ds045077.mongolab.com:45077/DemoMvcMongoLab";
        readonly string databaseName = "DemoMvcMongoLab";

        public AppContext()
        {
            var mongoClient = new MongoClient(connnectionstring);
            var server = mongoClient.GetServer();
            Database = server.GetDatabase(databaseName);
        }
    }
}