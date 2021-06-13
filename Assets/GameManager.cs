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

    public Vector2 lastPos;
    bool hasSpawned;
    int maxFlockCount;
    public GameObject track;
    public List<GameObject> FlockList;
    public List<GameObject> Enemies;
    public List<GameObject> Obstacles;

    Canvas UICanvas;
    // Start is called before the first frame update
    void Start()
    {
        UICanvas = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        //  SpawnFlock();
        //Debug.Log(Camera.main.WorldToScreenPoint(track.transform.position));
    }
    [Button]
    void SpawnFlock()
    {
        Vector2 screenCentre = new Vector2(Screen.width / 2, Screen.height / 2);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        Instantiate(track, Camera.main.ScreenToWorldPoint(screenCentre), Quaternion.identity);
        if (!hasSpawned)
        {
            for(int i=0; i< maxFlockCount; i++)
            {
               
            }
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
