using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive;
using System.IO;
using GarModels;
using Microsoft.Data.SqlClient;
using GarModels.Services;
using ReactiveUI;
using Splat;

namespace GarDataView.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private const string ConnectionString = @"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=123456;TrustServerCertificate=True";
  
  private readonly IGarDataService<IAddressObject> addressObjectsDataService = Locator.Current.GetService<IGarDataService<IAddressObject>>();
  private readonly IGarDataService<IItemRelation> itemRelationsDataService = Locator.Current.GetService<IGarDataService<IItemRelation>>();
  private readonly IGarDataService<IHouse> housesDataService = Locator.Current.GetService<IGarDataService<IHouse>>();

  private string folderPath = @"C:\Users\Jhayphal\Downloads\93";

  public MainWindowViewModel()
  {
    var canExecute = this.WhenAnyValue(x => x.FolderPath, s => !string.IsNullOrWhiteSpace(s) && Directory.Exists(s));

    InsertAddressObjects = ReactiveCommand.CreateFromTask(InsertObjects, canExecute);
    InsertItemRelations = ReactiveCommand.CreateFromTask(InsertRelations, canExecute);
    InsertHouseObjects = ReactiveCommand.CreateFromTask(InsertHouses, canExecute);
  }

  public string FolderPath
  {
    get => folderPath;
    set => this.RaiseAndSetIfChanged(ref folderPath, value);
  }

  public ReactiveCommand<Unit, Unit> InsertAddressObjects { get; }
  
  public ReactiveCommand<Unit, Unit> InsertItemRelations { get; }
  
  public ReactiveCommand<Unit, Unit> InsertHouseObjects { get; }

  private async Task InsertObjects()
  {
    foreach (var file in GetFilesByMask("AS_ADDR_OBJ_2*.XML"))
    {
      using var stream = new StreamReader(file);
      var connection = new SqlConnection(ConnectionString);
      await using var _ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await addressObjectsDataService.InsertRecordsFromAsync(connection, "ADDRESS_OBJECTS_TEST", stream);
    }
  }

  private async Task InsertRelations()
  {
    foreach (var file in GetFilesByMask("AS_ADM_HIERARCHY_*.XML"))
    {
      using var stream = new StreamReader(file);
      var connection = new SqlConnection(ConnectionString);
      await using var _ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await itemRelationsDataService.InsertRecordsFromAsync(connection, "ADM_HIERARCHY", stream);
    }
  }
  
  private async Task InsertHouses()
  {
    foreach (var file in GetFilesByMask("AS_HOUSES_2*.XML"))
    {
      using var stream = new StreamReader(file);
      var connection = new SqlConnection(ConnectionString);
      await using var _ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await housesDataService.InsertRecordsFromAsync(connection, "HOUSES_TEST", stream);
    }
  }

  private IEnumerable<string> GetFilesByMask(string mask) => Directory.GetFiles(FolderPath, mask, SearchOption.AllDirectories);
}