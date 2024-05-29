using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO eventChannel;
    [SerializeField] private bool oneShot = false;

    protected virtual void TriggerEvent()
    {
        Debug.Log($"void \"{eventChannel.description}\" broadcasted.");
        eventChannel.RaiseEvent();
        if (oneShot) Destroy(this);
    }
}
