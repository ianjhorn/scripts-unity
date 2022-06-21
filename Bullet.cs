using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform target; 
    public float BulletSpeed = 50f;
    public int BulletDamage = 1;


    public void ChaseTarget(Transform targetA)
    {
        target = targetA;
    }

    void Update() // // als de target al weg is voordat de bullet aankomt, gaat de bullet weg.
    {
        if (target == null)                 
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position; // Dit maakt een vector die naar de target wijst.
        float distanceperframe = BulletSpeed * Time.deltaTime;

        if (dir.magnitude <= distanceperframe) // dir.magnitude is afstand van bullet tot target. Dus als de afstand van bullet tot target kleiner is 
        {                                      // dan wat er in het volgend frame wordt afgelegd, wordt hij geraakt, want anders gaat hij er langs.
            HitTarget();                                                  
            return;
        }
        transform.Translate(dir.normalized * distanceperframe, Space.World); //normalized: bullet gaat met constante snelheid, beweegt ten 
                                                                             //opzichte van World space en niet local space anders krijg je een circulaire beweging
    }
    void HitTarget()               
    {
        Destroy(target.gameObject);                    // hier staat dat als hij geraakt wordt, dan wordt de enemy gelijk vernietigd. Ik heb geprobeerd om te coderen dat het 
    }                                                  // na meerdere schoten dood gaat. Hiervoor moet je de enemy in de enemy script een hitpoint waarde geven en een component aanmaken
}                                                      // waarin staat dat hij hitpoints verliest als HitTarget() gebeurt. Dit gaf een error die ik niet kon oplossen, dus heb ik het eruit gelaten.
