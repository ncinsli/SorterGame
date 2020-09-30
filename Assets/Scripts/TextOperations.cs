using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

//Свалка функций, полезных для текста
public class TextOperations : MonoBehaviour{

    private Text selfText;
    private TrashChest chest;

    private void Start(){
        chest = FindObjectOfType<TrashChest>(); //Только одна мусорка
        selfText = GetComponent<Text>();
        var timer = FindObjectOfType<Timer>();
        selfText.text = chest.moneyInRound.ToString();        
    }

    public void AddNumber(int num) {
        int.TryParse(selfText.text, out int textInt);
        selfText.text = (textInt + num).ToString();
    }
    public void BindValue(string text) => selfText.text = text;
}
