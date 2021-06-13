using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject FlockPrefab;
    public GameObject Enemy1Prefab;
    public GameObject Enemy2Prefab;
    public GameObject[] AsteroidPrefabs;    //scale is default 2

    public GameObject EndPoint;

    public Vector2 lastPos;
    int maxFlockCount;


    public GameObject track;
    int obstacleCount;
    int flockCount;
    int enemyCount;

    float obstacleSpawnTime;
    float enemySpawnTime;
    float flockSpawnTime;
    float nextSpawnTime = 5f;

    bool endSpawned;
    // Start is called before the first frame update
    void Start()
    {
        Spawn(Enemy2Prefab);
        Spawn(Enemy1Prefab);
        for (int i = 0; i< 6; i++)
        {
            SpawnObstacle();
            SpawnFlock();
            obstacleCount++;
            flockCount++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //  SpawnFlock();
        //Debug.Log(Camera.main.WorldToScreenPoint(track.transform.position));
        if (Time.time >= obstacleSpawnTime && obstacleCount < 35)
        {
            obstacleSpawnTime += Time.time + Random.Range(nextSpawnTime, nextSpawnTime * 2);
            SpawnObstacle();
        }
        if(Time.time >= enemySpawnTime)
        {
            enemySpawnTime += Time.time + Random.Range(nextSpawnTime, nextSpawnTime * 2);
            int rand = Random.Range(0, 101);
            if(rand < 30)
            {
                Spawn(Enemy1Prefab);
            }
            else
            {
                Spawn(Enemy2Prefab);
            }
        }
        if(Time.time >= flockSpawnTime && flockCount<11)
        {
            flockSpawnTime = Time.time + 5f;
            SpawnFlock();
            flockCount++;
        }
        if(!endSpawned)
        {
            SpawnEndPoint(EndPoint);
        }
    }
    [Button]
    void SpawnFlock()
    {
        if(Time.time >= obstacleSpawnTime)
        {
            obstacleSpawnTime += Time.time+ Random.Range(nextSpawnTime, nextSpawnTime * 2);


        }
        Spawn(FlockPrefab);

    }
    [Button]
    void SpawnEnemy()
    {
        Spawn(Enemy2Prefab);
    }

    [Button]
    void SpawnObstacle()
    {
        int randIndex = Random.Range(0, AsteroidPrefabs.Length);
        Spawn(AsteroidPrefabs[randIndex],true);
    }
    void Spawn(GameObject objToSpawn, bool randomRot = false)
    {
        //float xMult = Random.Range(0, 2) * 2 - 1;
        //Debug.Log(xMult);
        //float yMult = Random.Range(0, 2) * 2 - 1;
        //float xOffset = 10.7f;
        //float yOffset = 8f;
        float offset = Random.Range(0, 25);
        Vector2 playerPos = PlayerScript.instance.transform.position;
        Vector2 rand = Random.insideUnitCircle * 30f + playerPos;
        if(!((rand.x > playerPos.x-12f && rand.x < playerPos.x+12f) && (rand.y > playerPos.y- 10f && rand.y < playerPos.y + 10f)))
        {
            if (!randomRot)
                Instantiate(objToSpawn, rand, Quaternion.identity);
            else
                Instantiate(objToSpawn, rand, Quaternion.Euler(0, 0, Random.Range(0,359)));
        }
        else
        {
   
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void SpawnEndPoint(GameObject objToSpawn)
    {
        //float xMult = Random.Range(0, 2) * 2 - 1;
        //Debug.Log(xMult);
        //float yMult = Random.Range(0, 2) * 2 - 1;
        //float xOffset = 10.7f;
        //float yOffset = 8f;
        Vector2 playerPos = PlayerScript.instance.transform.position;
        Vector2 rand = Random.insideUnitCircle * 180f + playerPos;
        if (!((rand.x > playerPos.x - 12f && rand.x < playerPos.x + 12f) && (rand.y > playerPos.y - 10f && rand.y < playerPos.y + 10f)))
        {
            Instantiate(objToSpawn, rand, Quaternion.identity);
            endSpawned = true;
        }
        else
        {

        }
    }

}
public static class ExtendingVector2
{
    public static bool IsLesserThan(this Vector2 local, Vector2 other)
    {
        if (local.x <= other.x && local.y <= other.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsLesserThan(this Vector2Int local, Vector2Int other)
    {
        if (local.x <= other.x && local.y <= other.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool IsMoreThan(this Vector2Int local, Vector2Int other)
    {
        if (local.x >= other.x && local.y >= other.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool DifferenceIsMoreThan(this Vector2 local, Vector2 other, float diff)
    {
        float x = local.x - other.x;
        float y = local.y - other.y;
        Mathf.Abs(x);
        Mathf.Abs(y);
        if(x>diff && y> diff)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  
}
