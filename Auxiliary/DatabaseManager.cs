using MongoDB.Bson;
using MongoDB.Driver;
using Auxiliary.Events;
using Auxiliary.Logging;
using Auxiliary.Configuration;

namespace Auxiliary
{
    public sealed class DatabaseManager
    {
        private readonly static MongoClient? _client;
        private readonly static MongoDatabaseBase? _database;

        static DatabaseManager()
        {
            if (!Configuration<AuxiliarySettings>.Loaded)
                Configuration<AuxiliarySettings>.Load("Auxiliary");

            var url = new MongoUrl(Configuration<AuxiliarySettings>.Settings.ConnectionString);

            _client = new MongoClient(url);

            if (string.IsNullOrEmpty(Configuration<AuxiliarySettings>.Settings.DefaultDbName))
                throw new ArgumentNullException(nameof(Configuration<AuxiliarySettings>.Settings.DefaultDbName));

            if (_client != null)
            {
                _database = _client.GetDatabase(Configuration<AuxiliarySettings>.Settings.DefaultDbName) as MongoDatabaseBase;

                if (!IsConnected)
                    throw new InvalidOperationException("Database could not connect.");
            }
            else
                throw new InvalidOperationException("Client cannot resolve and was found null.");
        }

        /// <summary>
        ///     Checks if the database is connected or not.
        /// </summary>
        public static bool IsConnected
        {
            get
            {
                try
                {
                    _client?.ListDatabaseNames();
                    return true;
                }
                catch (MongoException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        ///     Runs mongo commands as if in shell.
        /// </summary>
        /// <param name="command">The command to run.</param>
        /// <returns>The resulting string.</returns>
        public static string RunCommand(string command)
        {
            try
            {
                var result = _database?.RunCommand<BsonDocument>(BsonDocument.Parse(command));
                return result.ToJson();
            }
            catch (Exception ex) when (ex is FormatException or MongoCommandException)
            {
                throw;
            }
        }

        /// <summary>
        ///     Extracts a collection and its current data from the database collection.
        /// </summary>
        /// <typeparam name="TEntity">The entity to base this collection on.</typeparam>
        /// <param name="collection">The name of the collection.</param>
        /// <returns>An instance of <see cref="MongoCollectionBase{TDocument}"/></returns>
        /// <exception cref="NullReferenceException">Thrown if no collection was found.</exception>
        public static MongoCollectionBase<TEntity> GetCollection<TEntity>(string collection) where TEntity : IEntity
            => _database?.GetCollection<TEntity>(collection) as MongoCollectionBase<TEntity>
            ?? throw new NullReferenceException();
    }
}
