using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
//Скрипт написал: @ncinsli (vk.com/ncinsli)
public class ScriptsToggler : MonoBehaviour{
    [SerializeField] private string status = "Сцена стартовала";
    [SerializeField] private bool disableOnStart = false;
    private MonoBehaviour[] activeScripts;

    private void Start(){
        if (disableOnStart){ 
            DisableScripts();
            status = "Отключены все скрипты";
        }
        else status = "Сцена стартовала";
    }

    public void DisableScripts(){
        activeScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var i in activeScripts) i.enabled = false;
        status = "Отключены все скрипты";
    }

    public void EnableScripts(){
        activeScripts = FindObjectsOfType<MonoBehaviour>();
        foreach (var i in activeScripts) i.enabled = true;
        status = "Включены все скрипты";
    }


}


#if UNITY_EDITOR
[CustomEditor(typeof(ScriptsToggler))]
public class ScriptsTogglerEditor : Editor{
    public override void OnInspectorGUI(){
        var scriptsToggler = (ScriptsToggler)target;
        if (GUILayout.Button("Отключить все скрипты")) scriptsToggler.DisableScripts();
        if (GUILayout.Button("Включить все скрипты")) scriptsToggler.EnableScripts();
        base.OnInspectorGUI();
    }
}    
#endif