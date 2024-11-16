using UnityEngine;

public class WizardScript : MonoBehaviour
{
    private string[] wizardTexts = new string[]
    {
        "Welcome, hero!",
        "I have been waiting for you.",
        "Your journey is just beginning.",
        "You must defeat the evil that plagues this land.",
        "For that, you will need to create tools",
        "and gather items",
        "unfortunately, the pencil which I used to create you broke during the process",
        "so you will have to find the pieces to repair it",
        "walk around the to find the pieces of wood",
    };

    private int currentTextIndex = 0;
    private bool nearWizard = false;
    public float interactionDistance = 2.0f; // Distance within which the player can interact
    private Transform playerTransform;
    private bool isDisplayingText = false;

    void Start()
    {
        Debug.Log("WizardScript Start");
        // Find the player GameObject by tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
            Debug.Log("Player found: " + player.name);
        }
        else
        {
            Debug.LogError("Player not found");
        }
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Check the distance between the player and the wizard
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            nearWizard = distance <= interactionDistance;

            if (nearWizard && !isDisplayingText)
            {
                Debug.Log("Player is near the wizard");

                // Check if the "Interact" button is pressed
                if (Input.GetButtonDown("Interact"))
                {
                    Debug.Log("E key pressed near wizard");
                    DisplayWizardText();
                }
            }

            // Advance text on any key press if text is being displayed
            if (isDisplayingText && Input.anyKeyDown)
            {
                currentTextIndex++;
                if (currentTextIndex < wizardTexts.Length)
                {
                    DisplayWizardText();
                }
                else
                {
                    // Hide the text box when all texts have been displayed
                    Debug.Log("Hiding text box after displaying all texts");
                    TextBoxController.Instance.HideTextBox();
                    isDisplayingText = false;
                    currentTextIndex = 0; // Reset the index for future interactions

                    // Show the task box again
                    if (TaskBoxController.Instance != null)
                    {
                        Debug.Log("Showing task box again");
                        TaskBoxController.Instance.ShowTaskBox("find the 4 sticks to fix the pencil");
                    }
                }
            }
        }
    }

    void DisplayWizardText()
    {
        Debug.Log("DisplayWizardText called");
        if (currentTextIndex < wizardTexts.Length)
        {
            Debug.Log($"Displaying wizard text[{currentTextIndex}]: {wizardTexts[currentTextIndex]}");

            // Check TextBoxController references
            if (TextBoxController.Instance != null)
            {
                // Hide the task box if it is active
                if (TaskBoxController.Instance != null && TaskBoxController.Instance.taskBox.activeSelf)
                {
                    Debug.Log("Task box is active, hiding it now");
                    TaskBoxController.Instance.HideTaskBox();
                    Debug.Log("Task box hidden");
                }
                else
                {
                    Debug.Log("Task box is not active or TaskBoxController.Instance is null");
                }

                Debug.Log("Showing text box");
                TextBoxController.Instance.ShowTextBox(wizardTexts[currentTextIndex]);
                Debug.Log($"Wizard text shown: {wizardTexts[currentTextIndex]}");
                isDisplayingText = true;
            }
            else
            {
                Debug.LogError("TextBoxController Instance is null!");
            }
        }
    }
}