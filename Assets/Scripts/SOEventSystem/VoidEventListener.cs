using UnityEngine;


public class VoidEventListener : MonoBehaviour
{
    [SerializeField] protected VoidEventChannelSO eventChannel;

    public void Subscribe(VoidEventChannelSO channel)
    {
        Unsubscribe();
        eventChannel = channel;
        if (eventChannel != null)
        {
            eventChannel.AddAction(HandleEvent);
            eventChannel.numListeners++;
        }
    }
    private void Unsubscribe()
    {
        if (eventChannel != null)
        {
            eventChannel.RemoveAction(HandleEvent);
            eventChannel.numListeners--;
        }
    }
    private void OnEnable()
    {
        Subscribe(eventChannel);
    }
    private void OnDisable()
    {
        Subscribe(null);
    }
    protected virtual void HandleEvent()
    {
        Debug.Log($"{gameObject.name} is notified by void channel \"{eventChannel.description}\"");
    }
}
