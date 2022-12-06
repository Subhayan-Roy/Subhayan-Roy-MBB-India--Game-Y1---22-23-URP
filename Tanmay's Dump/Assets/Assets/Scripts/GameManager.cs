using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Cannon cannon;
    public enum GameStatesDATA //Define a custom data type which can only accept the values in the following code block (code block is curly bracket start and end)
    {   
        GAMESTART,
        PREPARATION,
        WAIT,
        ACTION,
        LEVELVICTORY,
        LEVELDEFEAT,
        GAMEVICTORY
    }

    public GameStatesDATA gameStatesVAR; //Make a public variable out of our custom Data Type, GameStateDATA

     InputManager inputManager;

    public float gameStartTime = 3f;

    private void Awake()
    {
        
            gameStatesVAR = GameStatesDATA.GAMESTART; //Make sure the default state is set to GameStart state or as soon as the game is launched the gameStatesVAR is set to the GameStart state

            inputManager = GetComponent<InputManager>(); //Find a gameobject in the hierarchy which has the script InputManager; Access the script InputManager script component attached to the Managers game Object.
                                                         //FindObjectOfType would work if the one of the scripts was on another game object (scripts is also called a component)

        cannon = FindObjectOfType<Cannon>();
    }

    private void Start()
    {
        StartCoroutine(StartGameCountDown());
    }

    private void Update()
    {
        switch (gameStatesVAR)
        {
            case GameStatesDATA.GAMESTART:
                if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
                {
                    inputManager.DisableInputManager(); //then disable is to make sure that it runs only ONCE.
                }
                break;


            case GameStatesDATA.PREPARATION:
                if (!inputManager.IsInputManagerActive() /*if input manager is disabled*/)
                {   
                    cannon.ResetCannon(); 
                    inputManager.EnableInputManager();//then enable it to make sure it runs only ONCE 
                }
                break;

            case GameStatesDATA.WAIT:
                break;

            case GameStatesDATA.ACTION:
                if (inputManager.IsInputManagerActive() /*if input manager is enabled*/)
                {
                    inputManager.DisableInputManager(); //then disable is to make sure that it runs only ONCE.
                }
                break;

            case GameStatesDATA.LEVELVICTORY:
                break;

            case GameStatesDATA.LEVELDEFEAT:
                break;

            case GameStatesDATA.GAMEVICTORY:
                break;
        }
    }
    public void BrickIsBorn()
    {
      
    }

    public void BrickHasDied()
    {

    }

    public void ChangeGameState(GameStatesDATA gamestate)
    {
        gameStatesVAR = gamestate;
    }
      
    /// <summary>
    /// Activates a timer at the beginning of the level before the start of the game. Define a function of type IENumerator 
    /// </summary>
    IEnumerator StartGameCountDown()
    {
       
        yield return new WaitForSeconds(gameStartTime); //This is the syntax to remember. Put the time you want to wait for within the parantheses 
       
        gameStatesVAR = GameStatesDATA.PREPARATION; //Change the state of the game manager 

    }
}
