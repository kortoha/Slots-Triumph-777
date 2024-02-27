using System.Collections;
using UnityEngine;

public class TableCard : Card
{
    private int _winnigMoneyCount;

    private System.Collections.IEnumerator ChangeCardState()
    {
        yield return new WaitForSeconds(0.90f);
        _cardText.text = "Your win:\n" + _winnigMoneyCount;
        MoneyAndChipManager.Instance.WinMoney(_winnigMoneyCount);
        _cardText.gameObject.SetActive(true);
        TableMinigame.Instance._congrats.Play();
    }

    private void OnEnable()
    {
        _cardText.gameObject.SetActive(false);
        _winnigMoneyCount = UnityEngine.Random.Range(5, 50);
    }

    public override void StartRoll()
    {
        StartCoroutine(ChangeCardState());
    }
}
