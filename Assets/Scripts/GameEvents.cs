using System;

public static class GameEvents
{
    
    public static event Action<int> OnZombieKilled;
    public static event Action<int> OnWaveCleared;
    public static event Action OnPlayerDeath;

}
