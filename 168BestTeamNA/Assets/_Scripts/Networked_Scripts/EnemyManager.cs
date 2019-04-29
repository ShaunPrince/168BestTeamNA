using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class EnemyManager : NetworkBehaviour
{
    /*
     *  Sync when an asteroid is spawned
     *      sync location, angle, speed, color
     *  Sync when an asteroid is hit (to get rid of it for all players)
     *  
     *  The rest should technically be deterministic, but shoot me in the foot when that proves wrong 
     */
    public float maxTimeBetweenSpawns;
    public float minTImeBetweenSpawns;

    public float widthOfPlaySpace;
    public float heightOfPlaySpace;

    public float astroidTravelSPeed;

    public GameObject astroidPrefab;

    // game manager
    public gameManager gameManager;

    private float timeSinceLastSpawn;
    private float currentTimeBetweenSpawns;

    public int numPlayers;

    private Vector3 lastRedSpawnCoord;
    private Vector3 lastBlueSpawnCoord;
    private Vector3 lastGreenSpawnCoord;
    private Vector3 lastYellowSpawnCoord;

    private Vector3 sumSpawn;
    private int numberOfSpawns;
    public int dificultySpikeThresh;

    public float graySpawnThresh;


    // Start is called before the first frame update
    void Start()
    {
        currentTimeBetweenSpawns = Random.Range(minTImeBetweenSpawns, maxTimeBetweenSpawns);
        timeSinceLastSpawn = 0;
        lastRedSpawnCoord = new Vector3(0, heightOfPlaySpace, 0);
        lastBlueSpawnCoord = new Vector3(0, heightOfPlaySpace, 0);
        lastGreenSpawnCoord = new Vector3(0, heightOfPlaySpace, 0);
        lastYellowSpawnCoord = new Vector3(0, heightOfPlaySpace, 0);

        sumSpawn = new Vector3(0, heightOfPlaySpace, 0);
        numberOfSpawns = 1;

        gameManager = GameObject.FindWithTag("GameManager").GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the game has started, start spawning enemies
        if (gameManager.gameStarted())
        {
            CheckForAndSpawn();
        }

    }

    public void setNumPlayers(int num)
    {
        numPlayers = num;
    }

    public void CheckForAndSpawn()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= currentTimeBetweenSpawns)
        {
            spawnRandomAstroid();
        }
        else
        {
            timeSinceLastSpawn += Time.deltaTime;
        }


    }

    private Vector3 GenerateRandomDirectionToGround(Vector3 curPosition)
    {
        //Debug.Log(curPosition);
        Vector3 dest = this.transform.TransformPoint(new Vector3((Random.Range(-1f, 1f) * widthOfPlaySpace / 2), 0, 0));
        //Debug.Log(dest);
        //Debug.Log(dest - curPosition);
        //Debug.DrawRay(curPosition, (dest - curPosition) * 100, Color.blue, 10);
        return (dest - curPosition);
    }

    [Server]
    public void spawnRandomAstroid()
    {
        ColoredEntity.EColor newColor = (ColoredEntity.EColor)Random.Range(1, numPlayers + 1);

        if (Random.Range(0f, 1f) <= graySpawnThresh)
        {
            newColor = ColoredEntity.EColor.Gray;
        }

        Vector3 lastSpawn = Vector3.zero;


        //debug for testing
        //newColor = ColoredEntity.EColor.Red;

        switch (newColor)
        {
            case ColoredEntity.EColor.Red:
                lastSpawn = lastRedSpawnCoord;
                break;
            case ColoredEntity.EColor.Blue:
                lastSpawn = lastBlueSpawnCoord;
                break;
            case ColoredEntity.EColor.Green:
                lastSpawn = lastGreenSpawnCoord;
                break;
            case ColoredEntity.EColor.Yellow:
                lastSpawn = lastYellowSpawnCoord;
                break;
            case ColoredEntity.EColor.Gray:
                lastSpawn = new Vector3(0, heightOfPlaySpace, 0);
                break;
        }

        //Spawn an astroid at the top of the playspace and somewhere between the horizontal boundries
        float leftSpawnLine = Mathf.Max(-widthOfPlaySpace / 2, lastSpawn.x - widthOfPlaySpace / 2);
        float rightSpawnLine = Mathf.Min(widthOfPlaySpace / 2, lastSpawn.x + widthOfPlaySpace / 2);
        GameObject newAstroid = Instantiate(astroidPrefab, new Vector3(Random.Range(leftSpawnLine, rightSpawnLine), heightOfPlaySpace, 0), Quaternion.identity);
        newAstroid.GetComponent<Astroid>().ReColor(newColor);
        newAstroid.GetComponent<Rigidbody>().velocity = GenerateRandomDirectionToGround(newAstroid.transform.position) * astroidTravelSPeed;

        lastSpawn = newAstroid.transform.position;

        sumSpawn += lastSpawn;
        ++numberOfSpawns;
        if (numberOfSpawns > dificultySpikeThresh)
        {
            minTImeBetweenSpawns *= .9f;
            maxTimeBetweenSpawns *= .9f;
            dificultySpikeThresh += 2;
            numberOfSpawns = 0;
        }
        //Debug.Log("Avg: " + sumSpawn / numberOfSpawns);
        Debug.Log("Spawned");

        switch (newColor)
        {
            case ColoredEntity.EColor.Red:
                lastRedSpawnCoord = lastSpawn;
                break;
            case ColoredEntity.EColor.Blue:
                lastBlueSpawnCoord = lastSpawn;
                break;
            case ColoredEntity.EColor.Green:
                lastGreenSpawnCoord = lastSpawn;
                break;
            case ColoredEntity.EColor.Yellow:
                lastYellowSpawnCoord = lastSpawn;
                break;
            case ColoredEntity.EColor.Gray:
                newAstroid.transform.localScale = new Vector3(3, 3, 3);
                newAstroid.GetComponent<DamagableEntity>().health = 3;
                newAstroid.GetComponent<Astroid>().damage = 15;
                lastSpawn = new Vector3(0, heightOfPlaySpace, 0);
                break;
        }

        NetworkServer.Spawn(newAstroid);

        currentTimeBetweenSpawns = Random.Range(minTImeBetweenSpawns, maxTimeBetweenSpawns);
        timeSinceLastSpawn = 0;
    }
}
