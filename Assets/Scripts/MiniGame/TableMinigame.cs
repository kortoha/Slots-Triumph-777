using System;
using UnityEngine;

public class TableMinigame : ChooseCard
{
    public static TableMinigame Instance { get; private set; }

    private Table _table;
    [NonSerialized] public AudioSource _congrats;

    private void OnEnable()
    {
        Instance = this;
        _congrats = GetComponent<AudioSource>();
    }

    public override void CardInteraction()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Vector3 touchPosition = Input.mousePosition;

            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
            }

            Ray ray = Camera.main.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (!isChoseOnce)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("1"))
                    {
                        MakeChoose(hit.collider, 1);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("2"))
                    {
                        MakeChoose(hit.collider, 2);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("3"))
                    {
                        MakeChoose(hit.collider, 3);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("4"))
                    {
                        MakeChoose(hit.collider, 4);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("5"))
                    {
                        MakeChoose(hit.collider, 5);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("6"))
                    {
                        MakeChoose(hit.collider, 6);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("7"))
                    {
                        MakeChoose(hit.collider, 7);
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("8"))
                    {
                        MakeChoose(hit.collider, 8);
                    }
                }
            }
        }
    }

    public void SetTable(Table table)
    {
        _table = table;
    }

    public override void CardWork(Collider collider)
    {
        Card tableCard = collider.gameObject.GetComponent<Card>();
        tableCard.StartRoll();
        tableCard.StartCloseCard();
    }

    public void MakeChoose(Collider collider, int score)
    {
        isChoseOnce = true;
        CardWork(collider);
        cardAnimator.SetInteger("CardID", score);
    }

    private void OnDisable()
    {
        isChoseOnce = false;
        _table.MadeIsPlayed();
        cardAnimator.SetInteger("CardID", 0);
    }
}
