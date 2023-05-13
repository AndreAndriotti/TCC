using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private SQLiteExample database;
    // Start is called before the first frame update
    void Start()
    {
        database = this.gameObject.AddComponent<SQLiteExample>();
        database.createUserDatabase();
        //database.testInsertValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene (sceneName:"ScenarioScene");
    }
}
