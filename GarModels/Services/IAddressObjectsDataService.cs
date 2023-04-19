using System.Data;

namespace GarModels.Services;

public interface IAddressObjectsDataService
{
  IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(TextReader reader);

  Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader);
}