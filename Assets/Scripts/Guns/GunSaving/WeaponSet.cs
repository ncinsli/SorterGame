using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Weapon))]
public class WeaponSet : MonoBehaviour{
    private Weapon weaponHandlerAttached;
    private void Awake(){ 
        this.enabled = true;
        Invoke(nameof(SetWeapon), 0.2f);
    }
    private void SetWeapon(){
        weaponHandlerAttached = GetComponent<Weapon>();
//        Debug.Log($"Player weapon type: {weaponHandlerAttached.CastToWeaponType(PlayerPrefs.GetInt("WeaponTypeInt"))}");
        weaponHandlerAttached._weaponType = weaponHandlerAttached.CastToWeaponType(PlayerPrefs.GetInt("WeaponTypeInt"));
        weaponHandlerAttached.power = PlayerPrefs.GetInt("WeaponTypeInt");
//        Debug.Log($"Player weapon type 2: {weaponHandlerAttached.weaponType}");
    }
    private void OnEnable() => SetWeapon(); 
}
