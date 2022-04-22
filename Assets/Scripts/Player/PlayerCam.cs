using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCam : CoreFunctionality
{
    #region [ PARAMETERS ]

    private GameObject player;
    private float followHeight;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        FollowPosition();
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void SetFollow(GameObject player, float height)
    {
        this.player = player;
        followHeight = height;
    }

    public void FollowPosition()
    {
        Vector3 pos = player.transform.position;
        pos[1] += followHeight;
        this.transform.position = pos;
    }

    public void SetRot(Vector3 rotation)
    {
        transform.eulerAngles = rotation;
    }
}
