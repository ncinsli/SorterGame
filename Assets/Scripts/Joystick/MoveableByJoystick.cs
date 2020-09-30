using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveableByJoystick : MonoBehaviour{
    
    [SerializeField] private float speed = 1f;
    public JumpButton jumpButton{private get; set;} //Ссылка на JumpButton, чтобы работал прыжок при удержании
    private Rigidbody2D rigidbody;
    [HideInInspector] public FeetScript feetScript;

    private void Start(){ 
        rigidbody = GetComponent<Rigidbody2D>();
        feetScript = GetComponentInChildren<FeetScript>();
    }

    public void MoveByAxis(float axis) {
        float flipAxis = axis >= 0f ? 1f : -1f;
        transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x) * flipAxis, transform.localScale.y);
        rigidbody.position += Vector2.right * axis * speed;
    }

    public void MoveUp(float power) => rigidbody.position += Vector2.up * power * 3f;
    
}
