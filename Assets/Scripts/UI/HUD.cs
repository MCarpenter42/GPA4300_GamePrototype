using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

public class HUD : UI
{
    #region [ PARAMETERS ]

    [SerializeField] GameObject notification;
    private TextMeshProUGUI notifText;
    private List<Image> notifImages = new List<Image>();
    private Coroutine notifFlash;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }

    void Start()
    {
        notification.SetActive(false);
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetComponents()
    {
        for (int i = 0; i < notification.transform.childCount; i++)
        {
            GameObject child = notification.transform.GetChild(i).gameObject;
            if (child.CompareTag("Text"))
            {
                notifText = child.GetComponent<TextMeshProUGUI>();
            }
            else
            {
                notifImages.Add(child.GetComponent<Image>());
            }
        }
    }

    public void Notification(string text)
    {
        notification.SetActive(true);
        notifText.text = text;

        if (notifFlash != null)
        {
            StopCoroutine(notifFlash);
        }
        notifFlash = StartCoroutine(NotifFlash(4.0f, 0.8f));
    }
    
    public void Notification(float visTime, float fadeTime, string text)
    {
        notification.SetActive(true);
        notifText.text = text;

        if (notifFlash != null)
        {
            StopCoroutine(notifFlash);
        }
        notifFlash = StartCoroutine(NotifFlash(visTime, fadeTime));
    }

    private IEnumerator NotifFlash(float visTime, float fadeTime)
    {
        notifText.color = new Color(notifText.color[0], notifText.color[1], notifText.color[2], 1.0f);
        foreach (Image img in notifImages)
        {
            img.color = new Color(img.color[0], img.color[1], img.color[2], 1.0f);
        }

        float aDuration = fadeTime;
        int aFrames = 80;
        float aFrameTime = aDuration / (float)aFrames;

        visTime -= aDuration;

        yield return new WaitForSecondsRealtime(visTime);

        for (int i = aFrames - 1; i >= 0; i--)
        {
            float delta = (float)i / (float)aFrames;
            yield return new WaitForSecondsRealtime(aFrameTime);

            notifText.color = new Color(notifText.color[0], notifText.color[1], notifText.color[2], delta);
            foreach (Image img in notifImages)
            {
                img.color = new Color(img.color[0], img.color[1], img.color[2], delta);
            }
        }

        notification.SetActive(false);
    }

}
