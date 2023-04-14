using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName="ADDRESSOBJECTS")]
public sealed class AddressObjects : IAddressObjects
{
  [XmlElement(ElementName = "OBJECT")]
  public List<AddressObject> Objects { get; set; } = new();

  IEnumerable<IAddressObject> IAddressObjects.Objects => Objects;
}

[XmlRoot(ElementName = "OBJECT")]
public sealed class AddressObject : IAddressObject
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