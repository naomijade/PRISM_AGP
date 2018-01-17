using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalk : MonoBehaviour
{

 
    public string npcTextMessage = "Hello, I'm an NPC"; //Message to show on the screen
    public Rect popupBox = new Rect(0.25f, 0.75f, 0.5f, 0.1f); //This is the size of your popup box in screen size
    public Rect messageBox = new Rect(0.1f, 0.7f, 0.8f, 0.2f); //This is the size of your message box in screen size
    public Transform player; //The player that we're checking the distance from
    public float minDistance = 1f; //How far away we have to be in order to show the message
    GameObject camera;
    public GameObject textBox;
    public GameObject canvas;
    private bool inRange = false; //Controls showing the 'ready to talk' message
    private bool showText = false; //Controls showing the NPC message

    public int NPCHP;
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        NPCHP = 3;
        //If you've forgotten to setup the player in the Inspector.. then I'm going to send you an angry message
        if (player == null)
        {
            Debug.LogError("Variable 'player' not set up on " + gameObject.name + ". Disabling the associated script.");
            this.enabled = false;
        }
    }

    void Update()
    {
        if(NPCHP <= 0)
        {
            Destroy(gameObject);
        }

        if (Vector3.Distance(transform.position, player.position) <= minDistance) //Are we close enough?
        {
            //Toggle showing the "press X to talk" message
            inRange = true;

            //Toggle showing the text every time we press Fire1
            if (Input.GetButtonDown("Fire1"))
            {
                camera.GetComponent<randomSpawner>().NPCTalkedTo++;
                showText = !showText;
            }
        }
        else
        {
            //Hide the text if we walk out of range
            inRange = false;
            showText = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Bullet")
        {
            NPCHP--;
        }


    }

        void OnGUI()
    {
        if (inRange != showText)
            {
            GUI.Box(new Rect(Screen.width * popupBox.x, Screen.height * popupBox.y, Screen.width * popupBox.width, Screen.height * popupBox.height), "Press to talk to " + gameObject.name + ".");
        }

        if (showText)
        {
            GUI.Box(new Rect(Screen.width * messageBox.x, Screen.height * messageBox.y, Screen.width * messageBox.width, Screen.height * messageBox.height), npcTextMessage);
        }
    }
}

