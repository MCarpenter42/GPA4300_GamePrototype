using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// SINGLETON CLASS
public class GameManager : CoreFunctionality
{
    private static GameManager instance = null;

    #region [ PARAMETERS ]

    private float gravityScale = 30.0f;
    public Vector3 gravVector { get; private set; }
    private List<Rigidbody> gravityBodies = new List<Rigidbody>();

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
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        this.gravVector = new Vector3(0.0f, -gravityScale, 0.0f);
    }

    void Start()
    {
        GetGravityObjects();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void SetGravityScale(float scale)
    {
        gravityScale = scale;
    }

    private void GetGravityObjects()
    {
        gravityBodies = ArrayToList(FindObjectsOfType<Rigidbody>());
        Debug.Log(gravityBodies.Count);
        Debug.Log(gravVector);
    }

    private void ApplyGravity()
    {
        foreach (Rigidbody rb in gravityBodies)
        {
            rb.AddForce(gravVector * rb.mass);
        }
    }
}
