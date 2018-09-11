using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToScene(int sceneToGo)
    {
        SceneManager.LoadScene(sceneToGo);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
