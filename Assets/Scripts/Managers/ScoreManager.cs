using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    //Player Stats
    public int currentScore { get; private set; }
    public int currentCurrency { get; private set; }
    public int currentWave { get; private set; }
    public int highScore { get; private set; }
    public int totalCurrency { get; private set; }
    public int highestWave { get; private set; }

    private const string HIGH_SCORE_KEY = "HighScore";
    private const string TOTAL_CURRENCY_KEY = "TotalCurrency";
    private const string HIGHEST_WAVE_KEY = "HighestWave";

    

        void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadStats();
            ResetRun();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        GameEvents.OnZombieKilled += HandleEnemyKilled;
        GameEvents.OnWaveCleared += HandleWaveCleared;
        GameEvents.OnPlayerDeath += HandlePlayerDeath;
    }

    void OnDisable()
    {
        GameEvents.OnZombieKilled -= HandleEnemyKilled;
        GameEvents.OnWaveCleared -= HandleWaveCleared;
        GameEvents.OnPlayerDeath -= HandlePlayerDeath;
    }
    void Start()
    {
        GameEvents.ScoreChanged(currentScore);
        GameEvents.CurrentCurrencyChanged(currentCurrency);
        GameEvents.TotalCurrencyChange(totalCurrency);
        GameEvents.WaveChanged(currentWave);
    }

    private void HandleEnemyKilled(int dollars)
    {
        AddCurrency(dollars);
        currentScore += 10;
        GameEvents.ScoreChanged(currentScore);
    }
    private void HandleWaveCleared(int WaveNumber, int SpawnCount)
    {
        AddScore(SpawnCount, WaveNumber);
        SaveStats();
        GameEvents.WaveChanged(WaveNumber);
    }
    private void HandlePlayerDeath()
    {
        SaveStats();
        ResetRun();
    }

   private void AddScore(int scorePoints, int WaveNumber)
    {
        currentWave = WaveNumber;

        if (currentScore > highScore)
        {
            highScore = currentScore;
        }
        if (currentWave > highestWave)
        {
            highestWave = currentWave;
        }


    }

    private void AddCurrency(int amount)
    {
        currentCurrency += amount;
        totalCurrency += amount;
        GameEvents.CurrentCurrencyChanged(currentCurrency);
        GameEvents.TotalCurrencyChange(totalCurrency);
    }

    private void SaveStats()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.SetInt(TOTAL_CURRENCY_KEY, totalCurrency);
        PlayerPrefs.SetInt(HIGHEST_WAVE_KEY, highestWave);
        PlayerPrefs.Save();
    }

    private void LoadStats()
    {
  
            highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY);
            highestWave = PlayerPrefs.GetInt(HIGHEST_WAVE_KEY);
            totalCurrency = PlayerPrefs.GetInt(TOTAL_CURRENCY_KEY);
      
        
    }

    public void ResetRun()
    {
        currentScore = 0;
        currentCurrency = 0;
        currentWave = 0;

        SaveStats();
    }

    void OnApplicationQuit()
    {
        SaveStats();
    }

    
}
