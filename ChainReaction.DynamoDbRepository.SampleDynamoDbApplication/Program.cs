using System;
using System.Collections.Generic;
using System.Text;
using ChainReaction.DynamoDbRepository.SampleDynamoDbApplication.Models;
using Microsoft.Practices.Unity;

namespace ChainReaction.DynamoDbRepository.SampleDynamoDbApplication
{
    class Program
    {
        private static IUnityContainer _container;
        static void Main()
        {
            // Initialize the dependency injection - Here I'm using Unity
            _container = IocConfig.Initialize();

            TestSampleRepository();

            Console.Read();
        }

        private static void TestSampleRepository()
        {
            // Using the dependency injection container, resolve the repository
            // which in turn identifies the required dependencies, IDynamoDbEntityMapper, 
            // and inject the instance to the repository.
            // The entity mapper depends on the entity configuration defined as a singleton
            // in the dependency injection container. Refer to IocConfig
            var sampleEntityRepository = _container.Resolve<IRepository<SampleEntity, Guid>>();

            // Start Tests
            Console.WriteLine("Start Test Create");
            var createdId = TestCreate(sampleEntityRepository);
            Console.WriteLine("Test Create Finished - Id: {0}",createdId);

            Console.WriteLine("Start Test Retrieve");
            var retrievedEntity = TestRetrieve(sampleEntityRepository, createdId);
            Console.WriteLine("Retrieved Entity:");
            PrintSampleEntity(retrievedEntity);
            Console.WriteLine("Test Retrieve Finished");

            Console.WriteLine("Start Test Update");
            var updatedEntity = TestUpdate(sampleEntityRepository, createdId);

            Console.WriteLine("Updated Entity:");
            PrintSampleEntity(updatedEntity);
            Console.WriteLine("Test Updated Finished");

            Console.WriteLine("Start Test Delete");
            var deletedEntity = TestDelete(sampleEntityRepository, createdId);
            Console.WriteLine("Deleted Entity:");
            PrintSampleEntity(deletedEntity);
            Console.WriteLine("Test Delete Finished");

        }
        private static Guid TestCreate(IRepository<SampleEntity,Guid> sampleEntityRepository)
        {
            var entity = new SampleEntity
            {
                Id = Guid.NewGuid(),
                Title = "Sample Record 001",
                Description =
                    "Senectus eros. Sit sodales sem. Semper senectus elit aliquam vulputate. Inceptos Velit adipiscing tincidunt et vulputate a inceptos phasellus pretium",
                CreatedOn = DateTime.UtcNow,
                Price = 241.84M,
                Tags = new List<string> {"Senectus", "Semper", "Inceptos"}
            };

            sampleEntityRepository.Create(entity);

            return entity.Id;
        }

        private static SampleEntity TestRetrieve(IRepository<SampleEntity, Guid> sampleEntityRepository, Guid idToRetrieve)
        {
            var retrievedEntity = sampleEntityRepository.FindById(idToRetrieve);

            return retrievedEntity;
        }

        private static SampleEntity TestUpdate(IRepository<SampleEntity, Guid> sampleEntityRepository, Guid idToUpdate)
        {
            var entity = sampleEntityRepository.FindById(idToUpdate);

            entity.UpdatedOn = DateTime.UtcNow;
            entity.Tags.Add("New Tag");

            var updatedEntity = sampleEntityRepository.Save(entity);

            return updatedEntity;
        }
        private static SampleEntity TestDelete(IRepository<SampleEntity, Guid> sampleEntityRepository, Guid idToRetrieve)
        {
            var deletedEntity = sampleEntityRepository.Delete(idToRetrieve);

            return deletedEntity;
        }

        private static void PrintSampleEntity(SampleEntity entity)
        {
            var tagsBuilder = new StringBuilder("[");
            entity.Tags.ForEach(tag => tagsBuilder.AppendFormat("{0} ,", tag));
            tagsBuilder.Remove(tagsBuilder.Length - 2, 2);
            tagsBuilder.Append("]");
            Console.WriteLine("Id: {0}\r\nTitle: {1}\r\nDescription: {2}\r\nCreatedOn: {3}\r\nUpdated On: {4}\r\nPrice: {5},Tags: {6}",
                entity.Id,
                entity.Title,
                entity.Description,
                entity.CreatedOn,
                entity.UpdatedOn,
                entity.Price,
                tagsBuilder);
        }
    }
}
