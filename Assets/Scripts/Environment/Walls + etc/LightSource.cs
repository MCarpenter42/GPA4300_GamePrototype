using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSource : CoreFunctionality
{
    #region [ PARAMETERS ]

    [SerializeField] GameObject lightSource;
    [SerializeField] bool startLit;
    private bool isLit;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        if (startLit)
        {
            isLit = true;
            lightSource.SetActive(true);
        }
        else
        {
            isLit = false;
            lightSource.SetActive(false);
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {

    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void SetLit(bool lit)
    {
        if (lit)
        {
            isLit = true;
            lightSource.SetActive(true);
        }
        else
        {
            isLit = false;
            lightSource.SetActive(false);
        }
    }

    public void ToggleLit()
    {
        SetLit(!isLit);
    }

}
