using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GarModels;
using GarModels.Services;

namespace GarDataLoader.Services;

public sealed class AddressObjectsDataService : IAddressObjectsDataService
{
  public IAddressObjects LoadFromXmlWholeObject(StreamReader reader)
    => (AddressObjects)new XmlSerializer(typeof(AddressObjects)).Deserialize(reader)!;

  public async Task<IAddressObjects> LoadFromXmlWholeObjectAsync(StreamReader reader)
    => await Task.Run(() => LoadFromXmlWholeObject(reader));

  public IEnumerable<IAddressObject> LoadFromXmlAsStream(StreamReader stream)
  {
    using XmlReader xmlReader = XmlReader.Create(stream);
    xmlReader.MoveToContent();

    while (xmlReader.Read())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "OBJECT", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return MapXmlElementToAddressObject(element);
      }
    }
  }

  public async IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(StreamReader stream)
  {
    using XmlReader xmlReader = XmlReader.Create(stream, new XmlReaderSettings { Async = true });
    await xmlReader.MoveToContentAsync();

    while (await xmlReader.ReadAsync())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "OBJECT", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        AddressObject? addressObject = null;
        try
        {
          addressObject = MapXmlElementToAddressObject(element);
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.WriteLine(e.Message);
        }
        
        if (addressObject is not null)
        {
          yield return addressObject;
        }
      }
    }
  }
  
  private AddressObject MapXmlElementToAddressObject(XElement element) => new()
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