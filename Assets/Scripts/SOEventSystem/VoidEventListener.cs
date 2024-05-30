using UnityEngine;


public class VoidEventListener : MonoBehaviour
{
    [SerializeField] protected VoidEventChannelSO eventChannel;

    protected virtual void OnEnable()
    {
        eventChannel.OnEventRaised += HandleEvent;
    }
    protected virtual void OnDisable()
    {
        eventChannel.OnEventRaised -= HandleEvent;
    }
    protected virtual void HandleEvent()
    {
        Debug.Log($"{gameObject.name} is notified by void channel \"{eventChannel.description}\"");
    }
}
