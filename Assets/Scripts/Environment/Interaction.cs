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

    protected GameObject mainCam;

    public bool isEnabled { get; protected set; }
    [SerializeField] protected UnityEvent interactEvent;

    protected GameObject indicator;

    Sprite[] icons;
    [SerializeField] protected Sprite press;
    [SerializeField] protected Sprite pickup;
    [SerializeField] protected Sprite push;
    [SerializeField] protected Sprite locked;
    [SerializeField] protected Sprite unlock;
    [SerializeField] protected int startIcon;

    protected Player player;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    
    public Interaction()
    {
        this.isEnabled = true;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    protected void Awake()
    {
        GetIndicator();
        GetCamera();
        IconsToArray();
    }

    protected void Start()
    {
        SetIcon(startIcon);
        GetPlayer();
    }

    protected void Update()
    {
        if (indicator.activeSelf)
        {
            RotateIndicator();
        }
    }

    protected void FixedUpdate()
    {
        
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual bool InteractEvent()
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

    protected void IconsToArray()
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

    protected void GetIndicator()
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

    protected void GetCamera()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    public void ShowIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    protected void RotateIndicator()
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

    protected void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetEnabled(bool enable)
    {
        isEnabled = enable;
    }
}
