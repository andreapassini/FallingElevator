using UnityEngine;

public class Obstacle: MonoBehaviour, IHittable
{
    
    
    public void Hit(Transform transformHitter)
    {
        Debug.Log("Hit Obstacle");
        
        // Invoke the hit event and let the level controller manage this stuff
    }
}
