using System;

public class GameState
{
    public static GameState Instance { get; } = new GameState();

    public DateTime LastUpdate;
    public int People;
    public ResourceDatabase Resources = new ResourceDatabase();
}