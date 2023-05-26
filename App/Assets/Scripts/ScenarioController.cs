using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioController : MonoBehaviour
{
    private Database database;

    public void OnClickBackButton()
    {
        SceneManager.LoadScene(sceneName:"MenuScene");
    }

    public void OnClickRestaurantButton()
    {
        SceneManager.LoadScene(sceneName:"RestaurantScene");
    }

    public void OnClickHelpButton()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();
        
        // PARA ZERAR O SITUATIONID:
        database.SetSituationNumber("restaurante", 0);
        //
    }
}
