using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using KKSpeech;

public class SituationController : MonoBehaviour
{
  private Database database;

  public class SituationData
  {
    public string context;
    public string question;
	  public string op1;
	  public string op2;
	  public string op3;
	  public char opOK;
	  //private string fb1;
	  //private string fb2;
	  //private string fb3;
    public float audioDuration;

    public SituationData( string context,
                          string question, 
                          string op1, 
                          string op2, 
                          string op3,
                          char opOK,
                          float audioDuration) 
    {
      this.context = context;
      this.question = question;
      this.op1 = op1;
      this.op2 = op2;
      this.op3 = op3;
      this.opOK = opOK;
      this.audioDuration = audioDuration;
    }
  }

  public Button startRecordingButton;
  public Button op1Button;
  public Button op2Button;
  public Button op3Button;
  public Text contextText;
  public Text instructionText;
  private int situationID;
  private string situationName;
  private string opsAttempts;
  
  private double similarityPercent;
  private int countAttempts;
  private int maxAttempts;
  private Color lightBlue;

  void Start()
  {
    if (SpeechRecognizer.ExistsOnDevice())
    {
      SpeechRecognizerListener listener = GameObject.FindObjectOfType<SpeechRecognizerListener>();
      listener.onAuthorizationStatusFetched.AddListener(OnAuthorizationStatusFetched);
      listener.onAvailabilityChanged.AddListener(OnAvailabilityChange);
      listener.onErrorDuringRecording.AddListener(OnError);
      listener.onErrorOnStartRecording.AddListener(OnError);
      listener.onFinalResults.AddListener(OnFinalResult);
      listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
      SpeechRecognizer.RequestAccess();
    }
    else
    {
      instructionText.text = "Este dispositivo não suporta reconhecimento de voz";
      startRecordingButton.enabled = false;
      startRecordingButton.GetComponent<Image>().color = Color.white * 0.6f;
    }

    database = this.gameObject.AddComponent<Database>();
    database.createUserDatabase();

    situationName = "restaurante";

    situationID = database.GetSituationNumber(situationName);
    opsAttempts = database.GetSituationOpsAttempts(situationName);

    StartCoroutine(EnableRecording());
    //EnableOptions(false);

    maxAttempts = 3;
    countAttempts = 1;

    lightBlue = new Color(0.769f, 0.878f, 0.976f);
  }

  public void OnFinalResult(string result)
  {
    instructionText.text = "Toque aqui para gravar sua resposta";
    startRecordingButton.enabled = true;
    startRecordingButton.GetComponent<Image>().color = Color.white;
    MatchOption(result);
  }

  public void OnAvailabilityChange(bool available)
  {
    startRecordingButton.enabled = available;
    if (!available)
    {
      instructionText.text = "Reconhecimento de voz não disponível";
      startRecordingButton.GetComponent<Image>().color = Color.white * 0.6f;
    }
  }

  public void OnAuthorizationStatusFetched(AuthorizationStatus status)
  {
    switch (status)
    {
      case AuthorizationStatus.Authorized:
        startRecordingButton.enabled = true;
        startRecordingButton.GetComponent<Image>().color = Color.white;
        break;
      default:
        startRecordingButton.enabled = false;
        instructionText.text = "Autorização para reconhecimento de voz: " + status;
        startRecordingButton.GetComponent<Image>().color = Color.white * 0.6f;
        break;
    }
  }

  public void OnEndOfSpeech()
  {
    instructionText.text = "Toque aqui para gravar sua resposta";

  }

  public void OnError(string error)
  {
    Debug.LogError(error);
    instructionText.text = "Toque aqui para gravar sua resposta";
    startRecordingButton.enabled = true;
    startRecordingButton.GetComponent<Image>().color = Color.white;
  }

  public void OnStartRecordingPressed()
  {
    if (SpeechRecognizer.IsRecording())
    {
#if UNITY_IOS && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.enabled = false;
#elif UNITY_ANDROID && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
#endif
    }
    else
    {
      op1Button.GetComponent<Image>().color = Color.white;
      op2Button.GetComponent<Image>().color = Color.white;
      op3Button.GetComponent<Image>().color = Color.white;
      SpeechRecognizer.StartRecording(true);
      startRecordingButton.GetComponent<Image>().color = lightBlue;
      instructionText.text = "Ouvindo...";
    }
  }

  public static int getEditDistance(string X, string Y)
    {
        int m = X.Length;
        int n = Y.Length;
 
        int[][] T = new int[m + 1][];
        for (int i = 0; i < m + 1; ++i) {
            T[i] = new int[n + 1];
        }
 
        for (int i = 1; i <= m; i++) {
            T[i][0] = i;
        }
        for (int j = 1; j <= n; j++) {
            T[0][j] = j;
        }
 
        int cost;
        for (int i = 1; i <= m; i++) {
            for (int j = 1; j <= n; j++) {
                cost = X[i - 1] == Y[j - 1] ? 0: 1;
                T[i][j] = Mathf.Min(Mathf.Min(T[i - 1][j] + 1, T[i][j - 1] + 1),
                        T[i - 1][j - 1] + cost);
            }
        }
 
        return T[m][n];
    }
 
    public static double findSimilarity(string x, string y) {
        if (x == null || y == null) {
            return -1;
        }
 
        double maxLength = Mathf.Max(x.Length, y.Length);
        if (maxLength > 0) {
            // opcionalmente ignora maiúsculas e minúsculas se necessário
            return (maxLength - getEditDistance(x, y)) / maxLength;
        }
        return 1.0;
    }

  public void MatchOption(string result) {
    similarityPercent = 0.7;

    string op1text = op1Button.GetComponentInChildren<Text>().text;
    string op2text = op2Button.GetComponentInChildren<Text>().text;
    string op3text = op3Button.GetComponentInChildren<Text>().text;

    string context_situation = contextText.text;

    string situationOps = database.GetSituationOptions(situationName);
    int newOpAttempt = int.Parse(opsAttempts[situationID].ToString())+1;

    if(findSimilarity(result.ToUpper(), op1text.ToUpper()) > similarityPercent){
      op1Button.GetComponent<Image>().color = lightBlue;
      situationOps = situationOps.Substring(0, situationID) + '1' + situationOps.Substring(situationID + 1);
      database.SetSituationOptions(situationName, situationOps);
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op1text, newOpAttempt);  
      StartCoroutine(GoToFeedbackScene());
    }
    else if(findSimilarity(result.ToUpper(), op2text.ToUpper()) > similarityPercent){
      op2Button.GetComponent<Image>().color = lightBlue;
      situationOps = situationOps.Substring(0, situationID) + '2' + situationOps.Substring(situationID + 1);
      database.SetSituationOptions(situationName, situationOps);
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op2text, newOpAttempt);
      StartCoroutine(GoToFeedbackScene());
    }
    else if(findSimilarity(result.ToUpper(), op3text.ToUpper()) > similarityPercent){
      op3Button.GetComponent<Image>().color = lightBlue;
      situationOps = situationOps.Substring(0, situationID) + '3' + situationOps.Substring(situationID + 1);
      database.SetSituationOptions(situationName, situationOps);
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op3text, newOpAttempt);
      StartCoroutine(GoToFeedbackScene());
    }
    else {
      if(countAttempts < maxAttempts){
        instructionText.text = "Não entendi. Tente novamente!";
        countAttempts++;
      }
      else{
        instructionText.text = "Não foi possível entender.\nClique na opção desejada!";
        EnableOptions(true);
      }
    }
  }
  
  public void OnClickOption()
  {
    string currentButtonName = EventSystem.current.currentSelectedGameObject.name;
    string situationOps = database.GetSituationOptions(situationName);
    char opChosen = '0';
    int newOpAttempt = int.Parse(opsAttempts[situationID].ToString())+1;
    string context_situation = contextText.text;

    if(currentButtonName == "op1Button"){
      op1Button.GetComponent<Image>().color = lightBlue;
      opChosen = '1';
      string op1text = op1Button.GetComponentInChildren<Text>().text;
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op1text, newOpAttempt);  
    }
    else if(currentButtonName == "op2Button"){
      op2Button.GetComponent<Image>().color = lightBlue;
      opChosen = '2';
      string op2text = op2Button.GetComponentInChildren<Text>().text;
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op2text, newOpAttempt); 
    }
    else if(currentButtonName == "op3Button"){
      op3Button.GetComponent<Image>().color = lightBlue;
      opChosen = '3';
      string op3text = op3Button.GetComponentInChildren<Text>().text;
      database.InsertIntoReportTrackerTable(situationName, situationID, context_situation, op3text, newOpAttempt); 
    }
    
    situationOps = situationOps.Substring(0, situationID) + opChosen + situationOps.Substring(situationID + 1);
    database.SetSituationOptions(situationName, situationOps);
    StartCoroutine(GoToFeedbackScene());
  }

  public void OnClickBackButton()
  {
    SceneManager.LoadScene(sceneName:"ScenarioScene");
  }

  private void EnableOptions(bool state)
  {
    op1Button.enabled = state;
    op2Button.enabled = state;
    op3Button.enabled = state;
  }

  /*public void ResetSituation() 
  {
    countAttempts = 1;
    instructionText.text = "Toque aqui para gravar sua resposta";
  }*/

  IEnumerator GoToFeedbackScene()
  {
    int newOpAttempt = (int)opsAttempts[situationID] + 1;
    opsAttempts = opsAttempts.Substring(0, situationID) + (char)newOpAttempt + opsAttempts.Substring(situationID + 1);
    database.SetSituationOpsAttempts(situationName, opsAttempts);

    string situationOps = database.GetSituationOptions(situationName);

    yield return new WaitForSeconds(1.5F);
    SceneManager.LoadScene(sceneName:"FeedbackScene");
  }

  IEnumerator EnableRecording()
  {
    startRecordingButton.enabled = false;
    startRecordingButton.GetComponent<Image>().color = Color.white * 0.6f;
    yield return new WaitForSeconds(4.16F + 1.5F); // Audio Duration 4.16
    startRecordingButton.enabled = true;
    startRecordingButton.GetComponent<Image>().color = Color.white;
    instructionText.text = "Toque aqui para gravar sua resposta";
    //EnableOptions(true); <- poder clicar quando a narração termina
  }
}
