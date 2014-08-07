using System.Collections.Generic;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Represents the interface used to hold entity mapping information
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to map to DynamoDb</typeparam>
    public interface IEntityConfiguration<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// Database table name
        /// </summary>
        string TableName { get; set; }

        /// <summary>
        /// List of fields and their mapping information
        /// </summary>
        List<IFieldConfiguration> Fields { get; set; }

        /// <summary>
        /// References the hash key field of the table
        /// </summary>
        IFieldConfiguration HashKey { get; set; }

        /// <summary>
        /// References the range key field of the table
        /// </summary>
        IFieldConfiguration RangeKey { get; set; }

        /// <summary>
        /// Validates the entity information
        /// </summary>
        bool IsValid { get; }
        
        /// <summary>
        /// Defines the table name that the current entity configuration maps to 
        /// </summary>
        IEntityConfiguration<TEntity> ToTable(string tableName);

        /// <summary>
        /// Defines a hash key field for the current entity with the specified property name
        /// </summary>
        IFieldConfiguration HasHashKey(string propertyName);

        /// <summary>
        /// Defines a range key field for the current entity with the specified property name
        /// </summary>
        IFieldConfiguration HasRangeKey(string propertyName);

        /// <summary>
        /// Defines a regular field for the current entity with the specified property name
        /// </summary>
        IFieldConfiguration HasField(string propertyName);
        
        /// <summary>
        /// Defines a generic field for the current entity with the specified property name
        /// and if the field is a hash key or range key
        /// </summary>
        IFieldConfiguration HasField(string propertyName, bool isHashKey, bool isRangeKey);

        /// <summary>
        /// Defines a generic field for the current entity with the specified field instance
        /// </summary>
        IFieldConfiguration HasField(IFieldConfiguration field);

        /// <summary>
        /// Defines a set of fields
        /// </summary>
        IEntityConfiguration<TEntity> HasFields(List<IFieldConfiguration> fields);
    }
}
