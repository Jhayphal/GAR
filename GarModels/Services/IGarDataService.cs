using System.Data;

namespace GarModels.Services;

public interface IGarDataService<out TObject> where TObject : IGarObject
{
  IAsyncEnumerable<TObject> LoadFromXmlAsStreamAsync(TextReader reader);

  Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader);
}