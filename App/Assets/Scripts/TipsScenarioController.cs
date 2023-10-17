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
    private AudioSource audioSource;
    public AudioClip buttonSound;

    void Start()
    {
        helpText.enabled = false;
        helpBalloon.SetActive(false);

        audioSource = GetComponent<AudioSource>();
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
        audioSource.PlayOneShot(buttonSound);

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
