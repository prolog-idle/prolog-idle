public class GameSimulation
{
    private readonly GameState _state = GameState.Instance;

    public void Start()
    {
        _state.Fruits = 10;
        _state.People = 6;
    }

    public void Update(float delta)
    {
        var gathererCount = _state.People;
        var gatherFruitsPerSecond = 1.01f;
        _state.Fruits += gathererCount * gatherFruitsPerSecond * delta;
        var gatherStickPerSecond = 0.02f;
        _state.Sticks += gathererCount * gatherStickPerSecond * delta;
        var gatherStonePerSecond = 0.015f;
        _state.Stones += gathererCount * gatherStonePerSecond * delta;

        var consumeFruitsPerSecond = 1f;
        _state.Fruits -= _state.People * consumeFruitsPerSecond * delta;
    }
}