using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo.DataBaseContext
{
    public class DBContext: IDBContext
    {
        private MongoDatabaseBase _dbCTX { get; set; }
        private DatabaseSettings _dbSettings { get; set; }
        public MongoDatabaseBase DBCTX 
        { 
            get
            {
                CreateDBContext();
                return _dbCTX;
            }
        }
        private IMongoClient mongoDBClient  { get; set; }
        public DBContext(DatabaseSettings dbSettings)
        {
            _dbSettings = dbSettings;
            CreateDBContext();
        }
        private void CreateDBContext()
        {
            if (_dbCTX == null)
            {

                mongoDBClient = new MongoClient(_dbSettings.ConnectionString);
                _dbCTX = (MongoDatabaseBase)mongoDBClient.GetDatabase(_dbSettings.DBName);
            }
        }
    }

   
}
