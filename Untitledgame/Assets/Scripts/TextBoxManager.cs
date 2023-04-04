using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
//Controls the text inside the text box
public class TextBoxManager : MonoBehaviour
{
    //Reference to the textbox game object
    public GameObject TextBox;
    //Refernce to the text inside the text box component
    public TMP_Text theText;
    public TextAsset textFile;// the Dialogue script
    public string[] lines;//An array that holds all dialogue line by line
    //Current Line 
    public int currentLine = 0;
    //End line
    public int endLine = 0;
    //Reference to the player
    public GameObject player;
    //reference to the tutorial canvas UI
    public GameObject TutorialCanvas;
    //If the textbox is active
    public bool isActive = true;
    //used to stop the player when the text is active
    public bool stopPlayer;
    //Used to create the typing effect in the text box
    private bool isTyping = false;
    private bool interuptTyping = false;
    //speed at whic the text is typed
    public float typeSpeed;
    //reference to the boss scream sound
    public AudioSource Scream;
    void Start()
    {
        //split up the dialogue by lines
        if (textFile != null){
            lines = (textFile.text.Split("\n"));
        }
       if(endLine==0){
            endLine = lines.Length - 1;
        }
        //enable/disable the textbox
        if (isActive){
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
            //Stop the player movement
            player.GetComponent<playermovement>().enabled = false;
            //Allows the player to skip the typing effect and used to move to the next line in the dialogue
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
    //creates the typing effect
    private IEnumerator TextScroll(string lineOfText, int num){
        
           
        if(num==34)
        {
            //playes the scream sound at line 34 of the dialogue
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
    //what to do when the text box is enabled
    public void EnableTextBox(){
        TextBox.SetActive(true);
        isActive = true;
        if(stopPlayer){
            player.GetComponent<playermovement>().animator.SetFloat("Speed",0);//stop all player movement
            player.GetComponent<playermovement>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<Attack>().enabled = false;
        }
        //start the text typing out
        StartCoroutine(TextScroll(lines[currentLine],currentLine));
    }
    //allows the player to move again after the text is disabled
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
    //resets the script on replay
    public void ReloadScript(TextAsset newText){
        if(newText!= null){
            lines = new string[1];
            lines = (newText.text.Split("\n"));
        }
    }
}
