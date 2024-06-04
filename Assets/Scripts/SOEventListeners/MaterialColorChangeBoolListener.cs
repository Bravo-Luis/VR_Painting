using UnityEngine;

public class MaterialColorChangeBoolListener : BoolEventListener
{
    [SerializeField] private Color trueColor;
    [SerializeField] private Color falseColor;
    Material materialCopy;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        materialCopy = new Material(renderer.material);
        renderer.material = materialCopy;
    }

    protected override void HandleEvent(bool param)
    {
        base.HandleEvent(param);
        materialCopy.color = param ? trueColor : falseColor;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        materialCopy.color = falseColor;
    }
}
