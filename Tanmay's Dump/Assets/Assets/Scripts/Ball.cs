using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball : MonoBehaviour
{

    Cannon cannon;
    BottomBlock bottomBlock;

    private void Awake()
    {   
        //Find the Cannon instance in the Scene 

        cannon = FindObjectOfType<Cannon>(); //Eventually we provide this info when the Ball is instantiated by the Cannon
        bottomBlock = FindObjectOfType<BottomBlock>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (transform.position.y < bottomBlock.transform.position.y)
        {
            cannon.BallCollision(transform.position);
            Destroy(this.gameObject);
        }
    }

    



















}