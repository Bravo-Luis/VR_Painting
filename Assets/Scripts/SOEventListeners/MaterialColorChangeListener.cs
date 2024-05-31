using UnityEngine;

public class MaterialColorChangeListener : Vector4EventListener
{
    Material materialCopy;

    private void Awake()
    {
        Renderer renderer = GetComponent<Renderer>();
        materialCopy = new Material(renderer.material);
        renderer.material = materialCopy;
    }

    protected override void HandleEvent(Vector4 param)
    {
        base.HandleEvent(param);
        materialCopy.color = param;
    }
}
