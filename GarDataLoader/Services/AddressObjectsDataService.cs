using System.Data;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GarModels;
using GarModels.Services;
using Dapper;

namespace GarDataLoader.Services;

public sealed class AddressObjectsDataService : IAddressObjectsDataService
{
  public const string SqlTableName = "ADDRESS_OBJECTS";
  
  public IAddressObjects LoadFromXmlWholeObject(TextReader reader)
    => (AddressObjects)new XmlSerializer(typeof(AddressObjects)).Deserialize(reader)!;

  public async Task<IAddressObjects> LoadFromXmlWholeObjectAsync(TextReader reader)
    => await Task.Run(() => LoadFromXmlWholeObject(reader));

  public IEnumerable<IAddressObject> LoadFromXmlAsStream(TextReader reader)
    => LoadFromXmlAsStreamUnmapped(reader).Select(MapXmlElementToAddressObject);

  public async IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(TextReader reader)
  {
    await foreach (var element in LoadFromXmlAsStreamUnmappedAsync(reader).ConfigureAwait(false))
    {
      yield return MapXmlElementToAddressObject(element);
    }
  }

  public async Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader)
  {
    var hasTable = (await connection.QueryAsync<int>($"SELECT TOP 1 1 FROM sys.tables WHERE name = '{SqlTableName}'")).Any();
    if (!hasTable)
    {
      await connection.ExecuteAsync($"CREATE TABLE {SqlTableName} (" +
                                    $"[ID] INT NOT NULL DEFAULT(0), " +
                                    $"[OBJECTID] INT NOT NULL DEFAULT(0), " +
                                    $"[OBJECTGUID] CHAR(36) NOT NULL DEFAULT(''), " +
                                    $"[CHANGEID] INT NOT NULL DEFAULT(0), " +
                                    $"[NAME] NVARCHAR(1024) NOT NULL DEFAULT(''), " +
                                    $"[TYPENAME] NVARCHAR(16) NOT NULL DEFAULT(''), " +
                                    $"[LEVEL] INT NOT NULL DEFAULT(0), " +
                                    $"[OPERTYPEID] INT NOT NULL DEFAULT(0), " +
                                    $"[PREVID] INT NOT NULL DEFAULT(0), " +
                                    $"[NEXTID] INT NOT NULL DEFAULT(0), " +
                                    $"[UPDATEDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[STARTDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ENDDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ISACTUAL] BIT NOT NULL DEFAULT(0), " +
                                    $"[ISACTIVE] BIT NOT NULL DEFAULT(0))");
    }
    
    await foreach (var element in LoadFromXmlAsStreamAsync(reader).ConfigureAwait(false))
    {
      await connection.ExecuteAsync(
        $"INSERT INTO {SqlTableName} ([ID], [OBJECTID], [OBJECTGUID], [CHANGEID], " +
        "[NAME], [TYPENAME], [LEVEL], [OPERTYPEID], [PREVID], [NEXTID], [UPDATEDATE], " +
        "[STARTDATE], [ENDDATE], [ISACTUAL], [ISACTIVE]) " +
        $@"@VALUES (@{nameof(AddressObject.Id)}, @{nameof(AddressObject.ObjectId)}, @{nameof(AddressObject.ObjectGuid)}, " +
        $@"@{nameof(AddressObject.ChangeId)}, @{nameof(AddressObject.Name)}, @{nameof(AddressObject.TypeName)}, " +
        $@"@{nameof(AddressObject.Level)}, @{nameof(AddressObject.OperationTypeId)}, @{nameof(AddressObject.PrevId)}, " +
        $@"@{nameof(AddressObject.NextId)}, @{nameof(AddressObject.UpdateDate)}, @{nameof(AddressObject.StartDate)}, " +
        $@"@{nameof(AddressObject.EndDate)}, @{nameof(AddressObject.IsActual)}, @{nameof(AddressObject.IsActive)})", element);
    }
  }

  private static IEnumerable<XElement> LoadFromXmlAsStreamUnmapped(TextReader stream)
  {
    using XmlReader xmlReader = XmlReader.Create(stream);
    xmlReader.MoveToContent();

    while (xmlReader.Read())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "OBJECT", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return element;
      }
    }
  }
  
  private static async IAsyncEnumerable<XElement> LoadFromXmlAsStreamUnmappedAsync(TextReader stream)
  {
    using XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings { Async = true });
    await xmlReader.MoveToContentAsync();

    while (await xmlReader.ReadAsync())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "OBJECT", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return element;
      }
    }
  }
  
  private static AddressObject MapXmlElementToAddressObject(XElement element) => new()
  {
    Id = element.GetInt("ID"),
    ObjectId = element.GetInt("OBJECTID"),
    ObjectGuid = element.GetString("OBJECTGUID"),
    ChangeId = element.GetInt("CHANGEID"),
    Name = element.GetString("NAME"),
    TypeName = element.GetString("TYPENAME"),
    Level = element.GetInt("LEVEL"),
    OperationTypeId = element.GetInt("OPERTYPEID"),
    PrevId = element.GetInt("PREVID"),
    NextId = element.GetInt("NEXTID"),
    UpdateDate = element.GetDateOnly("UPDATEDATE"),
    StartDate = element.GetDateOnly("STARTDATE"),
    EndDate = element.GetDateOnly("ENDDATE"),
    IsActual = element.GetInt("ISACTUAL"),
    IsActive = element.GetInt("ISACTIVE")
  };
}