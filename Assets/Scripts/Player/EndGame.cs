using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class EndGame : UI
{
    #region [ PARAMETERS ]

    private GameObject endScreen;
    private Image endScreenImg;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        endScreen = transform.GetChild(0).gameObject;
        endScreenImg = endScreen.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    void Start()
    {
        endScreenImg.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        endScreen.SetActive(false);
    }
	
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public void DoEnd()
    {
        StartCoroutine(EndSequence());
    }

    private IEnumerator EndSequence()
    {
        GameManager.state.gameEnded = true;
        yield return new WaitForSeconds(1.0f);
        Pause();
        endScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        for (int i = 1; i <= 100; i++)
        {
            float delta = (float)i / 100.0f;
            yield return new WaitForSecondsRealtime(0.01f);
            endScreenImg.color = new Color(1.0f, 1.0f, 1.0f, delta);
        }
    }
}
