using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;

public class InvenSlot : UI, IPointerEnterHandler, IPointerExitHandler
{
    #region [ PARAMETERS ]

    private InvenFrame invenDisplay;
    [HideInInspector] public int index;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        invenDisplay = FindObjectOfType<InvenFrame>().GetComponent<InvenFrame>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        invenDisplay.ShowTooltip(index);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        invenDisplay.HideTooltip();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
}
