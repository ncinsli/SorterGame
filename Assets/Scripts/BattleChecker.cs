using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BattleChecker : MonoBehaviour{
    [SerializeField] private UnityEvent onWin;
    [SerializeField] private UnityEvent onFail;
    [SerializeField] private string textOnWin = "Вы выиграли";
    [SerializeField] private string textOnFail = "Вы проиграли";
    [SerializeField] private Text resultText;
    [SerializeField] private string sceneOnWin;
    protected float botCount;
    private void Awake(){ 
        botCount = FindObjectsOfType<Bot>().Length;
        PlayerPrefs.SetInt("KilledBots", 0);
    }
    public void Check(){
        if (PlayerPrefs.GetInt("KilledBots") != botCount) StartCoroutine(OnFail());
        else StartCoroutine(OnWin());
    }

    private IEnumerator OnFail(){
        resultText.text = textOnFail;
        Debug.Log(PlayerPrefs.GetInt("KilledBots"));
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("KilledBots", 0);
        onFail.Invoke();
    }

    private IEnumerator OnWin(){
        resultText.text = textOnWin;
        yield return new WaitForSeconds(1f);
        PlayerPrefs.SetInt("KilledBots", 0);
        onWin.Invoke();
    }
    
    private void OnApplicationQuit() => PlayerPrefs.SetInt("KilledBots", 0);
}
