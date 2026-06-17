using System;

public static class GameEvents
{
    public static event Action<int> OnZombieKilled;
    public static event Action<int, int> OnWaveCleared;
    public static event Action OnPlayerDeath;
    
    public static void ZombieKilled(int points)
    {
        OnZombieKilled?.Invoke(points);
    }

    public static void WaveCleared(int wave, int spawnCount)
    {
        OnWaveCleared?.Invoke(wave, spawnCount);
    }


    public static void PlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }

}
