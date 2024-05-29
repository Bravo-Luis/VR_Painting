using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class InstructionsText : VoidEventListener
{
    private TMP_Text textComponent;
    private List<string> fullText = new List<string>(10);
    private XRInputActions inputActions;
    private bool isTyping = false;

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
        fullText.Add("Welcome to VR Painting. Let's learn to paint a soccer ball. Press next to continue.");
        fullText.Add("To start tracing the Soccer Ball model please click next after you finish each step");
        fullText.Add("First, change your brush color to white by pressing the trigger on your left controller");
        fullText.Add("Position yourself close to the transparent model of the soccer ball");
        fullText.Add("Adjust the size of your brush using the (    ) button on the left controller so that it is easy to make small paint strokes");
        fullText.Add("Use your right controller to fill in the white portions of the soccer ball");
        fullText.Add("Change your brush color to black");
        fullText.Add("Now fill in the black portions of the soccer ball");
        fullText.Add("Great work! If you want to try a more difficult model you can try tracing the mushroom by selecting (    )");

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

    protected override void HandleEvent()
    {
        base.HandleEvent();
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }


    IEnumerator TypeText()
    {
        textComponent.text = "";
        Debug.Log(fullText[currTextInstructionIndex]);
        isTyping = true;
        foreach (char c in fullText[currTextInstructionIndex])
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
    }

    private void OnNextInstruction(InputAction.CallbackContext context)
    {
        Debug.Log("NextInstruction");

        if(currTextInstructionIndex < fullText.Count - 1 && !isTyping) {
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
