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

    public int currTextInstructionIndex = 0;
    public float typingSpeed = 100f;
    public bool playOnAwake;

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
        if (param <= 0.6f)
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
            textComponent.color = new Color32(200, 0, 0, 255);
            textComponent.text = "You have made a brush stroke that is too far from the model. Please undo your last stroke using the menu button on your left controller and try again. Press next to return to the instructions";
        }
        else
        {
            textComponent.color = new Color32(255, 255, 255, 255);
            Debug.Log(fullText[currTextInstructionIndex]);
            isTyping = true;
            foreach (char c in fullText[currTextInstructionIndex])
            {
                textComponent.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }
            isTyping = false;
        }
    }

    private void OnNextInstruction(InputAction.CallbackContext context)
    {
        Debug.Log("NextInstruction");

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
