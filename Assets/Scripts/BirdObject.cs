using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObject : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D _rb;
    SpriteRenderer _sr;

    
    public bool isCollected;
    public Transform postionToTrack;

    Vector2 dirToTarget;
    Vector2 desiredVelocity;
    public float moveSpeed = 1f;



    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity += (Vector2)transform.right * moveSpeed;
        SeekBehaviour();
    }
    void SeekBehaviour()
    {
        dirToTarget = postionToTrack.position-transform.position;
        dirToTarget.Normalize();
        desiredVelocity = dirToTarget * moveSpeed;

        Vector2 steer = desiredVelocity-_rb.velocity;
        _rb.velocity += steer;
    }

}
