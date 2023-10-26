using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SituationAudioController : MonoBehaviour
{
    private Database database;
    public AudioSource audioSource;
    public static bool isAudioPlaying;
    public static bool audioEnded = false;
    public Button repeatAudioButton;
    public AudioClip buttonSound;
    private float delay = 1F;
    private string audioName;
    private string fullPath;
    private AudioClip selectedAudio;
    private int situationID;
    private string situationName = "restaurante";
    private string audioPath = "Audios/restaurantAudios/questionAudios";
    private Color blue;

    void Start()
    {
        database = this.gameObject.AddComponent<Database>();
        database.createUserDatabase();

        situationID = database.GetSituationNumber(situationName);
        blue = new Color(0.565f, 0.765f, 1f);

        audioName = "questionAudio" + (situationID+1);
        fullPath = System.IO.Path.Combine(audioPath, audioName);
        selectedAudio = Resources.Load<AudioClip>(fullPath);

        PlayAudio(selectedAudio);
    }

    void Update()
    {
        if (!audioEnded && !audioSource.isPlaying)
        {
            isAudioPlaying = false;
            audioEnded = true;
            repeatAudioButton.enabled = true;
            repeatAudioButton.GetComponent<Image>().color = Color.white;
        }
    }

    void PlayAudio(AudioClip selectedAudio)
    {
        if (selectedAudio != null)
        {
            repeatAudioButton.enabled = false;
            repeatAudioButton.GetComponent<Image>().color = blue * 0.8f;
            audioSource.clip = selectedAudio;
            audioSource.PlayDelayed(delay);
            audioEnded = false;
            isAudioPlaying = true;
        }
        else
        {
            Debug.LogError("Áudio não encontrado para a situação atual.");
        }
    }

    public void OnClickRepeatAudioButton()
    {
        audioSource.PlayOneShot(buttonSound);
        PlayAudio(selectedAudio);
    }
}
