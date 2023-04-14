using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using GarModels;
using GarModels.Services;

namespace GarDataView.Models;

public sealed class AddressObjectsModel
{
  private readonly IAddressObjectsDataService service;

  public AddressObjectsModel(IAddressObjectsDataService service)
  {
    this.service = service;
  }

  public ObservableCollection<IAddressObject> Objects { get; } = new();

  public async Task Load(string xmlFileName)
  {
    Objects.Clear();
    
    using var stream = new StreamReader(xmlFileName);
    await foreach (var @object in service.LoadFromXmlAsStreamAsync(stream))
    {
      Objects.Add(@object);
    }
  }
}