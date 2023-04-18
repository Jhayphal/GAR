using System;
using GarDataView.Models;
using ReactiveUI;

namespace GarDataView.ViewModels;

public class AddressObjectViewModel : ViewModelBase
{
  private readonly AddressObjectModel model;
  
  public AddressObjectViewModel(AddressObjectModel model)
  {
    this.model = model;
  }

  public int Id
  {
    get => model.Id;
    set
    {
      if (model.Id != value)
      {
        this.RaisePropertyChanging();
        model.Id = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int ObjectId   {
    get => model.ObjectId;
    set
    {
      if (model.ObjectId != value)
      {
        this.RaisePropertyChanging();
        model.ObjectId = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public string ObjectGuid
  {
    get => model.ObjectGuid;
    set
    {
      if (!string.Equals(model.ObjectGuid, value, StringComparison.OrdinalIgnoreCase))
      {
        this.RaisePropertyChanging();
        model.ObjectGuid = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int ChangeId 
  {
    get => model.ChangeId;
    set
    {
      if (model.ChangeId != value)
      {
        this.RaisePropertyChanging();
        model.ChangeId = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public string Name 
  {
    get => model.Name;
    set
    {
      if (!string.Equals(model.Name, value, StringComparison.OrdinalIgnoreCase))
      {
        this.RaisePropertyChanging();
        model.Name = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public string TypeName
  {
    get => model.TypeName;
    set
    {
      if (!string.Equals(model.TypeName, value, StringComparison.OrdinalIgnoreCase))
      {
        this.RaisePropertyChanging();
        model.TypeName = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int Level 
  {
    get => model.Level;
    set
    {
      if (model.Level != value)
      {
        this.RaisePropertyChanging();
        model.Level = value;
        this.RaisePropertyChanged();
      }
    }
  }
  
  public int OperationTypeId 
  {
    get => model.OperationTypeId;
    set
    {
      if (model.OperationTypeId != value)
      {
        this.RaisePropertyChanging();
        model.OperationTypeId = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int PrevId 
  {
    get => model.PrevId;
    set
    {
      if (model.PrevId != value)
      {
        this.RaisePropertyChanging();
        model.PrevId = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int NextId 
  {
    get => model.NextId;
    set
    {
      if (model.NextId != value)
      {
        this.RaisePropertyChanging();
        model.NextId = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public DateTime UpdateDate
  {
    get => model.UpdateDate;
    set
    {
      if (model.UpdateDate != value)
      {
        this.RaisePropertyChanging();
        model.UpdateDate = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public DateTime StartDate 
  {
    get => model.StartDate;
    set
    {
      if (model.StartDate != value)
      {
        this.RaisePropertyChanging();
        model.StartDate = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public DateTime EndDate
  {
    get => model.EndDate;
    set
    {
      if (model.EndDate != value)
      {
        this.RaisePropertyChanging();
        model.EndDate = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int IsActual
  {
    get => model.IsActual;
    set
    {
      if (model.IsActual != value)
      {
        this.RaisePropertyChanging();
        model.IsActual = value;
        this.RaisePropertyChanged();
      }
    }
  }

  public int IsActive
  {
    get => model.IsActive;
    set
    {
      if (model.IsActive != value)
      {
        this.RaisePropertyChanging();
        model.IsActive = value;
        this.RaisePropertyChanged();
      }
    }
  }
}