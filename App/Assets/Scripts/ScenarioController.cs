using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    private Database database;
    public Text helpText;
    public GameObject helpBalloon;
    private int situationID;
    private char opAttempts;
    private string allOpsChosen;
    private string situationName = "restaurante";

    void Start()
    {
        helpText.enabled = false;
        helpBalloon.SetActive(false);

        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);
        allOpsChosen = database.GetSituationOptions(situationName);
        if(situationID >= allOpsChosen.Length)
        {
            situationID -= 1;
        }
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(sceneName:"MenuScene");
    }

    public void OnClickRestaurantButton()
    {
        
        opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];

        if (situationID >= (allOpsChosen.Length-1))
        {
            if(JSONReader.isCorrectOp || opAttempts >= '2')
            {
                ResetScenario();
            }
        }
        SceneManager.LoadScene(sceneName:"RestaurantScene");
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

    private void ResetScenario()
    {
        char[] emptyOps = allOpsChosen.ToCharArray();

        for (int i = 0; i < emptyOps.Length; i++)
        {
            emptyOps[i] = '0';
        }
        string emptyOpsStr = new string(emptyOps);

        database.SetSituationNumber(situationName, 0);
        database.SetSituationOptions(situationName, emptyOpsStr);
        database.SetSituationOpsAttempts(situationName, emptyOpsStr);
        database.deleteTableReport();
    }
}
