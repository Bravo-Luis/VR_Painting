using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "EventChannelSOs/Void Channel", fileName = "VoidEventChannelSO")]
public class VoidEventChannelSO : DescriptionSO
{
    private UnityAction OnEventRaised;
    public int numListeners;


    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }

    public void AddAction(UnityAction action)
    {
        OnEventRaised += action;
    }
    public void RemoveAction(UnityAction action)
    {
        OnEventRaised -= action;
    }
}
