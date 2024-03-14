using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLogic : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;

    public void Start()
    {
        EnemyController.OnAllEnemiesDied += GoToCredits;
        Player.OnPlayerDiedEvent += GoToCredits;
        CreditsController.OnCreditsEnded += SwitchToTitle;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);     
    }

    public void ConsoleTest()
    {
        Debug.Log("Console test");
    }

    public void StartGame()
    {
        Debug.Log("Running");
        StartCoroutine(FindPlayer());
    }

    public void GoToCredits()
    {
        StartCoroutine(SwitchToCredits());
    }

    IEnumerator SwitchToCredits()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("Credits");
        while (!asyncOp.isDone)
        {
            yield return null;

        }
    }

    void SwitchToTitle()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    IEnumerator FindPlayer()
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync("Game");
        while (!asyncOp.isDone)
        {
            yield return null;

        }
        GameObject playerObj = GameObject.Find("Player");
        Debug.Log(playerObj);
    }
}
