namespace GarModels;

public interface IGarObject
{
  int Id { get; }

  int ObjectId { get; }

  int ChangeId { get; }

  int PrevId { get; }

  int NextId { get; }

  DateTime UpdateDate { get; }

  DateTime StartDate { get; }

  DateTime EndDate { get; }

  int IsActive { get; }
}