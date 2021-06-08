using System;
using UnityEngine;

public class GameSimulationHost : MonoBehaviour
{
    private readonly GameSimulation _gameSimulation = new GameSimulation();

    public void Start()
    {
        _gameSimulation.Start();
    }

    private void Update()
    {
        _gameSimulation.Update(DateTime.UtcNow);
    }
}