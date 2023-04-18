using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using GarDataView.Models;
using GarModels.Services;
using Microsoft.Data.SqlClient;
using Splat;

namespace GarDataView.ViewModels;

public class AddressObjectsViewModel : ViewModelBase
{
  private readonly IAddressObjectsDataService service = Locator.Current.GetService<IAddressObjectsDataService>();

  public ObservableCollection<AddressObjectViewModel> Objects { get; } = new();

  public async Task Load(string xmlFileName)
  {
    Objects.Clear();
    
    using var stream = new StreamReader(xmlFileName);

    var connection = new SqlConnection(@"Data Source=NJNOUTE;Database=Empty;User ID=sa;Password=123456;TrustServerCertificate=True");
    await using var _ = connection.ConfigureAwait(false);
    await connection.OpenAsync();
    await service.InsertRecordsFromAsync(connection, "ADDRESS_OBJECTS_TEST", stream);
    
    await foreach (var @object in service.LoadFromXmlAsStreamAsync(stream).ConfigureAwait(false))
    {
      Objects.Add(new AddressObjectViewModel(new AddressObjectModel(@object)));

      await Task.Delay(50);
    }
  }
}