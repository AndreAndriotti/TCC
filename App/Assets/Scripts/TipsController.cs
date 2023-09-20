using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TipsController : MonoBehaviour
{
    public GameObject dicaPrefab;
    public Transform contentPanel;
    //public int numDicasIniciais = 11;
    public ScrollRect scrollRect;
    private float dicaSpacing = 700f;

    private Database database;
    private string situationName = "restaurante";
    private int situationID;
    public TextAsset textJSON;
    private string context;
    private string question;
    private string op;
    private char opOK;

    // JSON READER

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

    //

    private void Start()
    {
        mySituationList = JsonUtility.FromJson<SituationList>(textJSON.text);
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);

        
        for (int i = 0; i < 11; i++)
        {
            context = mySituationList.situation[i].context;
            question = mySituationList.situation[i].question;

            opOK = mySituationList.situation[i].opOK[0]; 
            switch(opOK)
            {
                case '1':
                    op = mySituationList.situation[i].op1;
                    break;
                case '2':
                    op = mySituationList.situation[i].op2;
                    break;
                case '3':
                    op = mySituationList.situation[i].op3;
                    break;
            };

            AdicionarDica(context, question, op, i);
        }

        scrollRect.verticalNormalizedPosition = 1.0f;
    }

    private void AdicionarDica(string context, string question, string op, int i)
    {
        GameObject novaDicaObj = Instantiate(dicaPrefab, contentPanel);

        RectTransform novaDicaTransform = novaDicaObj.GetComponent<RectTransform>();
        float yPos = -dicaSpacing * contentPanel.childCount;
        novaDicaTransform.anchoredPosition = new Vector2(novaDicaTransform.anchoredPosition.x, yPos);

        Text[] novaDicaText = novaDicaObj.GetComponentsInChildren<Text>();
        if(i < situationID)
        {
            novaDicaText[0].text = context;
            novaDicaText[1].text = question;
            novaDicaText[2].text = op;
        }
        else
        {
            novaDicaText[1].text = "BLOQUEADO";
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(sceneName:"TipsScenarioScene");
    }
}
