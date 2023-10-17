using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterController : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public TMP_InputField EmailInputField;
    private Database database;
    private string userName;
    private string email;
    
    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        userName = database.GetUserName();
        email = database.GetEmail();

        if (userName != "Jogador" && email != "email")
        {
            NameInputField.text = userName;
            EmailInputField.text = email;
        }
    }

    public void OnClickRegisterButton()
    {
        if (!string.IsNullOrEmpty(NameInputField.text) && !string.IsNullOrEmpty(EmailInputField.text))
        {
            database.SetUserName(NameInputField.text);
            database.SetEmail(EmailInputField.text);

            SceneManager.LoadScene(sceneName:"MenuScene");
        }
    }
}
