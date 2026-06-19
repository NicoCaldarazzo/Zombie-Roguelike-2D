using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeData", menuName = "Upgrades/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public UpgradeType upgradeType;
    public float upgradeValue;
    public bool isPercentageBased;

    public string GetDescription()
    {
        if (isPercentageBased)
            return $"{description} {upgradeValue}%";
        else    
            return $"{description} +{upgradeValue}";
    }
}
