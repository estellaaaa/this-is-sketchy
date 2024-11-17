using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public Image backgroundImage;    // Background image
    public Sprite[] backgroundFrames; // Array of background frames (sprites)
    public float frameSwitchInterval = 0.1f; // Interval for switching background frames
    public Font customFont; // Custom font for buttons

    void Start()
    {
        Debug.Log("MainMenu script started");

        // Pause the game when the main menu is active
        Time.timeScale = 0;
        mainMenuUI.SetActive(true);

        // Start background animation if frames are available
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
        // Check for Escape key press to resume the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResumeGame();
        }
    }

    public void StartGame()
    {
        Debug.Log("StartGame called");
        // Resume the game when starting
        Time.timeScale = 1;
        SceneManager.LoadScene("Intro"); // Replace "Intro" with the name of your game scene
    }

    public void QuitGame()
    {
        Debug.Log("QuitGame called");
        Application.Quit();
    }

    public void ResumeGame()
    {
        Debug.Log("ResumeGame called");
        // Resume the game
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("MainMenu"); // Unload the main menu scene
    }

    IEnumerator AnimateBackground()
    {
        int frameIndex = 0;
        while (true) // Infinite loop for continuous animation
        {
            Debug.Log($"Switching to frame {frameIndex}");
            backgroundImage.sprite = backgroundFrames[frameIndex];
            frameIndex = (frameIndex + 1) % backgroundFrames.Length;
            yield return new WaitForSecondsRealtime(frameSwitchInterval);
        }
    }
}