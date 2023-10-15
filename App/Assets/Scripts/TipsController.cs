using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TipsController : MonoBehaviour
{
    public GameObject dicaPrefab;
    public Transform contentPanel;
    public ScrollRect scrollRect;
    public Text introText;
    private float dicaSpacing = 700f;

    private Database database;
    private string situationName = "restaurante";
    private int situationID;
    public TextAsset textJSON;
    private string context;
    private string question;
    private string op;
    private char opOK;
    private string fbOK;


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

        introText.text = $"Dicas para\n{situationName}";

        
        for (int i = 0; i < mySituationList.situation.Length; i++)
        {
            context = mySituationList.situation[i].context;
            question = mySituationList.situation[i].question;

            fbOK = mySituationList.situation[i].fbOK; 
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
            novaDicaText[1].text = op;
            novaDicaText[2].text = fbOK;
        }
        else
        {
            novaDicaText[0].text = $"Situação {i+1}";
            novaDicaText[1].text = "BLOQUEADO";
            novaDicaText[2].text = "Jogue para desbloquear!";
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(sceneName:"TipsScenarioScene");
    }
}
