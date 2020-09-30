using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class WorldSlider : MonoBehaviour{
    protected float lastValue;
    protected SpriteRenderer spriteRenderer;
    protected float originScaleX;
    protected float originPositionX;
    private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
    private void Start(){
        originPositionX = transform.localPosition.x;
        originScaleX = transform.localScale.x;
        lastValue = 1f;
    }
    public void SetValue(float value, float side){ //Для нормальной работы pivot установи на left-center
        transform.localScale = new Vector3(originScaleX * value * 0.01f * -side, transform.localScale.y);
        spriteRenderer.color = new Color(100 / value, value / 100, spriteRenderer.color.b, spriteRenderer.color.a);
        lastValue = value * 0.01f;
    }

}
