using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float maxTimeBetweenSpawns;
    public float minTImeBetweenSpawns;

    public float widthOfPlaySpace;
    public float heightOfPlaySpace;

    public float astroidTravelSPeed;

    public GameObject astroidPrefab;

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
    }

    // Update is called once per frame
    void Update()
    {
        CheckForAndSpawn();
    }

    public void CheckForAndSpawn()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= currentTimeBetweenSpawns)
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

    public void spawnRandomAstroid()
    {
        ColoredEntity.EColor newColor = (ColoredEntity.EColor)Random.Range(0, numPlayers + 1);
        Vector3 lastSpawn = Vector3.zero;


        //debug for testing
        //newColor = ColoredEntity.EColor.Red;

        switch(newColor)
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
        GameObject newAstroid = Instantiate(astroidPrefab, new Vector3(Random.Range(leftSpawnLine,rightSpawnLine), heightOfPlaySpace, 0), Quaternion.identity);
        newAstroid.GetComponent<Astroid>().ReColor(newColor);
        newAstroid.GetComponent<Rigidbody>().velocity = GenerateRandomDirectionToGround(newAstroid.transform.position) * astroidTravelSPeed;

        lastSpawn = newAstroid.transform.position;

        sumSpawn += lastSpawn;
        ++numberOfSpawns;
        if(numberOfSpawns > dificultySpikeThresh)
        {
            minTImeBetweenSpawns *= .9f;
            maxTimeBetweenSpawns *= .9f;
            dificultySpikeThresh += 2;
            numberOfSpawns = 0;
        }
        //Debug.Log("Avg: " + sumSpawn / numberOfSpawns);

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
                lastSpawn = new Vector3(0, heightOfPlaySpace, 0);
                break;
        }


        currentTimeBetweenSpawns = Random.Range(minTImeBetweenSpawns, maxTimeBetweenSpawns);
        timeSinceLastSpawn = 0;
    }
}
