using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
 
public class FeedbackController : MonoBehaviour
{
    public GameObject canvas;
    private Database database;
    private string situationName;
    private int situationID;
    private char opAttempts;
    private char opChosen;
    private string allOpsChosen;
    private Report report;
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
        if(JSONReader.isCorrectOp || opAttempts == '2')
        {
            StartCoroutine(CheckForLastSituation());
        }
        else
        {
            SceneManager.LoadScene(sceneName:"RestaurantScene");
        }

    }

    private IEnumerator CheckForLastSituation() {
        if (situationID >= (allOpsChosen.Length-1))
        {
            database.UpdateSituation(situationName);
            continueButton.GetComponentInChildren<Text>().text = "Enviando relatório...";
            yield return new WaitForSeconds(1.0f);
            report.SendEmail(GetBodyText(situationName));
            SceneManager.LoadScene(sceneName:"ScenarioCompletedScene");
        }
        else 
        {
            database.UpdateSituation(situationName);
            SceneManager.LoadScene(sceneName:"RestaurantScene");
        }

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
