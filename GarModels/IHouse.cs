namespace GarModels;

public interface IHouse : IGarObject
{
  string ObjectGuid { get; }

  string HouseNumber { get; }

  int HouseType { get; }

  int OperationTypeId { get; }

  int IsActual { get; }
}