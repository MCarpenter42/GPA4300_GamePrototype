using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    #region [ PARAMETERS ]

    private bool isOpen = false;
    [SerializeField] bool canReClose = false;

    [SerializeField] Interaction unobstructedInteract;
    private LockedInteract unobLock = null;
    [SerializeField] Interaction obstructedInteract;
    private LockedInteract obLock = null;

    private Vector3 rotClosed;
    [SerializeField] Vector3 rotOpen;
    private bool isMoving = false;

    [SerializeField] bool startOpen;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        rotClosed = gameObject.transform.eulerAngles;
        CheckLocks();
    }
    private void Start()
    {
        if (startOpen)
        {
            Debug.Log("OPEN!!");
            SetOpen(true);
        }
        else
        {
            Debug.Log("NOT OPEN!!!!");
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void CheckLocks()
    {
        if (unobstructedInteract.GetComponent<LockedInteract>() != null)
        {
            unobLock = unobstructedInteract.GetComponent<LockedInteract>();
        }
        if (obstructedInteract.GetComponent<LockedInteract>() != null)
        {
            obLock = obstructedInteract.GetComponent<LockedInteract>();
        }
    }

    public void ToggleOpen()
    {
        SetOpen(!isOpen);
    }

    public void SetOpen(bool open)
    {
        if (!isMoving)
        {
            StartCoroutine(RotTransition(open));
        }
    }

    private IEnumerator RotTransition(bool opening)
    {
        isMoving = true;

        if (obstructedInteract != null)
        {
            obstructedInteract.SetEnabled(false);
        }


        Vector3 rotStart = GetRotPoints(opening)[0];
        Vector3 rotEnd = GetRotPoints(opening)[1];

        int aFrames = 80;
        float aDuration = 0.8f;
        float aFrameTime = aDuration / (float)aFrames;
        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            yield return new WaitForSeconds(aFrameTime);
            transform.eulerAngles = Vector3.Lerp(rotStart, rotEnd, delta);
        }

        isMoving = false;
        isOpen = opening;

        if (unobLock != null && obLock != null)
        {
            if (!unobLock.isLocked || !obLock.isLocked)
            {
                unobLock.SetLockState(false);
                obLock.SetLockState(false);
            }
        }

        if (canReClose)
        {
            unobstructedInteract.SetEnabled(true);
            if (!isOpen && obstructedInteract != null)
            {
                obstructedInteract.SetEnabled(true);
            }
        }
        else if (!isOpen)
        {
            unobstructedInteract.SetEnabled(true);
        }
    }

    private Vector3[] GetRotPoints(bool opening)
    {
        Vector3 rotStart = transform.eulerAngles;
        if (rotStart[1] > 180.0f)
        {
            rotStart[1] -= 360.0f;
        }
        else if (rotStart[1] < -180.0f)
        {
            rotStart[1] += 360.0f;
        }

        Vector3 rotEnd;
        if (opening)
        {
            rotEnd = rotOpen;
        }
        else
        {
            rotEnd = rotClosed;
        }

        return new Vector3[] { rotStart, rotEnd };
    }




}
