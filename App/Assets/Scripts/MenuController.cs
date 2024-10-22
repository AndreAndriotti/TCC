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

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        //database.deleteTableSituation();
        database.createUserDatabase();
        
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
