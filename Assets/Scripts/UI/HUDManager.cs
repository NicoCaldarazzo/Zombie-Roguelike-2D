using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    //Add a field in the inspector to drag the HUD Texts in
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI dollarsText;
    [SerializeField] private TextMeshProUGUI totalMoney;


    void Start()
    {

        scoreText.text = "Score: 0";
        waveText.text = "Wave: 1";
        dollarsText.text = "Dollars: 0";
        totalMoney.text = "All Monies: 0";

    }

    //Subscribe to events using methods
        void OnEnable()
    {
        GameEvents.OnScoreChanged += UpdateScore;
        GameEvents.OnWaveChange += UpdateWave;
        GameEvents.OnCurrentCurrencyChange += UpdateCurrentDollars;
        GameEvents.OnTotalCurrencyChange += UpdateTotalMoney;

    }
    void OnDisable()
    {
        GameEvents.OnScoreChanged -= UpdateScore;
        GameEvents.OnWaveChange -= UpdateWave;
        GameEvents.OnCurrentCurrencyChange -= UpdateCurrentDollars;
        GameEvents.OnTotalCurrencyChange -= UpdateTotalMoney;
    }
    

    //The Methods that get called from subscribing to the event
    private void UpdateScore(int Score)
    {
        scoreText.text = "Score: " + Score;
    }
    private void UpdateWave(int WaveNum)
    {
        waveText.text = "Wave: " + WaveNum;
    }
    private void UpdateCurrentDollars(int Dollars)
    {
        dollarsText.text = "Dollars: " + Dollars;
    }
    private void UpdateTotalMoney(int TotalDollars)
    {
        totalMoney.text = "All Monies: " + TotalDollars;
    }


}
