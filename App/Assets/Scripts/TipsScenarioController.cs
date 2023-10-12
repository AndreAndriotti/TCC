using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TipsScenarioController : MonoBehaviour
{
    private Database database;
    public Text helpText;
    public GameObject helpBalloon;

    void Start()
    {
        helpText.enabled = false;
        helpBalloon.SetActive(false);
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(sceneName:"MenuScene");
    }

    public void OnClickRestaurantButton()
    {
        SceneManager.LoadScene(sceneName:"TipsScene");
    }

    public void OnClickHelpButton()
    {
        if(helpText.enabled)
        {
            helpText.enabled = false;
            helpBalloon.SetActive(false);
        }
        else
        {
            helpText.enabled = true;
            helpBalloon.SetActive(true);
        }
    }
}
