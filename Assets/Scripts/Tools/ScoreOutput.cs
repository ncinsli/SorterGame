using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreOutput : MonoBehaviour{
    [SerializeField] private uint collectorOrder = 1; //У нас несколько коллекторов, => даём выбрать, для какого будет выводиться счёт
    [SerializeField] public List<UnityEvent> outputBehaviour;
    [SerializeField] private typeOutput outputType = typeOutput.Score;
    private Collector collector;
    private Text textScore;
    private TrashChest trashChestMain;

    private void Start(){ 
        collector = FindObjectsOfType<Collector>()[collectorOrder];
        trashChestMain = FindObjectsOfType<TrashChest>()[collectorOrder]; //Мусорка только одна!
        textScore = GetComponent<Text>();
        foreach (var i in outputBehaviour) i.Invoke();
        trashChestMain.onTrashChestUpdate += collector => { if (outputType == typeOutput.ItemCount) textScore.text = $"0/{collector.maxPropCount}"; };
    }

    public void UpdateScore(){
        int totalScore = 0;
        foreach (var i in trashChestMain.totalProps) totalScore += i.valuation;
        if (outputType == typeOutput.Score) textScore.text = totalScore.ToString();
    }

    public void UpdateItemCount(){ 
        collector.onCollectedObject += propVal => 
            textScore.text = $"{(Mathf.Clamp(collector.propsCountCollected + 1, 0, 3)).ToString()}/{collector.maxPropCount}";
    }

    private enum typeOutput{
        Score = 0, ItemCount = 1
    }
}
