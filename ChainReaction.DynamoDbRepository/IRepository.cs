namespace ChainReaction.DynamoDbRepository
{
    /// <summary>
    /// Abstracts the basic CRUD operations exposed by repository
    /// </summary>
    /// <typeparam name="TEntity">The entity which the current instance is used to persist to data store</typeparam>
    /// <typeparam name="TKey">They entity key data type used for primary retrieval</typeparam>
    public interface IRepository<TEntity, in TKey> where TEntity : class, new()
    {
        /// <summary>
        /// Creates the specified entity into the data store
        /// </summary>
        void Create(TEntity entity);

        /// <summary>
        /// Updates the specified entity to the data store
        /// </summary>
        TEntity Save(TEntity entity);

        /// <summary>
        /// Retrieves the entity by the specified Id from the data store
        /// </summary>
        TEntity FindById(TKey id);
        
        /// <summary>
        /// Deletes an entity identified by the specified Id from the data store
        /// and returns the deleted entity
        /// </summary>
        TEntity Delete(TKey id);
    }
}
