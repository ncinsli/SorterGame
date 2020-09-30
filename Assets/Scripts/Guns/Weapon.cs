using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType : int { none = 0, pistol = 10000, shotgun = 25000, ak47 = 100000, m4a1 = 200000}

[RequireComponent(typeof(SpriteRenderer))]
public class Weapon : MonoBehaviour{
    protected const int textureOffsetValue = 2;
    [SerializeField] public float power;
    [SerializeField] public int price;
    [SerializeField] public WeaponType _weaponType;
    [SerializeField] public WeaponType weaponType{get => _weaponType; set{
        GetComponent<SpriteRenderer>().sprite = GetWeaponSprite[weaponType]();
        _weaponType = value;
        Debug.Log($"SET VALUE WEAPONTYPE {value}");
    }}

    public Dictionary<WeaponType, Func<Sprite>> GetWeaponSprite = new Dictionary<WeaponType, Func<Sprite>>(){
        {WeaponType.pistol, () => Resources.LoadAll<Sprite>("Textures")[52]},
        {WeaponType.shotgun, () => Resources.LoadAll<Sprite>("Textures")[53]},
        {WeaponType.ak47, () => Resources.LoadAll<Sprite>("Textures")[57]}, 
        {WeaponType.m4a1, () => Resources.LoadAll<Sprite>("Textures")[58]}, 
        {WeaponType.none, () => new GameObject().AddComponent<SpriteRenderer>().sprite}  
    };

    [Space] [Space] [Space] [Space] [Space]
    [SerializeField] private Sprite bulletTexture;
    [SerializeField] private Color bulletColor = new Color(1f, 1f, 1f, 1f);
    public int GetPowerByType(WeaponType type) => (int)weaponType;
    public WeaponType CastToWeaponType(int value) => Enum.GetValues(typeof(WeaponType)).Cast<WeaponType>().ToArray()[value]; //Костыль, так как enum конвертируетася неправильно  
    //Так как вне Weapon-класса тип WeaponType невидим (это ж енум), то можно его кастить прямо тут
    //28 + weaponType - порядковый номер текстуры в тайлсете
    private void SetSprite() => GetComponent<SpriteRenderer>().sprite = GetWeaponSprite[weaponType]();
    private void Start() => Invoke("SetSprite", 0.4f); 
    private void Awake(){ 
        //var l = WeaponType.GetValues(typeof(WeaponType)).Cast<WeaponType>().ToList();
        //l.ForEach(t => Debug.Log($"{t} --- index {l.IndexOf(t)}"));
        power = (int)weaponType;
    }
    private void Update(){ 
        if (Input.GetKeyDown(KeyCode.Mouse0)){ 
            Vector2 screenDir = Camera.main.ScreenPointToRay((Vector2)Input.mousePosition).direction * 2f; 
            transform.rotation = Quaternion.LookRotation(screenDir) * Quaternion.Euler(0f, -90f * transform.parent.localScale.x / Mathf.Abs(transform.parent.localScale.x), 0f); 
            if (screenDir.x < 0f) transform.parent.localScale = new Vector2(-Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
            else transform.parent.localScale = new Vector2(Mathf.Abs(transform.parent.localScale.x), transform.parent.localScale.y);
            Debug.DrawRay(transform.position, screenDir);
            Shoot(screenDir); 
        }
    }

    protected BoxCollider2D collider;
    private void Shoot(Vector2 direction){
        var bulletObject = new GameObject();
        bulletObject.transform.localScale = new Vector2(2f, 2f);
        bulletObject.transform.position = transform.position; //Устанавливаем соответствие позиций
        var spriteRenderer = bulletObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 20;
        collider = bulletObject.AddComponent<BoxCollider2D>();
        collider.size = new Vector2(0.01f, 0.01f);
        var rigidbody = bulletObject.AddComponent<Rigidbody2D>();
        var shootData = bulletObject.AddComponent<ShootData>();
        shootData.power = (int)weaponType;
        spriteRenderer.sprite = bulletTexture;
        spriteRenderer.color = bulletColor;
        if (weaponType != WeaponType.none)
            rigidbody.AddForce(direction * 600f); //БАХ! И грянул выстрел
    }
}


