using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Unity.VisualScripting;

[RequireComponent(typeof(TMP_Text))]
public class InstructionsText : FloatEventListener
{
private TMP_Text textComponent;
    private List<string> fullText = new List<string>(10);
    private XRInputActions inputActions;
    private bool isTyping = false;
    private bool hasError = false;
    private bool activatedError = false;
    private bool isFirstTime2 = true;
    private bool isFirstTime5 = true;
    private bool isFirstTime6 = true;
    private bool isFirstTime10 = true;
    private bool isFirstTime14 = true;
    private bool isFirstTime15 = true;
    private bool isFirstTime16 = true;

    public int currTextInstructionIndex = 0;
    public float typingSpeed = 100.0f;
    public bool playOnAwake;

    public TextMeshProUGUI colorText;

    public SurfaceAccuracyChangeEventListener surfaceAccuracyChangeEventListener;

    public AudioClip[] instructionAudioClips; // Array to hold audio clips
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        textComponent = GetComponent<TMP_Text>();
        textComponent.text = "";
        if (playOnAwake) StartCoroutine(TypeText());

        inputActions = new XRInputActions();
    }

    void Start() {
        fullText.Add("Welcome to VR Painting. Let's learn to paint a soccer ball. Please press the A button on your right controller to continue to the next step.");
        fullText.Add("To get started with tracing the soccer ball, please select it by pointing your left controller at the soccer ball on the table and pressing the left grip button");
        fullText.Add("First, change your brush color to white by pressing the trigger on your left controller");
        fullText.Add("Position yourself close to the transparent model of the soccer ball by moving near the pedestal");
        fullText.Add("Adjust the size of your brush using the left joystick so that it is easy to make detailed paint strokes");
        fullText.Add("Move your right controller towards the soccerball model until it shows 100%. Use your right controller to trace over the white portions of the soccer ball. Try to keep your paint strokes as close to 100% accurate as possible while tracing the model");
        fullText.Add("Now change your brush color to black");
        fullText.Add("Lastly, fill in the black portions of the soccer ball while keeping as high an accuracy as possible");
        fullText.Add("Great work! If you want to try a more difficult model you can try tracing the mushroom by selecting it from the table in front of you");

        fullText.Add("To get started with tracing the mushroom, please make sure it is selected by pointing your left controller at the mushroom on the table and pressing the left grip button");
        fullText.Add("Change your brush color to yellow by pressing the trigger on your left controller");
        fullText.Add("Make sure the size of your brush is suiable for making detailed paint strokes");
        fullText.Add("Fill in the yellow areas of the mushroom stem with your right controller while maintaining as high an accuracy as possible");
        fullText.Add("Next, change your brush color to black and fill in the black spots on the stem");
        fullText.Add("Great job! The next step is to step change your brush color to red and fill in the red parts of the mushroom cap, make sure to fill in the underside of the cap as well");
        fullText.Add("Lastly, change your brush color to white and fill in the white areas on the mushroom cap");
        fullText.Add("Great work! You have finished the guided VR painting tutorial!");

        StartCoroutine(TypeText());
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        inputActions.Enable();
        inputActions.XRActions.NextInstruction.performed += OnNextInstruction;
        inputActions.XRActions.PrevInstruction.performed += OnPrevInstruction;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        inputActions.Disable();
        inputActions.XRActions.NextInstruction.performed -= OnNextInstruction;
        inputActions.XRActions.PrevInstruction.performed -= OnPrevInstruction;
    }


    protected override void HandleEvent(float param)
    {
        base.HandleEvent(param);
        if (param <= 0.6f && inputActions.XRActions.DrawAction.ReadValue<float>() > 0.1f)
        {
            hasError = true;
            activatedError = true;
            StopAllCoroutines();
            StartCoroutine(TypeText());
        }
        else
        {
            hasError = false;
        }
    }



    IEnumerator TypeText()
    {
        textComponent.text = "";
        string printText;
        // inputActions.XRActions.DrawAction.ReadValue<float>() > 0.1f
        if (hasError)
        {
            Debug.Log("Painting with error");
            hasError = false;
            textComponent.color = new Color32(200, 0, 0, 255);
            textComponent.text = "You have made a brush stroke that is too far from the model. Please undo your last stroke using the menu button on your left controller and try again. Press next to return to the instructions";
        }
        else
        {
            textComponent.color = new Color32(255, 255, 255, 255);
            Debug.Log(fullText[currTextInstructionIndex]);
            isTyping = true;
            string perc = surfaceAccuracyChangeEventListener.GetComponent<SurfaceAccuracyChangeEventListener>().percent;
            if(currTextInstructionIndex == 2 && colorText.text != "White" && !isFirstTime2) {
                printText = "You have not changed your brush color to white. Please try again by using the left trigger until the color is white";
            } else if(currTextInstructionIndex == 5 && perc != "100.00%" && !isFirstTime5) {
                printText = "Please move your right controller until the it says 100% to ensure you are in the right spot.";
            } else if(currTextInstructionIndex == 6 && colorText.text != "Black" && !isFirstTime6) {
                printText = "You have not changed your brush color to black. Please try again by using the left trigger until the color is black";
            } else if(currTextInstructionIndex == 10 && colorText.text != "Yellow" && !isFirstTime10) {
                printText = "You have not changed your brush color to yellow. Please try again by using the left trigger until the color is yellow";
            } else if(currTextInstructionIndex == 14 && colorText.text != "Black" && !isFirstTime14) {
                printText = "You have not changed your brush color to black. Please try again by using the left trigger until the color is black";
            } else if(currTextInstructionIndex == 15 && colorText.text != "Red" && !isFirstTime15) {
                printText = "You have not changed your brush color to red. Please try again by using the left trigger until the color is black";
            } else if(currTextInstructionIndex == 16 && colorText.text != "White" && !isFirstTime16) {
                printText = "You have not changed your brush color to white. Please try again by using the left trigger until the color is black";
            } else {
                printText = fullText[currTextInstructionIndex];
            }
            // Play the corresponding audio clip
            if (currTextInstructionIndex < instructionAudioClips.Length)
            {
                audioSource.clip = instructionAudioClips[currTextInstructionIndex];
                audioSource.Play();
            }
            foreach (char c in printText)
            {
                textComponent.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;
        }
    }


    private void OnNextInstruction(InputAction.CallbackContext context)
    {
        NextInstruction();
    }
    private void OnPrevInstruction(InputAction.CallbackContext context)
    {
        PrevInstruction();
    }

    public void NextInstruction()
    {
        Debug.Log("NextInstruction");
        if(currTextInstructionIndex == 2 && colorText.text != "White" && isFirstTime2 && !isTyping) {
            isFirstTime2 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        string perc = surfaceAccuracyChangeEventListener.GetComponent<SurfaceAccuracyChangeEventListener>().percent;

        if(currTextInstructionIndex == 5 && perc != "100.00%" && isFirstTime5 && !isTyping) {
            isFirstTime5 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 6 && colorText.text != "Black" && isFirstTime6 && !isTyping) {
            isFirstTime6 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 10 && colorText.text != "Yellow" && isFirstTime10 && !isTyping) {
            isFirstTime10 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 14 && colorText.text != "Black" && isFirstTime14 && !isTyping) {
            isFirstTime14 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 15 && colorText.text != "Red" && isFirstTime15 && !isTyping) {
            isFirstTime15 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 16 && colorText.text != "White" && isFirstTime16 && !isTyping) {
            isFirstTime16 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 2 && colorText.text != "White" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 5 && perc != "100.00%" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 6 && colorText.text != "Black" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 11 && colorText.text != "Yellow" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 14 && colorText.text != "Black" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 15 && colorText.text != "Red" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 16 && colorText.text != "White" && !hasError) {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if (activatedError)
        {
            activatedError = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }
        else if(currTextInstructionIndex < fullText.Count - 1 && !isTyping) {
            currTextInstructionIndex++;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }
    }
    public void PrevInstruction()
    {
        Debug.Log("PrevInstruction");

        if (currTextInstructionIndex > 0 && !isTyping)
        {
            currTextInstructionIndex--;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }
    }
}