using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreFunctionality : MonoBehaviour
{
    #region [ PARAMETERS ]

    public static Controls controls = new Controls();
    public static Settings settings = new Settings();

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public static void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public static float ToRad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float ToDeg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public static string[] StopwatchTime(float time)
    {
        int seconds = (int)Mathf.FloorToInt(time);
        int subSeconds = (int)Mathf.Floor((time - seconds) * 100.0f);

        int tMinutes = seconds - seconds % 60;
        int tSeconds = seconds % 60;

        string strMinutes = tMinutes.ToString();
        string strSeconds = tSeconds.ToString();
        string strSubSecs = subSeconds.ToString();

        if (strSeconds.Length < 2)
        {
            strSeconds = "0" + strSeconds;
        }
        if (strSubSecs.Length < 2)
        {
            strSubSecs = "0" + strSubSecs;
        }

        return new string[] { strMinutes, strSeconds, strSubSecs };
    }

    public static List<T> ArrayToList<T>(T[] array)
    {
        List<T> listOut = new List<T>();
        foreach (T item in array)
        {
            listOut.Add(item);
        }
        return listOut;
    }
    
    public static List<GameObject> GetChildrenWithComponent<T>(GameObject parentObj)
    {
        List<GameObject> childrenWithComponent = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if (child.GetComponent<T>() != null)
                {
                    childrenWithComponent.Add(child);
                }
            }
        }
        return childrenWithComponent;
    }

    public static List<GameObject> GetChildrenWithTag(GameObject parentObj, string tag)
    {
        List<GameObject> childrenWithTag = new List<GameObject>();
        if (parentObj.transform.childCount > 0)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if (child.CompareTag(tag))
                {
                    childrenWithTag.Add(child);
                }
            }
        }
        return childrenWithTag;
    }
}
