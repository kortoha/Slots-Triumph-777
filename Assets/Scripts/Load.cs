using UnityEngine;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour
{
    private float _remainingTime;

    private void Awake()
    {
        _remainingTime = Random.Range(3, 5);
    }

    private void Update()
    {
        _remainingTime -= Time.deltaTime;

        if(_remainingTime <= 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}
