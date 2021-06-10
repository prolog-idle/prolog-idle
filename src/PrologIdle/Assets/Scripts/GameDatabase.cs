using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Razensoft.Mapper;
using UnityEngine;

public class GameDatabase
{
    static GameDatabase()
    {
        Instance = FromJson(UnityEngine.Resources.Load<TextAsset>("data").text);
    }
    
    public static GameDatabase Instance { get; }
    
    public List<Resource> Resources { get; private set; }

    public List<ProductionAction> ProductionActions { get; private set; }

    public List<Unit> Units { get; private set; }

    public Resource FindResource(string id) => Resources.Find(r => r.Id == id);

    public static GameDatabase FromJson(string json)
    {
        var descriptor = JsonConvert.DeserializeObject<GameDatabaseDto>(json);
        return new GameDatabase
        {
            Resources = Resource.Mapper.Instance.MapList(descriptor.Resources),
            ProductionActions = ProductionAction.Mapper.Instance.MapList(descriptor.ProductionActions),
            Units = Unit.Mapper.Instance.MapList(descriptor.Units)
        };
    }
}

[Serializable]
public class GameDatabaseDto
{
    public List<ResourceDto> Resources;
    public List<ProductionActionDto> ProductionActions;
    public List<UnitDto> Units;
}

public class Resource
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public double Gatherable { get; private set; }
    
    public IReadOnlyList<string> Traits { get; set; }

    public class Mapper : IMapper<ResourceDto, Resource>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(ResourceDto source, Resource destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Gatherable = source.Gatherable;
            destination.Traits = source.Traits;
        }
    }
}

[Serializable]
public class ResourceDto
{
    public string Id;
    public string Name;
    public double Gatherable;
    public List<string> Traits;
}

public class Unit
{
    public string Id { get; private set; }

    public string Name { get; private set; }
    
    public string RequiredTrait;

    public List<UnitEffect> Effects;

    public class Mapper : IMapper<UnitDto, Unit>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(UnitDto source, Unit destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.RequiredTrait = source.RequiredTrait;
            destination.Effects = UnitEffect.Mapper.Instance.MapList(source.Effects);
        }
    }
}

public class UnitEffect
{
    public string Type { get; private set; }

    public double Value { get; private set; }

    public class Mapper : IMapper<UnitEffectDto, UnitEffect>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(UnitEffectDto source, UnitEffect destination)
        {
            destination.Type = source.Type;
            destination.Value = source.Value;
        }
    }
}

[Serializable]
public class UnitDto
{
    public string Id;
    public string Name;
    public string RequiredTrait;
    public List<UnitEffectDto> Effects;
}

[Serializable]
public class UnitEffectDto
{
    public string Type;
    public double Value;
}

public class ProductionAction
{
    public string Id { get; private set; }

    public string Name { get; private set; }

    public IReadOnlyList<ProductionActionIngredient> Consume { get; set; }

    public IReadOnlyList<ProductionActionIngredient> Produce { get; set; }

    public class Mapper : IMapper<ProductionActionDto, ProductionAction>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(ProductionActionDto source, ProductionAction destination)
        {
            destination.Id = source.Id;
            destination.Name = source.Name;
            destination.Consume = ProductionActionIngredient.Mapper.Instance.MapList(source.Consume);
            destination.Produce = ProductionActionIngredient.Mapper.Instance.MapList(source.Produce);
        }
    }
}

public class ProductionActionIngredient
{
    public string Id { get; private set; }

    public double Amount { get; private set; }

    public class Mapper : IMapper<ProductionActionIngredientDto, ProductionActionIngredient>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(ProductionActionIngredientDto source, ProductionActionIngredient destination)
        {
            destination.Id = source.Id;
            destination.Amount = source.Amount;
        }
    }
}

[Serializable]
public class ProductionActionDto
{
    public string Id;
    public string Name;
    public List<ProductionActionIngredientDto> Consume;
    public List<ProductionActionIngredientDto> Produce;
}

[Serializable]
public class ProductionActionIngredientDto
{
    public string Id;
    public double Amount;
}