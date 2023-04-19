using System.Xml.Serialization;
using GarModels;

namespace GarDataLoader;

[XmlRoot(ElementName="ITEMS")]
public sealed class ItemRelations : IItemRelations
{
  [XmlElement(ElementName = "ITEM")]
  public ItemRelation Item { get; set; } = new();

  IItemRelation IItemRelations.Item => Item;
}