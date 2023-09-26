using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterController : MonoBehaviour
{
    public TMP_InputField EmailInputField;
    public TMP_InputField NameInputField;
    private Database database;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();
    }

    public void OnClickRegisterButton()
    {
        if (!string.IsNullOrEmpty(NameInputField.text) && !string.IsNullOrEmpty(EmailInputField.text))
        {
            database.SetEmail(EmailInputField.text);
            database.SetUserName(NameInputField.text);
            SceneManager.LoadScene(sceneName:"MenuScene");
        }
    }


}
