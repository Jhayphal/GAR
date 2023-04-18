using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace GarDataLoader;

public static class SqlQueryGenerator
{
  public static string MakeCreateTable<T>(string tableName)
  {
    var type = typeof(T);
    var fields = type.GetProperties()
      .Where(x => x.CanRead && x.CustomAttributes.OfType<XmlAttributeAttribute>().Any())
      .Select(x => new
        { x.Name, x.PropertyType, x.CustomAttributes.OfType<XmlAttributeAttribute>().FirstOrDefault().AttributeName })
      .ToArray();

    if (fields.Length == 0)
    {
      throw new InvalidDataContractException(
        $"Тип {typeof(T).FullName} не содержит свойств доступных для чтения, помеченных аттрибутом XmlAttribute.");
    }
    
    StringBuilder builder = new();
    builder.AppendFormat("CREATE TABLE {0} (", tableName);
    builder.AppendLine();

    for (int i = 0; i < fields.Length; ++i)
    {
      var field = fields[i];
      
      if (i > 0)
      {
        builder.AppendFormat("\t, [{0}] {1} NOT NULL DEFAULT({2})", field.AttributeName, MapTypeToSql(field.PropertyType), GetDefaultValue(field.PropertyType));
      }
      else
      {
        builder.AppendFormat("\t[{0}] {1} NOT NULL DEFAULT({2})", field.AttributeName, MapTypeToSql(field.PropertyType), GetDefaultValue(field.PropertyType));
      }
      
      builder.AppendLine();
    }

    builder.Append(")");

    return builder.ToString();
  }

  public static string MakeInsert<T>(string tableName, T value)
  {
    return string.Empty;
  }
  
  private static string MapTypeToSql(Type type)
  {
    if (type == typeof(int))
    {
      return "INT";
    }

    if (type == typeof(string))
    {
      return "NVARCHAR(1024)";
    }
    
    if (type == typeof(DateOnly))
    {
      return "DATE";
    }

    throw new ArgumentException($"Тип {type.FullName} не поддерживается.");
  }

  private static string GetDefaultValue(Type type)
  {
    if (type == typeof(int))
    {
      return "0";
    }

    if (type == typeof(string))
    {
      return "''";
    }
    
    if (type == typeof(DateOnly))
    {
      return "'0001-01-01'";
    }

    throw new ArgumentException($"Тип {type.FullName} не поддерживается.");
  }
}