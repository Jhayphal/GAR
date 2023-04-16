namespace GarModels.Services;

public interface IAddressObjectsDataService
{
  IAddressObjects LoadFromXmlWholeObject(TextReader reader);

  Task<IAddressObjects> LoadFromXmlWholeObjectAsync(TextReader reader);

  IEnumerable<IAddressObject> LoadFromXmlAsStream(TextReader stream);

  IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(TextReader stream);
}