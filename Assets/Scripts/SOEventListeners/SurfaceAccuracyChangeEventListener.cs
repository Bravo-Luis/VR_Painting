using System.Collections;
using TMPro;
using UnityEngine;


public class SurfaceAccuracyChangeEventListener : FloatEventListener
{
    [SerializeField] private TMP_Text m_Text;

    protected override void HandleEvent(float param)
    {
        base.HandleEvent(param);
        if (param <= 0.5f)
        {
            m_Text.text = string.Empty;
            return;
        }
        param = Mathf.Clamp01(param);   
        float percentage = param * 100;

        StopAllCoroutines();
        m_Text.text = percentage.ToString("F2") + "%";
        StartCoroutine(HideText());
    }

    public string GetPercentText () { return m_Text.text; }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(5);
        m_Text.text = string.Empty;
    }
}
