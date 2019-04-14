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

    // Start is called before the first frame update
    void Start()
    {
        currentTimeBetweenSpawns = Random.Range(minTImeBetweenSpawns, maxTimeBetweenSpawns);
        timeSinceLastSpawn = 0;
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
            //Spawn an astroid at the top of the playspace and somewhere between the horizontal boundries
            GameObject newAstroid = Instantiate(astroidPrefab,new Vector3(Random.Range(-1f,1f) * widthOfPlaySpace/2, heightOfPlaySpace, 0),Quaternion.identity);
            newAstroid.GetComponent<Rigidbody>().velocity = GenerateRandomDirectionToGround(newAstroid.transform.position) * astroidTravelSPeed;
            currentTimeBetweenSpawns = Random.Range(minTImeBetweenSpawns, maxTimeBetweenSpawns);
            timeSinceLastSpawn = 0;
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
        Debug.DrawRay(curPosition, (dest - curPosition) * 100, Color.blue, 10);
        return (dest - curPosition); 
    }
}
