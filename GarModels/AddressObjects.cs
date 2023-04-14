namespace GarModels;

public interface IAddressObjects
{
  IEnumerable<IAddressObject> Objects { get; }
}

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

  DateOnly UpdateDate { get; }

  DateOnly StartDate { get; }

  DateOnly EndDate { get; }

  int IsActual { get; }

  int IsActive { get; }
}