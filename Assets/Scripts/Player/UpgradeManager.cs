using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeData> allUpgrades;
    [SerializeField] private UpgradeStats upgradeStats;
    private List<UpgradeData> currentChoices;
    

    void OnEnable()
    {
        GameEvents.OnWaveCleared += HandleWaveCleared;
    }
    void OnDisable()
    {
        GameEvents.OnWaveCleared -= HandleWaveCleared;
    }

    private void HandleWaveCleared(int waveNum, int spawnCount)
    {
        currentChoices = GetRandomUpgrades(3);
        GameEvents.UpgradeScreenOpened();
    }

    private List<UpgradeData> GetRandomUpgrades(int count)
    {
        List<UpgradeData> shuffled = new List<UpgradeData>(allUpgrades);

        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            UpgradeData temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }
        return shuffled.GetRange(0, count);
    }

    public void SelectUpgrade(UpgradeData upgrade)
    {
        upgradeStats.ApplyUpgrade(upgrade);
        GameEvents.UpgradeScreenClosed();
    }

    public List<UpgradeData> GetCurrentChoices()
    {
        return new List<UpgradeData>(currentChoices);
    }
}
