using System;
using System.Collections.Generic;
using System.Linq;
using Razensoft.Functional;
using Razensoft.Mapper;

public class GameDatabasePatch
{
    public List<ResourcePatch> Resources { get; } = new List<ResourcePatch>();
    public List<UnitPatch> Units { get; } = new List<UnitPatch>();
    public List<ProductionActionPatch> ProductionActions { get; } = new List<ProductionActionPatch>();

    public static GameDatabasePatch FromJson(string json)
    {
        var result = new GameDatabasePatch();
        var dict = (Dictionary<string, object>) Json.Deserialize(json);
        if (dict.ContainsKey("resources"))
        {
            foreach (var entry in (List<object>) dict["resources"])
            {
                var resourcePatch = ResourcePatch.Mapper.Instance.Map((Dictionary<string, object>) entry);
                result.Resources.Add(resourcePatch);
            }
        }
        if (dict.ContainsKey("units"))
        {
            foreach (var entry in (List<object>) dict["units"])
            {
                var unitPatch = UnitPatch.Mapper.Instance.Map((Dictionary<string, object>) entry);
                result.Units.Add(unitPatch);
            }
        }
        if (dict.ContainsKey("production"))
        {
            foreach (var entry in (List<object>) dict["production"])
            {
                var unitPatch = ProductionActionPatch.Mapper.Instance.Map((Dictionary<string, object>) entry);
                result.ProductionActions.Add(unitPatch);
            }
        }

        return result;
    }
}

public class ResourcePatch
{
    public string Id { get; private set; }

    public Maybe<string> Name { get; private set; }

    public double? Gatherable { get; private set; }

    public Maybe<IReadOnlyList<string>> Traits { get; private set; }

    public class Mapper : IMapper<Dictionary<string, object>, ResourcePatch>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(Dictionary<string, object> source, ResourcePatch destination)
        {
            if (!source.ContainsKey("id"))
            {
                throw new Exception("Resource patch doesn't have id");
            }

            destination.Id = (string) source["id"];
            destination.Name = source.TryFind<string>("name");
            destination.Gatherable = source.TryFindDouble("gatherable");
            destination.Traits = source.TryFind<List<object>>("traits")
                .Map(l => (IReadOnlyList<string>) l.Cast<string>().ToList());
        }
    }
}

public class UnitPatch
{
    public string Id { get; private set; }

    public Maybe<string> Name { get; private set; }

    public Maybe<string> RequiredTrait;

    public Maybe<List<UnitEffectPatch>> Effects;

    public class Mapper : IMapper<Dictionary<string, object>, UnitPatch>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(Dictionary<string, object> source, UnitPatch destination)
        {
            if (!source.ContainsKey("id"))
            {
                throw new Exception("Resource patch doesn't have id");
            }

            destination.Id = (string) source["id"];
            destination.Name = source.TryFind<string>("name");
            destination.RequiredTrait = source.TryFind<string>("required_trait");
            destination.Effects = source.TryFind<Dictionary<string, object>>("effects").Map(MapEffects);
        }

        private static List<UnitEffectPatch> MapEffects(Dictionary<string, object> dict)
        {
            return dict.Select(kvp => new UnitEffectPatch(kvp.Key, Convert.ToDouble(kvp.Value))).ToList();
        }
    }
}

public class UnitEffectPatch
{
    public UnitEffectPatch(string type, double value)
    {
        Type = type;
        Value = value;
    }
    
    public string Type { get; private set; }

    public double Value { get; private set; }
}

public static class DictionaryExtensions
{
    public static Maybe<T> TryFind<T>(this Dictionary<string, object> dict, string key)
    {
        return dict.TryFind(key).Map(o => (T) o);
    }

    public static double? TryFindDouble(this Dictionary<string, object> dict, string key)
    {
        return dict.TryGetValue(key, out var result) ? (double?) (double) result : null;
    }
}

public class ProductionActionPatch
{
    public string Id { get; private set; }

    public Maybe<string> Name { get; private set; }

    public Maybe<IReadOnlyList<ProductionActionIngredientPatch>> Consume { get; set; }

    public Maybe<IReadOnlyList<ProductionActionIngredientPatch>> Produce { get; set; }

    public class Mapper : IMapper<Dictionary<string, object>, ProductionActionPatch>
    {
        public static Mapper Instance { get; } = new Mapper();

        public void Map(Dictionary<string, object> source, ProductionActionPatch destination)
        {
            if (!source.ContainsKey("id"))
            {
                throw new Exception("Resource patch doesn't have id");
            }

            destination.Id = (string) source["id"];
            destination.Name = source.TryFind<string>("name");
            destination.Consume = source.TryFind<Dictionary<string, object>>("consume").Map(MapIngredients);
            destination.Produce = source.TryFind<Dictionary<string, object>>("produce").Map(MapIngredients);
        }

        private static IReadOnlyList<ProductionActionIngredientPatch> MapIngredients(Dictionary<string, object> dict)
        {
            return dict.Select(kvp => new ProductionActionIngredientPatch(kvp.Key, Convert.ToDouble(kvp.Value))).ToList();
        }
    }
}

public class ProductionActionIngredientPatch
{
    public ProductionActionIngredientPatch(string id, double amount)
    {
        Id = id;
        Amount = amount;
    }
    
    public string Id { get; }
    public double Amount { get; }
}