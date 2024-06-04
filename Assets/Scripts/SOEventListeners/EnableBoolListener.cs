using UnityEngine;

public class EnableBoolListener : BoolEventListener
{
    [SerializeField] private AudioSource m_AudioSource;

    protected override void HandleEvent(bool param)
    {
        base.HandleEvent(param);
        if (param) m_AudioSource.Play();
        else m_AudioSource.Stop();
    }
}
