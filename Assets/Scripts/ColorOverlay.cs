using UnityEngine;
using TMPro; 

public class ColorOverlay : MonoBehaviour
{
    public TextMeshProUGUI colorText;
    private DrawingController drawingController;

    void Start()
    {
        drawingController = FindObjectOfType<DrawingController>();

        if (drawingController == null)
        {
            Debug.LogError("DrawingController not found in the scene.");
        }
    }

    void Update()
    {
        if (drawingController != null)
        {
            Color currentColor = drawingController.GetCurrentColor();
            colorText.text = $"{ColorToString(currentColor)}";
            colorText.color = currentColor;
            SendMessage("TriggerEvent", (Vector4)currentColor);
        }
    }

    private string ColorToString(Color color)
    {
        if (color == Color.red)
            return "Red";
        if (color == Color.green)
            return "Green";
        if (color == Color.blue)
            return "Blue";
        if (color == Color.yellow)
            return "Yellow";
        if (color == Color.magenta)
            return "Magenta";
        if (color == Color.white)
            return "White";
        if (color == Color.black)
            return "Black";
        if (color == new Color(1f, 0.65f, 0f))
            return "Orange";
        if (color == new Color(0.65f, 0.16f, 0.16f))
            return "Brown";
        return "Color";
    }
}
