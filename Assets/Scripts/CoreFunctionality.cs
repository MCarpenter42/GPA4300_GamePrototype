using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreFunctionality : MonoBehaviour
{
    #region [ PARAMETERS ]

    public enum ItemNames { emptyItem, basicKey, emerald, lockpickPins, noteA, noteB, noteC, hiddenKey };
    public static Controls controls = new Controls();
    public static Settings settings = new Settings();
    public static ItemDatabase itemDB = new ItemDatabase();

    public enum rotVectors { X, Y, Z };

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public static void Pause()
    {
        GameManager.state.isPaused = true;
        Time.timeScale = 0.0f;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        GameManager.state.isPaused = false;
    }

    public static float ToRad(float degrees)
    {
        return degrees * Mathf.PI / 180.0f;
    }

    public static float ToDeg(float radians)
    {
        return radians * 180.0f / Mathf.PI;
    }

    public static int ToInt(bool intBool)
    {
        if (intBool)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static bool ToBool(int boolInt)
    {
        if (boolInt > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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

    public static int RandomInt(int valMin, int valMax)
    {
        float r = Random.Range(valMin, valMax + 1);
        int i = Mathf.FloorToInt(r);
        if (i > valMax)
        {
            i = valMax;
        }
        return i;
    }

    public static bool InBounds<T>(int index, T[] array)
    {
        if (index > -1 && index < array.Length)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static bool InBounds<T>(int index, List<T> list)
    {
        if (index > -1 && index < list.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
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
