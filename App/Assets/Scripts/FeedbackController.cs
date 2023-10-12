using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FeedbackController : MonoBehaviour
{
    public GameObject canvas;
    private Database database;
    private Report report;
    private string situationName;
    private int situationID;
    private char opOK;
    private char opAttempts;
    private char opChosen;
    private string allOpsChosen;
    public Button continueButton;
    //private bool isCorrectOp;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        report = this.gameObject.AddComponent<Report>();
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
                continueButton.GetComponentInChildren<Text>().text = "Enviando relatório...";
                continueButton.enabled = false;
                report.SendEmail(GetBodyText(situationName));
                // ResetScenario();
                SceneManager.LoadScene(sceneName:"ScenarioScene");
            }
        }

        if(JSONReader.isCorrectOp || opAttempts == '2')
        {
            database.UpdateSituation(situationName);
        }
        SceneManager.LoadScene(sceneName:"RestaurantScene");
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

    private string GetBodyText(string scenarioName) {
        string body;
        int newOpAttempt = int.Parse(opAttempts.ToString());
        
        int countSituationsScenario = database.GetSituationsTotalInScenario(scenarioName);
        
        int numberOfTries;

        body = $"RELATÓRIO DO PACIENTE {database.GetUserName()} DO CENÁRIO {scenarioName}\n";
        for(int countAux = 0; countAux != countSituationsScenario; countAux++) {
            body = body + $"\nSituação {countAux+1}:\n";
            body = body + $"Contexto: {database.GetSituationContext(countAux)}\n";
            numberOfTries = database.GetNumberOfTriesInSituation(scenarioName, countAux);
            
            for (int i = 1; i != numberOfTries+1; i++){
                body = body + $"Tentativa {i}: {database.GetOptionChoosen(scenarioName, countAux, i)}\n";
            }


        }

        return body;
    }
}
