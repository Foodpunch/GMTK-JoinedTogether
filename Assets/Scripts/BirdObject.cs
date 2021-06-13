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


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _col = GetComponent<Collider2D>();
    }


    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosion, transform.position, transform.rotation);
        CameraManager.instance.Shake(0.15f);
        //gameObject.transform.GetComponentInParent<BirdManager>().
    }
}
