using UnityEngine;
using System.Collections;

public class ChameleonColorChange : MonoBehaviour
{
    [Header("Blend Settings")]
    public float blendDuration = 0.5f;

    private Renderer rend;
    private float currentBlend = 0f;
    private static readonly int BlendID = Shader.PropertyToID("_BlendValue");

    private Coroutine blendRoutine;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("Renderer not found!");
            enabled = false;
            return;
        }

        currentBlend = 0f;
        rend.material.SetFloat(BlendID, currentBlend);
    }

    public void ToggleTextureChange()
    {
        float target = (currentBlend < 0.5f) ? 1f : 0f;

        if (blendRoutine != null)
            StopCoroutine(blendRoutine);

        blendRoutine = StartCoroutine(BlendTo(target));
    }

    private IEnumerator BlendTo(float targetBlend)
    {
        float start = currentBlend;
        float t = 0f;

        while (t < blendDuration)
        {
            t += Time.deltaTime;
            float p = Mathf.Clamp01(t / blendDuration);
            float smooth = Mathf.SmoothStep(0f, 1f, p);

            currentBlend = Mathf.Lerp(start, targetBlend, smooth);
            rend.material.SetFloat(BlendID, currentBlend);

            yield return null;
        }

        currentBlend = targetBlend;
        rend.material.SetFloat(BlendID, currentBlend);
        blendRoutine = null;
    }
}