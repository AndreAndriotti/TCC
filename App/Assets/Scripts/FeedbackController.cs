using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackController : MonoBehaviour
{
    private Database database;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickContinueButton()
    {
        database.UpdateSituation("restaurante");
        SceneManager.LoadScene (sceneName:"RestaurantScene");
    }
}
