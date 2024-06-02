using UnityEngine;

public class VoidEventRaiser : MonoBehaviour
{
    [SerializeField] protected VoidEventChannelSO eventChannel;
    [SerializeField] private bool oneShot = false;

    protected virtual void TriggerEvent()
    {
        Debug.Log($"void \"{eventChannel.description}\" is broadcasted by {gameObject.name}");
        eventChannel.RaiseEvent();
        if (oneShot) Destroy(this);
    }
}
