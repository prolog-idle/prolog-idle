using System;
using System.Collections.Generic;

public class GameState
{
    public static GameState Instance { get; } = new GameState();

    public DateTime LastUpdate;
    public int People;
    public ResourceDatabase Resources = new ResourceDatabase();
    public List<WorkplaceModifier> WorkplaceModifiers = new List<WorkplaceModifier>();
}