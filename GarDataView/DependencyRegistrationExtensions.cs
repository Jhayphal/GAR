using Avalonia;
using GarDataLoader.Services;
using GarDataView.ViewModels;
using GarDataView.Views;
using GarModels;
using GarModels.Services;
using Splat;

namespace GarDataView;

public static class DependencyRegistrationExtensions
{
  public static AppBuilder RegisterDependencies(this AppBuilder builder)
  {
    var services = Locator.CurrentMutable;
    
    services.Register(() => new AddressObjectsDataService(), typeof(IGarDataService<IAddressObject>));
    services.Register(() => new ItemRelationsDataService(), typeof(IGarDataService<IItemRelation>));
    services.Register(() => new HousesDataService(), typeof(IGarDataService<IHouse>));
    services.Register(() => new MainWindowViewModel(), typeof(MainWindowViewModel));
    services.Register(() => new MainWindow { DataContext = Locator.Current.GetService<MainWindowViewModel>() }, typeof(MainWindow));
    
    return builder;
  }
}