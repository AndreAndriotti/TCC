using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackController : MonoBehaviour
{
    public GameObject canvas;
    private Database database;
    private string situationName;
    private int situationID;
    private char opOK;
    private char opAttempts;
    private char opChosen;
    private string allOpsChosen;
    //private bool isCorrectOp;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationName = "restaurante";
        situationID = database.GetSituationNumber(situationName);
        //isCorrectOp = canvas.GetComponent<JSONReader>().isCorrectOp;
        opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];
        allOpsChosen = database.GetSituationOptions(situationName);
        opChosen = allOpsChosen[situationID];
    }

    public void OnClickContinueButton()
    {
        /*if (situationID >= (allOpsChosen.Length-1))
        {
            if(JSONReader.isCorrectOp || opAttempts == '2')
            {
                // ENVIAR RELATORIO() <---- CARLOS
                ResetScenario();
                SceneManager.LoadScene(sceneName:"ScenarioCompletedScene");
            }
        }
        
        if(JSONReader.isCorrectOp || opAttempts == '2')
        {
                database.UpdateSituation(situationName);
        }
        SceneManager.LoadScene(sceneName:"RestaurantScene");*/

        if(JSONReader.isCorrectOp || opAttempts == '2')
        {
            if (situationID >= (allOpsChosen.Length-1))
            {
                // ENVIAR RELATORIO() <---- CARLOS
                ResetScenario();
                SceneManager.LoadScene(sceneName:"ScenarioCompletedScene");
            }
            else 
            {
                database.UpdateSituation(situationName);
                SceneManager.LoadScene(sceneName:"RestaurantScene");
            }
        }
        else
        {
            SceneManager.LoadScene(sceneName:"RestaurantScene");
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
    }
}
