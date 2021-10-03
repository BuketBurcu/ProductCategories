using MongoDB.Driver;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductCategories.DataAccess
{
    public class MongoRepository<T> : IRepository<T>, IDisposable where T : class
    {
        private IMongoDatabase _database;
        private IMongoCollection<T> _collection;
        private MongoClient _client;

        public MongoRepository()
        {
            GetDatabase();
            GetCollection();
        }

        private void GetCollection()
        {
            if (_database.GetCollection<T>(typeof(T).Name) == null)
                _database.CreateCollection(typeof(T).Name);
            _collection = _database.GetCollection<T>(typeof(T).Name);
        }
        private void GetDatabase()
        {
            _client = new MongoClient(Environment.GetEnvironmentVariable("MONGO_URI"));
            _database = _client.GetDatabase(Environment.GetEnvironmentVariable("MONGO_DB"));
        }
        public void Add(T entity)
        {
            this._collection.InsertOne(entity);
        }

        public void Delete(Expression<Func<T, bool>> predicate, bool forceDelete = false)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            this._collection.DeleteMany(filter);
        }

        public void Update(Expression<Func<T, bool>> predicate, T entity)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            this._collection.ReplaceOneAsync(filter, entity);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            return (int)this._collection.Find(filter).Count();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            return this._collection.Find(filter).ToEnumerable().AsQueryable();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            return this._collection.Find(filter).FirstOrDefault();
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Where(predicate);
            return this._collection.Find(filter).FirstOrDefault() != null;
        }

        //public bool Update(Expression<Func<T, bool>> predicate, T entity)
        //{
        //    _collection.ReplaceOneAsync(predicate, entity);
        //    return true;
        //}
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity, bool forceDelete = false)
        {
            throw new NotImplementedException();
        }
        public void Delete<TField>(FieldDefinition<T, TField> field, TField date)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Lte(field, date);
            this._collection.DeleteMany(filter);
        }

        public void DeleteEq<TField>(FieldDefinition<T, TField> field, TField eq)
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq(field, eq);
            this._collection.DeleteMany(filter);
        }

        public void Dispose()
        {

        }

        public IQueryable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
