using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FeetScript : MonoBehaviour{

    [HideInInspector] public bool onGround;
    [HideInInspector] public Action onTriggerEnter2D;

    private void OnTriggerEnter2D(Collider2D collision){ 
        onGround = true; 
        if (onTriggerEnter2D != null) onTriggerEnter2D();
    }
    private void OnTriggerStay2D(Collider2D collision) => onGround = true;
    private void OnTriggerExit2D(Collider2D collision) => onGround = false;
    
}
