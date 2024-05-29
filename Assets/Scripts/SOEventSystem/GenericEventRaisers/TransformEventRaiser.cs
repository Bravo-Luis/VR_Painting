using UnityEngine;
public class TransformEventRaiser : GenericEventRaiser<Transform>
{
    private void TriggerEvent()
    {
        TriggerEvent(transform);
    }
}