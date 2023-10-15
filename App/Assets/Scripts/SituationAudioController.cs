using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SituationAudioController : MonoBehaviour
{
    private Database database;
    public AudioSource audioSource;
    public static bool isAudioPlaying;
    private float delay = 1F;
    private bool audioEnded = false;
    private string audioName;
    private string fullPath;
    private AudioClip selectedAudio;
    private int situationID;
    private string situationName = "restaurante";
    private string audioPath = "Audios/restaurantAudios/questionAudios";

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);

        audioName = "questionAudio" + (situationID+1);
        fullPath = System.IO.Path.Combine(audioPath, audioName);

        selectedAudio = Resources.Load<AudioClip>(fullPath);

        if (selectedAudio != null)
        {
            audioSource.clip = selectedAudio;
            audioSource.PlayDelayed(delay);
            isAudioPlaying = true;
        }
        else
        {
            Debug.LogError("Áudio não encontrado para a situação atual.");
        }
    }

    void Update()
    {
        if (!audioEnded && !audioSource.isPlaying)
        {
            isAudioPlaying = false;
            audioEnded = true;
        }
    }
}
