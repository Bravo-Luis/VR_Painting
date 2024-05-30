using UnityEngine;

public class IntEventRaiser : GenericEventRaiser<int>
{
    [SerializeField] private int defaultParam;

    private void TriggerEvent()
    {
        TriggerEvent(defaultParam);
    }
}