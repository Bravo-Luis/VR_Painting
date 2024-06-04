using UnityEngine;

public class LastColorChangeListener : Vector4EventListener
{
    public Vector4 lastColorRecieved;

    protected override void HandleEvent(Vector4 param)
    {
        base.HandleEvent(param);
        lastColorRecieved = param;  
    }
}
