namespace GarModels.Services;

public interface IAddressObjectsDataService
{
  IAddressObjects LoadFromXmlWholeObject(StreamReader reader);

  Task<IAddressObjects> LoadFromXmlWholeObjectAsync(StreamReader reader);

  IEnumerable<IAddressObject> LoadFromXmlAsStream(StreamReader stream);

  IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(StreamReader stream);
}