using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName="ITEM")]
public sealed class ItemRelation : IItemRelation
{
  [XmlAttribute(AttributeName="ID")] 
  public int Id { get; set; }

  [XmlAttribute(AttributeName="OBJECTID")] 
  public int ObjectId { get; set; }

  [XmlAttribute(AttributeName="PARENTOBJID")] 
  public int ParentObjectId { get; set; }

  [XmlAttribute(AttributeName="CHANGEID")] 
  public int ChangeId { get; set; }

  [XmlAttribute(AttributeName="REGIONCODE")] 
  public int RegionCode { get; set; }

  [XmlAttribute(AttributeName="AREACODE")] 
  public int AreaCode { get; set; }

  [XmlAttribute(AttributeName="CITYCODE")] 
  public int CityCode { get; set; }

  [XmlAttribute(AttributeName="PLACECODE")] 
  public int PlaceCode { get; set; }

  [XmlAttribute(AttributeName="PLANCODE")] 
  public int PlanCode { get; set; }

  [XmlAttribute(AttributeName="STREETCODE")] 
  public int StreetCode { get; set; }

  [XmlAttribute(AttributeName="PREVID")] 
  public int PrevId { get; set; }

  [XmlAttribute(AttributeName="NEXTID")] 
  public int NextId { get; set; }

  [XmlAttribute(AttributeName="UPDATEDATE")] 
  public DateTime UpdateDate { get; set; }

  [XmlAttribute(AttributeName="STARTDATE")] 
  public DateTime StartDate { get; set; }

  [XmlAttribute(AttributeName="ENDDATE")] 
  public DateTime EndDate { get; set; }

  [XmlAttribute(AttributeName="ISACTIVE")] 
  public int IsActive { get; set; }

  [XmlAttribute(AttributeName = "PATH")]
  public string Path { get; set; } = string.Empty;
}