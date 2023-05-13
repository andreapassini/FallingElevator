using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

public class LevelGenerator: MonoBehaviour
{
    [SerializeField] private List<LevelSection> prefabs;
    private int _index = 0;

    private IEnumerator _spawnerCor;
    private float _currentSpeed = 0f;
    
    private LevelSection _lastLevelSection;
    public LevelSection startingSection;
    
    [Space]
    public float minVerticalSpeed = 5f;
    public float maxVerticalSpeed = 25f;
    public float acceleration = .5f;
    private float _verticalSpeed = 0f;

    private List<LevelSection> _sections;
    
    private void Awake()
    {
        _lastLevelSection = startingSection;
        
        _lastLevelSection.verticalSpeed = minVerticalSpeed;
        _lastLevelSection.minVerticalSpeed = minVerticalSpeed;
        _lastLevelSection.maxVerticalSpeed = maxVerticalSpeed;
    }

    private void Start()
    {
        _spawnerCor = SpawnerCor();
        StartCoroutine(SpawnerCor());
    }

    private void FixedUpdate()
    {
        // Calculate speed
        _verticalSpeed += (_verticalSpeed * acceleration) * Time.fixedDeltaTime;
    
        if (_verticalSpeed >= maxVerticalSpeed)
            _verticalSpeed = maxVerticalSpeed;
        
        foreach (var section in _sections)
        {
            section.transform.position = new Vector3(transform.position.x, transform.position.y + _verticalSpeed);
        }
    }

    // Based on the length of the level and the current speed
    // decide the time to instantiate a new Section
    private IEnumerator SpawnerCor()
    {
        float time = 0f;
        float distance = 0f;
        
        while (true)
        {
            // Distance
            distance = Vector2.Distance(prefabs[_index].transform.position, prefabs[_index].endingPosition.position);
            // v = delta S / t
            // t = delta S  / v
            time = distance / maxVerticalSpeed;
            
            Debug.Log("wait time:" + time / 2f);
            
            yield return new WaitForSeconds(time / 2f);
            
            // Generate a rnd index
            _index = UnityEngine.Random.Range(0, prefabs.Count);

            _lastLevelSection = SpawnLevelSection(prefabs[_index], _lastLevelSection.endingPosition.position);
            _sections.Add(_lastLevelSection);
        }
    }

    private LevelSection SpawnLevelSection(LevelSection prefab , Vector3 spawnPosition)
    {
        // prefab.maxVerticalSpeed = maxVerticalSpeed;
        // prefab.minVerticalSpeed = _currentSpeed;
        LevelSection ending = Instantiate(prefab, spawnPosition, Quaternion.identity);
        _currentSpeed = _lastLevelSection.verticalSpeed;
        ending.maxVerticalSpeed = maxVerticalSpeed;
        ending.minVerticalSpeed = _currentSpeed;
        return ending;
    }
}
