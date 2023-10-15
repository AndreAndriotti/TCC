using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FeedbackAudioController : MonoBehaviour
{
    private Database database;
    public AudioSource audioSource;
    public static bool isAudioPlaying;
    private float delay = 0.5F;
    private bool audioEnded = false;
    private int situationID;
    private char opAttempts;
    private AudioClip selectedAudio;
    private string audioName;
    private string fullPath;
    private string situationName = "restaurante";
     private string audioPath = "Audios/restaurantAudios/feedbackAudios";

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);
        opAttempts = database.GetSituationOpsAttempts(situationName)[situationID];

        if(JSONReader.isCorrectOp)
        {
            audioName = "feedbackAudio" + (situationID+1);
        }
        else if(opAttempts == '1')
        {
            audioName = "feedbackFirstAttemptAudio";
        }
        else
        {
            audioName = "feedbackSecondAttemptAudio";
        }

        fullPath = System.IO.Path.Combine(audioPath, audioName);
        selectedAudio = Resources.Load<AudioClip>(fullPath);

        if (selectedAudio != null)
        {
            audioSource.clip = selectedAudio;
            audioSource.PlayDelayed(delay);
        }
        else
        {
            Debug.LogError("Áudio não encontrado para a situação atual.");
        }

    }
}
