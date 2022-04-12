using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureTiling : MonoBehaviour
{
    #region [ PARAMETERS ]

    private Vector2 faceSize = new Vector2();
    private Material surfaceMat;
    private float matScaleFactor = 0.2f;

    [SerializeField] Vector2 textureOffset;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        surfaceMat = gameObject.GetComponent<MeshRenderer>().materials[0];
        GetFaceSize();
        surfaceMat.mainTextureScale = faceSize * matScaleFactor;
        surfaceMat.mainTextureOffset = textureOffset;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    private void GetFaceSize()
    {
        Vector3 scale = gameObject.transform.localScale;
        if (scale.x < scale.y && scale.x < scale.z)
        {
            faceSize[0] = scale.z;
            faceSize[1] = scale.y;
        }
        else if (scale.y < scale.z)
        {
            faceSize[0] = scale.x;
            faceSize[1] = scale.z;
        }
        else
        {
            faceSize[0] = scale.x;
            faceSize[1] = scale.y;
        }
    }
}
