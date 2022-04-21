using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class FrameHandler : CoreFunctionality
{
    #region [ PARAMETERS ]

    private bool isMenu;
    private string searchTag;
    private bool excludeTag;

    private bool isInitialised;

    private GameObject parentObj;

    [HideInInspector] public List<GameObject> frames = new List<GameObject>();
    [HideInInspector] public List<GameObject> notFrames = new List<GameObject>();
    private int activeFrame = 0;
    private bool visible;

    [HideInInspector] public int index = -1;

    [HideInInspector] public delegate void OnShow(bool show);
    [HideInInspector] public OnShow onShow;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void SetValues(bool isMenu)
    {
        this.isMenu = isMenu;
        this.searchTag = null;
        this.excludeTag = false;
    }
    
    public void SetValues(bool isMenu, string searchTag, bool excludeTag)
    {
        this.isMenu = isMenu;
        this.searchTag = searchTag;
        this.excludeTag = excludeTag;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void GetComponents(GameObject obj)
    {
        parentObj = obj;

        if (searchTag == null)
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                frames.Add(child);
            }
        }
        else
        {
            for (int i = 0; i < parentObj.transform.childCount; i++)
            {
                GameObject child = parentObj.transform.GetChild(i).gameObject;
                if ((excludeTag && !child.CompareTag(searchTag)) || (!excludeTag && child.CompareTag(searchTag)))
                {
                    frames.Add(child);
                }
                else
                {
                    notFrames.Add(child);
                }
            }
        }
    }

    public void Show(bool show)
    {
        if (show)
        {
            if (InBounds(activeFrame, frames))
            {
                frames[activeFrame].SetActive(true);
            }
            if (!isMenu && isInitialised)
            {
                GameManager.frameController.AddOpen(parentObj.GetComponent<FrameHandler>());
            }
        }
        else
        {
            foreach (GameObject frame in frames)
            {
                frame.SetActive(false);
            }
            if (!isMenu && isInitialised)
            {
                GameManager.frameController.RemoveOpen(index);
            }
        }
        visible = show;
        if (onShow != null)
        {
            onShow.Invoke(show);
        }

        if (!isInitialised)
        {
            isInitialised = true;
        }
    }
    
    public void Show(bool show, bool calledByController)
    {
        if (show)
        {
            if (InBounds(index, frames))
            {
                frames[activeFrame].SetActive(show);
            }
            if (!calledByController && isInitialised)
            {
                GameManager.frameController.AddOpen(parentObj.GetComponent<FrameHandler>());
            }
        }
        else
        {
            foreach (GameObject frame in frames)
            {
                frame.SetActive(false);
            }
            if (!calledByController && isInitialised)
            {
                GameManager.frameController.RemoveOpen(index);
            }
        }
        visible = show;
        if (onShow != null)
        {
            onShow.Invoke(show);
        }

        if (!isInitialised)
        {
            isInitialised = true;
        }
    }

    public void Toggle()
    {
        Show(!visible);
    }

    public void ChangeFrame(int index)
    {
        if (InBounds(index, frames))
        {
            if (visible)
            {
                frames[activeFrame].SetActive(false);
                activeFrame = index;
                frames[activeFrame].SetActive(true);
            }
            else
            {
                activeFrame = index;
            }
        }
    }
}
