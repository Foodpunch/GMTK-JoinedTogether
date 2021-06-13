using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class BirdObject : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rb;
    Collider2D _col;
    public Collider2D BirdCollider { get { return _col; } }
    public GameObject explosion;

    float t;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }


    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
        if(t<Time.time)
        {
            t = Time.time + Random.Range(3f, 8f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosion, transform.position, transform.rotation);
        CameraManager.instance.Shake(0.15f);
        //gameObject.transform.GetComponentInParent<BirdManager>().
    }
}
