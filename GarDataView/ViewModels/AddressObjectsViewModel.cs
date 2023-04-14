using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GarDataView.Models;
using GarModels;

namespace GarDataView.ViewModels;

public class AddressObjectsViewModel : ViewModelBase
{
  private readonly AddressObjectsModel model;
  
  public AddressObjectsViewModel(AddressObjectsModel model)
  {
    this.model = model;
  }

  public ObservableCollection<IAddressObject> Objects => model.Objects;
  
  public async Task Load(string xmlFileName) => await model.Load(xmlFileName);
}