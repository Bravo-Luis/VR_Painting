using UnityEngine;

public abstract class GenericEventRaiser<T> : MonoBehaviour
{
    [SerializeField] private GenericEventChannelSO<T> eventChannel;
    [SerializeField] private T defaultValue;

    protected virtual void TriggerEvent(T param)
    {
        Debug.Log($"param {param} is broadcasted through \"{eventChannel.description}\"");
        eventChannel.RaiseEvent(param);
    }

    private void TriggerEvent()
    {
        TriggerEvent(defaultValue);
    }
}




