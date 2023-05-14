using System;
using UnityEngine;

public class LevelController: MonoBehaviour
{
    [SerializeField] private LevelGenerator _levelGenerator;
    [SerializeField] private ElevatorController _elevator;

    private void Awake()
    {
        _levelGenerator.enabled = false;
        _elevator.enabled = false;
    }

    public void StartGame()
    {
        // Enable Player and Level Generator
        _elevator.enabled = true;
        _levelGenerator.enabled = true;
    }
}
