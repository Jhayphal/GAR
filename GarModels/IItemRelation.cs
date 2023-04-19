namespace GarModels;

public interface IItemRelation
{
  int Id { get; }

  int ObjectId { get; }

  int ParentObjectId { get; }

  int ChangeId { get; }

  int RegionCode { get; }

  int AreaCode { get; }

  int CityCode { get; }

  int PlaceCode { get; }

  int PlanCode { get; }

  int StreetCode { get; }

  int PrevId { get; }

  int NextId { get; }

  DateTime UpdateDate { get; }

  DateTime StartDate { get; }

  DateTime EndDate { get; }

  int IsActive { get; }

  string Path { get; }
}