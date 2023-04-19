namespace GarModels;

public interface IItemRelation : IGarObject
{
  int ParentObjectId { get; }

  int RegionCode { get; }

  int AreaCode { get; }

  int CityCode { get; }

  int PlaceCode { get; }

  int PlanCode { get; }

  int StreetCode { get; }
  
  string Path { get; }
}