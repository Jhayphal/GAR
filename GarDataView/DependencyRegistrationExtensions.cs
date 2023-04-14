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
    services.Register(() => new AddressObjectsViewModel(Locator.Current.GetService<IAddressObjectsDataService>()), typeof(AddressObjectsViewModel));
    services.Register(() => new MainWindowViewModel(Locator.Current), typeof(MainWindowViewModel));
    services.Register(() => new MainWindow { DataContext = Locator.Current.GetService<MainWindowViewModel>() }, typeof(MainWindow));
    
    return builder;
  }
}