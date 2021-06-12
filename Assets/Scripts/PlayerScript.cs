using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rb;
    public SpriteRenderer _sr;
    public float moveSpeed =5f;
    public float movespeedMult = 1f;
    public static PlayerScript instance;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
        SetSpriteDirection();
    }
    void MovementInput()
    {
        float x = Input.GetAxis("Horizontal")*moveSpeed*movespeedMult;
        float y = Input.GetAxis("Vertical")*moveSpeed*movespeedMult;
        _rb.velocity = new Vector2(x, y);
    }
    void SetSpriteDirection() //Smooth this later!
    {
        _sr.gameObject.transform.up = _rb.velocity;
    }
}
