using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JSONReader : MonoBehaviour
{
    private Database database;

    public TextAsset textJSON;
    public Text contextText;
    public Text questionText;
    public Text feedbackText;
    public Button op1Button;
    public Button op2Button;
    public Button op3Button;
    public int situationID;
    public char opOK;

    public string situationName = "restaurante";
    public bool isFeedback;
    public static bool isCorrectOp;

    [System.Serializable]
    public class Situation
    {
        public string context;
        public string question;
        public string op1;
        public string op2;
        public string op3;
        public string opOK;
        public string fb1;
        public string fb2;
        public string fbOK;
        public float audioDuration;
    }

    [System.Serializable]
    public class SituationList
    {
        public Situation[] situation;
    }

    public SituationList mySituationList = new SituationList();

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);
        
        mySituationList = JsonUtility.FromJson<SituationList>(textJSON.text);

        opOK = mySituationList.situation[situationID].opOK[0]; 
        contextText.text = mySituationList.situation[situationID].context;
        questionText.text = mySituationList.situation[situationID].question;
        isCorrectOp = false;

        if(isFeedback)
        {
            char opChosen = database.GetSituationOptions(situationName)[situationID];
            char opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];

            op1Button.enabled = false;
            
            switch(opChosen)
            {
                case '1':
                    op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op1;
                    break;
                case '2':
                    op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op2;
                    break;
                case '3':
                    op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op3;
                    break;
            };
            
            if (opChosen == opOK)
            {
                isCorrectOp = true;
                op1Button.GetComponent<Image>().color = Color.green;
                feedbackText.text = mySituationList.situation[situationID].fbOK;
            }
            else if (opAttempts == '1')
            {
                feedbackText.text = mySituationList.situation[situationID].fb1;
            }
            else 
            {
                feedbackText.text = mySituationList.situation[situationID].fb2;
            }
        }

        else
        {
            op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op1;
            op2Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op2;
            op3Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op3;
        }
    }
}
