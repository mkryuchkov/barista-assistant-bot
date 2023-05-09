using UnitsNet;

namespace mkryuchkov.BaristaBot.Model;

public struct RecipeStep
{
    public Mass Water { get; set; }
    public Duration Time { get; set; }
}