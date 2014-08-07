using System;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Generic implementation of the basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to persist to DynamoDb</typeparam>
    /// <typeparam name="TKey">They entity key data type used for primary retrieval</typeparam>
    public class GenericDynamoDbRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, new()
    {
        #region Constructors
        /// <summary>
        /// Constructor with entity mapper injected that initializes the repository
        /// and creates a DynamoDb Client instance and loads DynamoDb Table instance
        /// </summary>
        /// <param name="entityMapper"></param>
        public GenericDynamoDbRepository(IDynamoDbEntityMapper<TEntity> entityMapper)
        {
            EntityMapper = entityMapper;

            Client = new AmazonDynamoDBClient();

            var tableName = EntityMapper.TableName;
            
            EntityTable = Table.LoadTable(Client, tableName);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates the specified entity into the DynamoDb
        /// </summary>
        public void Create(TEntity entity)
        {
            var entityDocument = EntityMapper.Serialize(entity);

            EntityTable.PutItem(entityDocument);
        }

        /// <summary>
        /// Updates the specified entity to the DynamoDb
        /// </summary>
        public void Save(TEntity entity)
        {
            //TODO: Add logic to handle the update
            throw new NotImplementedException();
        }

        /// <summary>
        /// Retrieves the entity by the specified Id from DynamoDb
        /// and returns null if entity is not found
        /// </summary>
        public TEntity FindById(TKey id)
        {
            dynamic idValue = id;
            var entityDocument = EntityTable.GetItem(idValue);

            var entity = EntityMapper.Deserialize(entityDocument);

            return entity;
        }

        /// <summary>
        /// Deletes an entity identified by the specified Id from the data store
        /// and returns the deleted entity
        /// </summary>
        public TEntity Delete(TKey id)
        {
            //TODO: Add logic to handle the delete operation 
            throw new NotImplementedException();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Provides the entity mapper used by the current repository
        /// </summary>
        public IDynamoDbEntityMapper<TEntity> EntityMapper { get; private set; }
        #endregion

        #region Protected Properties
        /// <summary>
        /// Provides the Amazon DynamoDb Table instance as per the entity mapper
        /// </summary>
        protected Table EntityTable { get; private set; }
        /// <summary>
        /// Provides the Amazon DynamoDb Client instance
        /// </summary>
        protected AmazonDynamoDBClient Client { get; private set; }
        #endregion
    }
}
