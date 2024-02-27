using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Table : MonoBehaviour
{
    public int tablePrice = 100;
    public GameObject tableMinigame;
    public GameObject tableZone;
    public AudioSource _tap;

    private TextMeshProUGUI _tablePriceText;
    private Button _tableButton;
    private bool _isPlayed = false;
    private Image _tableIcon;

    private void OnEnable()
    {
        UpdateTablePrice();
    }

    private void Start()
    {
        _tablePriceText = GetComponentInChildren<TextMeshProUGUI>();
        _tableButton = GetComponent<Button>();

        UpdateTablePrice();

        _tableIcon = _tableButton.GetComponent<Image>();

        _tableButton.onClick.AddListener(() => { PlayCards(); });
    }

    private void UpdateTablePrice()
    {
        if (_tablePriceText != null && _tableButton != null)
        {
            int discountCount = MoneyAndChipManager.Instance.countOfDiscont;

            int discountedTablePrice = tablePrice;

            for (int i = 0; i < discountCount; i++)
            {
                discountedTablePrice = Mathf.RoundToInt(discountedTablePrice * (1 - (MoneyAndChipManager.Instance.discontpercent / 100f)));
            }

            _tablePriceText.text = discountedTablePrice.ToString();
        }
        else
        {
            Debug.LogWarning("TablePriceText or TableButton is not initialized.");
        }
    }


    private void Update()
    {
        int discountedTablePrice = tablePrice;

        int discountCount = MoneyAndChipManager.Instance.countOfDiscont;

        for (int i = 0; i < discountCount; i++)
        {
            discountedTablePrice = Mathf.RoundToInt(discountedTablePrice * (1 - (MoneyAndChipManager.Instance.discontpercent / 100f)));
        }

        if (MoneyAndChipManager.Instance.chipCount >= discountedTablePrice)
        {
            _tableIcon.color = Color.white;
        }
        else
        {
            _tableIcon.color = Color.gray;
        }
    }

    public void PlayCards()
    {
        _tap.Play();
        if (!_isPlayed)
        {
            int discountedTablePrice = tablePrice;

            int discountCount = MoneyAndChipManager.Instance.countOfDiscont;

            for (int i = 0; i < discountCount; i++)
            {
                discountedTablePrice = Mathf.RoundToInt(discountedTablePrice * (1 - (MoneyAndChipManager.Instance.discontpercent / 100f)));
            }

            if (MoneyAndChipManager.Instance.chipCount >= discountedTablePrice)
            {
                StartCoroutine(OpenMiniGame());
            }
        }
    }

    private IEnumerator OpenMiniGame()
    {
        Fade.Instance.SetFade();
        yield return new WaitForSeconds(0.35f);

        int discountedTablePrice = tablePrice;

        int discountCount = MoneyAndChipManager.Instance.countOfDiscont;

        for (int i = 0; i < discountCount; i++)
        {
            discountedTablePrice = Mathf.RoundToInt(discountedTablePrice * (1 - (MoneyAndChipManager.Instance.discontpercent / 100f)));
        }

        MoneyAndChipManager.Instance.BuyForChip(discountedTablePrice);
        tableMinigame.SetActive(true);
        tableZone.SetActive(false);
        TableMinigame.Instance.SetTable(this);
        _tablePriceText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public bool IsPlayed()
    {
        return _isPlayed;
    }

    public void MadeIsPlayed()
    {
        _isPlayed = true;
    }
}
