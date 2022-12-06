using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public int maxBallCount = 10; //Set the maximum number of balls in this level, edit in Inspector for a different value
    
    public List<GameObject> ballsList;
    public GameObject ballPrefabGO; //Reference to the ball prefab

    private void Awake()
    {
        ballsList = new List<GameObject>(maxBallCount);//Create an empty list of the same size as the number of balls

        for (int i = 0; i < maxBallCount; i++) 
        {
            GameObject initialBallSpawn = Instantiate(ballPrefabGO); //Spawn the ball gameobject
            initialBallSpawn.SetActive(false); //Make the spawned balls invisible
            ballsList.Add(initialBallSpawn); //Add the ball GO to the ballsList
        }
    }

    /// <summary>
    /// Fetch an inactive ball from the Balls List located in the PoolManager, set it active 
    /// and do something with it
    /// </summary>
    /// <returns></returns>
    public GameObject FetchBallFromList()
    {
        if (ballsList.Count > 0 /*to check if the list is not empty*/)
        {
            foreach (GameObject balls in ballsList)
            {
                if (!balls.activeInHierarchy)
                {
                    balls.SetActive(true);
                    return balls;
                }
            }
        }

        return null;
    }
}
