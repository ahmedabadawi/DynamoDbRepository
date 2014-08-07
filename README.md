# ChainReaction.DynamoDbRepository
ChainReaction.DynamoDbRepository is a library that provides the bases for implementing repository pattern over Amazon DynamoDB
------------------
### Requirements
- Abstract the data store operations
- Provide concrete generic implementation for the entity mappers and repositories
- Have the dependency injection in mind for resolving dependencies between components
------------------
### Description
The library contains abstraction for the data access repository and entity mapping in IRepository and IEntityMapper interfaces respectively
The GenericDynamoDbEntityMapper implements the IEntityMapper and provides reflection based technique to serialize and deserialize entities
The mappers use mapping configuration abstracted by IEntityConfiguration and IFieldConfiguration
A concrete implementation for mapping configuration is provided by DynamoDbEntityConfiguration and DynamoDbFieldConfiguration 
and these implementations provide fluent API calls to construct the entity mapping 
A concrete implementation of the repository is provided by GenericDynamoDbRepository which implements the basic operations of the IRepository
and provides the basis on which application specific repositories can be implemented

------------------
### Future Releases
- Implementation for conditional update
- Implementation for querying
- Update sample to use range key
- Add asynchronous implementations for the repository operations
-----------------
### History
| Version | Description                                      |
|---------|--------------------------------------------------|
| 0.1     | Initial implementation of the repository pattern |