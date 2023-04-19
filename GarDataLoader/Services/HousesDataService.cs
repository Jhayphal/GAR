using System.Data;
using System.Xml;
using System.Xml.Linq;
using Dapper;
using GarModels;
using GarModels.Services;

namespace GarDataLoader.Services;

public class HousesDataService : IGarDataService<IHouse>
{
  public async IAsyncEnumerable<IHouse> LoadFromXmlAsStreamAsync(TextReader reader)
  {
    await foreach (var element in LoadFromXmlAsStreamUnmappedAsync(reader).ConfigureAwait(false))
    {
      yield return MapXmlElementToRelationObject(element);
    }
  }

  public async Task InsertRecordsFromAsync(IDbConnection connection, string tableName, TextReader reader)
  {
    var hasTable = (await connection.QueryAsync<int>($"SELECT TOP 1 1 FROM sys.tables WHERE name = '{tableName}'")).Any();
    if (!hasTable)
    {
      await connection.ExecuteAsync($"CREATE TABLE {tableName} (" +
                                    $"[ID] INT NOT NULL DEFAULT(0), " +
                                    $"[OBJECTID] INT NOT NULL DEFAULT(0), " +
                                    $"[OBJECTGUID] CHAR(36) NOT NULL DEFAULT(''), " +
                                    $"[CHANGEID] INT NOT NULL DEFAULT(0), " +
                                    $"[HOUSENUM] NVARCHAR(64) NOT NULL DEFAULT(0), " +
                                    $"[HOUSETYPE] INT NOT NULL DEFAULT(0), " +
                                    $"[CITYCODE] INT NOT NULL DEFAULT(0), " +
                                    $"[OPERTYPEID] INT NOT NULL DEFAULT(0), " +
                                    $"[PREVID] INT NOT NULL DEFAULT(0), " +
                                    $"[NEXTID] INT NOT NULL DEFAULT(0), " +
                                    $"[UPDATEDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[STARTDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ENDDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ISACTUAL] BIT NOT NULL DEFAULT(0), " +
                                    $"[ISACTIVE] BIT NOT NULL DEFAULT(0))");
    }

    IHouse house;
    
    try
    {
      await foreach (var element in LoadFromXmlAsStreamAsync(reader).ConfigureAwait(false))
      {
        house = element;
        await connection.ExecuteAsync(
          $"INSERT INTO {tableName} ([ID], [OBJECTID], [OBJECTGUID], [CHANGEID], " +
          "[HOUSENUM], [HOUSETYPE], [OPERTYPEID], [PREVID], [NEXTID], " +
          "[UPDATEDATE], [STARTDATE], [ENDDATE], [ISACTUAL], [ISACTIVE]) " +
          $@"VALUES (@{nameof(element.Id)}, @{nameof(element.ObjectId)}, @{nameof(element.ObjectGuid)}, " +
          $@"@{nameof(element.ChangeId)}, @{nameof(element.HouseNumber)}, @{nameof(element.HouseType)}, " +
          $@"@{nameof(element.OperationTypeId)}, @{nameof(element.PrevId)}, @{nameof(element.NextId)}, " +
          $@"@{nameof(element.UpdateDate)}, @{nameof(element.StartDate)}, @{nameof(element.EndDate)}, " +
          $@"@{nameof(element.IsActual)}, @{nameof(element.IsActive)})", element);
      }
    }
    catch (Exception e)
    {
      System.Diagnostics.Debug.WriteLine(e.Message);
    }
  }
  
  private static async IAsyncEnumerable<XElement> LoadFromXmlAsStreamUnmappedAsync(TextReader reader)
  {
    using XmlReader xmlReader = XmlReader.Create(reader, new XmlReaderSettings { Async = true });
    await xmlReader.MoveToContentAsync();

    while (await xmlReader.ReadAsync())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "HOUSE", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return element;
      }
    }
  }

  private static House MapXmlElementToRelationObject(XElement element) => new()
  {
    Id = element.GetInt("ID"),
    ObjectId = element.GetInt("OBJECTID"),
    ObjectGuid = element.GetString("OBJECTGUID"),
    ChangeId = element.GetInt("CHANGEID"),
    HouseNumber = element.GetString("HOUSENUM"),
    HouseType = element.GetInt("HOUSETYPE"),
    OperationTypeId = element.GetInt("OPERTYPEID"),
    PrevId = element.GetInt("PREVID"),
    NextId = element.GetInt("NEXTID"),
    UpdateDate = element.GetDateTime("UPDATEDATE"),
    StartDate = element.GetDateTime("STARTDATE"),
    EndDate = element.GetDateTime("ENDDATE"),
    IsActual = element.GetInt("ISACTUAL"),
    IsActive = element.GetInt("ISACTIVE")
  };
}