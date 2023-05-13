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
    
    [Space]
    public float minVerticalSpeed = 5f;
    public float maxVerticalSpeed = 25f;
    public float acceleration = .5f;
    private float _verticalSpeed = 0f;

    [Space] public float sectionLifeTime = 10f;

    private List<LevelSection> _sections;

    private float _timeLastSpawn = 0f;
    
    private void Awake()
    {
        _timeLastSpawn = Time.time;
        // _sections = new List<LevelSection>();
        _verticalSpeed = minVerticalSpeed;
    }

    private void Start()
    {
        _lastLevelSection = SpawnLevelSection(prefabs[0], transform.position);
    }

    private void FixedUpdate()
    {
        // Calculate speed
        _verticalSpeed += (_verticalSpeed * acceleration);
    
        if (_verticalSpeed >= maxVerticalSpeed)
            _verticalSpeed = maxVerticalSpeed;
        
        if (IsTimeToSpawn())
        {
            // Generate a rnd index
            _index = UnityEngine.Random.Range(0, prefabs.Count);
            _lastLevelSection = SpawnLevelSection(prefabs[_index], _lastLevelSection.endingPosition.position);
            Debug.Log(_lastLevelSection);

            _timeLastSpawn = Time.time;
        }

        CheckSectionLifeTime();
        
        Debug.Log(_sections.Count);

        for (int i = 0; i <= _sections.Count; i++)
        {
            Debug.Log(_verticalSpeed);
            _sections[i].transform.position = new Vector3(transform.position.x, transform.position.y + (_verticalSpeed * Time.deltaTime));
        }
    }

    private bool IsTimeToSpawn()
    {
        // Based on the length of the level and the current speed
        // decide the time to instantiate a new Section
        // Distance
        float distance = Vector2.Distance(prefabs[_index].transform.position, prefabs[_index].endingPosition.position);
        // v = delta S / t
        // t = delta S  / v
        float time = distance / maxVerticalSpeed;

        if (_timeLastSpawn + time <= Time.time)
            return true;
        
        return false;
    }
    private void CheckSectionLifeTime()
    {
        foreach (var sec in _sections)
        {
            if (sec.spawnTime + sectionLifeTime >= Time.time)
            {
                Destroy(sec, 1f);
                _sections.Remove(sec);
                return;
            }
        }
    }
    private LevelSection SpawnLevelSection(LevelSection prefab , Vector3 spawnPosition)
    {
        LevelSection ending = Instantiate(prefab, spawnPosition, Quaternion.identity);
        
        ending.spawnTime = Time.time;

        _sections.Append(ending);
        Debug.Log("After Append" + _sections.Count);
        
        return ending;
    }
}
