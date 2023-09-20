using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TipsScenarioController : MonoBehaviour
{
    private Database database;

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

    }
}
