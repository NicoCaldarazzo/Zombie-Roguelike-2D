using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class UpgradeScreen : MonoBehaviour
{
   [SerializeField] private GameObject upgradePanel;
   [SerializeField] private Button[] upgradeCards;
   [SerializeField] private TextMeshProUGUI[] upgradeNames;
   [SerializeField] private TextMeshProUGUI[] upgradeDescriptions;
   [SerializeField] private Image[] upgradeIcons;
   [SerializeField] private UpgradeManager upgradeManager;
   private List<UpgradeData> currentChoices;

    void OnEnable()
    {
        GameEvents.OnUpgradeScreenOpened += HandleUpgradeScreenOpened;
        GameEvents.OnUpgradeScreenClosed += HandleUpgradeScreenClosed;
    } 
    void OnDisable()
    {
        GameEvents.OnUpgradeScreenOpened -= HandleUpgradeScreenOpened;
        GameEvents.OnUpgradeScreenClosed -= HandleUpgradeScreenClosed;
    }
    void Start()
    {
        upgradePanel.SetActive(false);

        for (int i = 0; i <upgradeCards.Length; i++)
        {
            int index = i;
            upgradeCards[i].onClick.AddListener(() => OnCardClicked(index));
        }
    }

    private void HandleUpgradeScreenOpened()
    {
        currentChoices = upgradeManager.GetCurrentChoices();
        PopulateCards();
        upgradePanel.SetActive(true);
    }
    private void HandleUpgradeScreenClosed()
    {
        upgradePanel.SetActive(false);
    }

    private void PopulateCards()
    {
        for (int i = 0; i <currentChoices.Count; i++)
        {
            upgradeNames[i].text = currentChoices[i].upgradeName;
            upgradeDescriptions[i].text = currentChoices[i].GetDescription();
            upgradeIcons[i].sprite = currentChoices[i].icon;
        }
    }

    private void OnCardClicked(int index)
    {
        upgradeManager.SelectUpgrade(currentChoices[index]);
    }

}
