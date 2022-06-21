using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Transform target;
    public float range = 15f;
    public string EnemyTag = "Enemy";
    public Transform AxisRotation;
    public float TurnSpeed = 10f;
    public float AttackSpeed = 1f;
    private float AttackTimer = 0;
    public GameObject BulletPrefab;
    public Transform BulletSpawn;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f); // roept hiermee de UpdateTarget methode (met herhaling). Hij doet dit 0 seconden na het begin en dit herhaalt om de 0.5s). 
    }
    void UpdateTarget() // Met UpdateTarget wordt de enemy gekozen die zich het dichtst bij de turret bevindt.
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag); // hiermee maak je een array van alle GameObjects die de EnemyTag hebben (Enemy). Dit is een tag die verbonden is aan de enemies.
        GameObject ClosestEnemy = null; 
        float ShortestDistance = Mathf.Infinity;  // als er nog geen enemies zijn, dan is de kortste afstand tussen de enemies en de turret oneindig.
        foreach (GameObject enemy in enemies) // voor elke enemy in de array enemies wordt het volgend stukje uitgevoerd. 
        {
            float EnemyDistance = Vector3.Distance(transform.position, enemy.transform.position); // hiermee wordt de afstand tussen de turret en de enemy aan de hand van een vector berekend en opgeslagen in een float genaamd EnemyDistance
            if (EnemyDistance < ShortestDistance) // Als er een afstand gemeten wordt tussen een andere enemy die kleiner is dan die die nu als 'enemy' wordt gezien, 
            {                                     //wordt deze afstand de nieuwe ShortestDistance (voor de eerste enemy die spawnt is dat gelijk het geval, want het moet minder dan oneindig zijn)
                ShortestDistance = EnemyDistance; // hier wordt dan de nieuwe ShortestDistance opgeslagen.
                ClosestEnemy = enemy; // ClosestEnemy is dan de enemy waarvan de afstand naar de turret het kortst is.
            }
        }
        if (ClosestEnemy != null && ShortestDistance <= range) // als er een 'ClosestEnemy' is gekozen en de kortste afstand (dus van de turret naar de ClosestEnemy) 
        {                                                      // korter is dan de range (bereik) van de turret, wordt deze een target voor de turret
            target = ClosestEnemy.transform;
        }
        else   // als de ShortestDistance nog steeds groter is dan de range dan is de ClosestEnemy nog geen target.
        {
            target = null;
        }
    }

    void Update()
    {
        if (target == null) // als er geen target is, gebeurt er nog niks. 
            return;

        Vector3 dir = target.position - transform.position; // Als er een target is, wordt er een vector gemaakt van de turret naar de target.
        Quaternion lookRotation = Quaternion.LookRotation(dir); // nu wordt er een draaibeweging gecreeerd voor de turret om langs de vector naar de target te kijken.
        Vector3 rotation = Quaternion.Lerp(AxisRotation.rotation, lookRotation, Time.deltaTime * TurnSpeed).eulerAngles; //GameObject AxisRotation (waaraan het draaiend deel van de turret zit verbonden) moet draaien naar de lookRotation met de eerder ingestelde TurnSpeed (rotatiesnelheid). 
        AxisRotation.rotation = Quaternion.Euler(0f, rotation.y, 0f); // moet alleen de y-as draaien. (Vorige regel: euler.Angles houdt in dat de Quaternion wordt omgezet wordt in een rotatie om de (x,y,z) assen.

        if (AttackTimer <= 0)  // als de AttackTimer gelijk is aan 0 of lager, vuurt hij (dit gebeurt gelijk omdat hij al in is gesteld op 0)
        {
            Attack();
            AttackTimer = 1f / AttackSpeed; // De AttackTimer wordt nu opnieuw ingesteld op 1 gedeeld door de AttackSpeed die ik heb gekozen.
        }

        AttackTimer -= Time.deltaTime; // AttackTimer loopt af met de tijd. 

    }

    void Attack() // Als Attack() wordt geroepen in void Update(), dan wordt een bullet (kogel) aangemaakt.
    {
        GameObject BulletGameObject = (GameObject)Instantiate(BulletPrefab, BulletSpawn.position, BulletSpawn.rotation); // kogel wordt aangemaakt aan de hand van de BulletPrefab, wordt geplaatst op de BulletSpawn locatie.
        Bullet bullet = BulletGameObject.GetComponent<Bullet>(); // Er wordt iets uit de component Bullet gehaald.

        if (bullet != null) 
            bullet.ChaseTarget(target); // als er een kogel is, wordt de methode public void ChaseTarget(Transform TargetA) geroepen.
    }

    void OnDrawGizmosSelected() // dit is gewoon een vormpje dat om de turret heen wordt getekend zodat je zijn bereik ziet als je erop klikt.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}