using System;
using System.Collections.Generic;
using ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.Models;

namespace ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.DataAccess
{
    public class SampleEntityConfiguration
    {
        public static IEntityConfiguration<SampleEntity> InitializeSampleEntity()
        {
            // This section is application specific configuration for the entities to be persisted to DynamoDb
            // Hint: A factory can be used to provide the different entity configurations used by the application
            var entityConfiguration = new DynamoDbEntityConfiguration<SampleEntity>();
            entityConfiguration
                .ToTable("TestTable")
                .HasFields(new List<IFieldConfiguration>
                {
                    new DynamoDbFieldConfiguration("Title"),
                    new DynamoDbFieldConfiguration("Description"),
                    new DynamoDbFieldConfiguration("CreatedOn").OfType(typeof(DateTime)),
                    new DynamoDbFieldConfiguration("UpdatedOn").OfType(typeof(DateTime)),
                    new DynamoDbFieldConfiguration("Price").OfType(typeof (decimal)),
                    new DynamoDbFieldConfiguration("Tags").OfType(typeof(List<string>))
                })
                .HasHashKey("Id").OfType(typeof (Guid));

            return entityConfiguration;
        } 
    }
}
