using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR && !UNITY_ANDROID
using UnityEditor;
#endif

[ExecuteInEditMode]
public class Rotating : MonoBehaviour{

    [Tooltip("Degrees by second")] [SerializeField] [Range(-180f, 180f)] private float rotatePower;
    [SerializeField] [Range(-1f, 100f)] private float rotatingTime;

    private void OnEnable(){
        if (rotatingTime == -1f) rotatingTime = float.MaxValue;
    }

    private void Update(){
        if (rotatingTime > 0f){ 
            transform.Rotate(0f, 0f, rotatePower * Time.fixedDeltaTime); //rotatePower = граудсы/секунда
            rotatingTime -= Time.deltaTime;
        }
    }
}
