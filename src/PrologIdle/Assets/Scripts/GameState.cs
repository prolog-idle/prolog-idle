public class GameState
{
    public static GameState Instance { get; } = new GameState();

    public int People;
    public float Fruits;
    public float Stones;
    public float Sticks;
    public float KnappedStones;
    public float Spears;
}