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
    public Button op1Button;
    public Button op2Button;
    public Button op3Button;
    private int situationID;

    [System.Serializable]
    public class Situation
    {
        public string context;
        public string question;
        public string op1;
        public string op2;
        public string op3;
        //private string fb1;
        //private string fb2;
        //private string fb3;
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

        PlayerPrefs.SetInt("situationID", database.GetSituationNumber("restaurante"));

        situationID = PlayerPrefs.GetInt("situationID");
        mySituationList = JsonUtility.FromJson<SituationList>(textJSON.text);

        contextText.text = mySituationList.situation[situationID].context + situationID.ToString();
        questionText.text = mySituationList.situation[situationID].question;
        op1Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op1;
        op2Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op2;
        op3Button.GetComponentInChildren<Text>().text = mySituationList.situation[situationID].op3;
    }
}
