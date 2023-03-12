using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TextBoxManager : MonoBehaviour
{
    public GameObject TextBox;
    public TMP_Text theText;
    public TextAsset textFile;
    public string[] lines;
    public int currentLine = 0;
    public int endLine = 0;
    public GameObject player;
    public GameObject TutorialCanvas;

    public bool isActive = true;

    public bool stopPlayer;

    private bool isTyping = false;
    private bool interuptTyping = false;

    public float typeSpeed;

    public AudioSource Scream;
    // Start is called before the first frame update
    void awake()
    {
        
    }
    void Start()
    {
        
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
       
        if (!isActive){
            return;
        }else{
            //theText.text = lines[currentLine]; 
            player.GetComponent<playermovement>().enabled = false;
            if (Input.GetKeyDown(KeyCode.Space)) {
                
                if (!isTyping){
            currentLine = currentLine + 1;

            if(currentLine>endLine){
                DisableTextBox();
            } else{
                StartCoroutine(TextScroll(lines[currentLine], currentLine));
            }
        }else if(isTyping&&!interuptTyping){
            interuptTyping = true;
        }
     }
        }
    }
    private IEnumerator TextScroll(string lineOfText, int num){
        
           
        if(num==34)
        {
            Debug.Log("Scream");

            Scream.Play();
        }
        int letter = 0;
        theText.text = "";
        isTyping = true;
        interuptTyping = false;
        while(isTyping&&!interuptTyping&&(letter<lineOfText.Length-1)){
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        theText.text = lineOfText;
        isTyping = false;
        interuptTyping = false;
    }
    public void EnableTextBox(){
        TextBox.SetActive(true);
        isActive = true;
        if(stopPlayer){
            player.GetComponent<playermovement>().animator.SetFloat("Speed",0);
            player.GetComponent<playermovement>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<Attack>().enabled = false;
        }
        StartCoroutine(TextScroll(lines[currentLine],currentLine));
    }
    public void DisableTextBox(){
        if(TutorialCanvas != null)
        {

        TutorialCanvas.GetComponent<TutorialInfoScript>().addFirstTutorial();
        }
        TextBox.SetActive(false);
        isActive = false;
        player.GetComponent<playermovement>().enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<Attack>().enabled = true;
    }

    public void ReloadScript(TextAsset newText){
        if(newText!= null){
            lines = new string[1];
            lines = (newText.text.Split("\n"));
        }
    }
}
