using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] id;

    void Awake () // maakt een array van alle waypoints
    {
        id = new Transform[transform.childCount]; 
        for (int i = 0; i < id.Length; i++)
        {
            id[i] = transform.GetChild(i);
        }  
    }
} 
