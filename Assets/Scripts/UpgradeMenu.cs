using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    private readonly string[] _upgradeNames = new []{"Increase Movement Speed", "More from chopping", "Faster Resource Spawn-Rate"};
    private readonly ResourceType[] _upgradeResources = new[] {ResourceType.Timber, ResourceType.Gold, ResourceType.Gold};
    private readonly int[] _upgradePrices = new[] {2, 5, 3};
    private Action[] _upgradeUpgrades;
    private GameObject upgradeMenuInterface;

    void Awake()
    {
        upgradeMenuInterface = transform.parent.gameObject;
        upgradeMenuInterface.SetActive(false);

        var upgradeButton = Resources.Load<GameObject>("Prefabs/UpgradePnl");
        var upgradeButtons = new GameObject[_upgradeNames.Length];
        _upgradeUpgrades = new Action[_upgradeNames.Length];

        _upgradeUpgrades[0] = () =>
        {
            GameController.Instance.playerController.speed += 1;
        };
        _upgradeUpgrades[1] = () =>
        {
            GameController.Instance.playerController.GatherSpeed += 3;
        };
        _upgradeUpgrades[2] = () =>
        {
            GameController.Instance.SpawnRate /= 2;
        };


        
        for (int i = 0; i < _upgradeNames.Length; i++)
        {
            var pos = new Vector2(410, 650 - (i * (-100)));
            // var pos = new Vector2(upgradeButton.transform.position.x, upgradeButton.transform.position.y);
            upgradeButtons[i] = Instantiate(upgradeButton, pos, Quaternion.identity);
            upgradeButtons[i].transform.parent = transform;
            upgradeButtons[i].GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            upgradeButtons[i].GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            var n = i;
            var panel = upgradeButtons[i].GetComponent<UpgradePanel>();
            panel.SetUpgrade(() =>
            {
                if (GameController.Instance.playerController.Resources[_upgradeResources[n]] <
                    _upgradePrices[n]) return;
                GameController.Instance.playerController.addResource(_upgradeResources[n], -_upgradePrices[n]);
                _upgradeUpgrades[n].Invoke();
            });
            panel.SetPrice(_upgradePrices[i], _upgradeResources[i]);
            panel.SetTitle(_upgradeNames[i]);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

