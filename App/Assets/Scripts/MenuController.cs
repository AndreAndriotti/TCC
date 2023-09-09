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
    private Report report;

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        report = this.gameObject.AddComponent<Report>();
        //database.deleteTableSituation();
        database.createUserDatabase();
        report.SendEmail();
        
        // CRIAR INFOS DO USER NA PRIMEIRA VEZ
        //database.testInsertValues();

        username = database.GetUserName();
        introText.text = "Olá, " + username + "!";
    }

    void Update()
    {
        
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene (sceneName:"ScenarioScene");
    }
}
