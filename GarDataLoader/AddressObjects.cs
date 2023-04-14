using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName="ADDRESSOBJECTS")]
public class AddressObjects : IAddressObjects
{
  [XmlElement(ElementName = "OBJECT")]
  public List<AddressObject> Objects { get; set; } = new();

  IEnumerable<IAddressObject> IAddressObjects.Objects => Objects;

  public static AddressObjects FromXmlObject(StringReader textReader)
    => (AddressObjects)new XmlSerializer(typeof(AddressObjects)).Deserialize(textReader)!;

  public static async Task<AddressObjects> FromXmlObjectAsync(StringReader textReader)
    => await Task.Run(() => FromXmlObject(textReader));

  public static IEnumerable<AddressObject> FromXmlStream(StringReader textReader)
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

  public static async IAsyncEnumerable<AddressObject> FromXmlStreamAsync(StringReader textReader)
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
  
  private static AddressObject MapXmlElementToAddressObject(XElement element) => new()
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

[XmlRoot(ElementName = "OBJECT")]
public class AddressObject : IAddressObject
{
  [XmlAttribute(AttributeName = "ID")]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "OBJECTID")]
  public int ObjectId { get; set; }

  [XmlAttribute(AttributeName = "OBJECTGUID")]
  public string ObjectGuid { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "CHANGEID")]
  public int ChangeId { get; set; }

  [XmlAttribute(AttributeName = "NAME")]
  public string Name { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "TYPENAME")]
  public string TypeName { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "LEVEL")]
  public int Level { get; set; }

  [XmlAttribute(AttributeName = "OPERTYPEID")]
  public int OperationTypeId { get; set; }

  [XmlAttribute(AttributeName = "PREVID")]
  public int PrevId { get; set; }

  [XmlAttribute(AttributeName = "NEXTID")]
  public int NextId { get; set; }

  [XmlAttribute(AttributeName = "UPDATEDATE", Type = typeof(DateOnly))]
  public DateOnly UpdateDate { get; set; }

  [XmlAttribute(AttributeName = "STARTDATE", Type = typeof(DateOnly))]
  public DateOnly StartDate { get; set; }

  [XmlAttribute(AttributeName = "ENDDATE", Type = typeof(DateOnly))]
  public DateOnly EndDate { get; set; }

  [XmlAttribute(AttributeName = "ISACTUAL")]
  public int IsActual { get; set; }

  [XmlAttribute(AttributeName = "ISACTIVE")]
  public int IsActive { get; set; }
}