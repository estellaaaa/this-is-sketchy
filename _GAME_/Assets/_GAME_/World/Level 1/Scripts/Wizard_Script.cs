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

    private string[] newTaskTexts = new string[]
    {
        "Great job collecting the sticks!",
        "Now, you must find the magic stone to complete the pencil.",
        "The magic stone is hidden deep in the forest.",
        "Good luck, hero!",
    };

    private int currentTextIndex = 0;
    private bool nearWizard = false;
    public float interactionDistance = 2.0f; // Distance within which the player can interact
    private Transform playerTransform;
    private bool isDisplayingText = false;
    private bool initialTaskStarted = false;

    public static WizardScript Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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

        // Show the initial task box
        UpdateTaskBox();
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
                    CheckTaskCompletion();
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

                    // Update the task box text based on the current task
                    UpdateTaskBox();
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

    void CheckTaskCompletion()
    {
        if (TaskBoxController.Instance.IsGoalReached("Stick"))
        {
            Debug.Log("Task completed: All sticks collected.");
            wizardTexts = newTaskTexts;
            currentTextIndex = 0;

            // Hide the old task box
            TaskBoxController.Instance.HideTaskBox();

            // Set the new task goal
            TaskBoxController.Instance.SetItemGoal("Rubber", 15);

            // Show the new task box
            TaskBoxController.Instance.ShowTaskBox("find the magic stone to complete the pencil");

            // Update the task box text
            TaskBoxController.Instance.UpdateTaskBoxText();
        }
        else
        {
            Debug.Log("Task not completed yet.");

            // Start the initial task if it hasn't been started yet
            if (!initialTaskStarted)
            {
                initialTaskStarted = true;
                StartInitialTask();
            }
        }
    }

    void UpdateTaskBox()
    {
        if (TaskBoxController.Instance != null)
        {
            if (TaskBoxController.Instance.IsGoalReached("Stick"))
            {
                TaskBoxController.Instance.ShowTaskBox("find the magic stone to complete the pencil");
            }
            else
            {
                TaskBoxController.Instance.ShowTaskBox("find the 4 sticks to fix the pencil");
            }
        }
    }

    void StartInitialTask()
    {
        // Notify the ItemSpawner to start spawning sticks
        ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner>();
        if (itemSpawner != null)
        {
            itemSpawner.StartSpawningSticks();
        }
    }
}