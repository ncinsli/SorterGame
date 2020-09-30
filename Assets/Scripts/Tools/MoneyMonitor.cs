using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class MoneyMonitor : MonoBehaviour{

    private Text selfText;
    private Client client;

    private void Start(){
        selfText = GetComponent<Text>();
        client = FindObjectOfType<Client>();
        selfText.text = client.moneyCurrent.ToString();
        client.onMoneyChange += () => selfText.text = client.moneyCurrent.ToString();
        Invoke("Refresh", 0.1f);
        /*Короче, рассказываю историю
        просидел половину вечера за нормальной структурой
        и всякими плюшками типа событийной настройки
        
        обнаружил, что сколько-бы я не включал Refresh и не перезагружал всё в OnEnable()/Awake()/Start(),
        ничего не работает. Час дебага. В инспекторе увидел, что при выключении деньги обнуляются (логично), а при старте
        они не сразу включаются. Понял, что refresh делаю до того, как деньги возвращаются на место. Поэтому тут задержка на 0.1 секунды*/
    }

    public void Refresh(){ 
        if (client == null) client = FindObjectOfType<Client>();
        if (selfText == null) selfText = GetComponent<Text>(); 
        selfText.text = client.moneyCurrent.ToString();
    }

}
