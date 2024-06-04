using System.Collections;
using TMPro;
using UnityEngine;


public class SurfaceAccuracyChangeEventListener : FloatEventListener
{
    [SerializeField] private TMP_Text m_Text;
    public string percent;

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


        //// Play sound if percentage is above 80%
        //if (percentage > 80)
        //{
        //    //audioSource.clip = successClip;
        //    audioSource.Play();
        //}
        //else
        //{
        //    audioSource.Stop();    
        //}

        StopAllCoroutines();
        
        m_Text.text = percentage.ToString("F2") + "%";
        percent = m_Text.text;
        StartCoroutine(HideText());
    }

    IEnumerator HideText()
    {
        yield return new WaitForSeconds(5);
        m_Text.text = string.Empty;
    }
}
