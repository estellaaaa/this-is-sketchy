using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public Image backgroundImage;   
    public Sprite[] backgroundFrames; 
    public float frameSwitchInterval = 0.1f; 

    void Start()
    {
        Debug.Log("MainMenu script started");

        Time.timeScale = 0;
        mainMenuUI.SetActive(true);

        if (backgroundFrames.Length > 0)
        {
            Debug.Log("Starting background animation");
            StartCoroutine(AnimateBackground());
        }
        else
        {
            Debug.LogWarning("No background frames assigned");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        Time.timeScale = 1;
        SceneManager.LoadScene("Intro"); 
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        Application.Quit();
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    IEnumerator AnimateBackground()
    {
        int frameIndex = 0;
        while (true)
        {
            Debug.Log($"Switching to frame {frameIndex}");
            backgroundImage.sprite = backgroundFrames[frameIndex];
            frameIndex = (frameIndex + 1) % backgroundFrames.Length;
            yield return new WaitForSecondsRealtime(frameSwitchInterval);
        }
    }
}