using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoLogic : MonoBehaviour
{
    public TMPro.TextMeshProUGUI titleText;

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
