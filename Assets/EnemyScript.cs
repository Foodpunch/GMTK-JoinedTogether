using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int health = 2;
    public float moveSpeed =5f;
    public float rotationSpeed;
    public float disappearDistance = 30f;
    public float stoppingDistance = 15f;
    Rigidbody2D _rb;
    SpriteRenderer _sr;
    Vector2 desiredDir;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

     //   transform.up = (_rb.velocity).normalized;
        desiredDir = PlayerScript.instance.transform.position - transform.position;
        float sqrDist = desiredDir.sqrMagnitude;
        if(sqrDist > (disappearDistance*disappearDistance))
        {
            gameObject.SetActive(false);
        }
        float stopDist = Map(PlayerScript.instance.playerBirdManager.birdList.Count, stoppingDistance, 100f, 0, 50);
        float f = Map(sqrDist, 0, moveSpeed, stopDist,disappearDistance);
        _rb.velocity = (desiredDir.normalized) * f;

        if(_rb.velocity.sqrMagnitude >0)
        {
            _sr.transform.up = _rb.velocity;
        }
    }
    public void Move()
    {
         _rb.velocity = (desiredDir.normalized) * moveSpeed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Bullet")
        {
            if(health <=1)
            {
                //Die
                gameObject.SetActive(false);
                Instantiate(explosion, transform.position, Quaternion.identity);
                AudioManager.instance.PlaySound(AudioManager.SoundType.ENEMYDIE, transform.position);
            }
            health--;
            //player hurt sound here
        }
    }
    public float Map(float value, float from, float to, float from2, float to2)
    {
        if (value <= from2)
        {
            return from;
        }
        else if (value >= to2)
        {
            return to;
        }
        else
        {
            return (to - from) * ((value - from2) / (to2 - from2)) + from;
        }
    }

}
