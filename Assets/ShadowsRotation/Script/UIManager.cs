using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Button morningBtn;
    public Button noonBtn;
    public Button eveningBtn;

    private List<Button> flow = new List<Button>();
    Coroutine highlightRoutine;
    int index = 0;

    public GameObject popupmorning;
    public GameObject popupnoon;
    public GameObject popupSunset;
    public GameObject intropopup;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {

        intropopup.SetActive(true);
        // Flow order
        flow.Add(morningBtn);
        flow.Add(noonBtn);
        flow.Add(eveningBtn);

        // Disable sab pehle
        foreach (var btn in flow)
            btn.interactable = false;

        // Start first button highlight
        StartCoroutine(RunFlow());
    }

    IEnumerator RunFlow()
    {
        Button current = flow[index];
        current.interactable = true;

        // Start scale animation
        highlightRoutine = StartCoroutine(ScalePulse(current));

        yield break; // 👉 yahi ruk jayega, click ka wait ab nahi hoga
    }

    // 🔥 Scale highlight animation
    IEnumerator ScalePulse(Button btn)
    {
        RectTransform rt = btn.GetComponent<RectTransform>();
        float speed = 2f;
        float maxScale = 1.18f;
        float minScale = 1f;

        while (true)
        {
            float t = Mathf.PingPong(Time.time * speed, 1f);
            float scaleValue = Mathf.Lerp(minScale, maxScale, t);
            rt.localScale = Vector3.one * scaleValue;
            yield return null;
        }
    }

    // 🧹 Reset button scale after click/close
    void ResetButton(Button btn)
    {
        btn.transform.localScale = Vector3.one;
    }

    public void Morning()
    {
        DisableCurrentButton();
        popupmorning.SetActive(true);
    }

    public void Noon()
    {
        DisableCurrentButton();
        popupnoon.SetActive(true);
    }

    public void Evening()
    {
        DisableCurrentButton();
        popupSunset.SetActive(true);
    }


    // ❌ Popup Close → Next highlight chalega
    public void ClosePopup(GameObject popup)
    {
        popup.SetActive(false);

        // Stop and reset current highlight
        if (highlightRoutine != null)
            StopCoroutine(highlightRoutine);

        ResetButton(flow[index]);

        // Go to next button now
        index++;

        if (index < flow.Count)
        {
            StartCoroutine(RunFlow()); // NEXT button highlight starts
        }
        else
        {
            Debug.Log("⭐ Flow Complete!");
        }
    }


    void DisableCurrentButton()
    {
        // stop highlight
        if (highlightRoutine != null)
            StopCoroutine(highlightRoutine);

        // disable click + reset scale
        flow[index].interactable = false;
        ResetButton(flow[index]);
    }
}
