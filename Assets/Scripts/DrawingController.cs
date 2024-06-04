using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DrawingController : MonoBehaviour
{
    public GameObject lineRendererPrefab; 
    public float brushWidth = 0.02f;
    public Transform rightHand; 

    public float minBrushWidth = 0.01f; 
    public float maxBrushWidth = 0.1f; 
    private float brushSizeSensitivity = 0.005f; 

    private Color currentColor = Color.white;
    private XRInputActions inputActions;
    private LineRenderer currentLineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private List<GameObject> lineRenderers = new List<GameObject>();
    
    private List<Color> colorPalette = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.black , new Color(0.65f, 0.16f, 0.16f), new Color(1f, 0.65f, 0f), Color.white };
    private int currentColorIndex = 0;
    public GameObject[] textElements;


    void Awake()
    {
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRActions.SwitchColor.performed += OnSwitchColor;
        inputActions.XRActions.Undo.performed += OnUndo; 
        inputActions.XRActions.ToggleUI.performed += OnToggleUI;
    }

    void OnDisable()
    {
        inputActions.Disable();
        inputActions.XRActions.SwitchColor.performed -= OnSwitchColor;
        inputActions.XRActions.Undo.performed -= OnUndo; 
        inputActions.XRActions.ToggleUI.performed -= OnToggleUI;
    }

    void Update()
    {
        if (inputActions.XRActions.DrawAction.ReadValue<float>() > 0.1f)
        {
            if (currentLineRenderer == null)
            {
                StartNewLine();
            }
            UpdateLine();
        }
        else
        {
            currentLineRenderer = null;
        }

        UpdateBrushSize(); 
    }

    void StartNewLine()
    {
        if (lineRendererPrefab == null)
        {
            Debug.LogError("lineRendererPrefab is not assigned!");
            return;
        }
        
        GameObject lineRendererObject = Instantiate(lineRendererPrefab);
        currentLineRenderer = lineRendererObject.GetComponent<LineRenderer>();
        lineRenderers.Add(lineRendererObject); 
        
        if (currentLineRenderer == null)
        {
            Debug.LogError("LineRenderer component missing on the instantiated prefab!");
            return;
        }
        
        currentLineRenderer.startWidth = brushWidth;
        currentLineRenderer.endWidth = brushWidth;
        currentLineRenderer.material.color = currentColor;
        points.Clear();
    }

    void UpdateLine()
    {
        if (rightHand == null)
        {
            Debug.LogError("rightHand is not assigned!");
            return;
        }
        
        Vector3 handPosition = rightHand.position;
        
        if (points.Count == 0 || Vector3.Distance(points[points.Count - 1], handPosition) > 0.01f)
        {
            if (points.Count > 0)
            {
                Vector3 lastPoint = points[points.Count - 1];
                float distance = Vector3.Distance(lastPoint, handPosition);
                int numPoints = Mathf.CeilToInt(distance / 0.01f);
                for (int i = 1; i < numPoints; i++)
                {
                    Vector3 interpolatedPoint = Vector3.Lerp(lastPoint, handPosition, (float)i / numPoints);
                    points.Add(interpolatedPoint);
                }
            }
            points.Add(handPosition);
            currentLineRenderer.positionCount = points.Count;
            currentLineRenderer.SetPositions(points.ToArray());
        }
    }

    void UpdateBrushSize()
    {
        Vector2 joystickInput = inputActions.XRActions.BrushSize.ReadValue<Vector2>();
        float brushSizeChange = joystickInput.y * brushSizeSensitivity; 
        brushWidth += brushSizeChange;
        brushWidth = Mathf.Clamp(brushWidth, minBrushWidth, maxBrushWidth);

        if (currentLineRenderer != null)
        {
            currentLineRenderer.startWidth = brushWidth;
            currentLineRenderer.endWidth = brushWidth;
        }
    }

    public float GetBrushWidth()
    {
        return brushWidth;
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }

    private void OnSwitchColor(InputAction.CallbackContext context)
    {

        currentColorIndex = (currentColorIndex + 1) % colorPalette.Count;
        currentColor = colorPalette[currentColorIndex];
        Debug.Log("Switched color to: " + currentColor);
    }

    private void OnUndo(InputAction.CallbackContext context)
    {
        if (lineRenderers.Count > 0)
        {
            GameObject lastLineRenderer = lineRenderers[lineRenderers.Count - 1];
            lineRenderers.RemoveAt(lineRenderers.Count - 1);
            Destroy(lastLineRenderer);
            Debug.Log("Undo last action");
        }
    }
    private void OnToggleUI(InputAction.CallbackContext context)
    {
        foreach (GameObject textElement in textElements)
        {
            textElement.SetActive(!textElement.activeSelf);
        }
        Debug.Log("Toggled UI");
    }
}
