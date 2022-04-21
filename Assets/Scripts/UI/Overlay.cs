using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Overlay : UI
{
    #region [ PARAMETERS ]

    private FrameHandler frameHandler;

    private GameObject closeButton;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        frameHandler = gameObject.AddComponent<FrameHandler>();
        frameHandler.SetValues(false, "Button", true);
        GetComponents();
    }

    void Start()
    {
        HideOverlay();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetComponents()
    {
        frameHandler.GetComponents(gameObject);
        foreach (GameObject child in frameHandler.notFrames)
        {
            if (child.CompareTag("Button"))
            {
                closeButton = child;
            }
        }
    }

    public void ShowOverlay(int i)
    {
        if (InBounds(i, frameHandler.frames))
        {
            frameHandler.ChangeFrame(i);
            frameHandler.Show(true);
            closeButton.SetActive(true);
            float btnPosX = frameHandler.frames[i].GetComponent<RectTransform>().rect.width / 2.0f - 5.0f;
            float btnPosY = -(frameHandler.frames[i].GetComponent<RectTransform>().rect.height / 2.0f + 5.0f);
            closeButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(btnPosX, btnPosY);
        }
    }

    public void HideOverlay()
    {
        frameHandler.Show(false);
        closeButton.SetActive(false);
    }
}
