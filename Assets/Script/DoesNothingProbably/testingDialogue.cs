using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingDialogue : MonoBehaviour
{
    Rigidbody2D rigid;
    public RPGTalk rpgTalk;
    string touching;
    public bool controls;
    float speed;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return))
        {
            rpgTalk.EndTalk();
        }
    


			//if the player hits E, check if it is talking with someone
			if(Input.GetKeyDown(KeyCode.E)){
				if (touching == "NPC") {
					controls = false;
					rpgTalk.lineToStart = 15;
					rpgTalk.lineToBreak = 16;
					rpgTalk.callbackFunction = "WhoAreYou";
					rpgTalk.NewTalk ();
				}
				if (touching == "Girl") {
					controls = false;
					rpgTalk.lineToStart = 33;
					rpgTalk.lineToBreak = -1;
					rpgTalk.callbackFunction = "GiveBackControls";
					rpgTalk.shouldStayOnScreen = true;
					rpgTalk.NewTalk ();
				}
			}



		} 
	

	//give the controls to player
	public void GiveBackControls()
{
    controls = true;
}

void OnTriggerEnter2D(Collider2D col)
    {
        touching = col.name;

    }

    void OnTriggerExit2D(Collider2D col)
    {
        touching = "";
    }
}
