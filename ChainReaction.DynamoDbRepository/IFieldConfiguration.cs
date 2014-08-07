using System;

namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Represents the interface used to hold Field mapping information
    /// </summary>
    public interface IFieldConfiguration
    {
        /// <summary>
        /// Database field name
        /// </summary>
        string FieldName { get; set; }
        
        /// <summary>
        /// Entity field name
        /// </summary>
        string PropertyName { get; set; }
        
        /// <summary>
        /// Field type. Default: string
        /// </summary>
        Type Type { get; set; }
        
        /// <summary>
        /// Indicates if the field is the table's hash key
        /// </summary>
        bool IsHashKey { get; set; }
        
        /// <summary>
        /// Indicates if the field is the table's range key
        /// </summary>
        bool IsRangeKey { get; set; }

        /// <summary>
        /// Validates the field information
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Defines the type of the current field with the specified type
        /// </summary>
        IFieldConfiguration OfType(Type type);

        /// <summary>
        /// Defines the mapping database field for the current property
        /// </summary>
        IFieldConfiguration MapTo(string fieldName);

    }
}
