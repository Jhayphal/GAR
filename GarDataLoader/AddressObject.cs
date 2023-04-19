using System.ComponentModel;
using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName = "OBJECT")]
public sealed class AddressObject : IAddressObject
{
  [XmlAttribute(AttributeName = "ID")]
  [DefaultValue(0)]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "OBJECTID")]
  [DefaultValue(0)]
  public int ObjectId { get; set; }

  [XmlAttribute(AttributeName = "OBJECTGUID")]
  public string ObjectGuid { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "CHANGEID")]
  [DefaultValue(0)]
  public int ChangeId { get; set; }

  [XmlAttribute(AttributeName = "NAME")]
  public string Name { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "TYPENAME")]
  public string TypeName { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "LEVEL")]
  [DefaultValue(0)]
  public int Level { get; set; }

  [XmlAttribute(AttributeName = "OPERTYPEID")]
  [DefaultValue(0)]
  public int OperationTypeId { get; set; }

  [XmlAttribute(AttributeName = "PREVID")]
  [DefaultValue(0)]
  public int PrevId { get; set; }

  [XmlAttribute(AttributeName = "NEXTID")]
  [DefaultValue(0)]
  public int NextId { get; set; }

  [XmlAttribute(AttributeName = "UPDATEDATE", Type = typeof(DateOnly))]
  public DateTime UpdateDate { get; set; }

  [XmlAttribute(AttributeName = "STARTDATE", Type = typeof(DateOnly))]
  public DateTime StartDate { get; set; }

  [XmlAttribute(AttributeName = "ENDDATE", Type = typeof(DateOnly))]
  public DateTime EndDate { get; set; }

  [XmlAttribute(AttributeName = "ISACTUAL")]
  [DefaultValue(0)]
  public int IsActual { get; set; }

  [XmlAttribute(AttributeName = "ISACTIVE")]
  [DefaultValue(0)]
  public int IsActive { get; set; }
}