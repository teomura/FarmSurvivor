using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpMenu : MonoBehaviour
{
    private Upgrade[] upgrades;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private TextMeshProUGUI text1;
    [SerializeField] private TextMeshProUGUI text2;

    private void Awake()
    {
        upgrades = Resources.LoadAll("Upgrades", typeof(Upgrade)).Cast<Upgrade>().ToArray();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Show()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;

        int firstUpgradeIndex = Random.Range(0, upgrades.Length);
        int secondUpgradeIndex = Random.Range(0, upgrades.Length);
        while (secondUpgradeIndex == firstUpgradeIndex)
        {
            secondUpgradeIndex = Random.Range(0, upgrades.Length);
        }
        Upgrade firstUpgrade = upgrades[firstUpgradeIndex];
        Upgrade secondUpgrade = upgrades[secondUpgradeIndex];

        //update button image with upgrade image and text with upgrade text
        button1.image.sprite = firstUpgrade.image;
        button2.image.sprite = secondUpgrade.image;
        text1.text = string.Format(firstUpgrade.description, firstUpgrade.modifier);
        text2.text = string.Format(secondUpgrade.description, secondUpgrade.modifier);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}