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

    }
}
