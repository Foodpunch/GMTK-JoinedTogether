using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float moveSpeed =5f;
    Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Vector2 desiredVelocity = (PlayerScript.instance.transform.position - transform.position).normalized * moveSpeed;
        //Vector2 steer = desiredVelocity - _rb.velocity;
        //if (steer.sqrMagnitude > (moveSpeed * moveSpeed))
        //{
        //    steer = desiredVelocity.normalized * moveSpeed;
        //}
        //_rb.velocity = steer;
    }
    public void Move()
    {
        transform.up = (_rb.velocity).normalized;
        _rb.velocity = transform.up * moveSpeed;
    }

}
