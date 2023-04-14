using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using GarDataView.Models;
using GarModels.Services;

namespace GarDataView.ViewModels;

public class AddressObjectsViewModel : ViewModelBase
{
  private readonly IAddressObjectsDataService service;
  
  public AddressObjectsViewModel(IAddressObjectsDataService service)
  {
    this.service = service;
  }

  public ObservableCollection<AddressObjectViewModel> Objects { get; } = new();

  public async Task Load(string xmlFileName)
  {
    Objects.Clear();
    
    using var stream = new StreamReader(xmlFileName);
    await foreach (var @object in service.LoadFromXmlAsStreamAsync(stream).ConfigureAwait(false))
    {
      Objects.Add(new AddressObjectViewModel(new AddressObjectModel(@object)));

      await Task.Delay(50);
    }
  }
}