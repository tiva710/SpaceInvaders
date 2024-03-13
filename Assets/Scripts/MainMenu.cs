using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public static MainMenu Instance { get; private set; } //1 instance throughout the game
    void Awake()
    {
        
        //does instance exist? If yes, destroy. 
        if (Instance == null)
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        }else if (Instance != null)
        {
            Destroy(gameObject);
        }
       
    }

    public void StartGame()
    {
        SceneManager.LoadScene("DemoScene");
    }

    public void CreditsTime()
    {
        SceneManager.LoadScene("CreditsScene");

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene("MainMenu");
    }
}
