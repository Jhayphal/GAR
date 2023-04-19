using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reactive;
using System.IO;
using Microsoft.Data.SqlClient;
using GarModels.Services;
using ReactiveUI;
using Splat;

namespace GarDataView.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
  private const string ConnectionString = @"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=123456;TrustServerCertificate=True";
  
  private readonly IAddressObjectsDataService service = Locator.Current.GetService<IAddressObjectsDataService>();
  private readonly IItemRelationsDataService itemRelationsDataService = Locator.Current.GetService<IItemRelationsDataService>();

  private string folderPath = @"C:\Users\Jhayphal\Downloads\93";

  public MainWindowViewModel()
  {
    var canExecute = this.WhenAnyValue(x => x.FolderPath, s => !string.IsNullOrWhiteSpace(s) && Directory.Exists(s));
    InsertAddressObjects = ReactiveCommand.CreateFromTask(InsertObjects, canExecute);
    InsertItemRelations = ReactiveCommand.CreateFromTask(InsertRelations, canExecute);
  }

  public string FolderPath
  {
    get => folderPath;
    set => this.RaiseAndSetIfChanged(ref folderPath, value);
  }

  public ReactiveCommand<Unit, Unit> InsertAddressObjects { get; }
  
  public ReactiveCommand<Unit, Unit> InsertItemRelations { get; }

  private async Task InsertObjects()
  {
    foreach (var file in GetFilesByMask("AS_ADDR_OBJ_2*.XML"))
    {
      using var stream = new StreamReader(file);
      var connection = new SqlConnection(ConnectionString);
      await using var _ = connection.ConfigureAwait(false);
      await connection.OpenAsync();
      await service.InsertRecordsFromAsync(connection, "ADDRESS_OBJECTS_TEST", stream);
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

  private IEnumerable<string> GetFilesByMask(string mask) => Directory.GetFiles(FolderPath, Path.Combine(FolderPath, mask), SearchOption.AllDirectories);
}