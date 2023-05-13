using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator: MonoBehaviour
{
    [SerializeField] private List<LevelSection> prefabs;
    private int _index = 0;
    
    private LevelSection _lastLevelSection;

    [Space] public float sectionLifeTime = 10f;

    private List<LevelSection> _sections;

    private float _timeLastSpawn = 0f;

    public ElevatorController ElevatorController;
    
    private void Awake()
    {
        _timeLastSpawn = Time.time;
        _sections = new List<LevelSection>();
    }

    private void Start()
    {
        _lastLevelSection = SpawnLevelSection(prefabs[0], transform.position);
    }

    private void FixedUpdate()
    {
        if (IsTimeToSpawn())
        {
            // Generate a rnd index
            _index = UnityEngine.Random.Range(0, prefabs.Count);
            _lastLevelSection = SpawnLevelSection(prefabs[_index], _lastLevelSection.endingPosition.position);
        }

        CheckSectionLifeTime();
    }

    private bool IsTimeToSpawn()
    {
        // Based on the length of the level and the current speed
        // decide the time to instantiate a new Section
        // Distance
        float distance = Vector2.Distance(prefabs[_index].transform.position, prefabs[_index].endingPosition.position);
        // v = delta S / t
        // t = delta S  / v
        float time = distance / Mathf.Abs(ElevatorController.maxVerticalSpeed);

        if (_timeLastSpawn + (time/4) <= Time.time)
            return true;
        
        return false;
    }
    private void CheckSectionLifeTime()
    {
        foreach (var sec in _sections)
        {
            if (sec.spawnTime + sectionLifeTime <= Time.time)
            {
                _sections.Remove(sec);
                return;
            }
        }
    }
    private LevelSection SpawnLevelSection(LevelSection prefab , Vector3 spawnPosition)
    {
        LevelSection ending = Instantiate(prefab, spawnPosition, Quaternion.identity);
        
        ending.spawnTime = Time.time;
        _sections.Add(ending);
        _timeLastSpawn = Time.time;
        return ending;
    }
}
