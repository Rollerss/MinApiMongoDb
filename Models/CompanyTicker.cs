using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MinApiMongoDb.Models;
[BsonIgnoreExtraElements]
public class CompanyTicker
{
    //[BsonId]
    //public ObjectId Id {  get; set; }
    [BsonElement("cik_str")]
    public int Cik {  get; set; }
    [BsonElement("ticker")]
    public string Ticker {  get; set; }
    [BsonElement("title")]
    public string Title {  get; set; }
}
