using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D _rb;
    public SpriteRenderer _sr;
    public Sprite[] Sprites;
    public float moveSpeed =5f;
    public float movespeedMult = 1f;
    //public int playerBirdCount;
    public BirdManager playerBirdManager;
    public static PlayerScript instance;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerBirdManager = GetComponent<BirdManager>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
       // SetSpriteDirection();
    }
    void MovementInput()
    {
        float x = Input.GetAxis("Horizontal")*moveSpeed*movespeedMult;
        float y = Input.GetAxis("Vertical")*moveSpeed*movespeedMult;
        _rb.velocity = new Vector2(x, y);
    }
    void SetSpriteDirection() //Smooth this later!
    {
        if (_rb.velocity.x > 0)
        {
            _sr.sprite = Sprites[1];
        }
        else if (_rb.velocity.x < 0)
        {
            _sr.sprite = Sprites[2];
        }
        else
            _sr.sprite = Sprites[0];

        _sr.transform.up = Vector2.Lerp(_sr.transform.up, _rb.velocity,.05f);
    }
}
