using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO eventChannel;

    protected virtual void TriggerEvent()
    {
        Debug.Log($"void \"{eventChannel.description}\" broadcasted.");
        eventChannel.RaiseEvent();
    }
}
