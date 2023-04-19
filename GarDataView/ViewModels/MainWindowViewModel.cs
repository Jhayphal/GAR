using System.Collections.ObjectModel;
using System.Collections.Generic;
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
    @"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=123456;TrustServerCertificate=True";

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
  {
    foreach (var stream in ZipWalkerService.GetFiles(ArchivePath, "/AS_ADDR_OBJ_2"))
    {
      await using var _ = stream.ConfigureAwait(false);
      using var reader = new StreamReader(stream);
      var connection = new SqlConnection(ConnectionString);
      await using var __ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await addressObjectsDataService.InsertRecordsFromAsync(connection, "ADDRESS_OBJECTS", reader);
    }
  }

  private async Task InsertRelations()
  {
    foreach (var stream in ZipWalkerService.GetFiles(ArchivePath, "/AS_ADM_HIERARCHY_"))
    {
      await using var _ = stream.ConfigureAwait(false);
      using var reader = new StreamReader(stream);
      var connection = new SqlConnection(ConnectionString);
      await using var __ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await itemRelationsDataService.InsertRecordsFromAsync(connection, "ADM_HIERARCHY", reader);
    }
  }
  
  private async Task InsertHouses()
  {
    foreach (var stream in ZipWalkerService.GetFiles(ArchivePath, "/AS_HOUSES_2"))
    {
      using var reader = new StreamReader(stream);
      var connection = new SqlConnection(ConnectionString);
      await using var __ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await housesDataService.InsertRecordsFromAsync(connection, "HOUSES", reader);
    }
  }
}