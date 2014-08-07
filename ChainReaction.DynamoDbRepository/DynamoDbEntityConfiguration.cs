using System.Collections.Generic;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Holds the DynamoDB-Entity mapping and provides fluent calls to define
    /// the mapping on the entity level including its fields
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to map to DynamoDb</typeparam>
    public class DynamoDbEntityConfiguration<TEntity> : IEntityConfiguration<TEntity> where TEntity : class, new()
    {
        #region Properties

        /// <summary>
        /// Database table name
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// List of fields and their mapping information
        /// </summary>
        public List<IFieldConfiguration> Fields { get; set; }

        /// <summary>
        /// References the hash key field of the table
        /// </summary>
        public IFieldConfiguration HashKey { get; set; }

        /// <summary>
        /// References the range key field of the table
        /// </summary>
        public IFieldConfiguration RangeKey { get; set; }

        /// <summary>
        /// Validates the entity information
        /// </summary>
        public bool IsValid
        {
            get
            {
                var fieldsValid = true;
                Fields.ForEach(field => fieldsValid &= field.IsValid);

                return
                    !string.IsNullOrEmpty(TableName) &&
                    (HashKey != null) &&
                    fieldsValid;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// No-args constructor for the entity configuration
        /// </summary>
        public DynamoDbEntityConfiguration()
        {
            Fields = new List<IFieldConfiguration>();
        }

        #endregion

        #region Public Methods

        public DynamoDbEntityConfiguration<TEntity> ToTable(string tableName)
        {
            TableName = tableName;

            return this;
        }

        public IFieldConfiguration HasHashKey(string propertyName)
        {
            return HasField(propertyName, true, false);
        }

        public IFieldConfiguration HasRangeKey(string propertyName)
        {
            return HasField(propertyName, false, true);
        }

        public IFieldConfiguration HasField(string propertyName)
        {
            return HasField(propertyName, false, false);
        }

        public IFieldConfiguration HasField(string propertyName, bool isHashKey, bool isRangeKey)
        {
            var fieldMapping = new DynamoDbFieldConfiguration(propertyName, isHashKey, isRangeKey);

            Fields.Add(fieldMapping);
            if (fieldMapping.IsHashKey)
            {
                HashKey = fieldMapping;
            }
            if (fieldMapping.IsRangeKey)
            {
                RangeKey = fieldMapping;
            }

            return fieldMapping;
        }


        #endregion
    }
}
