using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrashChest : MonoBehaviour{

    [SerializeField] private ScoreOutput[] scoreOutputters;
    [SerializeField] public Collector collector;
    [SerializeField] public List<Prop> totalProps;
    [SerializeField] public int moneyInRound;
    [SerializeField] public string moneyPropertyName = "Money";
    public event Action<Collector> onTrashChestUpdate;

    private void Start(){
        scoreOutputters = FindObjectsOfType<ScoreOutput>();
        onTrashChestUpdate += collector => {
            if (collector.propsCollected.Count > 0) foreach (var prop in collector.propsCollected) totalProps.Add(prop);
            foreach (var scoreOutputter in scoreOutputters){
                foreach (var i in scoreOutputter.outputBehaviour) i.Invoke();
            }
            collector.propsCountCollected = 0;
            collector.propsCollected = new List<Prop>(); //Обнуляем список
        };
    } 
    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.GetComponent<Collector>() != null) {
            var newPropsList = collision.gameObject.GetComponent<Collector>().propsCollected;
            foreach (var i in newPropsList) moneyInRound += i.valuation;
            Collector collector = collision.gameObject.GetComponent<Collector>();
            onTrashChestUpdate(collector); //Очистка пропов у сборщика и прочие финтифлюшки
        }
    }

    public void SaveCollectData(){
        PlayerPrefs.SetInt(moneyPropertyName, moneyInRound);
        PlayerPrefs.Save();
    }
}
