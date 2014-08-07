using System;
using ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.DataAccess;
using ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.Models;
using Microsoft.Practices.Unity;

namespace ChainReaction.DynamoDbRepository.SampleDynamoDbApplication
{
    public class IocConfig
    {
        public static IUnityContainer Initialize()
        {
            var container = new UnityContainer();

            // Define a singleton instance with the application specific configuration of the entity
            container.RegisterInstance(
                SampleEntityConfiguration.InitializeSampleEntity());

            // Register the concrete implementation for the Entity Mapper
            // Hint: At this point, a custom entity mapper can be injected instead
            container.RegisterType<IDynamoDbEntityMapper<SampleEntity>, GenericDynamoDbEntityMapper<SampleEntity>>();

            // Register the concrete implementation for the repository
            // Hint: At this point, a custom repository implementation can be injected
            // Also the application can introduce extra functionalities for the controller by extending the GenericDynamoDbRepository
            container.RegisterType<IRepository<SampleEntity, Guid>, GenericDynamoDbRepository<SampleEntity, Guid>>();

            return container;
        }
    }
}
