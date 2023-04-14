namespace GarModels.Services;

public interface IAddressObjectsDataService
{
  IAddressObjects LoadFromXmlWholeObject(StringReader textReader);

  Task<IAddressObjects> LoadFromXmlWholeObjectAsync(StringReader textReader);

  IEnumerable<IAddressObject> LoadFromXmlAsStream(StringReader textReader);

  IAsyncEnumerable<IAddressObject> LoadFromXmlAsStreamAsync(StringReader textReader);
}