using UnityEngine;

public class Fade : MonoBehaviour
{
    public static Fade Instance { get; private set; }

    private Animator _animator;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetFade()
    {
        _animator.SetTrigger("Fade");
    }
}
