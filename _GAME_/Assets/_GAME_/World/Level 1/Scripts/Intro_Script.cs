using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroTextController : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public Image backgroundImage;    
    public Sprite[] backgroundFrames; 
    public float frameSwitchInterval = 0.1f; 
    public float fadeDuration = 2.0f; 
    public string nextSceneName = "Phase 1"; 
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
    private CanvasGroup fadeCanvasGroup; 

    void Start()
    {
        CreateFadeCanvas();

        DisplayText();
        if (backgroundFrames.Length > 0)
        {
            StartCoroutine(AnimateBackground());
        }
    }

    void Update()
    {
        if (Input.anyKeyDown)
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
        while (true)
        {
            backgroundImage.sprite = backgroundFrames[frameIndex];
            frameIndex = (frameIndex + 1) % backgroundFrames.Length;
            yield return new WaitForSeconds(frameSwitchInterval);
        }
    }

    IEnumerator EndIntro()
    {
        yield return StartCoroutine(FadeScreen(0f, 1f));

        yield return new WaitForSeconds(3f);

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

        GameObject fadeImageObject = new GameObject("FadeImage");
        fadeImageObject.transform.SetParent(fadeCanvas.transform, false);

        Image fadeImage = fadeImageObject.AddComponent<Image>();
        fadeImage.color = Color.black;
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
