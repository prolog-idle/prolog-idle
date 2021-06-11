using System.Collections.Generic;
using Razensoft.Functional;
using Razensoft.Mapper;
using UnityEngine;

public class GameDatabase
{
    static GameDatabase()
    {
        var json = UnityEngine.Resources.Load<TextAsset>("data").text;
        var patch = GameDatabasePatch.FromJson(json);
        var instance = new GameDatabase();
        instance.ApplyPatch(patch);
        Instance = instance;
    }
    
    public static GameDatabase Instance { get; }

    public List<Resource> Resources { get; } = new List<Resource>();

    public List<ProductionAction> ProductionActions { get; } = new List<ProductionAction>();

    public List<Unit> Units { get; } = new List<Unit>();

    public Resource FindResource(string id) => Resources.Find(r => r.Id == id);

    public Unit FindUnit(string id) => Units.Find(r => r.Id == id);
    public ProductionAction FindProductionAction(string id) => ProductionActions.Find(r => r.Id == id);

    public void ApplyPatch(GameDatabasePatch patch)
    {
        foreach (var resourcePatch in patch.Resources)
        {
            var resource = FindResource(resourcePatch.Id);
            if (resource == null)
            {
                resource = new Resource(resourcePatch.Id);
                Resources.Add(resource);
            }
            Resource.Mapper.Instance.Map(resourcePatch, resource);
        }
        foreach (var unitPatch in patch.Units)
        {
            var unit = FindUnit(unitPatch.Id);
            if (unit == null)
            {
                unit = new Unit(unitPatch.Id);
                Units.Add(unit);
            }
            Unit.Mapper.Instance.Map(unitPatch, unit);
        }
        foreach (var productionActionPatch in patch.ProductionActions)
        {
            var productionAction = FindProductionAction(productionActionPatch.Id);
            if (productionAction == null)
            {
                productionAction = new ProductionAction(productionActionPatch.Id);
                ProductionActions.Add(productionAction);
            }
            ProductionAction.Mapper.Instance.Map(productionActionPatch, productionAction);
        }
    }
}

public class Resource
{
    public Resource(string id)
    {
        Id = id;
    }
    
    public string Id { get; }

    public string Name { get; private set; }

    public double Gatherable { get; private set; }
    
    public IReadOnlyList<string> Traits { get; set; }
    
    public class Mapper : IMapper<ResourcePatch, Resource>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(ResourcePatch source, Resource destination)
        {
            destination.Name = source.Name.Unwrap(destination.Name);
            destination.Gatherable = source.Gatherable ?? destination.Gatherable;
            destination.Traits = source.Traits.Unwrap(destination.Traits);
        }
    }
}

public class Unit
{
    public Unit(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public string Name { get; private set; }
    
    public string RequiredTrait;

    public List<UnitEffect> Effects;

    public class Mapper : IMapper<UnitPatch, Unit>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(UnitPatch source, Unit destination)
        {
            destination.Name = source.Name.Unwrap(destination.Name);
            destination.RequiredTrait = source.Name.Unwrap(destination.RequiredTrait);
            destination.Effects = source.Effects.Unwrap(UnitEffect.Mapper.Instance.MapList, destination.Effects);
        }
    }
}

public class UnitEffect
{
    public string Type { get; private set; }

    public double Value { get; private set; }

    public class Mapper : IMapper<UnitEffectPatch, UnitEffect>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(UnitEffectPatch source, UnitEffect destination)
        {
            destination.Type = source.Type;
            destination.Value = source.Value;
        }
    }
}

public class ProductionAction
{
    public ProductionAction(string id)
    {
        Id = id;
    }

    public string Id { get; }

    public string Name { get; private set; }

    public IReadOnlyList<ProductionActionIngredient> Consume { get; set; }

    public IReadOnlyList<ProductionActionIngredient> Produce { get; set; }

    public class Mapper : IMapper<ProductionActionPatch, ProductionAction>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(ProductionActionPatch source, ProductionAction destination)
        {
            destination.Name = source.Name.Unwrap(destination.Name);
            destination.Consume = source.Consume.Unwrap(
                ProductionActionIngredient.Mapper.Instance.MapList,
                destination.Consume
            );
            destination.Produce = source.Produce.Unwrap(
                ProductionActionIngredient.Mapper.Instance.MapList,
                destination.Consume
            );
        }
    }
}

public class ProductionActionIngredient
{
    public string Id { get; private set; }

    public double Amount { get; private set; }

    public class Mapper : IMapper<ProductionActionIngredientPatch, ProductionActionIngredient>
    {
        public static Mapper Instance { get; } = new Mapper();
        
        public void Map(ProductionActionIngredientPatch source, ProductionActionIngredient destination)
        {
            destination.Id = source.Id;
            destination.Amount = source.Amount;
        }
    }
}