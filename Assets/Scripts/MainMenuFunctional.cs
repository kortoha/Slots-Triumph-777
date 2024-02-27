using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuFunctional : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelPages;
    [SerializeField] private GameObject[] _tutorialPages;

    [SerializeField] private Button[] _levelsButtons;

    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _tutorialButton;
    [SerializeField] private Button _bankButton;
    [SerializeField] private Button _levelsButton;

    [SerializeField] private AudioClip _tapSound;
    [SerializeField] private AudioClip _bankSound;
    [SerializeField] private Image _soundButtonImage;

    private bool _isMuted = false;

    private int levelPageID = 0;
    private int tutorialPageID = 0;

    public Sprite onSound;
    public Sprite offSound;

    public GameObject lobbyPanel;
    public GameObject startPanel;
    public GameObject tutorialPanel;
    public GameObject bankPanel;
    public GameObject levelPanel;

    public TextMeshProUGUI lobbyChipScore;
    public TextMeshProUGUI lobbyMoneyScore;

    public TextMeshProUGUI bankChipScore;
    public TextMeshProUGUI bankMoneyScore;

    public TextMeshProUGUI discontPriseText;

    private int _exchangePrice = 100;
    private int _discontPrice = 100;

    private void Start()
    {
        SetNumbersOnLevels();
        _discontPrice = PlayerPrefs.GetInt("DiscontPrice", _discontPrice);

        _soundButton.onClick.AddListener(() =>
        {
            PlayTapSound();
            _isMuted = !_isMuted;
            AudioListener.pause = _isMuted;
            _soundButtonImage.sprite = _isMuted ? offSound : onSound;
            PlayerPrefs.SetInt("SND", _isMuted ? 1 : 0);
            PlayerPrefs.Save();
        });
        _playButton.onClick.AddListener(() =>
        {
            PlayTapSound();
            lobbyPanel.SetActive(true);
            startPanel.SetActive(false);
        });
        _tutorialButton.onClick.AddListener(() =>
        {
            PlayTapSound();
            lobbyPanel.SetActive(false);
            tutorialPanel.SetActive(true);           
        });
        _bankButton.onClick.AddListener(() =>
        {
            PlayTapSound();
            lobbyPanel.SetActive(false);
            bankPanel.SetActive(true);
        });
        _levelsButton.onClick.AddListener(() =>
        {
            PlayTapSound();
            lobbyPanel.SetActive(false);
            levelPanel.SetActive(true);
        });

        MoneyAndChipManager.Instance.LoadPlayerPrefs();
    }

    private void Update()
    {
        lobbyChipScore.text = MoneyAndChipManager.Instance.chipCount.ToString();
        lobbyMoneyScore.text = MoneyAndChipManager.Instance.moneyScore.ToString();

        bankChipScore.text = MoneyAndChipManager.Instance.chipCount.ToString();
        bankMoneyScore.text = MoneyAndChipManager.Instance.moneyScore.ToString();

        discontPriseText.text = _discontPrice.ToString();
    }

    private void SetNumbersOnLevels()
    {
        for (int i = 0; i < _levelsButtons.Length; i++)
        {
            int id = i + 1;
            GetButtonText(_levelsButtons[i], id);
        }
    }

    private void GetButtonText(Button btn, int id)
    {
        TextMeshProUGUI btnsText = btn.GetComponentInChildren<TextMeshProUGUI>();

        if (btnsText != null)
        {
            btnsText.text = id.ToString();
        }
    }

    public void PlayTapSound()
    {
        AudioSource.PlayClipAtPoint(_tapSound, Camera.main.transform.position);
    }

    public void LevelsNext()
    {
        if (levelPageID < _levelPages.Length - 1)
        {
            PlayTapSound();
            _levelPages[levelPageID].SetActive(false);
            levelPageID++;
            _levelPages[levelPageID].SetActive(true);
        }
    }

    public void LevelsPrevious()
    {
        if (levelPageID > 0)
        {
            PlayTapSound();
            _levelPages[levelPageID].SetActive(false);
            levelPageID--;
            _levelPages[levelPageID].SetActive(true);
        }
    }

    public void TutorialPrevious()
    {
        if (tutorialPageID > 0)
        {
            PlayTapSound();
            _tutorialPages[tutorialPageID].SetActive(false);
            tutorialPageID--;
            _tutorialPages[tutorialPageID].SetActive(true);
        }
    }

    public void TutorialNext()
    {
        if (tutorialPageID < _tutorialPages.Length - 1)
        {
            PlayTapSound();
            _tutorialPages[tutorialPageID].SetActive(false);
            tutorialPageID++;
            _tutorialPages[tutorialPageID].SetActive(true);
        }
    }

    public void CloseTutorial()
    {
        PlayTapSound();
        tutorialPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void CloseBank()
    {
        PlayTapSound();
        bankPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void CloseLevels()
    {
        PlayTapSound();
        levelPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public void PlayLevel(int levelIndex)
    {
        MoneyAndChipManager.Instance.SavePlayerPrefs();
        PlayTapSound();
        SceneManager.LoadScene(levelIndex);
    }

    public void ExchangeMoney()
    {
        if(MoneyAndChipManager.Instance.chipCount >= _exchangePrice)
        {
            AudioSource.PlayClipAtPoint(_bankSound, Camera.main.transform.position);
            MoneyAndChipManager.Instance.BuyMoneyForChip(_exchangePrice);
        }
    }

    public void BuyDiscount()
    {
        if(MoneyAndChipManager.Instance.moneyScore >= _discontPrice)
        {
            AudioSource.PlayClipAtPoint(_bankSound, Camera.main.transform.position);
            MoneyAndChipManager.Instance.BuyForMoney(_discontPrice);
            _discontPrice += 100;
            MoneyAndChipManager.Instance.countOfDiscont++;
            PlayerPrefs.SetInt("DiscontPrice", _discontPrice);
            PlayerPrefs.Save();
        }
    }
}
