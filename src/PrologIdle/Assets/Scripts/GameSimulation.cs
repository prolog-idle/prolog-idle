using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ResourceIndex;
using static WorkplaceIndex;

public class GameSimulation
{
    private readonly GameState _state = GameState.Instance;
    private readonly List<Workplace> _workplaces = new List<Workplace>();

    public void Start()
    {
        var gatherer = new Workplace(
            Gatherer,
            new List<Resource>(),
            new List<Resource>
            {
                (Fruit, 1.01)
            }
        );
        _workplaces.Add(gatherer);

        _state.People = 10;
        _state.LastUpdate = DateTime.UtcNow;
    }

    public void Update(DateTime dateTime)
    {
        var delta = (float) (dateTime - _state.LastUpdate).TotalSeconds;
        var resources = _state.Resources;

        var freePeople = _state.People;
        var availableWorkplaces = _workplaces
            .Where(w => w.IsAvailable())
            .ToList();
        for (var i = 0; i < _workplaces.Count; i++)
        {
            var workplace = _workplaces[i];
            var workers = (int) Mathf.Ceil((float) freePeople / (availableWorkplaces.Count - i));
            freePeople -= workers;
            workplace.Tick(workers, delta);
        }

        var consumeFruitsPerSecond = (Fruit, 1);
        resources.Remove(consumeFruitsPerSecond, _state.People * delta);

        _state.LastUpdate = dateTime;
    }
}