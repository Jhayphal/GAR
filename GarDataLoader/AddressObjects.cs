using System.Xml.Serialization;

namespace GarDataLoader;

[XmlRoot(ElementName="ADDRESSOBJECTS")]
public class AddressObjects
{
  [XmlElement(ElementName = "OBJECT")]
  public List<AddressObject> Object { get; set; } = new();

  public static AddressObjects FromXml(string xml)
  {
    XmlSerializer serializer = new XmlSerializer(typeof(AddressObjects));
    using StringReader reader = new StringReader(xml);
    return (AddressObjects)serializer.Deserialize(reader)!;
  }
}

[XmlRoot(ElementName = "OBJECT")]
public class AddressObject
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

  [XmlAttribute(AttributeName = "UPDATEDATE")]
  public DateTime UpdateDate { get; set; }

  [XmlAttribute(AttributeName = "STARTDATE")]
  public DateTime StartDate { get; set; }

  [XmlAttribute(AttributeName = "ENDDATE")]
  public DateTime EndDate { get; set; }

  [XmlAttribute(AttributeName = "ISACTUAL")]
  public int IsActual { get; set; }

  [XmlAttribute(AttributeName = "ISACTIVE")]
  public int IsActive { get; set; }
}