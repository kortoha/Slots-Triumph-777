using System;
using UnityEngine;

public class _GameManager : MonoBehaviour
{
    public static _GameManager Instance { get; private set; }

    [NonSerialized] public bool minigameState = false;

    [SerializeField] private GameObject _minigame;
    [SerializeField] private GameObject _game;

    private void Awake()
    {
        Instance = this;

        maxSpinCount = spinCount;
    }

    public int spinCount = 20;

    private bool _isMinigameOnce = false;

    [NonSerialized] public int maxSpinCount;

    private void Start()
    {
        SlotMachine.Instance.OnSlotStop += OnSlotStop;
    }

    private void OnSlotStop(object sender, EventArgs e)
    {
        UseSpin();
    }

    public void WinSpins(int count)
    {
        spinCount += count;

        if (spinCount > maxSpinCount)
        {
            spinCount = maxSpinCount;
        }
    }

    private void Update()
    {
        if (spinCount == 0 && !minigameState && !_isMinigameOnce)
        {
            _isMinigameOnce = true;
            Invoke("MakeFade", 1.75f);
            Invoke("OpenMinigame", 2f);
        }
    }

    private void CloseMinigame()
    {
        minigameState = false;
        _minigame.SetActive(false);
        _game.SetActive(true);
        _isMinigameOnce = false;
    }

    public void UseSpin()
    {
        spinCount--;
    }

    private void OpenMinigame()
    {
        minigameState = true;
        _minigame.SetActive(true);
        _game.SetActive(false);
    }

    public void InvokeClose()
    {
        Invoke("CloseMinigame", 1.5f);
    }

    private void OnDisable()
    {
        SlotMachine.Instance.OnSlotStop -= OnSlotStop;
    }

    public void MakeFade()
    {
        Fade.Instance.SetFade();
    }
}
