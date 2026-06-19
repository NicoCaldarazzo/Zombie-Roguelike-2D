using System;

public static class GameEvents
{
    //Have to add an event to subscribe to it from anywhere with OnEnable() & OnDisable()
    public static event Action<int> OnZombieKilled;
    public static event Action<int, int> OnWaveCleared;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnTotalCurrencyChange;
    public static event Action<int> OnScoreChanged;
    public static event Action<int> OnCurrentCurrencyChange;
    public static event Action<int> OnWaveChange;
    public static event Action OnUpgradeApplied;
    public static event Action OnUpgradeScreenOpened;
    public static event Action OnUpgradeScreenClosed;
    
    //Have to invoke event from a call to it and add the values within method
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

     public static void CurrentCurrencyChanged(int amount)
    {
        OnCurrentCurrencyChange?.Invoke(amount);
    }
    public static void TotalCurrencyChange(int dollarAmount)
    {
        OnTotalCurrencyChange?.Invoke(dollarAmount);
    }
    public static void ScoreChanged(int scoreValue)
    {
        OnScoreChanged?.Invoke(scoreValue);
    }
    public static void WaveChanged(int newWaveNum)
    {
        OnWaveChange?.Invoke(newWaveNum);
    }
    public static void UpgradeApplied()
    {
        OnUpgradeApplied?.Invoke();
    }
     public static void UpgradeScreenOpened()
    {
        OnUpgradeScreenOpened?.Invoke();
    }
     public static void UpgradeScreenClosed()
    {
        OnUpgradeScreenClosed?.Invoke();
    }
}
