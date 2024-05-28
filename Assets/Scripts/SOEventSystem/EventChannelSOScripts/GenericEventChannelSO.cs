using UnityEngine.Events;

public abstract class GenericEventChannelSO<T> : DescriptionSO
{
    private UnityAction<T> OnEventRaised;
    public int numListeners;

    public void RaiseEvent(T parameter)
    {
        OnEventRaised?.Invoke(parameter);
    }

    public void AddAction(UnityAction<T> action)
    {
        OnEventRaised += action;
    }
    public void RemoveAction(UnityAction<T> action)
    {
        OnEventRaised -= action;
    }
}




