using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO eventChannel;

    protected virtual void TriggerEvent()
    {
        Debug.Log($"void event  \"{eventChannel.description}\" is triggered.");
        eventChannel.RaiseEvent();
    }
}
