using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSource : CoreFunctionality
{
    #region [ PARAMETERS ]

    [Header("General Properties")]
    [SerializeField] GameObject lightSource;
    [SerializeField] bool startLit;
    private bool isLit;

    [Header("Lit/Unlit Material")]
    [SerializeField] MeshRenderer targetMesh;
    [SerializeField] int targetMaterial = -1;
    [SerializeField] Material litMaterial;
    [SerializeField] Material unlitMaterial;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        if (startLit)
        {
            isLit = true;
            lightSource.SetActive(true);
            if (targetMaterial > -1 && targetMaterial < targetMesh.materials.Length && litMaterial != null)
            {
                targetMesh.materials[targetMaterial].CopyPropertiesFromMaterial(litMaterial);
            }
        }
        else
        {
            isLit = false;
            lightSource.SetActive(false);
            if (targetMaterial > -1 && targetMaterial < targetMesh.materials.Length && unlitMaterial != null)
            {
                targetMesh.materials[targetMaterial].CopyPropertiesFromMaterial(unlitMaterial);
            }
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
