using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private SQLiteExample database;
    private string username;
    public Text introText;

    void Start()
    {
        database = this.gameObject.AddComponent<SQLiteExample>();
        database.createUserDatabase();
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
