using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public enum LevelNumber
    {
        LEVEL1 = 1,
        LEVEL2 = 2,
        LEVEL3 = 3,
        LEVEL4 = 4,
        LEVEL5 = 5,
        LEVEL6 = 6
    }

    public GameObject mainMenuPanel;
    public GameObject levelSelectPanel;

    public void SetMainMenuActive()
    {
        mainMenuPanel.SetActive(true);
        levelSelectPanel.SetActive(false);
    }

    public void SetLevelSelectActive()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }


    public void SwitchLevel(int levelNumber)
    {
        switch ((LevelNumber) levelNumber)
        {
            case LevelNumber.LEVEL1:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
                break;

            case LevelNumber.LEVEL2:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2, LoadSceneMode.Single);
                break;

            case LevelNumber.LEVEL3:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3, LoadSceneMode.Single);
                break;

            case LevelNumber.LEVEL4:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4, LoadSceneMode.Single);
                break;

            case LevelNumber.LEVEL5:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5, LoadSceneMode.Single);
                break;

            case LevelNumber.LEVEL6:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 6, LoadSceneMode.Single);
                break;

        }
    }
}
