using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public BirdManager playerBirdManager;
    public GameObject BirdBulletPrefab;

    Vector3 mouseInput;
    Vector3 mouseDir;
    // Start is called before the first frame update
    void Start()
    {
        playerBirdManager = playerBirdManager.GetComponent<BirdManager>();
    }

    // Update is called once per frame
    void Update()
    {
        SetDirectionToMouse();
        CheckBirds();
        if (Input.GetMouseButtonDown(0))
        {
            
            if (playerBirdManager.birdList.Count > 0)
            {
                DeductBird();
                ShootBird();
            } 
        }
    }
    void CheckBirds()
    {
        if (playerBirdManager.birdList.Count == 0) return;
        for (int i=0; i< playerBirdManager.birdList.Count; i++)
        {
            if (!playerBirdManager.birdList[i].gameObject.activeInHierarchy)
                playerBirdManager.birdList.RemoveAt(i);
        }
    }
    void DeductBird()
    {
        playerBirdManager.birdList[playerBirdManager.birdList.Count - 1].gameObject.SetActive(false);
        playerBirdManager.birdList.RemoveAt(playerBirdManager.birdList.Count - 1);
    }
    void ShootBird()
    {
        AudioManager.instance.PlaySound(AudioManager.SoundType.SHOOTBIRD, transform.position);
        GameObject bulletClone = Instantiate(BirdBulletPrefab, transform.position, Quaternion.identity);
        bulletClone.GetComponent<Rigidbody2D>().AddForce(transform.up * 15f,ForceMode2D.Impulse);
    }
    void SetDirectionToMouse()
    {
        mouseInput = Input.mousePosition;       //mouse pos in pixel
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(mouseInput);         //convert to world pos
        mousePosWorld.z = 0;                                                        //make sure that z is 0 cos 2D
        mouseDir = mousePosWorld - transform.position;                              //get the direction, pos to player
        transform.up = mouseDir;
    }
}
