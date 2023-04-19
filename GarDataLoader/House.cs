using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName = "HOUSE")]
public struct House : IHouse
{
  public House()
  {
    Id = 0;
    ObjectId = 0;
    ChangeId = 0;
    HouseNumber = string.Empty;
    HouseType = 0;
    OperationTypeId = 0;
    PrevId = 0;
    NextId = 0;
    UpdateDate = default;
    StartDate = default;
    EndDate = default;
    IsActual = 0;
    IsActive = 0;
  }

  [XmlAttribute(AttributeName = "ID")]
  public int Id { get; set; }

  [XmlAttribute(AttributeName = "OBJECTID")]
  public int ObjectId { get; set; }

  [XmlAttribute(AttributeName = "OBJECTGUID")]
  public string ObjectGuid { get; set; } = string.Empty;

  [XmlAttribute(AttributeName = "CHANGEID")]
  public int ChangeId { get; set; }

  [XmlAttribute(AttributeName = "HOUSENUM")]
  public string HouseNumber { get; set; }

  [XmlAttribute(AttributeName = "HOUSETYPE")]
  public int HouseType { get; set; }

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