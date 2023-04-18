using Avalonia;
using GarDataLoader.Services;
using GarDataView.ViewModels;
using GarDataView.Views;
using GarModels.Services;
using Splat;

namespace GarDataView;

public static class DependencyRegistrationExtensions
{
  public static AppBuilder RegisterDependencies(this AppBuilder builder)
  {
    var services = Locator.CurrentMutable;
    
    services.Register(() => new AddressObjectsDataService(), typeof(IAddressObjectsDataService));
    services.Register(() => new ItemRelationsDataService(), typeof(IItemRelationsDataService));
    services.Register(() => new AddressObjectsViewModel(), typeof(AddressObjectsViewModel));
    services.Register(() => new MainWindowViewModel(), typeof(MainWindowViewModel));
    services.Register(() => new MainWindow { DataContext = Locator.Current.GetService<MainWindowViewModel>() }, typeof(MainWindow));
    
    return builder;
  }
}