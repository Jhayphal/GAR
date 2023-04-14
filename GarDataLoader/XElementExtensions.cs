using System.Xml.Linq;

namespace GarDataLoader.Services;

public static class XElementExtensions
{
  public static int GetInt(this XElement element, string name, int @default = 0)
  {
    var value = element.Attribute(name);
    return value is null ? @default : (int)value;
  }
  
  public static string GetString(this XElement element, string name, string @default = "")
  {
    var value = element.Attribute(name);
    return value is null ? @default : (string)value;
  }
  
  public static DateTime GetDateTime(this XElement element, string name, DateTime @default)
  {
    var value = element.Attribute(name);
    return value is null ? @default : (DateTime)value;
  }

  public static DateTime GetDateTime(this XElement element, string name)
  {
    var value = element.Attribute(name);
    return value is null ? new DateTime() : (DateTime)value;
  }

  public static DateOnly GetDateOnly(this XElement element, string name, DateOnly @default)
  {
    var value = element.Attribute(name);
    return value is null ? @default : DateOnly.FromDateTime((DateTime)value);
  }
  
  public static DateOnly GetDateOnly(this XElement element, string name)
  {
    var value = element.Attribute(name);
    return value is null ? new DateOnly() : DateOnly.FromDateTime((DateTime)value);
  }
}