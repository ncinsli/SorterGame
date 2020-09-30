using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Bot : MonoBehaviour{
    [SerializeField] private Vector2 forceByFrame;
    [SerializeField] [Range(0, 100f)] private float health = 100f;
    [SerializeField] private WorldSlider healthSlider;
    protected Transform mainCharacter;
    private Rigidbody2D botRigidbody;
    private void Start(){
        mainCharacter = FindObjectOfType<MoveableByJoystick>().transform;
        forceByFrame = (mainCharacter.position - transform.position).normalized;
        botRigidbody = GetComponent<Rigidbody2D>();
        Invoke("UpdateForce", 2f);
    }
    private void FixedUpdate() => botRigidbody.position += forceByFrame * Time.deltaTime;
    private void UpdateForce(){
        forceByFrame = (mainCharacter.position - transform.position).normalized;
        Invoke("UpdateForce", 2f); //Рекурсия экономит много сил компьютеру
    }

    private void OnCollisionEnter2D(Collision2D col){
        var shootData = col.gameObject.GetComponent<ShootData>();
        if (shootData != null){
            var power = shootData.power;    
            health -= power * 0.001f;
            try {healthSlider.SetValue(health, -1f);}
            catch{}
            if (health < 1f) DestroySelf();
        }
    }

    private void DestroySelf(){
        PlayerPrefs.SetInt("KilledBots", PlayerPrefs.GetInt("KilledBots") + 1);
        Destroy(gameObject, 0f);
    }
}
