using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public void LoadLevel()
    {
        Application.LoadLevel("PRISM");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        Application.LoadLevel("Credits");
    }
}
