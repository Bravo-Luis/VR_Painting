using UnityEngine;


public class TriggerEnterExitBoolRaiser : BoolEventRaiser
{

    private void OnTriggerEnter(Collider other)
    {
        TriggerEvent(true);
    }

    private void OnTriggerExit(Collider other)
    {
        TriggerEvent(false);
    }
}
