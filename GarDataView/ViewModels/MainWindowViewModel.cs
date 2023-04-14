using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;

namespace GarDataView.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private readonly AddressObjectsViewModel objectsViewModel;
  private string fileName = @"C:\Users\Jhayphal\Downloads\93\AS_ADDR_OBJ_20230313_a9b39241-718b-4ea6-a327-e578106ce7d1.XML";
  
  public MainWindowViewModel(IReadonlyDependencyResolver resolver)
  {
    objectsViewModel = resolver.GetService<AddressObjectsViewModel>();

    var canExecute = this.WhenAnyValue(x => x.FileName, x => !string.IsNullOrWhiteSpace(x));
    LoadObjects = ReactiveCommand.CreateFromTask(Load, canExecute);
  }

  public string FileName
  {
    get => fileName; 
    set => this.RaiseAndSetIfChanged(ref fileName, value);
  }
  
  public ObservableCollection<AddressObjectViewModel> Objects => objectsViewModel.Objects;
  
  public ReactiveCommand<Unit, Unit> LoadObjects { get; }

  private async Task Load() => await objectsViewModel.Load(FileName);
}