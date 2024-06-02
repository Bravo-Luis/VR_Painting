using UnityEngine;


public abstract class GenericEventRaiser<T> : MonoBehaviour
{
    [SerializeField] protected GenericEventChannelSO<T> eventChannel;

    protected virtual void TriggerEvent(T param)
    {
        Debug.Log($"param {param} is broadcasted through \"{eventChannel.description}\" by {gameObject.name}");
        eventChannel.RaiseEvent(param);
    }
}




