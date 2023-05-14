using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        }
    }
    private void OnEnable()
    {
        Obstacle.onHitObstacle += RegisterHit;
    }
    private void OnDisable()
    {
        Obstacle.onHitObstacle -= RegisterHit;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void RegisterHit()
    {
        LoadLevelScene();
    }
    public static void LoadLevelScene()
    {
        Scene scene = SceneManager.GetSceneByName("Level"); 
        SceneManager.LoadScene("Level");
    }

    public static void LoadStartingMenu()
    {
        SceneManager.LoadScene("StartingScene");
    }
}
