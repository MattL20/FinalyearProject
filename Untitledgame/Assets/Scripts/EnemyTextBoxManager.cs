using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
//Controls the text inside the enemy text box
public class EnemyTextBoxManager : MonoBehaviour
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
    //If the textbox is active
    public bool isActive;
    //used to stop the player when the text is active
    public bool stopPlayer;
    //Used to create the typing effect in the text box
    private bool isTyping = false;
    private bool interuptTyping = false;

    private bool moveOn = false;
    //speed at whic the text is typed
    public float typeSpeed;
    // Start is called before the first frame update
    void Start()
    {
        //Stop the player movement
        player.GetComponent<playermovement>().enabled = false;
        //split up the dialogue by lines
        if (textFile != null){
            lines = (textFile.text.Split("\n"));
        }
       if(endLine==0){
            endLine = lines.Length - 1;
        }
       //enable/disable the textbox
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
            //Used to move the dialogue on without player input
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
    //creates the typing effect
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
    //what to do when the text box is enabled
    public void EnableTextBox(){
        TextBox.SetActive(true);
        isActive = true;
        if(stopPlayer){
            player.GetComponent<playermovement>().animator.SetFloat("Speed",0);//stop all player movemment
            player.GetComponent<playermovement>().enabled = false;
            player.GetComponent<PlayerInput>().enabled = false;
            player.GetComponent<Attack>().enabled = false;
        }
        //start the text typing out
        StartCoroutine(TextScroll(lines[currentLine]));
    }
    public void DisableTextBox(){
        TextBox.SetActive(false);
        isActive = false;
        player.GetComponent<playermovement>().enabled = true;
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Boss");
        if(enemy.Length == 1)
        {
            AllowMove();
        }

    }
    //reset the dialogue if game is played agin without closing
    public void ReloadScript(TextAsset newText){
        if(newText!= null){
            lines = new string[1];
            lines = (newText.text.Split("\n"));
        }
    }
    //change the music after 10 seconds
    public void musicChange()
    {
        StartCoroutine(musicDelay());
    }
    IEnumerator musicDelay()
    {
        yield return new WaitForSeconds(10);
        player.GetComponent<playermovement>().bossMusicStart();
    }
    //allow the player to move again
    public void AllowMove()
    {
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<Attack>().enabled = true;
    }
}
