using System;
using static ResourceIndex;

public class GameSimulation
{
    private readonly GameState _state = GameState.Instance;

    public void Start()
    {
        _state.People = 10;
        _state.Resources[Fruit].Value = 6;
        _state.LastUpdate = DateTime.UtcNow;
    }

    public void Update(DateTime dateTime)
    {
        var delta = (float) (dateTime - _state.LastUpdate).TotalSeconds;
        var resources = _state.Resources;

        var gathererCount = _state.People;
        var gatherFruitsPerSecond = 1.01f;
        resources[Fruit].Value += gathererCount * gatherFruitsPerSecond * delta;
        var gatherStickPerSecond = 0.02f;
        resources[Stick].Value += gathererCount * gatherStickPerSecond * delta;
        var gatherStonePerSecond = 0.015f;
        resources[Stone].Value += gathererCount * gatherStonePerSecond * delta;

        var consumeFruitsPerSecond = 1f;
        resources[Fruit].Value -= _state.People * consumeFruitsPerSecond * delta;

        _state.LastUpdate = dateTime;
    }
}