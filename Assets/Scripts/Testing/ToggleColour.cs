using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColour : MonoBehaviour
{
    #region [ PARAMETERS ]

    [SerializeField] Material clrA;
    [SerializeField] Material clrB;
    private bool clrChanged = false;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public void Toggle()
    {
        if (clrChanged)
        {
            gameObject.GetComponent<MeshRenderer>().material = clrA;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = clrB;
        }
        clrChanged = !clrChanged;
    }
}
