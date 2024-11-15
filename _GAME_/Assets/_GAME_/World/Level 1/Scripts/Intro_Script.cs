using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class IntroTextController : MonoBehaviour
{
    public TextMeshProUGUI textField; // Text display for the intro
    public Image backgroundImage;    // Background image
    public Sprite[] backgroundFrames; // Array of background frames (sprites)
    public float frameSwitchInterval = 0.1f;

    public string[] introTexts = new string[]
    {
        "wak...p",
        "<b>*you hear a voice in the distance*</b>",
        "wake up",
        "...",
        "<i>ughh...</i>",
        "WAKE UP!1!!!1",
        "<i>what?</i>",
        "we have not much time",
        "<i>where... am i?</i>",
        "<b>*your eyes feel heavy but they start to open slowly*</b>",
        "...",
    };

    private int currentTextIndex = 0;

    void Start()
    {
        // Display the first text and set the initial background frame
        if (introTexts.Length > 0)
        {
            DisplayText();
        }
        else
        {
            Debug.LogWarning("No intro texts provided!");
        }

        // Start the background animation
        if (backgroundFrames.Length > 0)
        {
            StartCoroutine(AnimateBackground());
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) // Advance on any key press
        {
            currentTextIndex++;
            if (currentTextIndex < introTexts.Length)
            {
                DisplayText();
            }
            else
            {
                EndIntro();
            }
        }
    }

    void DisplayText()
    {
        textField.text = introTexts[currentTextIndex];
    }

    IEnumerator AnimateBackground()
    {
        int frameIndex = 0;

        while (true) // Infinite loop for continuous animation
        {
            // Cycle through frames
            backgroundImage.sprite = backgroundFrames[frameIndex];
            frameIndex = (frameIndex + 1) % backgroundFrames.Length; // Loop back to first frame
            yield return new WaitForSeconds(frameSwitchInterval); // Wait before switching to next frame
        }
    }

    void EndIntro()
    {
        // Action after intro (e.g., transition to another scene)
        Debug.Log("Intro finished!");
        // UnityEngine.SceneManagement.SceneManager.LoadScene("Phase 1"); // Replace with your scene name
    }
}
