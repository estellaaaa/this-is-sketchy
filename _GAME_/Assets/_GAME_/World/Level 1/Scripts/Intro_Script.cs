using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTextController : MonoBehaviour
{
    public TextMeshProUGUI textField; // Text display for the intro
    public Image backgroundImage;    // Background image
    public Sprite[] backgroundFrames; // Array of background frames (sprites)
    public float frameSwitchInterval = 0.1f; // Interval for switching background frames
    public float fadeDuration = 2.0f; // Duration of the fade effect
    public string nextSceneName = "Phase 1"; // Name of the next scene to load
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
    private CanvasGroup fadeCanvasGroup; // Fade canvas for transition

    void Start()
    {
        // Create fade canvas for transitions
        CreateFadeCanvas();

        // Display the first text and start background animation
        DisplayText();
        if (backgroundFrames.Length > 0)
        {
            StartCoroutine(AnimateBackground());
        }
    }

    void Update()
    {
        if (Input.anyKeyDown) // Advance text on any key press
        {
            currentTextIndex++;
            if (currentTextIndex < introTexts.Length)
            {
                DisplayText();
            }
            else
            {
                StartCoroutine(EndIntro());
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
            backgroundImage.sprite = backgroundFrames[frameIndex];
            frameIndex = (frameIndex + 1) % backgroundFrames.Length;
            yield return new WaitForSeconds(frameSwitchInterval);
        }
    }

    IEnumerator EndIntro()
    {
        // Fade to black
        yield return StartCoroutine(FadeScreen(0f, 1f));

        // Wait for 3 seconds before switching scenes
        yield return new WaitForSeconds(3f);

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }

    void CreateFadeCanvas()
    {
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        Canvas canvas = fadeCanvas.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        fadeCanvasGroup = fadeCanvas.AddComponent<CanvasGroup>();
        fadeCanvasGroup.alpha = 0f;

        fadeCanvas.AddComponent<GraphicRaycaster>();

        // Create a black fullscreen image
        GameObject fadeImageObject = new GameObject("FadeImage");
        fadeImageObject.transform.SetParent(fadeCanvas.transform, false);

        Image fadeImage = fadeImageObject.AddComponent<Image>();
        fadeImage.color = Color.black; // Black screen
        RectTransform rectTransform = fadeImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
    }

    IEnumerator FadeScreen(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = endAlpha;
    }
}
