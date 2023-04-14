using System;
using GarModels;

namespace GarDataView.Models;

public class AddressObjectModel
{
  public AddressObjectModel(IAddressObject @object)
  {
    Id = @object.Id;
    ObjectId = @object.ObjectId;
    ObjectGuid = @object.ObjectGuid;
    ChangeId = @object.ChangeId;
    Name = @object.Name;
    TypeName = @object.TypeName;
    Level = @object.Level;
    OperationTypeId = @object.OperationTypeId;
    PrevId = @object.PrevId;
    NextId = @object.NextId;
    UpdateDate = @object.UpdateDate;
    StartDate = @object.StartDate;
    EndDate = @object.EndDate;
    IsActual = @object.IsActual;
    IsActive = @object.IsActive;
  }

  public int Id { get; set; }

  public int ObjectId { get; set; }

  public string ObjectGuid { get; set; }

  public int ChangeId { get; set; }

  public string Name { get; set; }

  public string TypeName { get; set; }

  public int Level { get; set; }

  public int OperationTypeId { get; set; }

  public int PrevId { get; set; }

  public int NextId { get; set; }

  public DateOnly UpdateDate { get; set; }

  public DateOnly StartDate { get; set; }

  public DateOnly EndDate { get; set; }

  public int IsActual { get; set; }

  public int IsActive { get; set; }
}