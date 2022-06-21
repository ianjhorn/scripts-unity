using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform EnemyPrefab;
    public Transform spawnPoint;
    public float timebetweenwaves = 5f;
    public float countdown = 3f;
    public float waveNumber = 1f; 



    void Update()
    {
        if (countdown <= 0f && waveNumber <= 10f)  // gaat tot 10 waves, daarna stopt het.
        {
            StartCoroutine(SpawnWave());               
            countdown = timebetweenwaves;
        }
        countdown -= Time.deltaTime; //countdown wordt per seconde 1 kleiner

    }
    IEnumerator SpawnWave()  // moest hiervoor een IEnumerator gebruiken in plaats van een normale method. Met een IEnumerator kun je de code stopzetten.
    {  
        Debug.Log("Wave " + waveNumber);      
        waveNumber++;
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f); // 0.5s interval tussen de enemies.
        }
    }
    void SpawnEnemy()
    {
        Instantiate(EnemyPrefab, spawnPoint.position, spawnPoint.rotation); //enemy wordt gemaakt aan de hand van de enemyPrefab, wordt gespawnd bij de spawnpoint.
    }
}

   
