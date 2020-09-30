using System; 
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Client : MonoBehaviour{
    [SerializeField] public Weapon weaponBought;
    [SerializeField] public int moneyCurrent; 
    [SerializeField] public event Action onMoneyChange;
    [SerializeField] private string propertyName = "Money";
    protected int achievedMoney;

    public void Load() => moneyCurrent = PlayerPrefs.GetInt(propertyName);
    private void Start() => achievedMoney = moneyCurrent;
    private void Awake() => Load();

    public bool Buy(Weapon weapon){
        if (achievedMoney >= weapon.price){
            moneyCurrent = achievedMoney - weapon.price;
            if (onMoneyChange != null) onMoneyChange();
            weaponBought = weapon;
            Debug.Log($"BOUGHT {weapon.weaponType.ToString()} FOR {weapon.price}");
            PlayerPrefs.SetInt("WeaponTypeInt", Array.IndexOf(Enum.GetValues(typeof(WeaponType)), weaponBought.weaponType)); PlayerPrefs.Save();
            Debug.Log($"Weapon type in int: {PlayerPrefs.GetInt("WeaponTypeInt")}");
            if (PlayerPrefs.GetInt("WeaponTypeInt") == -1) Debug.LogError("Your weapon prefab object has unset enum flag (Assets/Prefabs/Guns)");
            return true;
        }
        else{ 
            Debug.Log($"DIDN'T BUY {weapon.weaponType.ToString()} FOR {weapon.price}");
            var moneyMonitor = FindObjectOfType<MoneyMonitor>();
            var animator = moneyMonitor.transform.parent.GetComponent<Animation>();
            animator.Play();
            return false;
        }
    }
}
