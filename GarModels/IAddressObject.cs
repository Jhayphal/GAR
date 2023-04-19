namespace GarModels;

public interface IAddressObject : IGarObject
{
  string ObjectGuid { get; }

  string Name { get; }

  string TypeName { get; }

  int Level { get; }

  int OperationTypeId { get; }
  
  int IsActual { get; }
}