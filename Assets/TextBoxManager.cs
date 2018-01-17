using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public bool startOnLoad;
    public RPGTalk rpgTalk;
    string touching;
    public bool showOnce;
    int timesSpacePressed;
    public bool talkingToNPC;
    bool hitSpace;

    public GameObject textBox;
    public GameObject canvas;

    TextAsset[] value;
    public Text theText;
    public TextAsset theText1;
    public TextAsset theText2;
    public TextAsset theText3;
    public TextAsset theText4;
    public TextAsset theText5;
    public TextAsset textFile;
    public string[] textLines;

    public int currLine;
    public int endAtLine;

    public int currLetter;
    public int endAtLetter;

    public float letterPause = 0.2f;
    public AudioSource typingSound;

    private bool inRange = false;

    public float minDistance = 100f;

    public Texture2D cursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    GameObject mainCam;

    GameObject player;

    
    void Start()
    {
        value = new TextAsset[5];
        value[0] = theText1;
        value[1] = theText2;
        value[2] = theText3;
        value[3] = theText4;
        value[4] = theText5;
        randomizeTest();

        if (startOnLoad == true)
        {
            StartCoroutine(TypeText(0));
        }
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 2;
        }
        //theText.text = "";
    }

    void Update()
    {

        if (showOnce != true)
        {
            if (mainCam.GetComponent<randomSpawner>().enemiesRemaining == 0)
            {
                if (Vector3.Distance(transform.position, player.GetComponent<Transform>().position) <= minDistance) //Are we close enough?
                {
                    //Toggle showing the "press X to talk" message
                    inRange = true;

                    //Toggle showing the text every time we press Fire1
                    //if (Input.GetButtonDown("Fire2"))
                    //{
                    //    textBox.SetActive(true);
                    //    talkingToNPC = true;
                    //    canvas.GetComponent<RectTransform>().localPosition = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -2);
                    //    StartCoroutine(TypeText(0));
                    //    currLine = 0;
                    //    timesSpacePressed = 0;

                    //}
                    //if(talkingToNPC == true && Input.GetKeyDown(KeyCode.Space))
                    //{
                    //    hitSpace = true;
                    //}

                    //if (talkingToNPC == true && timesSpacePressed == 1)
                    //{
                    //    if (currLine <= endAtLine && Input.GetKeyDown(KeyCode.Space))
                    //    {
                    //        hitSpace = false;
                    //        StopAllCoroutines();
                    //        theText.text = "";
                    //        currLine += 1;
                    //        StartCoroutine(TypeText(currLine));
                    //        timesSpacePressed = 0;
                    //    }
                    //    }

                    //}
                    //canvas.GetComponent<RectTransform>().localPosition = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, -2);
                    ////Vector2 pos = new Vector2(mainCam.transform.position.x, mainCam.transform.position.y);
                    ////Quaternion Rotation = Quaternion.identity;
                    ////Instantiate(canvas, pos, Rotation);
                    //StartCoroutine(TypeText());
                    //showOnce = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (touching == "NPC")
                        {
                            rpgTalk.NewTalk();
                            rpgTalk.lineToStart = 1;
                            rpgTalk.lineToBreak = 3;
                        }
                      
                    }
                }
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        touching = col.name;
      
    }

    void OnTriggerExit2D(Collider2D col)
    {
        touching = "";
    }



    //if (talkingToNPC1 != true)
    //{
    //    StartCoroutine(TypeText());
    //    talkingToNPC1 = true;
    //}
    //theText.text = textLines[currLine];


    //if (Input.GetKeyDown(KeyCode.Space))
    //{
    //    //theText.text = textLines[currLine];
    //    StartCoroutine(TypeText());
    //    //if (currLine <= endAtLine)
    //    //{   
    //    //    currLine += 1;
    //    //   // theText.text = "";
    //    //}

    //}



    public void randomizeTest()
    {

        int randomNumber = Random.Range(0, 5);
        if (randomNumber == 0)
        {
            textFile = theText1;

        }
        else if (randomNumber == 1)
        {
            textFile = theText2;

        }
        else if (randomNumber == 2)
        {
            textFile = theText3;

        }
        else if (randomNumber == 3)
        {
            textFile = theText4;

        }
        else if (randomNumber == 4)
        {
            textFile = theText5;

        }
    }
    IEnumerator TypeText(int currLine)
    {
 
        foreach (char letter in textLines[currLine])
        {
            if (hitSpace == true)
            {
                print("hambowbow");
                //for (int i = currLine; i < textLines[currLine].Length; i++)
                //{
                theText.text += letter;
                continue;
                ////}

            }

            else
            {
                theText.text += letter;
                if (letter != ' ' && letter != 13)
                {
                    typingSound.Play();
                }

            }
            yield return new WaitForSeconds(letterPause);
        }
        timesSpacePressed++;


        print("now");
       
       


    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
    }


}
