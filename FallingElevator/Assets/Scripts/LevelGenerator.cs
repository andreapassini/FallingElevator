using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator: MonoBehaviour
{
    [SerializeField] private List<LevelSection> prefabs;
    private int _index = 0;

    private IEnumerator _spawnerCor;
    private float _currentSpeed = 100f;
    
    private LevelSection _lastLevelSection;
    public LevelSection startingSection;
    private void Start()
    {
        _spawnerCor = SpawnerCor();
        StartCoroutine(SpawnerCor());

        _lastLevelSection = startingSection;
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
            time = distance / _currentSpeed;
            
            Debug.Log("wait time:" + time / 2f);
            
            yield return new WaitForSeconds(time / 2f);
            
            _lastLevelSection = SpawnLevelSection(prefabs[_index], _lastLevelSection.endingPosition.position);

            _index++;
        }
    }

    private LevelSection SpawnLevelSection(LevelSection prefab , Vector3 spawnPosition)
    {
        LevelSection ending = Instantiate(prefab, spawnPosition, Quaternion.identity);
        return ending;
    }
}
