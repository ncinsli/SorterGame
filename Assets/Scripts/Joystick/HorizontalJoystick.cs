using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(EventTrigger))]
public class HorizontalJoystick : MonoBehaviour{
    [SerializeField] private float resetSpeed = 1f;
    private MoveableByJoystick[] moveableObjects;
    private Slider slider;
    private EventTrigger eventTrigger;
    private float lastValue = 0f;

    private bool pointerDown = false;

    private void Start(){
        moveableObjects = FindObjectsOfType<MoveableByJoystick>();
        slider = GetComponent<Slider>();
        EventTrigger eventTrigger = GetComponent<EventTrigger>();

        slider.onValueChanged.AddListener((float value) => {
            foreach (var obj in moveableObjects) obj.MoveByAxis(value - lastValue);         
            lastValue = value;
        });     
    }

    private void FixedUpdate(){
        if (pointerDown && (slider.value != 0.5f))
            foreach (var obj in moveableObjects) obj.MoveByAxis(slider.value * 0.2f - 0.1f); 
    }

    public void OnPointerDownCustom() => pointerDown = true;
    
    public void OnPointerUpCustom(){ 
        pointerDown = false;
        foreach (var obj in moveableObjects) obj.MoveByAxis(slider.value * 0.2f - 0.1f); //Двигаем последний раз для инерции
        StartCoroutine(SmoothSliderReset(slider));
    }

    private IEnumerator SmoothSliderReset(Slider slider){
        if (slider.value > 0.5f){
            for (float i = slider.value; i > 0.5f; i-= 0.01f * resetSpeed){ 
                slider.value = i;
                yield return new WaitForSeconds(0.01f);
            }
        }
        else{ 
            for (float i = slider.value; i < 0.5f; i += 0.01f * resetSpeed){ 
                slider.value = i;
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
}

