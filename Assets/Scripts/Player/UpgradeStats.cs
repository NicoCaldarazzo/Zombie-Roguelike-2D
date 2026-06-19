using System.Collections.Generic;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    private Dictionary<UpgradeType, float> modifiers = new Dictionary<UpgradeType, float>();

    void OnEnable()
    {
        GameEvents.OnPlayerDeath += PlayerDied;
    }
    void OnDisable()
    {
        GameEvents.OnPlayerDeath -= PlayerDied;
    }

    public void ApplyUpgrade(UpgradeData upgrade)
    {
        if (modifiers.ContainsKey(upgrade.upgradeType))
            modifiers[upgrade.upgradeType] += upgrade.upgradeValue;
            
        else   
            modifiers.Add(upgrade.upgradeType, upgrade.upgradeValue);
        
        GameEvents.UpgradeApplied();    
    }

    public float GetModifier(UpgradeType type)
    {
        float value = 0f;
        if (modifiers.TryGetValue(type, out value))
            return value;
        else
            return 0f;
    }

    private void PlayerDied()
    {
        ResetStats();
    }
    private void ResetStats()
    {
        modifiers.Clear();
    }
}
