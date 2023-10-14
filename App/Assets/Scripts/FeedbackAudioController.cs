using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FeedbackAudioController : MonoBehaviour
{
    private Database database;
    public AudioSource audioSource;
    public AudioClip[] questionClips;
    public AudioClip feedbackFirstAttemptAudio;
    public AudioClip feedbackSecondAttemptAudio;
    public static bool isAudioPlaying;
    private float delay = 0.5F;
    private bool audioEnded = false;
    private int situationID;
    private char opAttempts;
    private string situationName = "restaurante";

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);
        opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];

        if (questionClips.Length > 0)
        {
            AudioClip selectedAudio;

            if(JSONReader.isCorrectOp)
            {
                selectedAudio = questionClips[situationID];
            }
            else if(opAttempts == '1')
            {
                selectedAudio = feedbackFirstAttemptAudio;
            }
            else
            {
                selectedAudio = feedbackSecondAttemptAudio;
            }

            audioSource.clip = selectedAudio;
            audioSource.PlayDelayed(delay);
        }
        else
        {
            Debug.LogError("Nenhum áudio encontrado na lista. Adicione pelo menos um AudioClip.");
        } 
    }
}
