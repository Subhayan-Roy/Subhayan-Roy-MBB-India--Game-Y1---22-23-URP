using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class BrickBase : MonoBehaviour
{
    [Header("Brick HP")]
    public float maxHp; //This will contain the maxmum HP a brick can have.

    [Header("Brick Sprites")]
    //public Sprite fullHPSprite; //This will contain the full hp sprite
    public Sprite halfHPSprite; //This will contain the half hp sprite

    bool spriteChange; //this is a boolean to keep track of the sprite change

    float currentHp; //a float to store current HP
    float minCurrentHp = 0; //
    int ballDamage = 1; //an integer to store ball Damage

    protected GameManager gameManager; //Access the Game Manager component
    SpriteRenderer spriteRenderer; //Access the Sprite Renderer component
    TextMeshPro brickText; //Access the brick text game object

    protected virtual void Awake()
    {
        brickText = GetComponentInChildren<TextMeshPro>();  //Find and populate the brick text component
        spriteRenderer = GetComponent<SpriteRenderer>();    //Find and populate the Sprite Renderer component
        currentHp = maxHp;  //Set current hp to max hp
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); //Find and populate the game manager script
        gameManager.BrickIsBorn(); //Call the brick is born function from Game Manager to count the number of bricks
        brickText.text = currentHp.ToString(); //Convert current hp to string and set it to the brick text component
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //We check if the colliding object is a ball
        Ball collidingBall = collision.gameObject.GetComponent<Ball>();
        //returns a reference to the Ball component of the incoming game object
        //(returns null if there is no Ball component)

        if (collidingBall != null) //if collidingBall is not a null object...
        {
            Damage(ballDamage);
        }
    }

    public void Damage(int damage)
    {
        currentHp -= damage;

        if (currentHp >= 0)
        {
            brickText.text = currentHp.ToString();
        }
        else
        {
            brickText.text = minCurrentHp.ToString();
        }


        if (halfHPSprite != null)
        {
            if (currentHp <= maxHp / 2 && !spriteChange)
            {
                spriteChange = true;
                spriteRenderer.sprite = halfHPSprite;
            }
        }
        

        if (currentHp <= 0)
        {
            Dead();
        }
    }

    protected abstract void Dead();
}

