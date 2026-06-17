using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject normalZombiePrefab;

    [SerializeField] private float spawnInterval = 2f;

    private Camera mainCamera;
    private float camHeight;
    private float camWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        camHeight = mainCamera.orthographicSize;
        camWidth = camHeight * Screen.width / Screen.height;

        StartCoroutine(SpawnWave(10));
    }

    void OnEnable()
    {
        GameEvents.OnWaveCleared += SpawnNextWave;
    }

    void OnDisable()
    {
        GameEvents.OnWaveCleared -= SpawnNextWave;
    }

    private void SpawnNextWave(int wave, int spawnCount)
    {
        StartCoroutine(SpawnWave(spawnCount));
    }

    private IEnumerator SpawnWave(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnZombie()
    {
        Vector3 spawnPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 0f);

         int edge = Random.Range(0, 4);

         switch (edge)
         {
             case 0: // Top
                 spawnPosition.y += camHeight + 1f;
                 spawnPosition.x += Random.Range(-camWidth - 1f, camWidth + 1f);
                 break;
             case 1: // Bottom
                 spawnPosition.y -= camHeight + 1f;
                 spawnPosition.x += Random.Range(-camWidth - 1f, camWidth + 1f);
                 break;
             case 2: // Left
                 spawnPosition.x -= camWidth + 1f;
                 spawnPosition.y += Random.Range(-camHeight - 1f, camHeight + 1f);
                 break;
             case 3: // Right
                 spawnPosition.x += camWidth + 1f;
                 spawnPosition.y += Random.Range(-camHeight - 1f, camHeight + 1f);
                 break;
         }
        
        Instantiate(normalZombiePrefab, spawnPosition, Quaternion.identity);
    }  
}
