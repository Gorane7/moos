using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextMessage : MonoBehaviour {

    private int currentMessageNr = 0;
    public int maxMessageNr;
    private int minMessageNr = 0;
    public GameObject parentMessage;
    private GameObject childMessage;
    private bool start = false;
    private bool menu = false;

    public void clickNext(){
        disableMessage();
        
        currentMessageNr += 1;
        if (currentMessageNr > maxMessageNr) {
            start = true;
        } else {
            enableMessage();
        }
    }

    public void clickBack() {
        disableMessage();

        currentMessageNr -= 1;
        if (currentMessageNr < minMessageNr) {
            menu = true;
        } else {
            enableMessage();
        }
    }

    public void disableMessage() {
        childMessage = getChild(currentMessageNr);
        childMessage.SetActive(false);
    }

    public void enableMessage() {
        childMessage = getChild(currentMessageNr);
        childMessage.SetActive(true);
    }

    public GameObject getChild (int number) {
        return parentMessage.transform.GetChild(number).gameObject;
    }

    public void StartGame() {
        if (start) {
            SceneManager.LoadScene(2);
        } if (menu) {
            SceneManager.LoadScene(0);
        }
    }
}
