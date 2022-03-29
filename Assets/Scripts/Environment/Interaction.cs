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

    private GameObject mainCam;

    private bool isEnabled = true;
    [SerializeField] UnityEvent interactEvent;

    private GameObject indicator;

    Sprite[] icons;
    [SerializeField] Sprite press;
    [SerializeField] Sprite pickup;
    [SerializeField] Sprite push;
    [SerializeField] Sprite locked;
    [SerializeField] Sprite unlock;
    [SerializeField] int startIcon;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetIndicator();
        GetCamera();
        IconsToArray();
    }
    
    void Start()
    {
        SetIcon(startIcon);
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

    private void IconsToArray()
    {
        icons = new Sprite[]
        {
            null,
            press,
            pickup,
            push,
            locked,
            unlock
        };
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

    public void SetIcon(int id)
    {
        for (int i = 0; i < indicator.transform.childCount; i++)
        {
            GameObject child = indicator.transform.GetChild(i).gameObject;
            if (child.GetComponent<Image>() != null)
            {
                child.GetComponent<Image>().sprite = icons[id];
            }
        }
    }


}
