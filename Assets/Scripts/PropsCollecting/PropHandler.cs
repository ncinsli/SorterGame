using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PropHandler : MonoBehaviour{
    [SerializeField] public Prop propAttached;

    public IEnumerator Indicate(){
        while (transform.localScale.x < 2f){ 
            transform.localScale += new Vector3(0.15f, 0.15f, 0f);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public IEnumerator DeIndicate(){
        while (transform.localScale.x > 1f){ 
            transform.localScale -= new Vector3(0.15f, 0.15f, 0f);
            yield return new WaitForSeconds(0.001f);
        }
    }

    public void SelfDestroy() => StartCoroutine(SelfDestroyCor());

    private IEnumerator SelfDestroyCor(){
        var spriteRenderer = GetComponent<SpriteRenderer>();
        var collider = GetComponent<Collider2D>();
        collider.enabled = false;
        for (float i = 1f; i > 0f; i -= 0.05f){ 
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject); //Тут можно сделать красивое исчезновение
    }

    //Так как деиндекация из внешнего скрипта работает далеко не полностью, добавим этого кода для стабильности
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.GetComponent<Collector>()){
            StartCoroutine(DeIndicate());
            SelfDestroy();
        }
    }

}
