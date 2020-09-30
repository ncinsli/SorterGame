using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Prop", menuName = "new Prop", order = 4)]
public class Prop : ScriptableObject{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string name;
    [SerializeField] public int valuation;
}
