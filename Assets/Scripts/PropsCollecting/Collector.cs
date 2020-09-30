using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Collector : MonoBehaviour{

    [SerializeField] [HideInInspector] private Collider2D collider;
    [SerializeField] public uint maxPropCount;
    [SerializeField] public uint propsCountCollected;
    [SerializeField] public List<Prop> propsCollected;
    [SerializeField] public event Action<int> onCollectedObject;
    [Space] [Space] [Space]
    [SerializeField] private Prop propDetected;
    [SerializeField] private PropHandler propHandlerDetected;
  
    private void Start(){
        collider = GetComponent<Collider2D>();
        if (!collider.isTrigger) collider.isTrigger = true;
        onCollectedObject += (int propValuation) => {
            try{ propHandlerDetected.SelfDestroy();}
            catch{ int a = 5; }
        };
    }

    protected Collider2D col;

    private void OnTriggerEnter2D(Collider2D coll){
        col = coll;
        if (col.GetComponent<PropHandler>() != null){ //Проверяем объект на наличие компонента в противовес проверки по тегу
            propHandlerDetected = col.gameObject.GetComponent<PropHandler>();
            propDetected = propHandlerDetected.propAttached;
            StartCoroutine(propHandlerDetected.Indicate());
            if (propsCountCollected < maxPropCount) propsCollected.Add(propDetected);
            Invoke("Collect", 0.4f);
        }
    }

    private void Collect(){
        if (col != null){
            propHandlerDetected = col.gameObject.GetComponent<PropHandler>();
            if (propHandlerDetected != null) propDetected = propHandlerDetected.propAttached;
            else{}
            if (propsCountCollected < maxPropCount){ 
                onCollectedObject(propDetected.valuation);
                propsCountCollected++;
            }
            else if (propHandlerDetected != null) StartCoroutine(propHandlerDetected.DeIndicate());
        }
    }
}
