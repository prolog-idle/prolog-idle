using System;
using System.Linq;

public class GameSimulation
{
    private readonly GameState _state = GameState.Instance;

    public void Start()
    {
        _state.People = 10;
        _state.LastUpdate = DateTime.UtcNow;
    }

    public void Update(DateTime dateTime)
    {
        var delta = (float) (dateTime - _state.LastUpdate).TotalSeconds;
        var resources = _state.Resources;

        var freePeople = _state.People;
        var gameDatabase = GameDatabase.Instance;
        var availableWorkplaces = gameDatabase.Units.ToList();
        foreach (var workplace in availableWorkplaces)
        {
            foreach (var effect in workplace.Effects)
            {
                if (effect.Type == "gather")
                {
                    var gatherables = gameDatabase.Resources.Where(r => r.Gatherable > 0);
                    foreach (var gatherable in gatherables)
                    {
                        var amount = freePeople * gatherable.Gatherable * effect.Value * delta;
                        _state.Resources.Add((gatherable, amount));
                    }
                }
            }
        }

        resources.Remove((GameDatabase.Instance.FindResource("fruit"), 1), _state.People * delta);

        _state.LastUpdate = dateTime;
    }
}