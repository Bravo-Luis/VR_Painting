using UnityEngine;

public class SetActiveBoolListener : BoolEventListener
{
    [SerializeField] GameObject obj;
    [SerializeField] bool setInactiveOnDisable = false;

    protected override void HandleEvent(bool b)
    {
        base.HandleEvent(b);
        obj.SetActive(b);
    }


    protected override void OnDisable()
    {
        base.OnDisable();
        if (setInactiveOnDisable) obj.SetActive(false);
    }
}
