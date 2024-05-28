using UnityEngine;

public abstract class GenericEventRaiser<T> : MonoBehaviour
{
    [SerializeField] private GenericEventChannelSO<T> eventChannel;
    [SerializeField] private T defaultValue;

    protected virtual void TriggerEvent(T param)
    {
        Debug.Log($"event \"{eventChannel.description}\" with param {param} is triggered.");
        eventChannel.RaiseEvent(param);
    }

    private void TriggerEvent()
    {
        TriggerEvent(defaultValue);
    }
}




