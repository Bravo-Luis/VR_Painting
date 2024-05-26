using UnityEngine;
using UnityEngine.UI;

public class BrushSizeIndicatorController : MonoBehaviour
{
    public DrawingController drawingController; 
    public RectTransform brushSizeIndicator; 
    public Image brushSizeImage;
    public float minIndicatorSize = 10f; 
    public float maxIndicatorSize = 100f; 

    private void Update()
    {
        if (drawingController != null && brushSizeIndicator != null)
        {

            float brushWidth = drawingController.GetBrushWidth();
            float minBrushWidth = drawingController.minBrushWidth;
            float maxBrushWidth = drawingController.maxBrushWidth;

            float t = Mathf.InverseLerp(minBrushWidth, maxBrushWidth, brushWidth);
            float indicatorSize = Mathf.Lerp(minIndicatorSize, maxIndicatorSize, t);

            brushSizeIndicator.sizeDelta = new Vector2(indicatorSize, indicatorSize);
            
            brushSizeImage.color = drawingController.GetCurrentColor();
        }
    }
}
