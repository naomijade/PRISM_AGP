using UnityEngine;
using System.Collections;

public class hit : MonoBehaviour
{
    public GameObject cube;

    public AudioSource killEnemy;
    public AudioSource bounceOffWall;
    public AudioSource wrongEnemy;
    int HPdrop;
    public GameObject HealthPoint;
    public AudioSource NPCinPainSound;
    public AudioSource NPCinPainSoundTwo;

    public AudioSource stopYellowNoise;

    public AudioSource bossHurtSound;
    public AudioSource bossHurtSoundTwo;

    public GameObject killStreakLight;

    bool canLerp = true;


    GameObject player;

    public GameObject mainCam;

    Vector3 bossSize;

    Vector2 newDirct;
    //tracks the color or the bullet: White = 0, Red = 1, Green = 2, Blue = 3, Yellow = 4
    public int currColor = 0;

    public GameObject enemyCounter;
    int enemiesLeft;

    public GameObject[] respawns;
    public GameObject[] greenBullets;

    public int timesHitWall;

    Material myMaterial;
    Material enemyPartMaterial;

    public ParticleSystem emit;
    public ParticleSystem collPart;
    public ParticleSystem enemyPart;
    //the point light that changes colors
    public Light light;

    public Light DeathLight;

    int greenInt = 0;
    public GameObject particleHolder;

    public GameObject[] allParticles;

    //bool restoreBulletHeart = true;

    GameObject bullet;
    void Start()
    {

        //killStreakLight.GetComponent<Light>().color = Color.red;
        bullet = this.gameObject;
        //sets start color to white
        particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 0, 0);
        //sets currColor to white
        currColor = 0;
        //loads white color
        myMaterial = Resources.Load("White") as Material;
        GetComponent<MeshRenderer>().material = myMaterial;
        //finds the player
        player = GameObject.FindGameObjectWithTag("Player");
        newDirct = cube.GetComponent<fireBullet>().direction;
        //sets the enemies remaining text
        GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
        //loads ther green enemy array
        respawns = GameObject.FindGameObjectsWithTag("GreenEnemy");
        //sets start speed to zero
        foreach (GameObject respawn in respawns)
        {
            respawn.GetComponent<WeepingEnemy>().moveSpeed = 0;

        }
        GetComponent<Collider2D>().isTrigger = true;
        // StartCoroutine(bulletTriggerTimer());
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Gravity")
        {
            //GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(newDirct.y, -newDirct.x) * Mathf.Rad2Deg);
            //newDirct.x = -newDirct.x;
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }



void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "RedWall" || col.gameObject.tag == "BlueWall" || col.gameObject.tag == "GreenWall" || col.gameObject.tag == "YellowWall")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }

        if (col.gameObject.name.Contains("NPCHolder"))
        {
            greenEnemyCheck();
            destroyBullet();
            int rng = Random.Range(0, 2);
            if (rng == 0)
            {
                NPCinPainSound.Play();
            }
            else if(rng == 1)
            {
                NPCinPainSoundTwo.Play();
            }
            col.gameObject.transform.parent.GetComponent<NPCBehavior>().NPCHP--;
        }

        if (col.gameObject.tag == "RedEnemy" || col.gameObject.tag == "SDEnemyRed")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 1)
            {
                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(1);
                killEnemy.Play();
                Destroy(col.gameObject);
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        else if (col.gameObject.name.Contains("wallRunner"))
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 4)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++; ;
                DeathLight.GetComponent<Light>().color = new Color(2, 2, 0);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);
                stopYellowNoise.Stop();

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                {
                    Instantiate(HealthPoint, pos, rotation);
                }

                light.GetComponent<Light>().intensity = .8f;
                //GameObject enemyAOE = GameObject.Find("AOEHolder(Clone)");
                //Destroy(enemyAOE);
                enemyPartSpawn(4);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;
                player.GetComponent<fireBullet>().notInAOE = true;
                Destroy(col.gameObject);
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        //if it collides with a Yellow enemy
        else if (col.gameObject.tag == "YellowEnemy")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 4)
            {
                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(4);
                killEnemy.Play();
                stopYellowNoise.Stop();
                Destroy(col.gameObject);
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        //if it collides with a Blue enemy
        else if (col.gameObject.tag == "BlueEnemy")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 3)
            {
                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(3);
                killEnemy.Play();
                Destroy(col.gameObject);
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        else if (col.gameObject.tag == "GreenSheild")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 2)
            {
                if (particleHolder.GetComponent<TrailRenderer>().startColor == new Color(0, 1, 0))
                {
                    particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 2, .01f);
                }
                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(2);
                killEnemy.Play();
                player.GetComponent<fireBullet>().notInAOE = true;
                Destroy(col.gameObject);
            }


            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag =="Player")
        {
            StartCoroutine(bulletCollisionTimer());
        }

        //in case you use drag on gravity so that the bullet speeds back up after it leaves
        if (col.gameObject.tag == "Gravity")
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;
            GetComponent<Rigidbody2D>().AddForce(dir * .004f);
        }
    }

    void Update()
    {

        if (mainCam.GetComponent<randomSpawner>().killStreak > 3)
        {
            player.GetComponent<fireBullet>().projectileSpeed = 13;
            player.GetComponent<fireBullet>().cooldown = .35f;
            if (player.GetComponent<fireBullet>().notInAOE == true)
            {
                player.GetComponent<PlayerMovement>().characterSpeed = 2;
            }
            killStreakLight.SetActive(true);
            if (canLerp == true)
            {
                canLerp = false;
                StartCoroutine(colorLerp());
            }

        }
        else
        {
            //StopCoroutine(colorLerp());
            player.GetComponent<fireBullet>().projectileSpeed = 10;
            player.GetComponent<fireBullet>().cooldown = .5f;
            if (player.GetComponent<fireBullet>().notInAOE == true)
            {
                player.GetComponent<PlayerMovement>().characterSpeed = 1.4f;
            }
            killStreakLight.SetActive(false);
        }
        //checks to see if wall was hit 4 times, if so destroy the bullet
        if (timesHitWall >= 4)
        {
            light.GetComponent<Light>().intensity = .8f;
            //detach the particle system and trail renderer from the bullet
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            greenEnemyCheck();
            //destroy bullet
            mainCam.GetComponent<randomSpawner>().killStreak = 0;
            destroyBullet();
        }


        //finds all the green bullets
        greenBullets = GameObject.FindGameObjectsWithTag("Bullet");
        allParticles = GameObject.FindGameObjectsWithTag("Respawn");

        foreach (GameObject greenBull in greenBullets)
        {
            if (particleHolder.GetComponent<TrailRenderer>().startColor == new Color(0, 1, 0))
            {
                greenBull.GetComponent<hit>().greenInt = 1;
                foreach (GameObject respawn in respawns)
                {
                    if (respawn != null)
                    {
                        if (respawn.GetComponent<WeepingEnemy>().canMove)
                                respawn.GetComponent<WeepingEnemy>().moveSpeed = 4;
                    }
                }
            }
        }


    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(timesHitWall == 0)
            {
                
            }
            else
            {
                greenEnemyCheck();
                wrongEnemy.Play();
                destroyBullet();
            }
        }
    }
        void OnCollisionEnter2D(Collision2D col)
    {
        //if the bullet collides with an object it will reflecct at an appropriate angle
        if (col.gameObject.tag == "RedWall" || col.gameObject.tag == "BlueWall" || col.gameObject.tag == "GreenWall" || col.gameObject.tag == "YellowWall")
        {
            GetComponent<Collider2D>().isTrigger = false;
        }


        if (col.gameObject.tag == "Player")
        {
            if (timesHitWall == 0)
            {

            }
            else
            {
                greenEnemyCheck();
                wrongEnemy.Play();
                destroyBullet();
            }
        }


        if (col.gameObject.name.Contains("Decoy"))
        {
            wrongEnemy.Play();
            destroyBullet();
        }
        if (col.gameObject.name.Contains("TownWall"))
        {
            wrongEnemy.Play();
            destroyBullet();
        }
        if (col.gameObject)
        {
            //GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, Mathf.Atan2(newDirct.y, -newDirct.x) * Mathf.Rad2Deg);
            //newDirct.x = -newDirct.x;
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //if (col.gameObject.name.Contains("obs"))
        //{
        //    Vector2 v = GetComponent<Rigidbody2D>().velocity;
        //    float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        //    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //}

        //if it collides with a red enemy
        if (col.gameObject.tag == "RedEnemy" || col.gameObject.tag == "SDEnemyRed")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 1)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++; ;
                DeathLight.GetComponent<Light>().color = new Color(1f, 0, 0);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                {
                    Instantiate(HealthPoint, pos, rotation);
                }

                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(1);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
                Destroy(col.gameObject);
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        //if it collides with a Yellow enemy
        else if (col.gameObject.tag == "YellowEnemy")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 4)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++; ;
                DeathLight.GetComponent<Light>().color = new Color(2, 2, 0);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);
                stopYellowNoise.Stop();

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                {
                    Instantiate(HealthPoint, pos, rotation);
                }

                light.GetComponent<Light>().intensity = .8f;
                //GameObject enemyAOE = GameObject.Find("AOEHolder(Clone)");
                //Destroy(enemyAOE);
                enemyPartSpawn(4);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;
                player.GetComponent<fireBullet>().notInAOE = true;
                Destroy(col.gameObject);
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        //if it collides with a Blue enemy
        else if (col.gameObject.tag == "BlueEnemy")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 3)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++;
                DeathLight.GetComponent<Light>().color = new Color(0, 0, 1);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                {
                    Instantiate(HealthPoint, pos, rotation);
                }

                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(3);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;

                Destroy(col.gameObject);
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet();
        }
        //if it collides with a white enemy
        else if (col.gameObject.tag == "StaticEnemy")
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 0)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++; ;
                DeathLight.GetComponent<Light>().color = new Color(1f, 1, 1);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                {
                    Instantiate(HealthPoint, pos, rotation);
                }

                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(0);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;

                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
                Destroy(col.gameObject);
            }
            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
            }
            greenEnemyCheck();
            destroyBullet(); ;
        }
        else if (col.gameObject.tag == "GreenEnemy" || col.gameObject.name.Contains("greenShootEnemy") || col.gameObject.name.Contains("GreenOrb"))
        {
            //detach the particle system and trail renderer from the bullet
            greenEnemyCheck();
            transform.DetachChildren();
            //stop the particle system emitions
            emit.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            if (currColor == 2)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++;
                DeathLight.GetComponent<Light>().color = new Color(0, 1, 0);
                Vector2 pos = gameObject.transform.position;
                Quaternion rotation = Quaternion.identity;
                Instantiate(DeathLight, pos, rotation);

                HPdrop = Random.Range(0, 11);
                if (HPdrop == 0)
                { 
                    Instantiate(HealthPoint, pos, rotation);
                }

                
                if (particleHolder.GetComponent<TrailRenderer>().startColor == new Color(0, 1, 0))
                    {
                        particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 2, .01f);
                    }
                light.GetComponent<Light>().intensity = .8f;
                enemyPartSpawn(2);
                killEnemy.Play();
                mainCam.GetComponent<randomSpawner>().decoyKills++;
                mainCam.GetComponent<randomSpawner>().bombKills++;

                player.GetComponent<fireBullet>().notInAOE = true;
                Destroy(col.gameObject);
                //respawns = null;
                //respawns = GameObject.FindGameObjectsWithTag("GreenEnemy");
                //print(respawns.Length);
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                GameObject.FindGameObjectWithTag("EnemiesRemaining").GetComponent<TextMesh>().text = "Enemies Left: " + enemyCounter.GetComponent<randomSpawner>().enemiesRemaining;
            }


            else
            {
                mainCam.GetComponent<randomSpawner>().killStreak = 0;
                wrongEnemy.Play();
                if (col.gameObject.name.Contains("greenShootEnemy"))
                {
                    col.gameObject.GetComponent<greenShootEnemy>().shotsToFire++;
                }
            }
            greenEnemyCheck();
            destroyBullet();
        }


        //if it collides with a Yellow Wall
        else if (col.gameObject.tag == "YellowWall")
        {
            timesHitWall++;
            myMaterial = Resources.Load("Yellow") as Material;
            GetComponent<MeshRenderer>().material = myMaterial;
            partCollSpawn();
            bounceOffWall.Play();
            particleHolder.GetComponent<TrailRenderer>().startColor = new Color(255, 255, 0);
            //collPart.transform.position = pos;
            currColor = 4;
            light.GetComponent<Light>().color = new Color(1.3f, 1f, 0);
            light.GetComponent<Light>().intensity += .6f;
            StartCoroutine(lightIntesityTimer());
            StartCoroutine(wallBounceTimer(col.gameObject));
            greenEnemyCheck();
        }

        //if it collides with a Red Wall
        else if (col.gameObject.tag == "RedWall")
        {

            timesHitWall++;

            myMaterial = Resources.Load("Red") as Material;
            GetComponent<MeshRenderer>().material = myMaterial;
            partCollSpawn();
            bounceOffWall.Play();
            currColor = 1;
            particleHolder.GetComponent<TrailRenderer>().startColor = new Color(1, 0, 0);
            light.GetComponent<Light>().color = new Color(1, 0, 0);
            light.GetComponent<Light>().intensity += .6f;
            StartCoroutine(lightIntesityTimer());
            StartCoroutine(wallBounceTimer(col.gameObject));
            greenEnemyCheck();
        }

        //if it collides with a Blue Wall
        else if (col.gameObject.tag == "BlueWall")
        {

            timesHitWall++;

            myMaterial = Resources.Load("Blue") as Material;
            GetComponent<MeshRenderer>().material = myMaterial;
            partCollSpawn();
            bounceOffWall.Play();
            currColor = 3;
            particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 0, 1);
            light.GetComponent<Light>().color = new Color(0, 0, 5);
            light.GetComponent<Light>().intensity += .6f;
            StartCoroutine(lightIntesityTimer());
            StartCoroutine(wallBounceTimer(col.gameObject));
            greenEnemyCheck();
        }

        //if it collides with a green Wall
        else if (col.gameObject.tag == "GreenWall")
        {

            
            myMaterial = Resources.Load("Green") as Material;
            GetComponent<MeshRenderer>().material = myMaterial;
            partCollSpawn();
            bounceOffWall.Play();
            currColor = 2;
            //ensures that the green enemy doesnt chase you if bullet is destroyed on green wall
            if (timesHitWall != 3)
            {
                particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 1, 0);
            }
            else
            {
                particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 2f, .01f);
            }
            light.GetComponent<Light>().color = new Color(0, 1, 0);
            light.GetComponent<Light>().intensity += .6f;
            StartCoroutine(lightIntesityTimer());
            StartCoroutine(wallBounceTimer(col.gameObject));

            //greenEnemyCheck();
            timesHitWall++;
        }


        if (col.gameObject.tag == "Boss")
        {
            greenEnemyCheck();
            enemyPart.startSize = .12f;
            enemyPartSpawn(currColor);
            enemyPart.startSize = .06f;
            bossSize = col.gameObject.GetComponent<Transform>().localScale;
            col.gameObject.GetComponent<Transform>().localScale = new Vector3(bossSize.x / 1.5f, bossSize.y / 1.5f, bossSize.z);
            mainCam.GetComponent<randomSpawner>().spawnEnemies(4);
            int rng = Random.Range(0, 2);
            if (rng == 0)
            {
                bossHurtSound.Play();
            }
            else if (rng == 1)
            {
                bossHurtSoundTwo.Play();
            }
            if (bossSize.x < .7f)
            {
                mainCam.GetComponent<randomSpawner>().killStreak++;
                enemyCounter.GetComponent<randomSpawner>().enemiesRemaining--;
                Destroy(col.gameObject);
                GameObject.FindGameObjectWithTag("Score").GetComponent<TextMesh>().text = "Enemies Left: " + enemiesLeft;
            }
            Destroy(gameObject);
        }
    }


    //maintains the green enemy and bullet checks
    void greenEnemyCheck()
    {
        if (particleHolder != null)
        {
            if (particleHolder.GetComponent<TrailRenderer>().startColor == new Color(0, 1, 0))
            {
                particleHolder.GetComponent<TrailRenderer>().startColor = new Color(0, 2, .01f);
            }
        }


        foreach (GameObject greenBull in greenBullets)
        {
            if (greenBull != null)
            {
                if (greenBull.GetComponent<hit>().greenInt == 1)
                {
                    foreach (GameObject respawn in respawns)
                    {
                        if (respawn != null)
                        {
                            respawn.GetComponent<WeepingEnemy>().moveSpeed = 0;
                        }
                    }
                }
            }
        }
    }

    void partCollSpawn()
    {
        Vector2 pos = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;
        collPart.GetComponent<Renderer>().material = myMaterial;
        Instantiate(collPart, pos, rotation);
    }

    void destroyBullet()
    {
        bullet.GetComponent<MeshRenderer>().enabled = false;
        bullet.GetComponent<Rigidbody2D>().simulated = false;
        //if (restoreBulletHeart && gameObject.name.Contains("MainBullet"))
        //{
        //    restoreBulletHeart = false;
        //    player.GetComponent<PlayerMovement>().bulletHearts++;
        //}
        Destroy(bullet, 2);
    }

    IEnumerator wallBounceTimer(GameObject col)
    {
        //change wall size
        Vector3 original = col.gameObject.GetComponent<Transform>().localScale;
        col.gameObject.GetComponent<Transform>().localScale += new Vector3(.3f, .3f, .3f);
        if (col.gameObject != null)
        {
            while (col.gameObject.GetComponent<Transform>().localScale.x > original.x)
            {
                if (col.gameObject != null)
                {
                    yield return new WaitForSeconds(.05f);
                    col.gameObject.GetComponent<Transform>().localScale -= new Vector3(.05f, .05f, .05f);
                }
                else
                {
                    yield return new WaitForSeconds(.05f);
                    col.gameObject.GetComponent<Transform>().localScale -= new Vector3(.05f, .05f, .05f);
                }
                if (col.gameObject.GetComponent<Transform>().localScale.x < original.x)
                {
                    col.gameObject.GetComponent<Transform>().localScale = original;
                }
            }
        }
    }

    //IEnumerator bulletTriggerTimer()
    //{
    //    yield return new WaitForSeconds(.1f);
    //    GetComponent<Collider2D>().isTrigger = false;
    //}

        IEnumerator lightIntesityTimer()
    {

        bullet.GetComponent<Rigidbody2D>().velocity = bullet.GetComponent<Rigidbody2D>().velocity * 1.15f;


        //change light intescity
        while (light.GetComponent<Light>().intensity > .6f)
        {
            yield return new WaitForSeconds(.3f);
            light.GetComponent<Light>().intensity -= .2f;
        }
        if (light.GetComponent<Light>().intensity < .6f)
        {
            light.GetComponent<Light>().intensity = .6f;
        }
    }


        void enemyPartSpawn(int currColor)
    {
        Vector2 pos = gameObject.transform.position;
        Quaternion rotation = gameObject.transform.rotation;
        if (currColor == 0)
        {
           enemyPartMaterial = Resources.Load("White") as Material;
        }
        else if (currColor == 1)
        {
            enemyPartMaterial = Resources.Load("Red") as Material;
        }
        else if (currColor == 2)
        {
            enemyPartMaterial = Resources.Load("Green") as Material;
        }
        else if (currColor == 3)
        {
            enemyPartMaterial = Resources.Load("Blue") as Material;
        }
        else if (currColor == 4)
        {
            enemyPartMaterial = Resources.Load("Yellow") as Material;
        }
        enemyPart.GetComponent<Renderer>().material = enemyPartMaterial;
        Instantiate(enemyPart, pos, rotation);
    }

    public void destroyBulletsInRoom()
    {
        //foreach (GameObject particles in allParticles)
        //{
        //    if (particles.GetComponent<TrailRenderer>().startColor == new Color(0, 1, 0))
        //    {
        //        particles.GetComponent<TrailRenderer>().startColor = new Color(0, 1.1f, 0);
        //    }
        //    {
        //        Destroy(particles);
        //    }
        //}
        foreach (GameObject greenBull in greenBullets)
        {
            if (greenBull != GameObject.Find("MainBullet") && greenBull != GameObject.Find("WeakBullet"))
            {
                Destroy(greenBull);
            }
        }
        greenBullets = new GameObject[0];
        //player.GetComponent<PlayerMovement>().bulletHearts = player.GetComponent<PlayerMovement>().maxBulletHearts;
    }

    IEnumerator colorLerp()
    {

        killStreakLight.GetComponent<Light>().color = new Color(1, 0, 0); ;
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color32(255, 127, 80, 255);
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color(255, 255, 1);
        killStreakLight.GetComponent<Light>().intensity = .01f;
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color(0, 255, 0);
        killStreakLight.GetComponent<Light>().intensity = .01f;
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color(0, 191, 255);
        killStreakLight.GetComponent<Light>().intensity = .01f;
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color32(255, 0, 255, 255);
        killStreakLight.GetComponent<Light>().intensity = 2.43f;
        yield return new WaitForSecondsRealtime(.5f);
        killStreakLight.GetComponent<Light>().color = new Color32(148, 0, 211, 255);
        yield return new WaitForSecondsRealtime(.5f);
        canLerp = true;
    }

    void OnDestroy()
    {
        //GameObject[] gam = FindObjectsOfType<GameObject>();

        ////looks for every gameobject with door in the name and loads it into a list
        //foreach (GameObject gameObj in gam)
        //{
        //    if (gameObj.name.Contains("big"))
        //    {
        //        print("AHH");
        //        //load all the doors into a list
        //        gameObj.GetComponent<Transform>().localScale = new Vector3(17f, 1, 1);
        //    }
        //    if (gameObj.name.Contains("small"))
        //    {
        //        //load all the doors into a list
        //        gameObj.GetComponent<Transform>().localScale = new Vector3(11f, 1, 1);
        //    }
        //}
    }


    IEnumerator bulletCollisionTimer()
    {
        yield return new WaitForSecondsRealtime(0);
        GetComponent<Collider2D>().isTrigger = false;
    }

        //void OnDestroy()
        //{
        //    player.GetComponent<fireBullet>().notInAOE = true;
        //}
    }