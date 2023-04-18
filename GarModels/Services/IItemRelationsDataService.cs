using System.Data;

namespace GarModels.Services;

public interface IItemRelationsDataService
{
  IAsyncEnumerable<IItemRelation> LoadFromXmlAsStreamAsync(TextReader reader);

  Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader);
}