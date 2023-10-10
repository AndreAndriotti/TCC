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
        if (situationID == (allOpsChosen.Length-1))
        {
            if(JSONReader.isCorrectOp || opAttempts == '2')
            {
                // ENVIAR RELATORIO() <---- CARLOS
                // ResetScenario();
                SceneManager.LoadScene(sceneName:"ScenarioScene");
            }
        }
        else
        {
            if(JSONReader.isCorrectOp || opAttempts == '2')
            {
                database.UpdateSituation(situationName);
            }
            SceneManager.LoadScene(sceneName:"RestaurantScene");
        }
    }

    private void ResetScenario()
    {
        string emptyOps = "0";
        for (int i = 1; i < allOpsChosen.Length; i++)
        {
            emptyOps += '0';
        }

        database.SetSituationNumber(situationName, 0);
        database.SetSituationOptions(situationName, emptyOps);
        database.SetSituationOpsAttempts(situationName, emptyOps);
    }
}
