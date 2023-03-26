using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScriptAtLine : MonoBehaviour
{
    public TextAsset theText;

    public int start;
    public int end;

    public TextBoxManager TextManager;

    public bool destroyWhenDone;

    private void OnTriggerEnter2D(Collider2D other) {
       if(other.name == "Player"){
        TextManager.ReloadScript(theText);
        TextManager.currentLine = start;
        TextManager.endLine = end;
        TextManager.EnableTextBox();
        if(destroyWhenDone){
            Destroy(gameObject);
        }
       } 
    }
}
