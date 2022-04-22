using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SINGLETON CLASS
public class GameManager : CoreFunctionality
{
    private static GameManager instance = null;

    #region [ HANDLERS ]

    public static GameState state = new GameState();

    public static FrameController frameController = new FrameController();

    #endregion

    #region [ PARAMETERS ]

    private bool setupRun = false;

    public static bool isCursorLocked;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public static GameManager Instance
    {
        get {
            if (instance == null)
            {
                GameManager inst = FindObjectOfType<GameManager>();
                if (inst == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();

                    instance.Init();

                    // Prevents game manager from being destroyed on loading of a new scene
                    DontDestroyOnLoad(obj);

                    Debug.Log(obj.name);
                }
            }
            return instance;
        }
    }

    // Initialiser function, serves a similar purpose to a constructor
    private void Init()
    {
        Setup();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        Setup();

        state.Init();
    }

    void Start()
    {
        OnStartDebugging();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void Setup()
    {
        if (!setupRun)
        {
            setupRun = true;
        }
    }

    private void OnStartDebugging()
    {

    }
}

public class GameState
{
    public bool isPaused;

    public void Init()
    {
        if (Time.timeScale > 0.0f)
        {
            isPaused = false;
        }
        else
        {
            isPaused = true;
        }
    }
}

public class FrameController : CoreFunctionality
{
    private List<FrameHandler> openFrames = new List<FrameHandler>();

    public int GetOpenCount()
    {
        return openFrames.Count;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void AddOpen(FrameHandler handler)
    {
        bool alreadyPresent = false;
        foreach (FrameHandler fh in openFrames)
        {
            if (handler == fh)
            {
                alreadyPresent = true;
            }
        }
        if (!alreadyPresent)
        {
            openFrames.Add(handler);
        }
    }

    public void RemoveOpen(int index)
    {
        if (InBounds(index, openFrames))
        {
            openFrames[index].index = -1;
            openFrames.RemoveAt(index);
            for (int i = 0; i < openFrames.Count; i++)
            {
                openFrames[i].index = i;
            }
        }
    }
    
    public void RemoveOpen(int index, bool correctIndices)
    {
        if (InBounds(index, openFrames))
        {
            openFrames[index].index = -1;
            openFrames.RemoveAt(index);
            if (correctIndices)
            {
                for (int i = 0; i < openFrames.Count; i++)
                {
                    openFrames[i].GetComponent<FrameHandler>().index = i;
                }
            }
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void OpenFrame(FrameHandler handler)
    {
        handler.GetComponent<FrameHandler>().Show(true, true);
        AddOpen(handler);
    }

    public void CloseFrame(int index)
    {
        if (InBounds(index, openFrames))
        {
            openFrames[index].Show(false, true);
            RemoveOpen(index, true);
        }
    }
    
    public void CloseFrame(int index, bool correctIndices)
    {
        if (InBounds(index, openFrames))
        {
            openFrames[index].Show(false);
            RemoveOpen(index, correctIndices);
        }
    }

    public void CloseLast()
    {
        int n = openFrames.Count - 1;
        CloseFrame(n, false);
    }

    public void CloseAll()
    {
        while (openFrames.Count > 0)
        {
            CloseLast();
        }
    }
}
