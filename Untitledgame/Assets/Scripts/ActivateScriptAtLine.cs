using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Used to Update the text box with where to start in the dialogue
public class ActivateScriptAtLine : MonoBehaviour
{
    public TextAsset theText;
    //Starting Line and Ending line
    public int start;
    public int end;
    //Reference to textbox manager
    public TextBoxManager TextManager;
    //If we should destroy the trigger when done
    public bool destroyWhenDone;
    //When the player enters the trigger box collider update the text box
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
