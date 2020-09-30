using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//Для опен-сурса/ Создатель: ncinsli
public class Timer : MonoBehaviour{
    [Range(0f,100f)] public float delayTime = 10f;
    [HideInInspector] public float defaultDelay = 10f;
    [Space] [Space]
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onTick;
    [SerializeField] private UnityEvent onSecondPassed;
    [SerializeField] private UnityEvent onTimerReachedEnd;
    [Space] [Space] [Space]
    [SerializeField] private Text[] textBinds;
    [SerializeField] private bool useFloatingValues = false;

    protected float antiDelayTime = -2f;
    protected float targetValue = 1f;

    private void Start(){ 
        defaultDelay = delayTime;
        if (onStart != null) onStart.Invoke();
    }

    private void FixedUpdate(){
        if (delayTime > 0f){
            delayTime -= Time.fixedDeltaTime;
            if (textBinds != null){
                foreach (var i in textBinds){ 
                    if (!useFloatingValues)
                        i.text = Mathf.RoundToInt(delayTime).ToString();
                    else i.text = delayTime.ToString().Substring(0, delayTime.ToString().Length - 2);
                }
            }
            antiDelayTime += Time.unscaledDeltaTime;
            if (targetValue - antiDelayTime < 0.1f && antiDelayTime < targetValue){
                targetValue++;
                onSecondPassed.Invoke();
            }
            onTick.Invoke();
        }

        if (delayTime < 0.1f && delayTime > 0f){ 
            delayTime = 0f;
            onTimerReachedEnd.Invoke();
            onSecondPassed.RemoveAllListeners();
        }
    }

    public void Refresh() => delayTime = defaultDelay;
}