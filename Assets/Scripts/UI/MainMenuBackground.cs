using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class MainMenuBackground : MonoBehaviour
{
    #region [ PARAMETERS ]

    private float aspectRatio;
    private float aspect1080p = 1920.0f / 1080.0f;

    private RectTransform rect;

    private RectTransform canvas;
    private CanvasScaler canvasScaler;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
/*
        aspectRatio = Screen.width / Screen.height;
        float heightRatio = 1080.0f / Screen.height;
        float h = 1080.0f * heightRatio;
        float w = (1920.0f / 1080.0f) * h;

        if (true)
        {

        }

        rect.sizeDelta = new Vector2(w, h);*/
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
}
