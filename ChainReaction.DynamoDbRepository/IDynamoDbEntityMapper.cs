using Amazon.DynamoDBv2.DocumentModel;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Abstracts the entity mapping between DynamoDb Document model and Entity 
    /// to be used by the repository
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to map to DynamoDb</typeparam>
    public interface IDynamoDbEntityMapper<TEntity> where TEntity : class, new()
    {        
        /// <summary>
        /// Serializes the specified entity using the entity configuration to 
        /// the corresponding DynamoDb Document model
        /// </summary>
        Document Serialize(TEntity entity);
        
        /// <summary>
        /// Deserializes the specified DynamoDb Document model to the 
        /// corresponding entity as per the entity configuration
        /// </summary>
        TEntity Deserialize(Document document);

        /// <summary>
        /// Returns the database table name specified in the entity configuration
        /// </summary>
        string TableName { get; }
    }
}
