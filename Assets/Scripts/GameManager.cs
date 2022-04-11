using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SINGLETON CLASS
public class GameManager : CoreFunctionality
{
    private static GameManager instance = null;

    public static GameState state;

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
    }

    void Start()
    {
        OnStartDebugging();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {

    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void Setup()
    {
        if (!setupRun)
        {
            itemDB.CompileDatabase();

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

    public GameState()
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
