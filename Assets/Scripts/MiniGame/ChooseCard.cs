using System;
using UnityEngine;

public class ChooseCard : MonoBehaviour
{
    public Animator cardAnimator;
    private AudioSource _congrats;

    [NonSerialized] public bool isChoseOnce = false;

    private void OnEnable()
    {
        _congrats = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CardInteraction();
    }

    public virtual void CardInteraction()
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
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("First"))
                    {
                        MakeChoose(hit.collider, "FirstRoll");
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Second"))
                    {
                        MakeChoose(hit.collider, "SecondRoll");
                    }
                    else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Third"))
                    {
                        MakeChoose(hit.collider, "ThirdRoll");
                    }
                }
            }
        }
    }

    public virtual void CardWork(Collider collider)
    {
        collider.gameObject.GetComponent<Card>().StartRoll();
    }

    private void OnDisable()
    {
        isChoseOnce = false;
    }

    private void MakeChoose(Collider collider, string triggerText)
    {
        _congrats.Play();
        isChoseOnce = true;
        cardAnimator.SetTrigger(triggerText);
        CardWork(collider);
        Invoke("MakeFade", 1.15f);
        _GameManager.Instance.InvokeClose();
    }

    private void MakeFade()
    {
        Fade.Instance.SetFade();
    }
}
