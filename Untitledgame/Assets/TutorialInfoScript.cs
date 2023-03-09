using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInfoScript : MonoBehaviour
{
    public GameObject TutorialCanvas;
    public static int FirstTutorial = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstTutorial==1)
        {
            TutorialCanvas.SetActive(true);
            StartCoroutine(DestroyMe());
        }
    }
    
    private IEnumerator DestroyMe()
    {
        yield return new WaitForSeconds(3);
        TutorialCanvas.SetActive(false);
        Destroy(gameObject);
    }
    public void addFirstTutorial()
    {
        FirstTutorial = FirstTutorial + 1;
    }
}
