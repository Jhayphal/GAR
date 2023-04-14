using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GarDataView.Models;

namespace GarDataView.ViewModels;

public class AddressObjectsViewModel : ViewModelBase
{
  private readonly AddressObjectsModel model;
  
  public AddressObjectsViewModel(AddressObjectsModel model)
  {
    this.model = model;
  }

  public ObservableCollection<AddressObjectViewModel> Objects { get; } = new();

  public async Task Load(string xmlFileName)
  {
    await model.Load(xmlFileName);

    foreach (var @object in model.Objects)
    {
      Objects.Add(new AddressObjectViewModel(@object));
    }
  }
}