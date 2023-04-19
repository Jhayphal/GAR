namespace GarModels;

public interface IAddressObject
{
  int Id { get; }

  int ObjectId { get; }

  string ObjectGuid { get; }

  int ChangeId { get; }

  string Name { get; }

  string TypeName { get; }

  int Level { get; }

  int OperationTypeId { get; }

  int PrevId { get; }

  int NextId { get; }

  DateTime UpdateDate { get; }

  DateTime StartDate { get; }

  DateTime EndDate { get; }

  int IsActual { get; }

  int IsActive { get; }
}