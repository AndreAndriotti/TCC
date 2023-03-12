using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using KKSpeech;

public class RecordingCanvas : MonoBehaviour
{
  public Button startRecordingButton;
  public Button op1Button;
  public Button op2Button;
  public Button op3Button;
  public Text questText;
  private string op1Text;
  private string op2Text;
  private string op3Text;
  public Text resultText;
  private double similarityPercent;
  private int countAttempts;
  private int maxAttempts;

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
      listener.onPartialResults.AddListener(OnPartialResult);
      listener.onEndOfSpeech.AddListener(OnEndOfSpeech);
      SpeechRecognizer.RequestAccess();
    }
    else
    {
      resultText.text = "Sorry, but this device doesn't support speech recognition";
      startRecordingButton.enabled = false;
    }

    op1Button.enabled = false;
    op2Button.enabled = false;
    op3Button.enabled = false;

    questText.text = "Bem-vindo ao OutBack! Tudo bem?";
    op1Text = "Olá! Onde eu poderia me sentar?";
    op1Button.GetComponentInChildren<Text>().text = op1Text;
    op2Text = "Oi! Gostaria de fazer um pedido! ";
    op2Button.GetComponentInChildren<Text>().text = op2Text;
    op3Text = "Olá! Gosto desse restaurante!";
    op3Button.GetComponentInChildren<Text>().text = op3Text;

    maxAttempts = 3;
    countAttempts = 1;
  }

  public void OnFinalResult(string result)
  {
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
    resultText.text = result;
    startRecordingButton.enabled = true;

    MatchOption(result);
  }

  public void OnPartialResult(string result)
  {
    resultText.text = result;
  }

  public void OnAvailabilityChange(bool available)
  {
    startRecordingButton.enabled = available;
    if (!available)
    {
      resultText.text = "Speech Recognition not available";
    }
    else
    {
      resultText.text = "Say something :-)";
    }
  }

  public void OnAuthorizationStatusFetched(AuthorizationStatus status)
  {
    switch (status)
    {
      case AuthorizationStatus.Authorized:
        startRecordingButton.enabled = true;
        break;
      default:
        startRecordingButton.enabled = false;
        resultText.text = "Cannot use Speech Recognition, authorization status is " + status;
        break;
    }
  }

  public void OnEndOfSpeech()
  {
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
  }

  public void OnError(string error)
  {
    Debug.LogError(error);
    startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
    startRecordingButton.enabled = true;
  }

  public void OnStartRecordingPressed()
  {
    if (SpeechRecognizer.IsRecording())
    {
#if UNITY_IOS && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Stopping";
			startRecordingButton.enabled = false;
#elif UNITY_ANDROID && !UNITY_EDITOR
			SpeechRecognizer.StopIfRecording();
			startRecordingButton.GetComponentInChildren<Text>().text = "Start Recording";
#endif
    }
    else
    {
      op1Button.GetComponent<Image>().color = Color.white;
      op2Button.GetComponent<Image>().color = Color.white;
      op3Button.GetComponent<Image>().color = Color.white;
      SpeechRecognizer.StartRecording(true);
      startRecordingButton.GetComponentInChildren<Text>().text = "Stop Recording";
      resultText.text = "Say something :-)";
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

    if(findSimilarity(result.ToUpper(), op1Text.ToUpper()) > similarityPercent){
      op1Button.GetComponent<Image>().color = Color.green;
    }
    else if(findSimilarity(result.ToUpper(), op2Text.ToUpper()) > similarityPercent){
      op2Button.GetComponent<Image>().color = Color.green;
    }
    else if(findSimilarity(result.ToUpper(), op3Text.ToUpper()) > similarityPercent){
      op3Button.GetComponent<Image>().color = Color.green;
    }
    else {
      if(countAttempts < maxAttempts){
        questText.text += countAttempts.ToString();
        countAttempts++;
      }
      else{
        op1Button.enabled = true;
        op2Button.enabled = true;
        op3Button.enabled = true;
      }
    }

  }
  
  public void OnClickOption() {
    string currentButtonName = EventSystem.current.currentSelectedGameObject.name;
    if(currentButtonName == "op1Button"){
      op1Button.GetComponent<Image>().color = Color.green;
    }
    else if(currentButtonName == "op2Button"){
      op2Button.GetComponent<Image>().color = Color.green;
    }
    else if(currentButtonName == "op3Button"){
      op3Button.GetComponent<Image>().color = Color.green;
    }
    countAttempts = 1;
    op1Button.enabled = false;
    op2Button.enabled = false;
    op3Button.enabled = false;
  }
}
