using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class AngleWithCamera : MonoBehaviour
{
    #region [ PARAMETERS ]

    private GameObject playerCam;
    [SerializeField] bool x;
    [SerializeField] bool y;
    [SerializeField] bool z;
    private bool[] rotOnAxis = new bool[3];
    private Vector3 rotScale = Vector3.zero;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        rotOnAxis[0] = x;
        rotOnAxis[1] = y;
        rotOnAxis[2] = z;
        for (int i = 0; i < 3; i++)
        {
            if (rotOnAxis[i])
            {
                rotScale[i] = 1.0f;
            }
        }
    }

    void Update()
    {
        transform.LookAt(playerCam.transform);
        Vector3 rot = transform.eulerAngles;
        transform.eulerAngles = new Vector3(rot[0] * rotScale[0], rot[1] * rotScale[1], rot[2] * rotScale[2]);
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
}
