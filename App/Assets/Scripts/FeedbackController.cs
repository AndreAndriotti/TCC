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
    private char opChosen;
    private char opAttempts;
    //private bool isCorrectOp;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationName = "restaurante";
        situationID = database.GetSituationNumber(situationName);
        //isCorrectOp = canvas.GetComponent<JSONReader>().isCorrectOp;
        opChosen = database.GetSituationOptions(situationName)[situationID];
        opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickContinueButton()
    {
        if(JSONReader.isCorrectOp || opAttempts == '2')
        {
            database.UpdateSituation(situationName);
        }
        SceneManager.LoadScene (sceneName:"RestaurantScene");
    }
}
