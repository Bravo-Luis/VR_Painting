
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class ShowHideTextBoolListener : BoolEventListener
{
    protected override void HandleEvent(bool param)
    {
        base.HandleEvent(param);
        GetComponent<TextMeshPro>().color = param ? Color.white : Color.clear;
    }
}
