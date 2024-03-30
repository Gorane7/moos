using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour {

    private int gameStartScene;
    
    public void StartGame() {
        SceneManager.LoadScene(gameStartScene);
    }

    public void SetScene(int sceneNumber) {
        gameStartScene = sceneNumber;
    }
}
