using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBullet : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D _rb;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosion, transform.position, transform.rotation);
        CameraManager.instance.Shake(0.25f);
    }
}
