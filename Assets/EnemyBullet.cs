using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject explosion;
    SpriteRenderer _sr;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _sr =GetComponentInChildren<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _sr.transform.up = _rb.velocity.normalized;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
