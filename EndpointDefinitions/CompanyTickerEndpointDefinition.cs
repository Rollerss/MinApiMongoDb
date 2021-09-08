using MinApiMongoDb.Models;
using MinApiMongoDb.Repositories;
using MongoDB.Driver;

namespace MinApiMongoDb.EndpointDefinitions;
public class CompanyTickerEndpointDefinition : IEndpointDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("/companyTickers", GetAllCompanyTickers<CompanyTicker>);
        app.MapGet("/companyByName/{name}", GetCompanyByName<CompanyTicker>);
        app.MapGet("/companyByTicker/{ticker}", GetCompanyByTicker<CompanyTicker>);
        app.MapGet("/companyByCik/{cik}", GetCompanyByCik<CompanyTicker>);
    }

    internal async Task<List<T>> GetAllCompanyTickers<T>(IMongoDbRepository repo, IMongoCollection<T> collection)
    {
        var sort = repo.Sort<T>("title");
        var result = await repo.GetListSorted(collection, null, sort);
        return result;
    }

    internal async Task<List<T>> GetCompanyByName<T>(IMongoDbRepository repo, IMongoCollection<T> collection, string name)
    {
        var filter = repo.FilterContains<T>("title", name);
        var result = await repo.GetList(collection, filter);
        return result;
    }

    internal async Task<T> GetCompanyByTicker<T>(IMongoDbRepository repo, IMongoCollection<T> collection, string ticker)
    {
        return await GetOne(repo, collection, "ticker", ticker);
    }

    internal async Task<T> GetCompanyByCik<T>(IMongoDbRepository repo, IMongoCollection<T> collection, int cik)
    {
        return await GetOne(repo, collection, "cik_str", cik);
    }

    internal async Task<T> GetOne<T>(IMongoDbRepository repo, IMongoCollection<T> collection, string field, object item)
    {
        var filter = repo.Filter<T>(field, item);
        var result = await repo.GetOne(collection, filter);
        return result;
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<IMongoCollection<CompanyTicker>, MongoCollectionBase<CompanyTicker>>(c =>
        {
            string tickers = c.GetRequiredService<IConfiguration>()["MongoTickers"];
            string database = c.GetRequiredService<IConfiguration>()["MongoSecDb"];
            var collection = new MongoClient().GetDatabase(database).GetCollection<CompanyTicker>(tickers);
            return (MongoCollectionBase<CompanyTicker>)collection;
        });
        services.AddSingleton<IMongoDbRepository, MongoDbRepository>();
    }
}
