using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ToggleUI : MonoBehaviour
{
    private XRInputActions inputActions;

    void Awake()
    {
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRActions.ToggleUI.performed += OnToggleUI;
    }

    void OnDisable()
    {
        inputActions.Disable();
        inputActions.XRActions.SwitchColor.performed -= OnToggleUI;
    }

    void Update()
    {

    }

    private void OnToggleUI(InputAction.CallbackContext context)
    {
        Debug.Log("Toggled UI");
    }
}
