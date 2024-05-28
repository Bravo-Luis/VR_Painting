using UnityEngine;
using System.Collections;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TypewriterEffect : VoidEventListener
{
    private TextMeshPro textComponent;
    public float typingSpeed = 0.05f;
    public string fullText;
    public bool playOnAwake;
    public float repeatInterval = 0;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshPro>();
        textComponent.text = "";
        if (playOnAwake) StartCoroutine(TypeText());
    }

    protected override void HandleEvent()
    {
        base.HandleEvent();
        StopAllCoroutines();
        StartCoroutine(TypeText());
    }


    IEnumerator TypeText()
    {
        textComponent.text = "";
        foreach (char c in fullText)
        {
            textComponent.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (repeatInterval > 0)
        {
            yield return new WaitForSeconds(repeatInterval);
            if (repeatInterval > 0) StartCoroutine(TypeText());
        }
    }
}
