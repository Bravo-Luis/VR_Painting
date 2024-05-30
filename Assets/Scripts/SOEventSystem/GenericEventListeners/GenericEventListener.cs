using UnityEngine;


public abstract class GenericEventListener<T> : MonoBehaviour
{
    [SerializeField] protected GenericEventChannelSO<T> eventChannel;


    protected virtual void OnEnable()
    {
        eventChannel.OnEventRaised += HandleEvent;
    }
    protected virtual void OnDisable()
    {
        eventChannel.OnEventRaised -= HandleEvent;
    }
    protected virtual void HandleEvent(T param)
    {
        Debug.Log($"{gameObject.name} receives param {param} through channel \"{eventChannel.description}\".");
    }
}

public class BoolEventListener : GenericEventListener<bool> { }
public class IntEventListener : GenericEventListener<int> { }
public class FloatEventListener : GenericEventListener<float> { }
public class Vector3EventListener : GenericEventListener<Vector3> { }
public class Vector4EventListener : GenericEventListener<Vector4> { }
public class TransformEventListener : GenericEventListener<Transform> { }
public class ScriptableObjectEventListener : GenericEventListener<ScriptableObject> { }
