using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Свалка функций для всякой всячины в гуи элементах
[CreateAssetMenu(menuName = "SceneControl", order = 5)]
public class SceneControl : ScriptableObject{
    private void OnEnable(){
        if (PlayerPrefs.GetString("NextInShopScene") == "") PlayerPrefs.SetString("NextInShopScene", "Battle0");
    }
    public void LoadScene(string name) => SceneManager.LoadScene(name); 
    public void LoadNextInShopScene() => SceneManager.LoadScene(PlayerPrefs.GetString("NextInShopScene"));
    public void ReloadCurrent() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public void LoadPrevious(int minusIndex){
        try { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - Mathf.Abs(minusIndex)); }
        catch { Debug.Log("buildIndex error in SceneControl"); }
    }
    public void LoadNext(int index){
        try { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + Mathf.Abs(index)); }
        catch { Debug.Log("buildIndex error in SceneControl"); }
    }
    public void LoadShop(string nextInShop){ 
        PlayerPrefs.SetString("NextInShopScene", nextInShop);
        SceneManager.LoadScene("Shop");
    }
}
