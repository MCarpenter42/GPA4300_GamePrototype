using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class adds functionality to a chest object that allows the
// lid to open and close smoothly. It's inteded for use with the
// interaction handler, but it can also be triggered from elsewhere.

public class Chest : CoreFunctionality
{
    #region [ PARAMETERS ]

    [Header("Attributes")]
    [SerializeField] GameObject lid;
    [SerializeField] float openAngle;
    [SerializeField] rotVectors hingeVector;
    private Vector3 rotVector;
    [SerializeField] bool invertOpen;

    [SerializeField] bool autoClose;
    [SerializeField] float closeDelay;

    [SerializeField] float openTime = 0.8f;
    [SerializeField] float closeTime;
    private float aDuration;

    [Header("Interaction Point")]
    [SerializeField] Interaction interact;
    [SerializeField] LockedInteract lockInteract;

    private bool isMoving;
    private bool isOpen;
    private Vector3 rotClosed;
    private Vector3 rotOpen;

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
        GetRotValues();
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

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetRotValues()
    {
        rotClosed = lid.transform.localEulerAngles;

        int inv;
        if (invertOpen)
        {
            inv = -1;
        }
        else
        {
            inv = 1;
        }

        if (hingeVector == rotVectors.X)
        {
            rotVector = new Vector3(1.0f, 0.0f, 0.0f);
        }
        else if (hingeVector == rotVectors.Y)
        {
            rotVector = new Vector3(0.0f, 1.0f, 0.0f);
        }
        else if (hingeVector == rotVectors.Z)
        {
            rotVector = new Vector3(0.0f, 0.0f, 1.0f);
        }
        rotOpen = rotClosed + rotVector * openAngle * inv;
    }

    public void SetOpen(bool open)
    {
        if (!isMoving)
        {
            if (autoClose)
            {
                StartCoroutine(OpenCloseSequence());
            }
            else
            {
                StartCoroutine(RotTransition(open));
            }
        }
    }

    private IEnumerator OpenCloseSequence()
    {
        StartCoroutine(RotTransition(true));
        yield return new WaitForSeconds(aDuration + closeDelay);
        StartCoroutine(RotTransition(false));
    }

    private IEnumerator RotTransition(bool opening)
    {
        isMoving = true;

        Vector3 rotStart = lid.transform.localEulerAngles;
        Vector3 rotEnd;

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

        if (rotStart.magnitude > 180.0f)
        {
            float x = rotStart.magnitude - 360.0f;
            rotStart = rotVector * x;
        }
        
        if (rotEnd.magnitude > 180.0f)
        {
            float x = rotEnd.magnitude - 360.0f;
            rotEnd = rotVector * x;
        }

        if (opening && interact != null && interact.gameObject.activeSelf)
        {
            interact.SetEnabled(false);
        }
        if (opening && lockInteract != null && lockInteract.gameObject.activeSelf)
        {
            lockInteract.SetEnabled(false);
        }

        int aFrames = 80;
        aDuration = RotTime(opening);
        float aFrameTime = aDuration / (float)aFrames;
        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            yield return new WaitForSeconds(aFrameTime);
            lid.transform.localEulerAngles = Vector3.Lerp(rotStart, rotEnd, delta);
        }


        if (!autoClose || (autoClose && !opening))
        {
            isMoving = false;
        }
        isOpen = opening;

        if (!opening && interact != null && interact.gameObject.activeSelf)
        {
            interact.SetEnabled(true);
        }
        if (!opening && lockInteract != null && lockInteract.gameObject.activeSelf)
        {
            lockInteract.SetEnabled(true);
        }
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
