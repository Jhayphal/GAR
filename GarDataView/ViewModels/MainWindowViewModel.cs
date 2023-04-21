using System.Threading.Tasks;
using System.Reactive;
using System.IO;
using GarDataLoader.Services;
using GarModels;
using Microsoft.Data.SqlClient;
using GarModels.Services;
using ReactiveUI;
using Splat;

namespace GarDataView.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private readonly IGarDataService<IAddressObject> addressObjectsDataService = Locator.Current.GetService<IGarDataService<IAddressObject>>();
  private readonly IGarDataService<IItemRelation> itemRelationsDataService = Locator.Current.GetService<IGarDataService<IItemRelation>>();
  private readonly IGarDataService<IHouse> housesDataService = Locator.Current.GetService<IGarDataService<IHouse>>();

  private string archivePath = @"C:\Users\Jhayphal\Downloads\gar_xml.zip";

  private string connectionString =
    @"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=******;TrustServerCertificate=True";

  public MainWindowViewModel()
  {
    var canExecute = this.WhenAnyValue(
      vm => vm.ConnectionString, 
      vm => vm.ArchivePath, 
      (connection, archive) => !string.IsNullOrWhiteSpace(archive) 
                               && !string.IsNullOrWhiteSpace(connection) 
                               && File.Exists(archive));

    InsertAddressObjects = ReactiveCommand.CreateFromTask(InsertObjects, canExecute);
    InsertItemRelations = ReactiveCommand.CreateFromTask(InsertRelations, canExecute);
    InsertHouseObjects = ReactiveCommand.CreateFromTask(InsertHouses, canExecute);
  }

  public string ConnectionString
  {
    get => connectionString;
    set => this.RaiseAndSetIfChanged(ref connectionString, value);
  }

  public string ArchivePath
  {
    get => archivePath;
    set => this.RaiseAndSetIfChanged(ref archivePath, value);
  }

  public ReactiveCommand<Unit, Unit> InsertAddressObjects { get; }
  
  public ReactiveCommand<Unit, Unit> InsertItemRelations { get; }
  
  public ReactiveCommand<Unit, Unit> InsertHouseObjects { get; }

  private async Task InsertObjects()
    => await LoadData(fileMask: "/AS_ADDR_OBJ_2", service: addressObjectsDataService, tableName: "ADDRESS_OBJECTS").ConfigureAwait(false);

  private async Task InsertRelations()
    => await LoadData(fileMask: "/AS_ADM_HIERARCHY_", service: itemRelationsDataService, tableName: "ADM_HIERARCHY").ConfigureAwait(false);

  private async Task InsertHouses()
    => await LoadData(fileMask: "/AS_HOUSES_2", service: housesDataService, tableName: "HOUSES").ConfigureAwait(false);

  private async Task LoadData<TObject>(string fileMask, IGarDataService<TObject> service, string tableName)
    where TObject : IGarObject
  {
    foreach (var stream in ZipWalkerService.GetFiles(ArchivePath, fileMask))
    {
      await using var _ = stream.ConfigureAwait(false);
      using var reader = new StreamReader(stream);
      var connection = new SqlConnection(ConnectionString);
      await using var __ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await service.InsertRecordsFromAsync(connection, tableName, reader);
    }
  }
}