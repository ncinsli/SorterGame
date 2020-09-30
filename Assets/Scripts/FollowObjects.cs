using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjects : MonoBehaviour{
    
    private Vector3 fixedTarget;
    
    [SerializeField] private float multiplierY = 6f;
    [Header("Настройки следования за объектом")]
    [SerializeField] public Transform targetTransform; //Сам объект
    [SerializeField] [Range(0.001f,1f)] public float delta = 0.85f; //Дельта
    [SerializeField] public bool useX = true;
    [SerializeField] public bool useY = true;
    [SerializeField] public float zCamCoord = -10f;
    
    private void Awake(){
        Application.targetFrameRate = 300;
        targetTransform = FindObjectsOfType<MoveableByJoystick>()[0].transform;
    }

    private void FixedUpdate(){
        if (targetTransform != null){
            float fixedTargetY = targetTransform.position.y;
            if (useX && useY) fixedTarget = new Vector3(targetTransform.position.x, fixedTargetY, zCamCoord);
            if (!useX && !useY) fixedTarget = transform.position;
            if (!useX && useY) fixedTarget = new Vector3(transform.position.x, fixedTargetY, zCamCoord);
            if (useX && !useY) fixedTarget = new Vector3(targetTransform.position.x, fixedTargetY, zCamCoord);
            fixedTarget.y += Camera.main.transform.localScale.y * multiplierY;
            Vector3 finalVector = Vector3.Lerp(transform.position, fixedTarget, delta);
            transform.position = finalVector;
        }
    }
}
