using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public GameObject brokenCage;
    public GameObject flockPrefab;


    // Start is called before the first frame update
    void Start()
    {
      //  AudioManager.instance.PlaySound(AudioManager.SoundType.BIRDCAGE, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag =="Bullet")
        {
            CageBreak();
        }
    }
    public void CageBreak()
    {
        gameObject.SetActive(false);
        Instantiate(flockPrefab, transform.position, Quaternion.identity);
        Instantiate(brokenCage, transform.position, Quaternion.identity);
        AudioManager.instance.PlaySound(AudioManager.SoundType.CAGEBREAK, transform.position);
    }
}
