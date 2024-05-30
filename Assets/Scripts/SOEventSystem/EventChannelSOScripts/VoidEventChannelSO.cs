using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "EventChannelSOs/Void Channel", fileName = "VoidEventChannelSO")]
public class VoidEventChannelSO : DescriptionSO
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
