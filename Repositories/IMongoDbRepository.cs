using MongoDB.Driver;

namespace MinApiMongoDb.Repositories
{
    public interface IMongoDbRepository
    {
        FilterDefinition<T> Filter<T>(string field, object item);
        FilterDefinition<T> FilterContains<T>(string field, string item);
        SortDefinition<T> Sort<T>(string field);
        Task<List<T>> GetList<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter);
        Task<List<T>> GetListSorted<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter, SortDefinition<T>? sort);
        Task<T> GetOne<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter);
    }
}