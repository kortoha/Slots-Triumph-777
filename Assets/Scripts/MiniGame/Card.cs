using UnityEngine;
using System.Collections;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] private Sprite _cardSprite;
    [SerializeField] private Sprite _backsideCardSprite;
    [SerializeField] private GameObject _tableZone;

    public TextMeshProUGUI _cardText;

    private SpriteRenderer _cardRenderer;

    private int _winnigSpinsCount;

    private IEnumerator ChangeCardState()
    {
        yield return new WaitForSeconds(0.25f);
        if(_cardSprite != null)
        {
            _cardRenderer.sprite = _cardSprite;
        }
        yield return new WaitForSeconds(0.25f);
        _cardText.text = "Free spin:\n" + _winnigSpinsCount;
        _GameManager.Instance.WinSpins(_winnigSpinsCount);
        _cardText.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _cardText.gameObject.SetActive(false);

        _cardRenderer = GetComponent<SpriteRenderer>();

        if(_backsideCardSprite!= null)
        {
            _cardRenderer.sprite = _backsideCardSprite;
        }
        _winnigSpinsCount = Random.Range(5, 30);
    }

    private IEnumerator CloseCard()
    {
        yield return new WaitForSeconds(2);
        _cardText.gameObject.SetActive(false);
        TableMinigame.Instance.enabled = false;
        yield return new WaitForSeconds(0.25f);
        Fade.Instance.SetFade();
        yield return new WaitForSeconds(0.25f);
        TableMinigame.Instance.gameObject.SetActive(false);
        TableMinigame.Instance.enabled = true;
        _tableZone.SetActive(true);
    }

    public void StartCloseCard()
    {
        StartCoroutine(CloseCard());
    }

    public virtual void StartRoll()
    {
        StartCoroutine(ChangeCardState());
    }
}
