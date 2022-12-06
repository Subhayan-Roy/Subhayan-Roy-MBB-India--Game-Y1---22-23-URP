using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject aimLineGO; //references to the Aim Line child GO (game object)
    public GameObject ballPrefabGO; //Reference to the ball prefab 

    public float ballSpeed = 10f; //Set the speed of the ball, edit in Inspector for a different value
    public int maxBallCount = 10; //Set the maximum number of balls in this level, edit in Inspector fr a different value 

    public float timeGapBetweenBalls = 0.1f; //Set the time gap between the balls, edit in Inspector fr a different value 

    int ballCollisionCounter;

    GameManager gameManager;
    private void Awake()
    {
        aimLineGO.SetActive(false);
    }
    public void DoOnButtonDown()
    {
        //Make aimline visible
        aimLineGO.SetActive(true);
        gameManager = FindObjectOfType<GameManager>();

    }

    public void DoOnButtonHold(float angleOfRotation)
    {   
        //Set the angle of the rotation of the transform to the angle calculator 
        transform.rotation = Quaternion.Euler(0f, 0f, -angleOfRotation);
       
    }


    public void DoOnButtonUp()
    {
        //Make aimline dissapear
        aimLineGO.SetActive(false);
        
    }


    public void ReleaseBallsWithGap()
    {
        StartCoroutine(ReleaseThyBalls());
    }

    public IEnumerator ReleaseThyBalls()
    {
       

        for (int i = 0; i<maxBallCount; i++)
        {
            //Instantiate(Gameobject or the blueprint to be spawned, where to spawn it, at what angle to be spawned)
            GameObject spawnedBall = Instantiate(ballPrefabGO, transform.position, Quaternion.identity) as GameObject;
            spawnedBall.GetComponent<Rigidbody2D>().AddForce(transform.up * ballSpeed, ForceMode2D.Impulse);//transform.up is the direction on y axis 

            yield return new WaitForSeconds(timeGapBetweenBalls);
        }



    }

    // Start is called before the first frame update
    void Start()
    {
        ResetCannon();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetCannon()
    {
        ballCollisionCounter = maxBallCount;
    }
    
    public void BallCollision(Vector2 pos)
    {
        ballCollisionCounter -= 1; //ballCollisionCounter = ballCollisionCounter - 1 //ballCollisionCounter--

        if (ballCollisionCounter <= 0)
        {
            gameManager.ChangeGameState(GameManager.GameStatesDATA.PREPARATION);
        }
    }

    public void DoOnPickupExtraBall()
    {

    }
}
