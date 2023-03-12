using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActivateAtLine : MonoBehaviour
{
    public TextAsset theText;

    public int start;
    public int end;

    public EnemyTextBoxManager TextManager;

    public bool destroyWhenDone;

    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
