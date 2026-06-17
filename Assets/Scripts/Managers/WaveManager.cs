using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] private int currentWave;

    [SerializeField] private int zombiesTotal;

    [SerializeField] private int baseCount = 10;

    private int spawnCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentWave = 1;
        spawnCount = baseCount;
        zombiesTotal = spawnCount;
    }

    void OnEnable()
    {
        GameEvents.OnZombieKilled += HandleZombieKilled;
    }

    void OnDisable()
    {
        GameEvents.OnZombieKilled -= HandleZombieKilled;
    }

    private void HandleZombieKilled( int points)
    {
        zombiesTotal--;

        if (zombiesTotal <= 0)
        {
            currentWave++;
            spawnCount = Mathf.RoundToInt(baseCount * (currentWave *1.5f));
            zombiesTotal = spawnCount;
            GameEvents.WaveCleared(currentWave, spawnCount);
        }
        
    }

   
}
