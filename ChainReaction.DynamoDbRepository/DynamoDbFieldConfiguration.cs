using System;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Holds the DynamoDB-Entity field mapping and provides fluent calls to define
    /// the mapping on the field level
    /// </summary>
    public class DynamoDbFieldConfiguration : IFieldConfiguration
    {
        #region Properties

        /// <summary>
        /// Database field name
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// Entity field name
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Field type. Default: string
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Indicates if the field is the table's hash key
        /// </summary>
        public bool IsHashKey { get; set; }

        /// <summary>
        /// Indicates if the field is the table's range key
        /// </summary>
        public bool IsRangeKey { get; set; }

        /// <summary>
        /// Validates the field information
        /// </summary>
        public bool IsValid
        {
            get
            {
                return
                    !string.IsNullOrEmpty(FieldName) &&
                    !string.IsNullOrEmpty(PropertyName) &&
                    (!IsHashKey || !IsRangeKey);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 1-arg constructor that creates a field with the specified propertyName
        /// and marks the field as a normal field not hash key not range key
        /// and defaults the data type to string
        /// and defaults the mapping database field to the same name as the property name 
        /// </summary>
        public DynamoDbFieldConfiguration(string propertyName) : this(propertyName, false, false)
        {
        }

        /// <summary>
        /// 2-args constructor that creates a field with the specified propertyName
        /// and sets the hash key flag according to the isHashKey parameter and marks
        /// the field as not range key
        /// and defaults the data type to string
        /// and defaults the mapping database field to the same name as the property name 
        /// </summary>
        public DynamoDbFieldConfiguration(string propertyName, bool isHashKey) : this(propertyName, isHashKey, false)
        {
        }

        /// <summary>
        /// 3-args constructor that creates a field with the specified propertyName
        /// and sets the hash key flag according to the isHashKey parameter
        /// and sets the range key flag according to the isRangeKey parameter
        /// and defaults the data type to string
        /// and defaults the mapping database field to the same name as the property name 
        /// </summary>
        /// <exception cref="System.ArgumentException">Thrown when the field is marked as hash key and range key at the same time</exception>
        public DynamoDbFieldConfiguration(string propertyName, bool isHashKey, bool isRangeKey)
        {
            if (isHashKey && isRangeKey)
            {
                throw new ArgumentException("The field cannot be hash key and range key on the table");
            }

            PropertyName = propertyName;
            FieldName = propertyName;
            IsHashKey = isHashKey;
            IsRangeKey = isRangeKey;

            Type = typeof (string); //Default data type
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Defines the type of the current field with the specified type
        /// Note: the type is used in serializing and deserializing the entities 
        /// to and from DynamoDb
        /// </summary>
        public DynamoDbFieldConfiguration OfType(Type type)
        {
            Type = type;

            return this;
        }

        /// <summary>
        /// Defines the mapping database field for the current property
        /// Note: If the mapping database field is the same as the property name, 
        /// no need to re-map the field
        /// </summary>
        public DynamoDbFieldConfiguration MapTo(string fieldName)
        {
            FieldName = fieldName;

            return this;
        }

        #endregion
    }
}
