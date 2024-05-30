using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class InstructionsText : FloatEventListener
{
    private TMP_Text textComponent;
    private List<string> fullText = new List<string>(10);
    private XRInputActions inputActions;
    private bool isTyping = false;
    private bool hasError = false;
    private bool activatedError = false;
    private bool isFirstTime1 = true;
    private bool isFirstTime4 = true;
    private bool isFirstTime5 = true;
    private bool userCorrect = false;

    public int currTextInstructionIndex = 0;
    public float typingSpeed = 100.0f;
    public bool playOnAwake;

    public TextMeshProUGUI colorText;
    public TMP_Text collisionText;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        textComponent.text = "";
        if (playOnAwake) StartCoroutine(TypeText());

        inputActions = new XRInputActions();
    }

    void Start() {
        fullText.Add("Welcome to VR Painting. Let's learn to paint a soccer ball. Please press the A button on your right controller to continue to the next step.");
        fullText.Add("First, change your brush color to white by pressing the trigger on your left controller");
        fullText.Add("Position yourself close to the transparent model of the soccer ball by moving near the pedestal");
        fullText.Add("Adjust the size of your brush using the left joystick so that it is easy to make detailed paint strokes");
        fullText.Add("Move your right controller towards the soccerball model until it shows 100%. Use your right controller to trace over the white portions of the soccer ball. Try to keep your paint strokes as close to 100% accurate as possible while tracing the model");
        fullText.Add("Now change your brush color to black");
        fullText.Add("Lastly, fill in the black portions of the soccer ball while keeping as high an accuracy as possible");
        fullText.Add("Great work! If you want to try a more difficult model you can try tracing the mushroom by selecting it from the table in front of you");

        StartCoroutine(TypeText());
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRActions.NextInstruction.performed += OnNextInstruction;
        inputActions.XRActions.PrevInstruction.performed += OnPrevInstruction;
    }

    void OnDisable()
    {
        inputActions.Disable();
        inputActions.XRActions.NextInstruction.performed -= OnNextInstruction;
        inputActions.XRActions.PrevInstruction.performed -= OnPrevInstruction;
    }

    protected override void HandleEvent(float arg)
    {
        base.HandleEvent(arg);
        if (arg <= 0.5f)
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
        if (hasError)
        {
            hasError = false;
            printText = "You have made a brush stroke that is too far from the model. Please undo your last stroke using the menu button on your left controller and try again. Press next to return to the instructions";
        }
        else
        {
            printText = fullText[currTextInstructionIndex];
        }

        userCorrect = false;
        if(currTextInstructionIndex == 1 && colorText.text != "White" && !isFirstTime1) {
            printText = "You have not changed your brush color to white. Please try again by using the left trigger until the color is white";
        } else if(currTextInstructionIndex == 4 && collisionText.text != "100%" && !isFirstTime4) {
            printText = "Please move your right controller until the it says 100% to ensure you are in the right spot.";
        } else if(currTextInstructionIndex == 5 && colorText.text != "Black" && !isFirstTime5) {
            printText = "You have not changed your brush color to black. Please try again by using the left trigger until the color is black";
        } else {
            userCorrect = true;
        }

        Debug.Log(fullText[currTextInstructionIndex]);
        isTyping = true;
        foreach (char c in printText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        isTyping = false;
    }

    private void OnNextInstruction(InputAction.CallbackContext context)
    {
        Debug.Log("NextInstruction");
        userCorrect = false;
        if(currTextInstructionIndex == 1 && colorText.text != "White" && isFirstTime1 && !isTyping) {
            isFirstTime1 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 4 && collisionText.text != "100%" && isFirstTime4 && !isTyping) {
            isFirstTime4 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 5 && colorText.text != "Black" && isFirstTime5 && !isTyping) {
            isFirstTime5 = false;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }

        if(currTextInstructionIndex == 1 && colorText.text != "White") {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 4 && collisionText.text != "100%") {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if(currTextInstructionIndex == 5 && colorText.text != "Black") {
            new WaitForSeconds(typingSpeed);
            return;
        }

        if (activatedError && !isTyping)
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

    private void OnPrevInstruction(InputAction.CallbackContext context)
    {
        Debug.Log("PrevInstruction");

        if(currTextInstructionIndex > 0 && !isTyping) {
            currTextInstructionIndex--;
            StartCoroutine(TypeText());
            new WaitForSeconds(typingSpeed);
        }
    }
}
