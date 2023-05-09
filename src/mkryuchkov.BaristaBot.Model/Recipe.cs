using UnitsNet;

namespace mkryuchkov.BaristaBot.Model;

public class Recipe
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Country { get; set; } = string.Empty;
    public string Region { get; set; } = string.Empty;
    public string Lot { get; set; } = string.Empty;

    public CoffeeProcessing Processing { get; set; } = CoffeeProcessing.Washed;

    public RecipeType Type { get; set; } = RecipeType.V60;
    public Mass Coffee { get; set; } = default;
    public Grind Grind { get; set; } = default;
    public Mass Water { get; set; } = default;
    public Temperature Temperature { get; set; } = default;

    public IList<RecipeStep> Steps { get; set; } = Array.Empty<RecipeStep>();
}
