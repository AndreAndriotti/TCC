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
    public Text opText;
    public Button op1Button;
    public Button op2Button;
    public Button op3Button;
    public int situationID;

    public string situationName = "restaurante";
    public bool isFeedback;

    [System.Serializable]
    public class Situation
    {
        public string context;
        public string question;
        public string op1;
        public string op2;
        public string op3;
        public int opOK;
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

        //PlayerPrefs.SetInt("situationID", database.GetSituationNumber("restaurante"));
        //situationID = PlayerPrefs.GetInt("situationID");
        situationID = database.GetSituationNumber(situationName);
        
        mySituationList = JsonUtility.FromJson<SituationList>(textJSON.text);

        contextText.text = mySituationList.situation[situationID].context;
        questionText.text = mySituationList.situation[situationID].question;

        if(isFeedback)
        {
            char opChosen = database.GetSituationOptions(situationName)[situationID];
            switch(opChosen)
            {
                case '1':
                    opText.text = mySituationList.situation[situationID].op1;
                    break;
                case '2':
                    opText.text = mySituationList.situation[situationID].op2;
                    break;
                case '3':
                    opText.text = mySituationList.situation[situationID].op3;
                    break;
            };  
        }
        else
        {
            op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op1;
            op2Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op2;
            op3Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op3;
        }
    }
}
