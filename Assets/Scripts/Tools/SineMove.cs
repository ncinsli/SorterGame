using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMove : MonoBehaviour{

    public float speed;
    private float timeFromStart = 0.5f;

    private void FixedUpdate(){
        timeFromStart += Time.deltaTime;
        transform.position += Vector3.right * speed * Mathf.Sin(timeFromStart) * 0.1f;    
    }
}
