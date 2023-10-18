using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource backgroundMusic;

    public AudioClip music1;
    public AudioClip music2;

    private string currentMusicScene = "";
    private float musicVolume = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            backgroundMusic = gameObject.AddComponent<AudioSource>();
            backgroundMusic.loop = true; // Ativa o loop
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        PlayBackgroundMusic();
    }

    private void PlayBackgroundMusic()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if ((sceneName == "RegisterScene" || sceneName == "MenuScene" || sceneName == "ScenarioScene" || sceneName == "TipsScenarioScene" || sceneName == "TipsScene") && currentMusicScene != "MenuScenes")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = music1;
            musicVolume = 0.6f;
            currentMusicScene = "MenuScenes";

            backgroundMusic.volume = musicVolume;
            backgroundMusic.Play();
        }
        else if ((sceneName == "RestaurantScene" || sceneName == "FeedbackScene") && currentMusicScene != "RestaurantScenes")
        {
            backgroundMusic.Stop();
            backgroundMusic.clip = music2;
            musicVolume = 0.15f;
            currentMusicScene = "RestaurantScenes";

            backgroundMusic.volume = musicVolume;
            backgroundMusic.Play();
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic();
    }
}
