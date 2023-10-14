using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SituationAudioController : MonoBehaviour
{
    private Database database;
    public AudioSource audioSource;
    public AudioClip[] questionClips;
    public static bool isAudioPlaying;
    private float delay = 1F;
    private bool audioEnded = false;
    private int situationID;
    private string situationName = "restaurante";

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);

        if (questionClips.Length > 0)
        {
            // Escolhe um áudio aleatório
            AudioClip selectedAudio = questionClips[situationID];
            
            // Define o áudio escolhido no AudioSource
            audioSource.clip = selectedAudio;
            
            // Reproduz o áudio com delay
            audioSource.PlayDelayed(delay);
            isAudioPlaying = true;
        }
        else
        {
            Debug.LogError("Nenhum áudio encontrado na lista. Adicione pelo menos um AudioClip.");
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
