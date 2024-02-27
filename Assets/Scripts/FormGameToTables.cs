using UnityEngine;

public class FormGameToTables : MonoBehaviour
{
    public GameObject tablesZone;
    public GameObject game;
    public AudioSource _tap;

    public void OpenGameWithFade()
    {
        _tap.Play();
        Fade.Instance.SetFade();
        Invoke("OpenGame", 0.35f);
    }

    public void OpenTablesZoneWithFade()
    {
        _tap.Play();
        Fade.Instance.SetFade();
        Invoke("OpenTableZone", 0.35f);
    }

    private void OpenGame()
    {
        game.SetActive(true);
        tablesZone.SetActive(false);
    }

    private void OpenTableZone()
    {
        tablesZone.SetActive(true);
        game.SetActive(false);
    }
}
