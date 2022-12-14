using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject victoryPanelGO;
    public GameObject losePanelGO;

    public GameObject recallButtonGO;
    
    public GameObject countDownPanelGO;
    TextMeshProUGUI countDownText;

    public int countDownNumber;

    public TextMeshProUGUI levelNumberText;
    public TextMeshProUGUI roundNumbertext;

    private void Awake()
    {
        countDownNumber = 3;
        countDownPanelGO.SetActive(true);
        countDownText = countDownPanelGO.GetComponentInChildren<TextMeshProUGUI>();
    }


    public void MakeUIObjectActive(GameObject uiObject)
    {
        uiObject.SetActive(true);
    }

    public void MakeUIObjectInactive(GameObject uiObject)
    {
        uiObject.SetActive(false);
    }

    public void DoOnRecallButton()
    {
        
    }

    public IEnumerator CountDown()
    {
        while (countDownNumber > 0)
        {
            countDownText.text = countDownNumber.ToString();
            yield return new WaitForSeconds(1f);
            countDownNumber--;
        }

        MakeUIObjectInactive(countDownPanelGO);
    }

    public void SetRoundNumber(int roundNumber)
    {
        roundNumbertext.text = "Round - " + roundNumber.ToString();
    }

    public void SetLevelNumber(int levelNumber)
    {
        levelNumberText.text = "Level - " + levelNumber.ToString();
    }
}
