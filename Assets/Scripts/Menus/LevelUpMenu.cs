using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelUpMenu : MonoBehaviour
{
    [SerializeField] private PlayerUpgradeManager playerUpgradeManager;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;
    private Upgrade firstUpgrade;
    private Upgrade secondUpgrade;
    private List<Upgrade> upgrades;

    private void Awake()
    {
        upgrades = Resources.LoadAll("Upgrades", typeof(Upgrade)).Cast<Upgrade>().ToList();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        int firstUpgradeIndex = Random.Range(0, upgrades.Count);
        firstUpgrade = upgrades[firstUpgradeIndex];
        while (!playerUpgradeManager.CanPerformUpgrade(firstUpgrade))
        {
            firstUpgradeIndex = Random.Range(0, upgrades.Count);
            firstUpgrade = upgrades[firstUpgradeIndex];
        }
        int secondUpgradeIndex = Random.Range(0, upgrades.Count);
        secondUpgrade = upgrades[secondUpgradeIndex];
        while (secondUpgradeIndex == firstUpgradeIndex || !playerUpgradeManager.CanPerformUpgrade(secondUpgrade))
        {
            secondUpgradeIndex = Random.Range(0, upgrades.Count);
            secondUpgrade = upgrades[secondUpgradeIndex];
        }

        //update button image with upgrade image and text with upgrade text
        button1.image.sprite = firstUpgrade.image;
        button2.image.sprite = secondUpgrade.image;
        text1.text = string.Format(firstUpgrade.description, firstUpgrade.modifier);
        text2.text = string.Format(secondUpgrade.description, secondUpgrade.modifier);

        EventSystem.current.SetSelectedGameObject(button1.gameObject);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnButton1()
    {
        playerUpgradeManager.AddUpgrade(firstUpgrade);
        if (firstUpgrade.upgradeType == UpgradeType.SecondaryAttackUnlock)
        {
            upgrades.Remove(firstUpgrade);
        }
    }

    public void OnButton2()
    {
        playerUpgradeManager.AddUpgrade(secondUpgrade);
        if (secondUpgrade.upgradeType == UpgradeType.SecondaryAttackUnlock)
        {
            upgrades.Remove(secondUpgrade);
        }
    }
}