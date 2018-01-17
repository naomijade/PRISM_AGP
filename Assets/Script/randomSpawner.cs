using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class randomSpawner : MonoBehaviour
{
    public GameObject bossMusic;
    public GameObject dungeionMusic;
    public GameObject safeRoomMusic;
    bool canTurnDownMusic = true;
    bool canTurnDownMusicBoss = true;

    public int roomsToBoss = 10;

    float shakeAmt = 0;
    GameObject mainCamera;
    GameObject player;
    Vector3 originalCameraPosition;

    public AudioSource victoryMusic;

    public bool doorsActive = true;


    public KeyCode Decoy;
    public bool decoyDroped = false;
    public bool canUseDecoy;
    public int decoyKills;
    public GameObject fadeToBlack;
    bool haveEnemiesSpawned = true;

    public GameObject decoy;
    bool resetDecoyOnce = true;

    public KeyCode Bomb;
    public GameObject bomb;
    public GameObject bombExpolsion;
    public int bombKills;
    public bool canUseBomb;

    public AudioSource bombExplosionSound;
    public AudioSource bombTimerSound;

    public int killStreak;

    //the 5 enemy types
    public GameObject RedEnemy;
    public GameObject SDEnemyRed;
    public GameObject BlueEnemy;
    public GameObject YellowEnemy;
    public GameObject WhiteEnemy;
    public GameObject GreenEnemy;
    public GameObject GravityBlueEnemy;
    public GameObject greenShootEnemy;
    public GameObject whiteOrbEnemy;
    public GameObject wallRunnerYellowEnemy;

    public GameObject blueSheild;
    public GameObject greenSheild;
    public GameObject redSheild;
    public GameObject yellowSheild;

    public GameObject boss;

    bool bossHasSpawned;

    //big walls for top and bottom
    public GameObject bigRedWall;
    public GameObject bigBlueWall;
    public GameObject bigYellowWall;
    public GameObject bigGreenWall;

    //small walls for left and right
    public GameObject smallRedWall;
    public GameObject smallBlueWall;
    public GameObject smallYellowWall;
    public GameObject smallGreenWall;

    public GameObject obsRedWall;
    public GameObject obsBlueWall;
    public GameObject obsYellowWall;
    public GameObject obsGreenWall;

    public GameObject octoRedWall;
    public GameObject octoBlueWall;
    public GameObject octoYellowWall;
    public GameObject octoGreenWall;

    //tracks where the enemies should spawn for each room
    public float enemySpawnXMin = 31;
    public float enemySpawnXMax = 8;
    public float enemySpawnYMin = -9.5f;
    public float enemySpawnYMax = 9.5f;

    public GameObject gameOver;
    public GameObject postIt;
    public GameObject NPC;

    public int NPCTalkedTo = 0;

    //tracks the 4 doors
    public GameObject rightDoor;
    public GameObject upDoor;
    public GameObject leftDoor;
    public GameObject downDoor;
    public GameObject backUpDoor;
    public GameObject backDownDoor;
    private float targetAlpha;

    int currNumofEnemies;

    //array for the enemies
    GameObject[] value;

    //list for the doors
    public List<GameObject> allDoors;

    public int totalNumOfEnemies;

    //tracks the number of rooms spawned
    int roomsSpawned = 0;

    //tracks what the last room spawned was
    int lastRoomSpawned;

    //the left wall X position
    float leftX = 12.8f;
    //the rigft wall X position
    float rightX = 30.8f;
    //the roof and floor X position
    float roofX = 21.8f;

    //the roof Y position
    int topY = 5;
    //the floor Y position
    int botY = -5;
    //the left and right wall Y position
    int wallsY = 0;

    //number of rooms to spawn
    public int roomsToSpawn;
    int octoRoom = -1;

    //int twoRoomsAgo = -1;
    //int threeRoomsAgo = -1;

    public GameObject floor;

    //spawns walls recursivly
    void spawnWalls(int randomNumber)
    {
        //used for spawning everything
        Vector3 position;
        Quaternion Rotation;
        octoRoom = Random.Range(0, 16);
        //keeps spawning walls till this is not true
        if (roomsSpawned < roomsToSpawn)
        {
            //0-3 spawns wall to the right
            if (randomNumber == 0)
            {
                rightX += 19;
                roofX += 19;
                leftX += 19;

                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                    if (roomsSpawned == 0)
                    {
                        position = new Vector3(leftX - 1.4f, wallsY, 0);

                        Rotation = Quaternion.Euler(0, 40, -90);
                        Instantiate(rightDoor, position, Rotation);
                    }
                }
                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                    if (roomsSpawned == 0)
                    {
                        position = new Vector3(leftX - 1.4f, wallsY, 0);

                        Rotation = Quaternion.Euler(0, 40, -90);
                        Instantiate(rightDoor, position, Rotation);
                    }

                }
            }
            else if (randomNumber == 1)
            {
                rightX += 19;
                roofX += 19;
                leftX += 19;

                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }
                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 2)
            {
                rightX += 19;
                roofX += 19;
                leftX += 19;

                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {

                    position = new Vector3(rightX, wallsY, 0);


                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigBlueWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallRedWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 3)
            {
                rightX += 19;
                roofX += 19;
                leftX += 19;

                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {

                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallRedWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigBlueWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }





            //spawns walls up
            else if (randomNumber == 4)
            {
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);

                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);
                }

                else
                {

                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallRedWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigBlueWall, position, Rotation);

                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 5)
            {
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 6)
            {
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 7)
            {
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(upDoor, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }


            //spawns walls down
            else if (randomNumber == 8)
            {
                wallsY -= 11;
                topY -= 11;
                botY -= 11;
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallRedWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigBlueWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 9)
            {
                wallsY -= 11;
                topY -= 11;
                botY -= 11;
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 10)
            {
                wallsY -= 11;
                topY -= 11;
                botY -= 11;
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallGreenWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigYellowWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallBlueWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigRedWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }
            else if (randomNumber == 11)
            {
                wallsY -= 11;
                topY -= 11;
                botY -= 11;
                if (octoRoom >= 0 && octoRoom <= 3)
                {
                    spawnOctogon(octoRoom);
                }

                else
                {
                    position = new Vector3(rightX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 180, 90);
                    Instantiate(smallYellowWall, position, Rotation);

                    position = new Vector3(roofX, topY, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(bigBlueWall, position, Rotation);

                    position = new Vector3(leftX, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    Instantiate(smallRedWall, position, Rotation);

                    position = new Vector3(roofX, botY, 0);

                    Rotation = Quaternion.Euler(180, 0, 0);
                    Instantiate(bigGreenWall, position, Rotation);

                    position = new Vector3(leftX + 1, topY, 1);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(floor, position, Rotation);
                }
            }


            //keeps track of three rooms before
            //if (roomsSpawned > 1)
            //{
            //    threeRoomsAgo = twoRoomsAgo;
            //}

            //keeps track of two rooms ago
            //if (roomsSpawned > 0)
            //{
            //    twoRoomsAgo = lastRoomSpawned;
            //}

            //sets last room 
            lastRoomSpawned = randomNumber;

            int obsInRoom = Random.Range(0, 16);
            if (roomsSpawned < roomsToSpawn - 1) //so that obs walls dont spawn in boss room
            {
                if (obsInRoom == 0)
                {

                    position = new Vector3(leftX + 5, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsGreenWall, position, Rotation);
                    position = new Vector3(leftX + 13, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsRedWall, position, Rotation);

                    position = new Vector3(leftX + 5, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsBlueWall, position, Rotation);

                    position = new Vector3(leftX + 13, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsYellowWall, position, Rotation);
                }
                else if (obsInRoom == 1)
                {

                    position = new Vector3(leftX + 5, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsYellowWall, position, Rotation);
                    position = new Vector3(leftX + 13, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsGreenWall, position, Rotation);

                    position = new Vector3(leftX + 5, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsRedWall, position, Rotation);

                    position = new Vector3(leftX + 13, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsBlueWall, position, Rotation);
                }
                else if (obsInRoom == 2)
                {

                    position = new Vector3(leftX + 5, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsRedWall, position, Rotation);
                    position = new Vector3(leftX + 13, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsBlueWall, position, Rotation);

                    position = new Vector3(leftX + 5, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsYellowWall, position, Rotation);

                    position = new Vector3(leftX + 13, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsGreenWall, position, Rotation);
                }
                else if (obsInRoom == 3)
                {

                    position = new Vector3(leftX + 5, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsBlueWall, position, Rotation);
                    position = new Vector3(leftX + 13, wallsY + 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsYellowWall, position, Rotation);

                    position = new Vector3(leftX + 5, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 135);
                    Instantiate(obsGreenWall, position, Rotation);

                    position = new Vector3(leftX + 13, wallsY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 45);
                    Instantiate(obsRedWall, position, Rotation);
                }
            }

            //generates a random number for a room
            int wallPosition = Random.Range(0, 12);

            //prevents up and down rooms from spawning inside eachother
            if (lastRoomSpawned >= 8 && lastRoomSpawned <= 11 && wallPosition >= 4 && wallPosition <= 7)
            {
                wallPosition = Random.Range(0, 4);
            }
            else if (lastRoomSpawned >= 4 && lastRoomSpawned <= 7 && wallPosition >= 8 && wallPosition <= 11)
            {
                wallPosition = Random.Range(0, 4);
            }

            //tracks where to spawn doors
            //rooms spawned is less then the second to last
            if (roomsSpawned < roomsToSpawn - 1)
            {
                //if the room is spawned to the right
                if (wallPosition >= 0 && wallPosition < 4)
                {
                    //spawn door to the right
                    position = new Vector3(rightX - .2f, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 40, -90);
                    Instantiate(rightDoor, position, Rotation);

                    position = new Vector3(leftX + .2f, wallsY, 0);

                    Rotation = Quaternion.Euler(0, 40, 90);
                    //if the last room wasnt up or down, spawn door back to the left
                    if (!(lastRoomSpawned >= 4 && lastRoomSpawned <= 7))
                    {
                        if (!(lastRoomSpawned >= 8 && lastRoomSpawned <= 11))
                        {
                            //as long as it is not the first room spawned
                            if (roomsSpawned != 0)
                            {
                                Instantiate(leftDoor, position, Rotation);
                            }
                        }
                    }
                }

                //if the new room spawned is a up
                else if (wallPosition >= 4 && wallPosition <= 7)
                {

                    //if (roomsSpawned == 0)
                    //{
                    //    position = new Vector3(rightX - 1, wallsY, 0);

                    //    Rotation = Quaternion.Euler(0, 0, 0);
                    //    Instantiate(rightDoor, position, Rotation);
                    //    rightX += 19;
                    //    roofX += 19;
                    //    leftX += 19;




                    //    position = new Vector3(leftX + 1, wallsY, 0);

                    //    Rotation = Quaternion.Euler(0, 0, 0);
                    //    Instantiate(leftDoor, position, Rotation);




                    //}


                    //else if (roomsSpawned != 0)
                    //{

                    //move the walls y position up
                    wallsY += 11;
                    topY += 11;
                    botY += 11;

                    //spawn a door up in the previous room
                    position = new Vector3(roofX, botY - 2, 0);

                    Rotation = Quaternion.Euler(0, 0, 90);
                    //Instantiate(upDoor, position, Rotation);

                    //spawn a door down in the next room
                    position = new Vector3(roofX, botY + .1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 180);
                    Instantiate(backDownDoor, position, Rotation);

                    //as long as this is not the first room spawned
                    if (roomsSpawned != 0)
                    {
                        //if the last room was to the right
                        if (lastRoomSpawned >= 0 && lastRoomSpawned < 4)
                        {
                            //spawn door to the left
                            position = new Vector3(leftX + .2f, wallsY - 11, 0);

                            Rotation = Quaternion.Euler(0, 40, 90);
                            Instantiate(leftDoor, position, Rotation);
                        }
                    }

                }

                //if the new room is down
                else if (wallPosition >= 8 && wallPosition <= 11)
                {
                    //spawn door down
                    position = new Vector3(roofX, botY + .1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 180);
                    Instantiate(downDoor, position, Rotation);

                    //spawn door up
                    position = new Vector3(roofX, botY - 1.1f, 0);

                    Rotation = Quaternion.Euler(0, 0, 0);
                    Instantiate(backUpDoor, position, Rotation);

                    //if this is not the first room spawned
                    if (roomsSpawned != 0)
                    {
                        //if last room was to the right
                        if (lastRoomSpawned >= 0 && lastRoomSpawned < 4)
                        {
                            position = new Vector3(leftX + .2f, wallsY, 0);
                            //spawn door to the left
                            Rotation = Quaternion.Euler(0, 40, 90);
                            Instantiate(leftDoor, position, Rotation);
                        }
                    }
                }
            }

            ////rooms that go to the left
            //if (wallPosition == 6)
            //{
            //    if (roomsSpawned == 0)
            //    {
            //        position = new Vector3(rightX - 1, wallsY, 0);

            //        Rotation = Quaternion.Euler(0, 0, 0);
            //        Instantiate(rightDoor, position, Rotation);
            //        rightX += 19;
            //        roofX += 19;
            //        leftX += 19;

            //    }
            //    else
            //    {
            //        //if the last room wasnt on the right, create a door to the left and a door back to the right
            //        if (!(lastRoomSpawned >= 0 && lastRoomSpawned < 4))
            //        {
            //            position = new Vector3(leftX + 1, wallsY, 0);

            //            Rotation = Quaternion.Euler(0, 0, 0);
            //            Instantiate(leftDoor, position, Rotation);

            //            position = new Vector3(rightX -1, wallsY, 0);

            //            Rotation = Quaternion.Euler(0, 0, 0);
            //            Instantiate(rightDoor, position, Rotation);
            //        }
            //        else
            //        //if the last room spawned was on the right
            //        {
            //            print("Something happened here");
            //            position = new Vector3(rightX - 1, wallsY, 0);

            //            Rotation = Quaternion.Euler(0, 0, 0);
            //            Instantiate(rightDoor, position, Rotation);

            //            position = new Vector3(leftX + 1, wallsY, 0);

            //            Rotation = Quaternion.Euler(0, 0, 0);
            //            if (lastRoomSpawned != 4)
            //            {
            //                if (roomsSpawned != 0 && lastRoomSpawned != 5)
            //                {
            //                    Instantiate(rightDoor, position, Rotation);
            //                }
            //            }
            //        }
            //    }
            //}

            //add 1 to number of rooms spawned
            roomsSpawned++;

            //run program again
            spawnWalls(wallPosition);

        }

        //base case
        // if right room spawned this creates the left door in the last room
        else
        {
            //if the last room was to the right
            if (lastRoomSpawned >= 0 && lastRoomSpawned < 4 && roomsSpawned == roomsToSpawn)
            {
                position = new Vector3(leftX + .2f, wallsY, 0);

                Rotation = Quaternion.Euler(0, 40, 90);
                //spawn door to the left
                Instantiate(leftDoor, position, Rotation);
            }
            //else if (lastRoomSpawned == 6 && roomsSpawned == roomsToSpawn)
            //{
            //    position = new Vector3(rightX - 1, wallsY, 0);

            //    Rotation = Quaternion.Euler(0, 0, 0);
            //    Instantiate(rightDoor, position, Rotation);
            //}
            //else if (lastRoomSpawned ==  && roomsSpawned == roomsToSpawn)
            //{
            //    position = new Vector3(rightX - 1, wallsY, 0);

            //    Rotation = Quaternion.Euler(0, 0, 0);
            //    Instantiate(rightDoor, position, Rotation);
            //}
        }
    }




    public int enemiesToSpawn = 5;
    public int enemiesRemaining = 0;
    // Use this for initialization
    void Start()
    {

        if (GameObject.Find("menuMusic") != null)
        {
            safeRoomMusic = GameObject.Find("menuMusic");
        }
        else
        {
            safeRoomMusic = GameObject.Find("failSafeMenuMusic");
        }
        if (safeRoomMusic.GetComponent<AudioSource>().enabled == false)
        {
            safeRoomMusic.GetComponent<AudioSource>().enabled = true;
            safeRoomMusic.GetComponent<AudioSource>().Play();
            safeRoomMusic.GetComponent<AudioSource>().volume = .65f;
        }
        gameOver.SetActive(false);
        player = GameObject.Find("Player");
        player.GetComponent<PlayerMovement>().decoyText.text = "";
        player.GetComponent<PlayerMovement>().bombText.text = "";

        decoyKills = 5;
        bombKills = 5;
        Vector3 position;
        if (PlayerPrefs.GetInt("NPCsTalkedTo") > 10)
        {
            PlayerPrefs.SetInt("NPCsTalkedTo", 10);
        }
        for (int j = 0; j < PlayerPrefs.GetInt("NPCsTalkedTo"); j++)
        {
            position = new Vector3(Random.Range(13, 29), Random.Range(-3.5f, 3), 0);
            Instantiate(NPC, position, Quaternion.identity);
        }

        bossHasSpawned = false;
        player = GameObject.Find("Player");
        mainCamera = GameObject.Find("Main Camera");
        //create a list of all the doors
        allDoors = new List<GameObject>();

        //array with all the enemies
        value = new GameObject[10];
        value[0] = RedEnemy;
        value[1] = BlueEnemy;
        value[2] = YellowEnemy;
        value[3] = GreenEnemy;
        value[4] = WhiteEnemy;
        value[5] = SDEnemyRed;
        value[6] = GravityBlueEnemy;
        value[7] = greenShootEnemy;
        value[8] = whiteOrbEnemy;
        value[9] = wallRunnerYellowEnemy;

    //first wall spawned is wall 0
    int wallPosition = Random.Range(0, 1);

        //spawnWalls
        spawnWalls(wallPosition);


        //spawn a set number of enemies in random positions
        spawnEnemies(1);


        int i = 1;

        //laods an array with every gameobject
        GameObject[] gam = FindObjectsOfType<GameObject>();

        //looks for every gameobject with door in the name and loads it into a list
        foreach (GameObject gameObj in gam)
        {
            if (gameObj.name.Contains("Door"))
            {
                //load all the doors into a list
                allDoors.Add(gameObj);
                i++;
            }
        }
        //sets the collider to disabled
        for (int j = 0; j < allDoors.Count; j++)
        {
            allDoors[j].GetComponent<Collider2D>().enabled = false;
        }


    }
    // Update is called once per frame
    GameObject currDecoy;

    GameObject currBomb;
    void Update()
    {

        if (player.GetComponent<PlayerMovement>().inTown == false)
        {
            if (canTurnDownMusic)
            {
                if (roomsToBoss != 1)
                {
                    StartCoroutine(turnSafeRoomDown(safeRoomMusic, dungeionMusic));
                    canTurnDownMusic = false;
                }
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha1))
        {
            roomsToBoss = player.GetComponent<PlayerMovement>().roomsTraversed + 1;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.Alpha0))
        {
            roomsToBoss = roomsToSpawn;
        }
        if (bossHasSpawned == true && enemiesRemaining == 0)
        {
            if (gameOver.activeInHierarchy == false)
            {
                StartCoroutine(Fade());
            }
            for (int i = 0; i < allDoors.Count; i++)
            {
                allDoors[i].GetComponent<SpriteRenderer>().enabled = false;
                allDoors[i].GetComponent<Collider2D>().enabled = false;
            }
            allDoors.Clear();
            gameOver.SetActive(true);

        }

        if (player.GetComponent<PlayerMovement>().hasDecoyItem == true)
        {
            if (decoyKills < 5)
            {
                player.GetComponent<PlayerMovement>().decoyText.text = decoyKills + "/5";
                player.GetComponent<PlayerMovement>().decoyText.fontSize = 40;
                //Vector3 oldTextPos = player.GetComponent<PlayerMovement>().decoyText.rectTransform.localPosition;
                player.GetComponent<PlayerMovement>().decoyText.rectTransform.localPosition = new Vector3(68, -70, 0);
            }
            else
            {
                player.GetComponent<PlayerMovement>().decoyText.text = "E";
                player.GetComponent<PlayerMovement>().decoyText.fontSize = 70;
                //Vector3 oldTextPos = player.GetComponent<PlayerMovement>().decoyText.rectTransform.localPosition;
                player.GetComponent<PlayerMovement>().decoyText.rectTransform.localPosition = new Vector3(86, -50, 0);
                player.GetComponent<PlayerMovement>().decoyKey.color = new Color32(0, 200, 0, 255);
            }
        }

        if (player.GetComponent<PlayerMovement>().hasBombItem == true)
        {

            if (bombKills < 5)
            {
                player.GetComponent<PlayerMovement>().bombText.text = bombKills + "/5";
                player.GetComponent<PlayerMovement>().bombText.fontSize = 40;
                player.GetComponent<PlayerMovement>().bombText.rectTransform.localPosition = new Vector3(75, -97, 0);
            }
            else
            {
                player.GetComponent<PlayerMovement>().bombText.text = "Q";
                player.GetComponent<PlayerMovement>().bombText.fontSize = 70;
                player.GetComponent<PlayerMovement>().bombText.rectTransform.localPosition = new Vector3(100, -70, 0);
                player.GetComponent<PlayerMovement>().bombKey.color = new Color32(0, 200, 0, 255);
            }
        }
        //PlayerPrefs.DeleteKey("NPCsTalkedTo");

        if (NPCTalkedTo == 1)
        {
            PlayerPrefs.SetInt("NPCsTalkedTo", (PlayerPrefs.GetInt("NPCsTalkedTo") + 1));
            NPCTalkedTo = 0;
        }

        if (bombKills >= 5)
        {
            if (player.GetComponent<PlayerMovement>().hasBombItem == true)
            {
                if (Input.GetKeyDown(Bomb))
                {
                    player.GetComponent<PlayerMovement>().bombText.text = "0/5";
                    player.GetComponent<PlayerMovement>().bombKey.color = Color.white;
                    Vector3 position = player.transform.position;
                    currBomb = Instantiate(bomb, position, Quaternion.identity);
                    bombTimerSound.Play();
                    StartCoroutine(bombTimer(currBomb));
                    bombKills = 0;

                }
            }
        }
        if (resetDecoyOnce)
        {
            if (decoyDroped == false)
            {
                resetDecoyOnce = false;
                GameObject[] gam = FindObjectsOfType<GameObject>();

                foreach (GameObject gameObj in gam)
                {
                    if (gameObj.name.Contains("ShootEnemy 1(Clone)"))
                    {
                        gameObj.GetComponent<shootEnemy>().player = player;
                    }
                    else if (gameObj.name.Contains("StaticEnemy(Clone)"))
                    {
                        gameObj.GetComponent<slowFollowAI>().Target = player.transform;
                    }
                    else if (gameObj.name.Contains("ChaseEnemy(Clone)"))
                    {
                        gameObj.GetComponent<ChaseAI>().Target = player.transform;
                    }
                    else if (gameObj.name.Contains("AOEEnemy 1(Clone)"))
                    {
                        gameObj.GetComponent<AOEEnemy>().Player = player.transform;
                    }
                    else if (gameObj.name.Contains("GreenEnemy(Clone)"))
                    {
                        gameObj.GetComponent<WeepingEnemy>().Target = player.transform;
                    }
                    else if (gameObj.name.Contains("greenShootEnemy"))
                    {
                        gameObj.GetComponent<greenShootEnemy>().Target = player.transform;
                    }
                    else if (gameObj.name.Contains("whiteOrbEnemy"))
                    {
                        gameObj.GetComponent<whiteOrbEnemy>().Target = player.transform;
                    }

                }
            }
        }


        if (decoyKills >= 5)
        {
            if (player.GetComponent<PlayerMovement>().hasDecoyItem == true)
            {
                if (Input.GetKeyDown(Decoy))
                {
                    decoyDroped = true;
                    resetDecoyOnce = true;
                    player.GetComponent<PlayerMovement>().decoyText.text = "0/5";
                    player.GetComponent<PlayerMovement>().decoyKey.color = Color.white;
                    Vector3 position = player.transform.position;
                    currDecoy = Instantiate(decoy, position, Quaternion.identity);

                    GameObject[] gam = FindObjectsOfType<GameObject>();

                    foreach (GameObject gameObj in gam)
                    {
                        if (gameObj.name.Contains("ShootEnemy 1(Clone)"))
                        {
                            gameObj.GetComponent<shootEnemy>().player = currDecoy;
                        }
                        else if (gameObj.name.Contains("StaticEnemy(Clone)"))
                        {
                            gameObj.GetComponent<slowFollowAI>().Target = currDecoy.transform;
                        }
                        else if (gameObj.name.Contains("ChaseEnemy(Clone)"))
                        {
                            gameObj.GetComponent<ChaseAI>().Target = currDecoy.transform;
                        }
                        else if (gameObj.name.Contains("AOEEnemy 1(Clone)"))
                        {
                            gameObj.GetComponent<AOEEnemy>().Player = currDecoy.transform;
                        }
                        else if (gameObj.name.Contains("GreenEnemy(Clone)"))
                        {
                            gameObj.GetComponent<WeepingEnemy>().Target = currDecoy.transform;
                        }
                        else if (gameObj.name.Contains("greenShootEnemy"))
                        {
                            gameObj.GetComponent<greenShootEnemy>().Target = currDecoy.transform;
                        }
                        else if (gameObj.name.Contains("whiteOrbEnemy"))
                        {
                            gameObj.GetComponent<whiteOrbEnemy>().Target = currDecoy.transform;
                        }
                    }
                    decoyKills = 0;
                }
            }
        }


        if (enemiesRemaining < currNumofEnemies)
        {
            originalCameraPosition = mainCamera.transform.position;
            shakeAmt = .02f;
            InvokeRepeating("CameraShake", 0, .01f);
            Invoke("StopShaking", 0.3f);
            //Time.timeScale = .2f;
            currNumofEnemies = enemiesRemaining;
            //StartCoroutine(slowMoTimer());
        }

        if (enemiesRemaining <= 0)
        {
            if (player.GetComponent<PlayerMovement>().roomsGoneBack == 0)
            {
                for (int i = 0; i < allDoors.Count; i++)
                {
                    allDoors[i].GetComponent<SpriteRenderer>().enabled = true;
                }
            }
            if (doorsActive)
            {
                //enemiesRemaining++;
                StartCoroutine(doorTimer());
                for (int i = 0; i < allDoors.Count; i++)
                {
                    allDoors[i].GetComponent<SpriteRenderer>().enabled = true;
                    allDoors[i].GetComponent<Collider2D>().enabled = true;
                }
            }
        }


    }

    void CameraShake()
    {
        if (player.GetComponent<PlayerMovement>().canShake == true)
        {
            if (shakeAmt > 0)
            {
                float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
                Vector3 pp = mainCamera.transform.position;
                pp.y += quakeAmt; // can also add to x and/or z
                pp.z += quakeAmt;
                pp.x += quakeAmt;
                mainCamera.transform.position = pp;
            }
        }
        else
        {
            mainCamera.transform.position = originalCameraPosition;
        }
    }

    public void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = originalCameraPosition;
    }


    void EndGame()
    {
        Application.LoadLevel("Intro");
    }

    IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("PRISM");
    }

    //IEnumerator slowMoTimer()
    //{
    //    while (Time.timeScale < 1)
    //    {
    //        yield return new WaitForSeconds(.1f);
    //        Time.timeScale += .2f;
    //    }
    //    if (Time.timeScale > 1)
    //    {
    //        Time.timeScale = 1;
    //    }
    //}

    public void spawnEnemies(int numOfEnemies)
    {
        StartCoroutine(enemySpawnTimer(numOfEnemies));
    }

    void spawnOctogon(int randomNumber)
    {
        Vector3 position;
        Quaternion Rotation;
        if (randomNumber == 0)
        {
            position = new Vector3(rightX - .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY - 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY - 4, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(roofX, topY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(leftX + .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(roofX, botY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(leftX + 1, topY, 1);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(floor, position, Rotation);
        }
        if (randomNumber == 1)
        {
            position = new Vector3(rightX - .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY - 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY - 4, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(roofX, topY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(leftX + .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(roofX, botY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(leftX + 1, topY, 1);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(floor, position, Rotation);
        }
        if (randomNumber == 2)
        {
            position = new Vector3(rightX - .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY - 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY - 4, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(roofX, topY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(leftX + .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(roofX, botY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(leftX + 1, topY, 1);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(floor, position, Rotation);
        }
        if (randomNumber == 3)
        {
            position = new Vector3(rightX - .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY + 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(rightX - 2.9f, wallsY - 4f, 0);

            Rotation = Quaternion.Euler(0, 0, 19);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(leftX + 2.9f, wallsY - 4, 0);

            Rotation = Quaternion.Euler(0, 0, -19);
            Instantiate(octoYellowWall, position, Rotation);

            position = new Vector3(roofX, topY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoGreenWall, position, Rotation);

            position = new Vector3(leftX + .2f, wallsY, 0);

            Rotation = Quaternion.Euler(0, 0, 90);
            Instantiate(octoRedWall, position, Rotation);

            position = new Vector3(roofX, botY, 0);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(octoBlueWall, position, Rotation);

            position = new Vector3(leftX + 1, topY, 1);

            Rotation = Quaternion.Euler(0, 0, 0);
            Instantiate(floor, position, Rotation);
        }
    }

    IEnumerator enemySpawnTimer(int numOfEnemies)
    {
        if (bossHasSpawned == false)
        {
            if (player.GetComponent<PlayerMovement>().roomsTraversed == roomsToBoss)
            {
                Vector3 position = new Vector3(GetComponent<Transform>().position.x, GetComponent<Transform>().position.y, 0);
                Instantiate(boss, position, Quaternion.identity);
                enemiesRemaining = 13;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemiesRemaining;
                //dont spawn any other enemies
                enemiesToSpawn = 0;
                numOfEnemies = 4;
                bossHasSpawned = true;
                if (canTurnDownMusicBoss)
                {
                    if (safeRoomMusic.GetComponent<AudioSource>().volume > 0)
                    {
                        StartCoroutine(turnSafeRoomDown(safeRoomMusic, bossMusic));
                    }
                    else
                    {
                        StartCoroutine(turnSafeRoomDown(dungeionMusic, bossMusic));
                    }
                    canTurnDownMusicBoss = false;
                }
                enemySpawnXMax = GetComponent<Transform>().position.x + 7.5f;
                enemySpawnXMin = GetComponent<Transform>().position.x - 7.5f;
                enemySpawnYMax = GetComponent<Transform>().position.y + 4;
                enemySpawnYMin = GetComponent<Transform>().position.y - 4;
                if (player.GetComponent<PlayerMovement>().isOctoRoom)
                {
                    enemySpawnYMax = GetComponent<Transform>().position.y + 3;
                    enemySpawnYMin = GetComponent<Transform>().position.y - 3;
                }
            }
        }
        // randomizeTest();
        //BlueEnemy.GetComponent<shootEnemy>().chaseRange = 0;
        //GreenEnemy.GetComponent<WeepingEnemy>().chaseRange = 0;
        //YellowEnemy.GetComponent<AOEEnemy>().chaseRange = 0;
        //int whiteEnemiesToSpawn = Random.Range(0, 0);
        //for (int i = 0; i < whiteEnemiesToSpawn; i++)
        //{
        //    Vector3 position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
        //    Instantiate(value[4], position, Quaternion.identity);
        //}

        int spawnNPC = Random.Range(0, 4);
        int NPCSpawned = 0;

        int yellowEnemiesSpawned = 0;
        int greenEnemiesSpawned = 0;
        int whiteEnemiesSpawned = 0;
        int spawnPostIt = Random.Range(0, 5);
        int postItSpawned = 0;
        GameObject currEnemy;
        int enemyNumber;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
            if (bossHasSpawned)
            {
                while (position.x < player.GetComponent<Transform>().position.x + 1.5f && position.x > player.GetComponent<Transform>().position.x - 1.5f && position.y < player.GetComponent<Transform>().position.y + 1.5f && position.y > player.GetComponent<Transform>().position.y - 1.5f)
                {
                    position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
                }
            }
            if (!player.GetComponent<PlayerMovement>().isOctoRoom)
            {
                enemyNumber = Random.Range(0, 10);
            }
            else
            {
                enemyNumber = Random.Range(0, 9);
            }

            if (enemyNumber == 2)
            {
                yellowEnemiesSpawned++;
            }
            else if (enemyNumber == 3)
            {
                greenEnemiesSpawned++;
            }
            else if (enemyNumber == 4)
            {
                whiteEnemiesSpawned++;
            }
            if (yellowEnemiesSpawned >= 2 && enemyNumber == 2)
            {
                enemyNumber = Random.Range(0, 9);
                i--;
                continue;
            }
            else if (greenEnemiesSpawned >= 3 && enemyNumber == 3)
            {
                enemyNumber = Random.Range(0, 9);
                i--;
                continue;
            }
            else if (whiteEnemiesSpawned >= 2 && enemyNumber == 4)
            {
                enemyNumber = Random.Range(0, 9);
                i--;
                continue;
            }
            currEnemy = Instantiate(value[enemyNumber], position, Quaternion.identity);
            if (enemyNumber != 9)
            {
                addSheild(currEnemy, enemyNumber, position);
            }
            if (enemyNumber == 5)
            {
                position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
                GameObject additionalSDEnemy = Instantiate(value[enemyNumber], position, Quaternion.identity);
                addSheild(additionalSDEnemy, enemyNumber, position);
                enemiesRemaining++;
            }
            if (bossHasSpawned == false)
            {
                if (NPCSpawned < 1)
                {
                    if (spawnNPC == 0)
                    {
                        NPCSpawned++;
                        position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
                        Instantiate(NPC, position, Quaternion.identity);
                    }
                }
            }
        }
        if (bossHasSpawned == false)
        {
            Time.timeScale = .001f;
            yield return new WaitForSeconds(.0007f);
            Time.timeScale = 1;
        }
        enemiesToSpawn = numOfEnemies;
        totalNumOfEnemies = enemiesToSpawn;
        currNumofEnemies = enemiesRemaining;
    }

    void addSheild(GameObject currEnemy, int enemyNumber, Vector3 position)
{

    int sheildNumber = Random.Range(0, 19);

    Quaternion redSheildRotation = Quaternion.Euler(0, 90, 0);

    if (enemyNumber == 0)

    {
        position.x += .6f;
        if (sheildNumber == 0)
        {
            Instantiate(blueSheild, position, redSheildRotation).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 1)
        {
            Instantiate(redSheild, position, redSheildRotation).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 2)
        {
            Instantiate(greenSheild, position, redSheildRotation).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 3)
        {
            Instantiate(yellowSheild, position, redSheildRotation).transform.parent = currEnemy.transform;
        }
    }
    else
    {
        if (sheildNumber == 0)
        {
            Instantiate(blueSheild, position, Quaternion.identity).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 1)
        {
            Instantiate(redSheild, position, Quaternion.identity).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 2)
        {
            Instantiate(greenSheild, position, Quaternion.identity).transform.parent = currEnemy.transform;
        }
        else if (sheildNumber == 3)
        {
            Instantiate(yellowSheild, position, Quaternion.identity).transform.parent = currEnemy.transform;
        }
    }
}


//if (postItSpawned < 1)
//{
//    if (spawnPostIt == 5)
//    {
//        postItSpawned++;
//        position = new Vector3(Random.Range(enemySpawnXMin, enemySpawnXMax), Random.Range(enemySpawnYMax, enemySpawnYMin), 0);
//        Instantiate(postIt, position, Quaternion.identity);
//    }
//}

IEnumerator bombTimer(GameObject currBomb)
{
    yield return new WaitForSeconds(1.5f);
    bombTimerSound.Stop();
    GameObject currExplosion = Instantiate(bombExpolsion, currBomb.transform.position, Quaternion.identity);
    Destroy(currBomb);
    bombExplosionSound.Play();
    float i = 0;
    Vector3 j = currExplosion.transform.localScale;
    while (i < 5 && currExplosion != null)
    {
        i = currExplosion.transform.localScale.x;
        currExplosion.transform.localScale = new Vector3(j.x + .15f, j.y + .15f, j.z + .15f);
        yield return new WaitForSeconds(.001f);
        if (currExplosion != null)
        {
            j = currExplosion.transform.localScale;
        }
    }
}

IEnumerator doorTimer()
{
    //doorsActive = false;
    yield return new WaitForSeconds(0);
    //doorsActive = true;

}

IEnumerator turnSafeRoomDown(GameObject turnOff, GameObject turnOn)
{
    while (turnOff.GetComponent<AudioSource>().volume > 0)
    {
        turnOff.GetComponent<AudioSource>().volume -= .02f;
        yield return new WaitForSecondsRealtime(.07f);
    }
    turnOff.GetComponent<AudioSource>().enabled = false;
    turnOn.GetComponent<AudioSource>().enabled = true;
    turnOn.GetComponent<AudioSource>().Play();
    while (turnOn.GetComponent<AudioSource>().volume < .2f)
    {
        turnOn.GetComponent<AudioSource>().volume += .02f;
        yield return new WaitForSecondsRealtime(.03f);
    }
}

IEnumerator Fade()
{
    yield return new WaitForSeconds(3f);
    for (float alpha = 0.0f; alpha < 1.0f; alpha += .01f)
    {
        fadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, alpha);
        yield return new WaitForSeconds(.03f);
    }
    Invoke("EndGame", 2f);

}


}

