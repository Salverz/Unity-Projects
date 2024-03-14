using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsController : MonoBehaviour
{
    public delegate void CreditsEnded();
    public static event CreditsEnded OnCreditsEnded;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GoToTitle", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GoToTitle()
    {
        OnCreditsEnded.Invoke();
    }
}
