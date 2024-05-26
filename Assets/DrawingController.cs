using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawingController : MonoBehaviour
{
    public GameObject lineRendererPrefab; // Prefab with a LineRenderer component
    public float brushWidth = 0.02f;
    public Transform rightHand; // Assign this in the Inspector

    private Color currentColor = Color.white; // Default color
    private XRInputActions inputActions;
    private LineRenderer currentLineRenderer;
    private List<Vector3> points = new List<Vector3>();
    private List<GameObject> lineRenderers = new List<GameObject>(); // Track created line renderers
    
    
    // List of colors to cycle through
    private List<Color> colorPalette = new List<Color> { Color.red, Color.green, Color.blue, Color.yellow, Color.magenta, Color.black , new Color(0.65f, 0.16f, 0.16f), new Color(1f, 0.65f, 0f), Color.white };
    private int currentColorIndex = 0;

void Awake()
    {
        inputActions = new XRInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        inputActions.XRActions.SwitchColor.performed += OnSwitchColor;
        inputActions.XRActions.Undo.performed += OnUndo; // Add undo action
    }

    void OnDisable()
    {
        inputActions.Disable();
        inputActions.XRActions.SwitchColor.performed -= OnSwitchColor;
        inputActions.XRActions.Undo.performed -= OnUndo; // Remove undo action
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
        lineRenderers.Add(lineRendererObject); // Track the line renderer
        
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
        // Cycle to the next color in the list
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
}
