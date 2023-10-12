using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Database database;
    private string username;
    public Text introText;
    public Text helpText;
    public GameObject helpBalloon;
    private int helpTextState;

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        //database.deleteTableSituation();
        //database.deleteTableReport();

        database.createUserDatabase();
        database.createReportTable();
        
        // CRIAR INFOS DO USER NA PRIMEIRA VEZ
        //database.testInsertValues();

        username = database.GetUserName();
        introText.text = "Olá, " + username + "!";

        helpText.enabled = false;
        helpBalloon.SetActive(false);
        helpTextState = 0;
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene(sceneName:"ScenarioScene");
    }
    
    public void OnClickTipsButton()
    {
        SceneManager.LoadScene(sceneName:"TipsScenarioScene");
    }

    public void OnClickRegisterButton()
    {
        SceneManager.LoadScene(sceneName:"RegisterScene");
    }

    public void OnClickHelpButton()
    {
        if(helpText.enabled)
        {
            helpTextState += 1;
            helpTextState %= 3;

            helpText.enabled = false;
            helpBalloon.SetActive(false);
        }
        else
        {
            switch(helpTextState)
            {
                case 0:
                    helpText.text = "Clique em 'Praticar' para iniciar o jogo!";
                    break;
                case 1:
                    helpText.text = "Clique em 'Dicas' para ver as escolhas ideias em cada cenário!";
                    break;
                case 2:
                    helpText.text = "Clique em 'Alterar cadastro' para trocar o nome e/ou o e-mail cadastrados!";
                    break;
            }

            helpText.enabled = true;
            helpBalloon.SetActive(true);
        }
    }
}
