using System.Data;
using System.Xml;
using System.Xml.Linq;
using Dapper;
using GarModels;
using GarModels.Services;

namespace GarDataLoader.Services;

public class ItemRelationsDataService : IItemRelationsDataService
{
  public async IAsyncEnumerable<IItemRelation> LoadFromXmlAsStreamAsync(TextReader reader)
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
                                    $"[PARENTOBJID] INT NOT NULL DEFAULT(0), " +
                                    $"[CHANGEID] INT NOT NULL DEFAULT(0), " +
                                    $"[REGIONCODE] INT NOT NULL DEFAULT(0), " +
                                    $"[AREACODE] INT NOT NULL DEFAULT(0), " +
                                    $"[CITYCODE] INT NOT NULL DEFAULT(0), " +
                                    $"[PLACECODE] INT NOT NULL DEFAULT(0), " +
                                    $"[PLANCODE] INT NOT NULL DEFAULT(0), " +
                                    $"[STREETCODE] INT NOT NULL DEFAULT(0), " +
                                    $"[PREVID] INT NOT NULL DEFAULT(0), " +
                                    $"[NEXTID] INT NOT NULL DEFAULT(0), " +
                                    $"[UPDATEDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[STARTDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ENDDATE] DATE NOT NULL DEFAULT('0001-01-01'), " +
                                    $"[ISACTIVE] BIT NOT NULL DEFAULT(0), " +
                                    $"[PATH] VARCHAR(2048) NOT NULL DEFAULT(''))");
    }

    try
    {
      await foreach (var element in LoadFromXmlAsStreamAsync(reader).ConfigureAwait(false))
      {
        await connection.ExecuteAsync(
          $"INSERT INTO {tableName} ([ID], [OBJECTID], [PARENTOBJID], [CHANGEID], " +
          "[REGIONCODE], [AREACODE], [CITYCODE], [PLACECODE], [PLANCODE], [STREETCODE], " +
          "[PREVID], [NEXTID], [UPDATEDATE], [STARTDATE], [ENDDATE], [ISACTIVE], [PATH]) " +
          $@"VALUES (@{nameof(ItemRelation.Id)}, @{nameof(ItemRelation.ObjectId)}, @{nameof(ItemRelation.ParentObjectId)}, " +
          $@"@{nameof(ItemRelation.ChangeId)}, @{nameof(ItemRelation.RegionCode)}, @{nameof(ItemRelation.AreaCode)}, " +
          $@"@{nameof(ItemRelation.CityCode)}, @{nameof(ItemRelation.PlaceCode)}, @{nameof(ItemRelation.PlanCode)}, " +
          $@"@{nameof(ItemRelation.StreetCode)}, @{nameof(ItemRelation.PrevId)}, @{nameof(ItemRelation.NextId)}, " +
          $@"@{nameof(ItemRelation.UpdateDate)}, @{nameof(ItemRelation.StartDate)}, @{nameof(ItemRelation.EndDate)}, " +
          $@"@{nameof(ItemRelation.IsActive)}, @{nameof(ItemRelation.Path)})", element);
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
          && string.Equals(xmlReader.Name, "ITEM", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return element;
      }
    }
  }

  private static ItemRelation MapXmlElementToRelationObject(XElement element) => new()
  {
    Id = element.GetInt("ID"),
    ObjectId = element.GetInt("OBJECTID"),
    ParentObjectId = element.GetInt("PARENTOBJID"),
    ChangeId = element.GetInt("CHANGEID"),
    RegionCode = element.GetInt("REGIONCODE"),
    AreaCode = element.GetInt("AREACODE"),
    CityCode = element.GetInt("CITYCODE"),
    PlaceCode = element.GetInt("PLACECODE"),
    PlanCode = element.GetInt("PLANCODE"),
    StreetCode = element.GetInt("STREETCODE"),
    PrevId = element.GetInt("PREVID"),
    NextId = element.GetInt("NEXTID"),
    UpdateDate = element.GetDateTime("UPDATEDATE"),
    StartDate = element.GetDateTime("STARTDATE"),
    EndDate = element.GetDateTime("ENDDATE"),
    IsActive = element.GetInt("ISACTIVE"),
    Path = element.GetString("PATH")
  };
}