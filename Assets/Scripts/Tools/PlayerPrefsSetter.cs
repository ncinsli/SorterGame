using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PlayerPrefsSetter : MonoBehaviour{
    [SerializeField] private string key;
    private void Awake() => GetComponent<Text>().text = PlayerPrefs.GetInt(key).ToString();
}
