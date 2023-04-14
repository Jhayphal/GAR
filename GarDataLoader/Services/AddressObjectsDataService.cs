using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GarModels;
using GarModels.Services;

namespace GarDataLoader.Services;

public sealed class AddressObjectsDataService : IAddressObjectsDataService
{
  public IAddressObjects LoadFromXmlWholeObject(StringReader textReader)
    => (AddressObjects)new XmlSerializer(typeof(AddressObjects)).Deserialize(textReader)!;

  public async Task<IAddressObjects> LoadFromXmlWholeObjectAsync(StringReader textReader)
    => await Task.Run(() => LoadFromXmlWholeObject(textReader));

  public IEnumerable<IAddressObject> LoadFromXmlAsStream(StringReader textReader)
  {
    using XmlReader xmlReader = XmlReader.Create(textReader);
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

  public async IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(StringReader textReader)
  {
    using XmlReader xmlReader = XmlReader.Create(textReader);
    await xmlReader.MoveToContentAsync();

    while (await xmlReader.ReadAsync())
    {
      if (xmlReader.NodeType == XmlNodeType.Element
          && string.Equals(xmlReader.Name, "OBJECT", StringComparison.OrdinalIgnoreCase)
          && XNode.ReadFrom(xmlReader) is XElement element)
      {
        yield return MapXmlElementToAddressObject(element);
      }
    }
  }
  
  private AddressObject MapXmlElementToAddressObject(XElement element) => new()
  {
    Id = (int)element.Attribute("ID")!,
    ObjectId = (int)element.Attribute("OBJECTID")!,
    ObjectGuid = (string)element.Attribute("OBJECTGUID")!,
    ChangeId = (int)element.Attribute("CHANGEID")!,
    Name = (string)element.Attribute("NAME")!,
    TypeName = (string)element.Attribute("TYPENAME")!,
    Level = (int)element.Attribute("LEVEL")!,
    OperationTypeId = (int)element.Attribute("OPERTYPEID")!,
    PrevId = (int)element.Attribute("PREVID")!,
    NextId = (int)element.Attribute("NEXTID")!,
    UpdateDate = DateOnly.FromDateTime((DateTime)element.Attribute("UPDATEDATE")!),
    StartDate = DateOnly.FromDateTime((DateTime)element.Attribute("STARTDATE")!),
    EndDate = DateOnly.FromDateTime((DateTime)element.Attribute("ENDDATE")!),
    IsActual = (int)element.Attribute("ISACTUAL")!,
    IsActive = (int)element.Attribute("ISACTIVE")!
  };
}