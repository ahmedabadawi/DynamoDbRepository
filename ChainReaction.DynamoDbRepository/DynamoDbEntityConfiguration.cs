using System;
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
        /// <summary>
        /// Defines the table name that the current entity configuration maps to 
        /// </summary>
        public IEntityConfiguration<TEntity> ToTable(string tableName)
        {
            TableName = tableName;

            return this;
        }

        /// <summary>
        /// Defines a hash key field for the current entity with the specified property name
        /// </summary>
        public IFieldConfiguration HasHashKey(string propertyName)
        {
            return HasField(propertyName, true, false);
        }

        /// <summary>
        /// Defines a range key field for the current entity with the specified property name
        /// </summary>
        public IFieldConfiguration HasRangeKey(string propertyName)
        {
            return HasField(propertyName, false, true);
        }

        /// <summary>
        /// Defines a regular field for the current entity with the specified property name
        /// </summary>
        public IFieldConfiguration HasField(string propertyName)
        {
            return HasField(propertyName, false, false);
        }

        /// <summary>
        /// Defines a generic field for the current entity with the specified property name
        /// and if the field is a hash key or range key
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when the field is marked as hash key and range key at the same time</exception>
        public IFieldConfiguration HasField(string propertyName, bool isHashKey, bool isRangeKey)
        {
            var fieldMapping = new DynamoDbFieldConfiguration(propertyName, isHashKey, isRangeKey);

            HasField(fieldMapping);

            return fieldMapping;
        }

        /// <summary>
        /// Defines a generic field for the current entity with the specified field instance
        /// </summary>
        public IFieldConfiguration HasField(IFieldConfiguration field)
        {
            if (field.IsHashKey && field.IsRangeKey) throw new ArgumentException("The field cannot be hash key and range key on the table");

            Fields.Add(field);

            if (field.IsHashKey)
            {
                HashKey = field;
            }
            if (field.IsRangeKey)
            {
                RangeKey = field;
            }

            return field;
        }

        /// <summary>
        /// Defines a set of fields
        /// </summary>
        public IEntityConfiguration<TEntity> HasFields(List<IFieldConfiguration> newFields)
        {
            if(newFields == null) throw new ArgumentNullException("newFields");

            var fieldsValid = true;
            newFields.ForEach(field => fieldsValid &= field.IsValid);
            if(!fieldsValid) throw new ArgumentException("One or more fields are not valid","newFields");

            Fields.AddRange(newFields);

            return this;
        }
        #endregion
    }
}
