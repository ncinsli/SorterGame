using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class BuyButton : MonoBehaviour{

    [SerializeField] private Weapon weaponAttached;
    private Button button;
    private Client client;

    private void OnEnable(){
        var moneyMonitor = FindObjectOfType<MoneyMonitor>();
        button = GetComponent<Button>();
        client = FindObjectOfType<Client>();
        button.onClick.AddListener(() =>{ foreach (var i in FindObjectsOfType<BuyButton>()) i.button.interactable = true; });
        button.onClick.AddListener(() =>{ if (client.Buy(weaponAttached)) button.interactable = false; });
        button.onClick.AddListener(() => moneyMonitor.Refresh());
        //client.onMoneyChange += () => button.interactable = false;
    }
}
