using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// This class is attached to the prefab object that handles player
// interaction. It uses Unity's events system to be able to handle
// anything it might be required to do - as long as the method is
// public, the interaction handler can call it.

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
    [SerializeField] protected bool startEnabled = true;

    protected GameObject indicator;

    // Having serialized sprites works just fine thanks to the
    // fact that this script is only attached to a prefab - makes
    // it a lot easier to pick the sprites for each interaction!
    Sprite[] icons;
    [SerializeField] protected Sprite press;
    [SerializeField] protected Sprite pickup;
    [SerializeField] protected Sprite push;
    [SerializeField] protected Sprite locked;
    [SerializeField] protected Sprite unlock;
    [SerializeField] protected int startIcon;

    protected Player player;

    protected HUD hud;

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
        hud = FindObjectOfType<HUD>();
        IconsToArray();
    }

    protected void Start()
    {
        SetIcon(startIcon);
        GetPlayer();
        if (!startEnabled)
        {
            isEnabled = false;
        }
    }

    protected void Update()
    {
        if (indicator.activeSelf)
        {
            RotateIndicator();
        }
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

    // Allows the sprites to be selected by index, but also adds in the
    // potential for a null option to act as a default sprite.
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

    // The next few methods all relate to the indicator sprite
    // that displays in the world space.
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

    // Every interaction handler stores a reference to the player at the
    // start, just in case. (This is mostly utilised by a child class.)
    protected void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Allows the indicator to be easiler enabled & disabled without
    // changing whether it's active.
    public void SetEnabled(bool enable)
    {
        isEnabled = enable;
    }

    // Just in case the indicator's position needs to be modified at runtime.
    public void OffsetIndicator(Vector3 offset)
    {
        indicator.transform.position += offset;
    }
}
