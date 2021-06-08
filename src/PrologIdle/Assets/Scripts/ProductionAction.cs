public class ProductionAction
{
    public ProductionAction(string name, Resource product, params Resource[] ingredients)
    {
        Name = name;
        Product = product;
        Ingredients = ingredients;
    }

    public string Name { get; }
    
    public Resource Product { get; }

    public Resource[] Ingredients { get; }
}