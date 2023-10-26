using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    private AudioSource backgroundMusic;

    public AudioClip music1;
    public AudioClip music2;

    private string currentMusicScene = "";
    private float musicVolume = 1.0f;
    private bool isMusicAllowed = true;
    public Button musicButton;
    private Color blue;
    public AudioClip buttonSound;
    private MusicManager musicManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            backgroundMusic = gameObject.AddComponent<AudioSource>();
            backgroundMusic.loop = true;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if(isMusicAllowed)
        {
            PlayBackgroundMusic();
        }

        blue = new Color(0.565f, 0.765f, 1f);
        musicButton.GetComponent<Image>().color = blue;
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
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            musicManager = FindObjectOfType<MusicManager>();
            musicButton = GameObject.Find("musicButton").GetComponent<Button>();

            musicButton.onClick.AddListener(musicManager.OnClickMusicButton);

            if(isMusicAllowed)
                musicButton.GetComponent<Image>().color = blue;
            else
                musicButton.GetComponent<Image>().color = Color.white;
        }

        if(isMusicAllowed)
            PlayBackgroundMusic();
    }

    public void OnClickMusicButton()
    {
        if(isMusicAllowed)
        {
            if(backgroundMusic.isPlaying)
                backgroundMusic.Stop();
            musicButton.GetComponent<Image>().color = Color.white;
            isMusicAllowed = false;
        }
        else 
        {
            backgroundMusic.Play();
            musicButton.GetComponent<Image>().color = blue;
            isMusicAllowed = true;
        }
        
        backgroundMusic.PlayOneShot(buttonSound);
    }
}
