using System.Data;

namespace GarModels.Services;

public interface IAddressObjectsDataService
{
  IAddressObjects LoadFromXmlWholeObject(TextReader reader);

  Task<IAddressObjects> LoadFromXmlWholeObjectAsync(TextReader reader);

  IEnumerable<IAddressObject> LoadFromXmlAsStream(TextReader reader);

  IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(TextReader reader);

  Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader);
}