using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace MinApiMongoDb.Repositories;
public class MongoDbRepository : IMongoDbRepository
{
    public FilterDefinition<T> Filter<T>(string field, object item)
    {
        var filter = Builders<T>.Filter.Eq(field, item);
        return filter;
    }

    public FilterDefinition<T> FilterContains<T>(string field, string item)
    {
        var query = new BsonRegularExpression(new Regex(item, RegexOptions.IgnoreCase));
        var filter = Builders<T>.Filter.Regex(field, query);
        return filter;
    }

    public SortDefinition<T> Sort<T>(string field)
    {
        var sort = Builders<T>.Sort.Ascending(field);
        return sort;
    }

    public async Task<List<T>> GetList<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter)
    {
        IAsyncCursor<T>? cursor = await collection.FindAsync<T>(filter);
        List<T>? result = await cursor.ToListAsync();
        return result;
    }

    public async Task<List<T>> GetListSorted<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter, SortDefinition<T>? sort)
    {
        var filters = filter == null ? Builders<T>.Filter.Empty : filter;
        List<T>? result = await collection.Find(filters).Sort(sort).ToListAsync();
        return result;
    }

    public async Task<T> GetOne<T>(IMongoCollection<T> collection, FilterDefinition<T>? filter)
    {
        IAsyncCursor<T>? cursor = await collection.FindAsync<T>(filter);
        T? result = await cursor.FirstOrDefaultAsync();
        return result;
    }
}