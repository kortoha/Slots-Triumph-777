using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameVisual : MonoBehaviour
{
    public TextMeshProUGUI chipCountText;
    public TextMeshProUGUI tablesChipCountText;
    public TextMeshProUGUI spinText;
    public Image spinBar;

    private void Update()
    {
        chipCountText.text = MoneyAndChipManager.Instance.chipCount.ToString();
        tablesChipCountText.text = MoneyAndChipManager.Instance.chipCount.ToString();
        UpdateSpinBar();
    }

    private void UpdateSpinBar()
    {
        spinText.text = _GameManager.Instance.spinCount.ToString() + " / " + _GameManager.Instance.maxSpinCount.ToString();

        float spinCountLevel = (float)_GameManager.Instance.spinCount / _GameManager.Instance.maxSpinCount;

        spinBar.fillAmount = spinCountLevel;
    }
}
