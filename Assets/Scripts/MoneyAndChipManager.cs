using UnityEngine;

public class MoneyAndChipManager : MonoBehaviour
{
    public static MoneyAndChipManager Instance { get; private set; }

    public int chipCount = 0;
    public int moneyScore = 0;

    public int countOfDiscont = 0;
    public int discontpercent = 10;

    private void Awake()
    {     
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        LoadPlayerPrefs();
    }

    public void WinMoney(int count)
    {
        moneyScore += count;
    }

    public void WinShip(int count)
    {
        chipCount += count;
    }

    public void BuyForChip(int conut)
    {
        if (chipCount >= conut)
        {
            chipCount -= conut;
        }
    }

    public void BuyForMoney(int conut)
    {
        if (moneyScore >= conut)
        {
            moneyScore -= conut;
        }
    }

    public void BuyMoneyForChip(int amount)
    {
        if (chipCount >= amount)
        {
            chipCount -= amount;
            moneyScore++;
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("ChipCount", chipCount);
        PlayerPrefs.SetInt("MoneyScore", moneyScore);
        PlayerPrefs.SetInt("DiscontCount", countOfDiscont);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        chipCount = PlayerPrefs.GetInt("ChipCount", 0);
        moneyScore = PlayerPrefs.GetInt("MoneyScore", 0);
        countOfDiscont =  PlayerPrefs.GetInt("DiscontCount", 0);
    }

}
