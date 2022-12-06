using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int hp;

    private GameManager gameManager;

    private void Awake()
    {  
        //Find the GameManager instance in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {   
        //The Brick informs the Game Manager that a brick is in the scene
        gameManager.BrickIsBorn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //returns a reference to the ball component of the incoming object (returns null if there is no Ball component)
        Ball collidingBall = collision.gameObject.GetComponent<Ball>(); 

        if (collidingBall != null) //if colliding Ball is not a null object...
        {
            Damage(); 
        }
    }

    private void Damage()
    {
        hp--;
        
        if (hp <= 0)
        {
            Die();
            
        }


    }

    private void Die()
    {
        gameManager.BrickHasDied();
        //instantiates destruction fx
        Destroy(gameObject);
    }
}
