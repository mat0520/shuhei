using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PushStartButton0()
    {
        //0→10球モード 1→三球三振モード
        gameManager.gameMode = 0;
        SceneManager.LoadScene("GameScene");
    }

    public void PushStartButton1()
    {
        //0→10球モード 1→三球三振モード
        gameManager.gameMode = 1;
        SceneManager.LoadScene("GameScene");
    }
}
