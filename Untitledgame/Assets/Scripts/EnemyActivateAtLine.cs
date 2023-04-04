using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Used to Update the enemy text box with where to start in the dialogue with the enemy dialogue
public class EnemyActivateAtLine : MonoBehaviour
{
    public TextAsset theText;
    //Starting Line and Ending line
    public int start;
    public int end;
    //Reference to enemy textbox manager
    public EnemyTextBoxManager TextManager;
    //If we should destroy the trigger when done
    public bool destroyWhenDone;
    public GameObject Player;
    //When the player enters the trigger box collider update the enemy text box
    private void OnTriggerEnter2D(Collider2D other) {
       if(other.name == "Player"){
        TextManager.ReloadScript(theText);
        TextManager.currentLine = start;
        TextManager.endLine = end;
        TextManager.EnableTextBox();
        TextManager.musicChange();
        if(destroyWhenDone){
            Destroy(gameObject);
        }
       } 
    }
    
}
