using System.Collections;
using UnityEngine;

public class FXDestroy : MonoBehaviour
{
    public float time;

    private void Update()
    {
        if(Time.timeScale == 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
