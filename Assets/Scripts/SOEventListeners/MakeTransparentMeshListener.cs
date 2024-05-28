using System.Linq;
using TMPro;
using UnityEngine;

public class MakeTransparentMeshListener : TransformEventListener
{
    public float alpha = 0.5f;

    protected override void HandleEvent(Transform param)
    {
        base.HandleEvent(param);
        GameObject copy = Instantiate(param.gameObject, transform);
        copy.transform.localPosition = Vector3.zero;
        Renderer[] renderers = copy.GetComponentsInChildren<Renderer>().Where(c => c.GetComponent<TextMeshPro>() == null).ToArray();
        foreach (Renderer renderer in renderers)
        {
            Material m = renderer.material = new Material(renderer.material);
            m.SetInt("_SurfaceType", 1);
            m.color = new Color(m.color.r, m.color.g, m.color.b, alpha);
        }
    }
}
