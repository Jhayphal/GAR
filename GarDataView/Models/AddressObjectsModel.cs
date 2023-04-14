using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GarModels.Services;

namespace GarDataView.Models;

public sealed class AddressObjectsModel
{
  private readonly IAddressObjectsDataService service;
  private readonly List<AddressObjectModel> objects = new();

  public AddressObjectsModel(IAddressObjectsDataService service)
  {
    this.service = service;
  }

  public IEnumerable<AddressObjectModel> Objects => objects;

  public async Task Load(string xmlFileName)
  {
    objects.Clear();
    
    using var stream = new StreamReader(xmlFileName);
    await foreach (var @object in service.LoadFromXmlAsStreamAsync(stream).ConfigureAwait(false))
    {
      objects.Add(new AddressObjectModel(@object));
    }
  }
}