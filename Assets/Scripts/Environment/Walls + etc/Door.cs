using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : CoreFunctionality
{
    #region [ PARAMETERS ]

    [Header("Interaction Points")]
    [SerializeField] Interaction unobstructedInteract;
    private LockedInteract unobLock = null;
    [SerializeField] Interaction obstructedInteract;
    private LockedInteract obLock = null;

    [Header("Attributes")]
    [SerializeField] Vector3 rotOpen;
    private Vector3 rotClosed;
    private bool isMoving = false;

    [SerializeField] bool startOpen;
    [SerializeField] bool canReClose = false;

    private bool isOpen = false;

    [SerializeField] float openTime = 0.8f;
    [SerializeField] float closeTime;

    [Header("Audio")]
    [SerializeField] AudioClip openSound;
    [SerializeField] float openPitchScale = 1.0f;
    [SerializeField] float openVolumeScale = 1.0f;
    [SerializeField] AudioClip closeSound;
    [SerializeField] float closePitchScale = 1.0f;
    [SerializeField] float closeVolumeScale = 1.0f;

    private Player player;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        rotClosed = gameObject.transform.eulerAngles;
        CheckLocks();
        if (openTime < 0.0f)
        {
            openTime *= -1.0f;
        }
        if (closeTime <= 0.0f)
        {
            closeTime = openTime;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Start()
    {
        if (startOpen)
        {
            SetOpen(true);
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void CheckLocks()
    {
        if (unobstructedInteract != null)
        {
            if (unobstructedInteract.GetComponent<LockedInteract>() != null)
            {
                unobLock = unobstructedInteract.GetComponent<LockedInteract>();
            }
        }
        if (obstructedInteract != null)
        {
            if (obstructedInteract.GetComponent<LockedInteract>() != null)
            {
                obLock = obstructedInteract.GetComponent<LockedInteract>();
            }
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

        if (unobstructedInteract != null)
        {
            unobstructedInteract.SetEnabled(false);
        }
        if (obstructedInteract != null)
        {
            obstructedInteract.SetEnabled(false);
        }

        Vector3 rotStart = GetRotPoints(opening)[0];
        Vector3 rotEnd = GetRotPoints(opening)[1];

        if (opening)
        {
            rotEnd = rotOpen;
            player.PlayClip(AudioSources.environment, openSound, openPitchScale, openVolumeScale);
        }
        else
        {
            rotEnd = rotClosed;
            player.PlayClip(AudioSources.environment, closeSound, closePitchScale, closeVolumeScale);
        }

        int aFrames = 80;
        float aDuration = RotTime(opening);
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

        if (isOpen)
        {
            if (canReClose)
            {
                if (unobstructedInteract != null)
                {
                    unobstructedInteract.SetEnabled(true);
                }
            }
        }
        else
        {
            if (unobstructedInteract != null)
            {
                unobstructedInteract.SetEnabled(true);
            }
            if (obstructedInteract != null)
            {
                obstructedInteract.SetEnabled(true);
            }
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

    private float RotTime(bool opening)
    {
        if (opening)
        {
            return openTime;
        }
        else
        {
            return closeTime;
        }
    }

}
