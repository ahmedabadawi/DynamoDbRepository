using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.IdentityManagement.Model;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Responsible for the serialization and deserialization of entities of type TEntity
    /// using the entity configuration injected from the constructor
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to map to DynamoDb</typeparam>
    public class GenericDynamoDbEntityMapper<TEntity> : IDynamoDbEntityMapper<TEntity> where TEntity : class, new()
    {
        #region Constructors
        /// <summary>
        /// Constructor with entity configuration injection that initializes the 
        /// generic entity mapper and default data type mapping
        /// </summary>
        public GenericDynamoDbEntityMapper(IEntityConfiguration<TEntity> entityConfiguration)
        {
            if (entityConfiguration == null)
            {
                throw new ArgumentNullException("entityConfiguration");
            }

            EntityConfiguration = entityConfiguration;

            InitializeDataTypeMapping();
            if (DataTypeMapping == null)
            {
                throw new ArgumentException("DataTypeMapping is not initialized");
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Serializes the specified entity using the entity configuration to 
        /// the corresponding DynamoDb Document model
        /// </summary>
        public Document Serialize(TEntity entity)
        {
            if (entity == null) return null;

            var entityDocument = new Document();

            if (EntityConfiguration != null)
            {
                foreach (var field in EntityConfiguration.Fields)
                {
                    dynamic value = entity.GetType().GetProperty(field.PropertyName).GetValue(entity, null);
                    
                    entityDocument[field.FieldName] = value;
                }
            }

            return entityDocument;
        }

        /// <summary>
        /// Deserializes the specified DynamoDb Document model to the 
        /// corresponding entity as per the entity configuration
        /// </summary>
        public TEntity Deserialize(Document document)
        {
            if (document == null) return null;

            var entity = Activator.CreateInstance<TEntity>();

            if (EntityConfiguration != null)
            {
                foreach (var field in EntityConfiguration.Fields)
                {
                    dynamic value = DataTypeMapping[field.Type](document[field.FieldName]);
                    entity.GetType().GetProperty(field.PropertyName).SetValue(entity, value);
                }
            }

            return entity;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Returns the database table name specified in the entity configuration
        /// </summary>
        public string TableName
        {
            get { return EntityConfiguration.TableName; }
        }
        #endregion
        #region Protected Methods

        /// <summary>
        /// Initializes the default data type mapping methods
        /// Note: this method can be overridden to provide special data type deserialization
        /// </summary>
        protected void InitializeDataTypeMapping()
        {
            DataTypeMapping =
                new Dictionary<Type, Func<DynamoDBEntry, dynamic>>
                {
                    {typeof (string), value => value.AsString()},
                    {typeof (Guid), value => value.AsGuid()},
                    {typeof (decimal), value => value.AsDecimal()},
                    {typeof (bool), value => value.AsBoolean()},
                    {typeof (DateTime), value => value.AsDateTime()},
                    {typeof (List<string>), value => value.AsListOfString()}

                };
        }

        #endregion

        #region Protected Properties
        /// <summary>
        /// Provides access to the entity configuration
        /// </summary>
        protected IEntityConfiguration<TEntity> EntityConfiguration { get; private set; }
        
        /// <summary>
        /// Provides the mapping methods corresponding to each type
        /// </summary>
        protected Dictionary<Type, Func<DynamoDBEntry, dynamic>> DataTypeMapping { get; set; }
        #endregion
    }
}
