using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public Button soundButton;
    public Button menuInPauseButton;
    public Button menuInWinButton;
    public Button resumeButton;
    public Button pauseButton;
    public Button replayInPauseButton;
    public Button replayInWinButton;
    public Button nextButton;

    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject gamePanel;

    public int sceneID;

    public bool isLast = false;
    public Image soundButtonImage;

    public Sprite onSound;
    public Sprite offSound;

    [SerializeField] private Table[] _tablesArray;
    [SerializeField] private AudioSource _tabSound;

    private bool _isMute = false;

    private void Start()
    {
        soundButton.onClick.AddListener(() =>
        {
            _tabSound.Play();
            _isMute = !_isMute;
            AudioListener.pause = _isMute;
            soundButtonImage.sprite = _isMute ? offSound : onSound;
            PlayerPrefs.SetInt("SND", _isMute ? 1 : 0);
            PlayerPrefs.Save();
        });

        pauseButton.onClick.AddListener(() =>
        {
            _tabSound.Play();
            pausePanel.SetActive(true);
            gamePanel.SetActive(false);
            Time.timeScale = 0f;
        });

        resumeButton.onClick.AddListener(() =>
        {
            _tabSound.Play();
            pausePanel.SetActive(false);
            gamePanel.SetActive(true);
            Time.timeScale = 1f;
        });

        menuInWinButton.onClick.AddListener(() =>
        {
            MoneyAndChipManager.Instance.SavePlayerPrefs();
            _tabSound.Play();
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        });

        replayInWinButton.onClick.AddListener(() =>
        {
            MoneyAndChipManager.Instance.SavePlayerPrefs();
            _tabSound.Play();
            SceneManager.LoadScene(sceneID);
            Time.timeScale = 1f;
        });

        menuInPauseButton.onClick.AddListener(() =>
        {
            _tabSound.Play();
            SceneManager.LoadScene(1);
            Time.timeScale = 1f;
        });

        replayInPauseButton.onClick.AddListener(() =>
        {
            _tabSound.Play();
            SceneManager.LoadScene(sceneID);
            Time.timeScale = 1f;
        });

        nextButton.onClick.AddListener(() =>
        {
            if (!isLast)
            {
                MoneyAndChipManager.Instance.SavePlayerPrefs();
                _tabSound.Play();
                sceneID++;
                SceneManager.LoadScene(sceneID);
                Time.timeScale = 1f;
            }
            else
            {
                MoneyAndChipManager.Instance.SavePlayerPrefs();
                _tabSound.Play();
                SceneManager.LoadScene(2);
                Time.timeScale = 1f;
            }
        });
    }

    private void Update()
    {
        bool allTablesIsClear = true;

        for (int i = 0; i < _tablesArray.Length; i++)
        {
            if (!_tablesArray[i].IsPlayed())
            {
                allTablesIsClear = false;
                break;
            }
        }

        if (allTablesIsClear)
        {
            Invoke("Completed", 0.5f);
        }
    }

    private void Completed()
    {
        winPanel.SetActive(true);
        gamePanel.SetActive(false);
    }
}

