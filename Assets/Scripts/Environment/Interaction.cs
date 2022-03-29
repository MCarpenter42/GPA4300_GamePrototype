using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interaction : CoreFunctionality
{
    #region [ PARAMETERS ]

    public Vector3 pos
    {
        get
        {
            return gameObject.transform.position;
        }
    }

    [SerializeField] UnityEvent interactEvent;

    private GameObject indicator;

    private GameObject mainCam;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetIndicator();
        GetCamera();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (indicator.activeSelf)
        {
            RotateIndicator();
        }
    }

    void FixedUpdate()
    {
        
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public bool InteractEvent()
    {
        if (interactEvent == null)
        {
            return false;
        }
        else
        {
            interactEvent.Invoke();
            return true;
        }
    }

    private void GetIndicator()
    {
        GameObject target;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            target = gameObject.transform.GetChild(i).gameObject;
            if (target.GetComponent<Canvas>() != null)
            {
                indicator = target;
            }
        }
    }

    private void GetCamera()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void ShowIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    private void RotateIndicator()
    {
        Vector3 dirToCam = mainCam.transform.position - indicator.transform.position;
        dirToCam = dirToCam.normalized * -1.0f;
        indicator.transform.rotation = Quaternion.LookRotation(dirToCam, Vector3.up);
    }

}
