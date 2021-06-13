using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    float shootTIme =2f;
    public GameObject Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > shootTIme)
        {
            GameObject bulletClone = Instantiate(Bullet, transform.position, Quaternion.identity);
            bulletClone.GetComponent<Rigidbody2D>().AddForce(transform.GetChild(0).transform.up * 15f,ForceMode2D.Impulse);
            AudioManager.instance.PlaySound(AudioManager.SoundType.ROCKETWHIZZ,transform.position);
            shootTIme += Time.time;
        }
    }
    
}
