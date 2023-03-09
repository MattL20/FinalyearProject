using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class EnemyTextBoxManager : MonoBehaviour
{
    public GameObject TextBox;
    public TMP_Text theText;
    public TextAsset textFile;
    public string[] lines;
    public int currentLine = 0;
    public int endLine = 0;
    public GameObject player;

    public bool isActive;

    public bool stopPlayer;

    private bool isTyping = false;
    private bool interuptTyping = false;

    private bool moveOn = false;

    public float typeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<playermovement>().enabled = false;
        if (textFile != null){
            lines = (textFile.text.Split("\n"));
        }
       if(endLine==0){
            endLine = lines.Length - 1;
        }
        if(isActive){
            EnableTextBox();
        }else{
            DisableTextBox();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive){
            return;
        }else{
     //theText.text = lines[currentLine]; 
     if(moveOn) {
        if(!isTyping){
            currentLine = currentLine + 1;

            if(currentLine>endLine){
                DisableTextBox();
            } else{
                StartCoroutine(TextScroll(lines[currentLine]));
            }
        }else if(isTyping&&!interuptTyping){
            interuptTyping = true;
        }
     }
        }
    }
    private IEnumerator TextScroll(string lineOfText){
        int letter = 0;
        theText.text = "";
        isTyping = true;
        interuptTyping = false;
        moveOn = false;
        while(isTyping&&!interuptTyping&&(letter<lineOfText.Length-1)){
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        yield return new WaitForSeconds(1);
        moveOn = true;
        isTyping = false;
        interuptTyping = false;
    }
    public void EnableTextBox(){
        TextBox.SetActive(true);
        isActive = true;
        if(stopPlayer){
            player.GetComponent<playermovement>().animator.SetFloat("Speed",0);
            player.GetComponent<playermovement>().enabled = false;
        }
        StartCoroutine(TextScroll(lines[currentLine]));
    }
    public void DisableTextBox(){
        TextBox.SetActive(false);
        isActive = false;
        player.GetComponent<playermovement>().enabled = true;
    }

    public void ReloadScript(TextAsset newText){
        if(newText!= null){
            lines = new string[1];
            lines = (newText.text.Split("\n"));
        }
    }
}
