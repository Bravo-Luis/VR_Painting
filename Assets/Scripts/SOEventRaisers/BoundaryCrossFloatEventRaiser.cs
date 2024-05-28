using UnityEngine;

public class BoundaryCrossFloatEventRaiser : FloatEventRaiser
{
    [SerializeField] private float value;

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent(value);
    }
    private void OnTriggerExit(Collider other)
    {
        TriggerEvent(value);
    }
}