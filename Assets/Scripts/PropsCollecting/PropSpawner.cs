using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class PropSpawner : MonoBehaviour{

    [SerializeField] public Prop currentChoice;
    [SerializeField] [Range(0f, 30f)] private float standartDelay = 4f;
    [SerializeField] private Prop[] allProps;
    [SerializeField] public UnityEvent onNewPropSpawned;
    protected float delay;
    private GameObject spawnedObject;
    [HideInInspector] public List<PropHandler> currentPropHandlers;
    
    private void Start() => delay = standartDelay;
    private void FixedUpdate(){
        delay -= Time.deltaTime;
        if (delay < 0f) SpawnRandomProp();
    }

    public void SpawnRandomProp(){
        currentChoice = allProps[Random.Range(0, allProps.Length)];
        delay = standartDelay; //Обнуляем задержку
        spawnedObject = new GameObject();
        spawnedObject.transform.localScale = new Vector2(1f, 1f);
        spawnedObject.transform.position = transform.position + Vector3.up;
        var spriteRenderer = spawnedObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentChoice.sprite;
        spriteRenderer.sortingOrder = 100;
        var propHandler = spawnedObject.AddComponent<PropHandler>();
        var rigidbody = spawnedObject.AddComponent<Rigidbody2D>();
        rigidbody.mass = 1f;
        rigidbody.gravityScale = 2f;
        var collider = spawnedObject.AddComponent<PolygonCollider2D>();
        rigidbody.AddForce(new Vector2(Random.Range(-350f, 350f), 1000f * rigidbody.mass / rigidbody.gravityScale));
        collider.autoTiling = true;
        currentPropHandlers.Add(propHandler);

        propHandler.propAttached = currentChoice;
        if (onNewPropSpawned != null) onNewPropSpawned.Invoke();
    }

    private IEnumerator RotateDuringFlying(){
        for (int i = 0; i < 180; i++){
            spawnedObject.transform.Rotate(0f, 0f, Random.Range(4f, 9f));
            yield return new WaitForSeconds(0.1f);
        }
    }
}
