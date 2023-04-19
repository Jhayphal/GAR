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