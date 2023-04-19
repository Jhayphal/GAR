using Ionic.Zip;

namespace GarDataLoader.Services;

public static class ZipWalkerService
{
  public static IEnumerable<Stream> GetFiles(string fileName, string fileMask)
  {
    using var zip = ZipFile.Read(fileName);
    foreach (var entryFileName in zip.EntryFileNames)
    {
      if (entryFileName.Contains(fileMask))
      {
        System.Diagnostics.Debug.WriteLine(entryFileName);
        yield return zip[entryFileName].OpenReader();
      }
    }
  }
}