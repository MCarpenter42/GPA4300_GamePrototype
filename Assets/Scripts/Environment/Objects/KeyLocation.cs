using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class KeyLocation : MonoBehaviour
{
    #region [ PARAMETERS ]

    private MeshRenderer meshRndr;
    [TextArea(3, 10)]
    [SerializeField] string hintText;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        meshRndr = gameObject.GetComponent<MeshRenderer>();
        meshRndr.enabled = false;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public string GetHintText()
    {
        return hintText;
    }
}
