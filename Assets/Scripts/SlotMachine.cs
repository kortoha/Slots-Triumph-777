using System;
using TMPro;
using UnityEngine;

public class SlotMachine : MonoBehaviour
{
    public static SlotMachine Instance { get; private set; }

    public event EventHandler OnSlotStop;

    public float speed = 30f;

    public SpriteRenderer firstSlotRenderer;
    public SpriteRenderer secondSlotRenderer;
    public SpriteRenderer thirdSlotRenderer;

    public SlotItem[] slotItemArray;

    public Transform middle;
    public Transform bottom;
    public Transform top;

    public TextMeshProUGUI spinButtonText;

    private Vector2 _firsUpPos;
    private Vector2 _secondUpPos;
    private Vector2 _thirdUpPos;

    private SlotItem _firstSlotItem;
    private SlotItem _secondSlotItem;
    private SlotItem _thirdSlotItem;

    private bool _isSpin = false;

    private bool _isSlotsUsed = false;

    public GameObject threeInRowFX;
    public Transform FXParentObject;

    public AudioClip threeInRowSound;

    public AudioSource slotSound;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _firsUpPos = new Vector2(firstSlotRenderer.transform.position.x, top.position.y);
        _secondUpPos = new Vector2(secondSlotRenderer.transform.position.x, top.position.y);
        _thirdUpPos = new Vector2(thirdSlotRenderer.transform.position.x, top.position.y);
    }

    private void SpinSlot()
    {
        _isSlotsUsed = false;
        firstSlotRenderer.transform.Translate(Vector2.down * speed * Time.deltaTime);
        secondSlotRenderer.transform.Translate(Vector2.down * speed * Time.deltaTime);
        thirdSlotRenderer.transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    public bool IsSpin()
    {
        return _isSpin;
    }

    private void UpdateSlotPosition()
    {
        if (firstSlotRenderer.transform.position.y <= bottom.position.y)
        {
            firstSlotRenderer.transform.position = _firsUpPos;
            ShangeSlotSprite(firstSlotRenderer, _firstSlotItem);
        }
        else if (secondSlotRenderer.transform.position.y <= bottom.position.y)
        {
            secondSlotRenderer.transform.position = _secondUpPos;
            ShangeSlotSprite(secondSlotRenderer, _secondSlotItem);
        }
        else if (thirdSlotRenderer.transform.position.y <= bottom.position.y)
        {
            thirdSlotRenderer.transform.position = _thirdUpPos;
            ShangeSlotSprite(thirdSlotRenderer, _thirdSlotItem);
        }
    }

    private void Update()
    {
        IsTreeInRow();

        if (_isSpin)
        {
            SpinSlot();
            UpdateSlotPosition();
        }
        else
        {
            if (!_isSlotsUsed)
            {
                slotSound.Stop();

                if (!IsTreeInRow())
                {          
                    StopSlotsPos(firstSlotRenderer, _firstSlotItem, 0, OnSlotStop);
                    StopSlotsPos(secondSlotRenderer, _secondSlotItem, 0.4f, null);
                    StopSlotsPos(thirdSlotRenderer, _thirdSlotItem, 0.8f, null);
                    _isSlotsUsed = true;
                }
                else
                {
                    GameObject fx = Instantiate(threeInRowFX);
                    AudioSource.PlayClipAtPoint(threeInRowSound, Camera.main.transform.position);
                    fx.transform.parent = FXParentObject;
                    StopSlotsPos(firstSlotRenderer, _firstSlotItem, 0.8f, OnSlotStop);
                    StopSlotsPos(secondSlotRenderer, _secondSlotItem, 1.2f, null);
                    StopSlotsPos(thirdSlotRenderer, _thirdSlotItem, 1.6f, null);
                    _isSlotsUsed = true;
                }
            }
        }
    }

    private void StopSlotsPos(SpriteRenderer spriteRenderer, SlotItem slotItem, float delay, EventHandler eventHandler)
    {
        spriteRenderer.transform.position = new Vector2(spriteRenderer.transform.position.x, middle.position.y);
        StartCoroutine(UseSlot(slotItem, delay, eventHandler));         
    }

    private void ShangeSlotSprite(SpriteRenderer spriteRenderer, SlotItem slotItem)
    {
        slotItem = slotItemArray[UnityEngine.Random.Range(0, slotItemArray.Length)];
        spriteRenderer.sprite = slotItem.slotSprite;

        if (spriteRenderer == firstSlotRenderer)
        {
            _firstSlotItem = slotItem;
        }
        else if (spriteRenderer == secondSlotRenderer)
        {
            _secondSlotItem = slotItem;
        }
        else if (spriteRenderer == thirdSlotRenderer)
        {
            _thirdSlotItem = slotItem;
        }
    }

    private System.Collections.IEnumerator UseSlot(SlotItem slotItem, float delay, EventHandler @event)
    {
        if(slotItem != null)
        {       
            yield return new WaitForSeconds(delay);

            UseSlot(slotItem);

            if (@event != null)
            {
                @event?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void UseSlot(SlotItem slotItem)
    {
        if (!IsTreeInRow())
        {
            MoneyAndChipManager.Instance.WinShip(slotItem.chipCount);
            AudioSource.PlayClipAtPoint(slotItem.soundFX[UnityEngine.Random.Range(0, slotItem.soundFX.Length)], Camera.main.transform.position);
            GameObject fx = Instantiate(slotItem.fx, Vector2.zero, Quaternion.identity);
            fx.transform.parent = FXParentObject;
        }
        else if (IsTreeInRow())
        {
            MoneyAndChipManager.Instance.WinShip(slotItem.chipCount * 2);
            GameObject fx = Instantiate(slotItem.fx, Vector2.zero, Quaternion.identity);
            fx.transform.parent = FXParentObject;
        }
    }

    private bool IsTreeInRow()
    {
        if(_firstSlotItem != null && _secondSlotItem != null && _thirdSlotItem != null)
        {
            int firstID = _firstSlotItem.ID;
            int secondID = _secondSlotItem.ID;
            int thirdID = _thirdSlotItem.ID;
            return firstID == secondID && secondID == thirdID;
        }
        else
        {
            return false;
        }
    }

    public void SpinSlotsUI()
    {
        if(_GameManager.Instance.spinCount > 0)
        {
            _isSpin = !_isSpin;

            if (_isSpin)
            {
                slotSound.Play();
                spinButtonText.text = "Stop";
            }
            else
            {
                spinButtonText.text = "Spin";
            }
        }
    }
}
