using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;
using GarModels.Services;
using Microsoft.Data.SqlClient;
using ReactiveUI;
using Splat;

namespace GarDataView.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private readonly AddressObjectsViewModel objectsViewModel = Locator.Current.GetService<AddressObjectsViewModel>();
  private readonly IItemRelationsDataService itemRelationsDataService = Locator.Current.GetService<IItemRelationsDataService>();

  private string addrObjFileName =
    @"C:\Users\Jhayphal\Downloads\93\AS_ADDR_OBJ_20230313_a9b39241-718b-4ea6-a327-e578106ce7d1.XML";
  private string admHierarchyFileName =
    @"C:\Users\Jhayphal\Downloads\93\AS_ADM_HIERARCHY_20230313_81ae6605-1391-4525-98f8-b68e96f35160.XML";

  public MainWindowViewModel()
  {
    var canExecute = this.WhenAnyValue(x => x.AddressObjectsFileName, x => !string.IsNullOrWhiteSpace(x));
    LoadObjects = ReactiveCommand.CreateFromTask(Load, canExecute);
    InsertItemRelations = ReactiveCommand.CreateFromTask(InsertRelations);
  }

  public string AddressObjectsFileName
  {
    get => addrObjFileName;
    set => this.RaiseAndSetIfChanged(ref addrObjFileName, value);
  }

  public ObservableCollection<AddressObjectViewModel> Objects => objectsViewModel.Objects;

  public ReactiveCommand<Unit, Unit> LoadObjects { get; }
  
  public ReactiveCommand<Unit, Unit> InsertItemRelations { get; }

  private async Task Load() => await objectsViewModel.Load(AddressObjectsFileName).ConfigureAwait(false);
  
  private async Task InsertRelations()
  {
    using var stream = new StreamReader(admHierarchyFileName);

    var connection = new SqlConnection(@"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=123456;TrustServerCertificate=True");
    await using var _ = connection.ConfigureAwait(false);
    await connection.OpenAsync();

    await itemRelationsDataService.InsertRecordsFromAsync(connection, "ADM_HIERARCHY_TEST", stream);
  }
}