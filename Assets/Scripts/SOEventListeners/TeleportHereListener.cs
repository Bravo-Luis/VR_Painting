using System.Collections;
using UnityEngine;

public class TeleportHereListener : TransformEventListener
{
    public Vector3 dropOffset = Vector3.zero;
    [SerializeField] int counter = 0;

    protected override void HandleEvent(Transform param)
    {
        base.HandleEvent(param);
        StartCoroutine(Teleport(param));
        counter++;
    }

    IEnumerator Teleport(Transform t)
    {
        if (t.TryGetComponent(out CharacterController c)) c.enabled = false;
        t.SetPositionAndRotation(transform.position + dropOffset, transform.rotation);
        yield return null;
        if (c != null) { c.enabled = true; }
    }
}
