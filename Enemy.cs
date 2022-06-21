using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;
    public float Hitpoints = 5;
    private Transform target;
    private int waypointIndex = 0; 

    void Start() // eerst gaat hij naar de eerste waypoint met waypoint punt 0.
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position; // vector naar volgende waypoint (target)
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // plaats wordt elke frame geupdatet volgens de snelheid die geselecteerd is, Space.World voorkomt een circulaire beweging.

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) // als hij 0.4 van de waypoint verwijderd is, gaat hij naar de volgende waypoint
        {
            GetNextWaypoint();
        }
    }
    void GetNextWaypoint() 
    {
        if (waypointIndex >= Waypoints.points.Length - 1) // als de waypointIndex gelijk is (kan niet hoger) dan het aantal waypoints, dan heeft hij de laatste gehaald en dat is dus de toren en wordt methode Arrive geroepen.
        {
            Arrive();  
            return;
        }
        waypointIndex++; // waypointIndex gaat 1 omhoog
        target = Waypoints.points[waypointIndex]; // zoekt nu de volgende waypoint met de nieuwe 'waypointIndex'

    
    }
    void Arrive () // als de enemy bij de 'tower' aankomt, verliest de speler een leven, verdwijnt de enemy, en wordt uitgeprint hoeveel levens er over blijven.
    {
        PlayerStats.lives--;
        Destroy(gameObject);
        Debug.Log(PlayerStats.lives);
    }
}