using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMove : CoreFunctionality
{
    public GameObject Key;
    // p is postion r is rotation
    public Vector3 P1;
    public Vector3 R1;

    public Vector3 P2;
    public Vector3 R2;

    public Vector3 P3;
    public Vector3 R3;

    public Vector3 P4;
    public Vector3 R4;

    int hold;

    // Similar to the rooms it will pick a location and rotation from a selection of 4 as of current
    void Start()
    {
        List<Vector3> Position = new List<Vector3> { P1, P2, P3, P4 };
        List<Vector3> Rotation = new List<Vector3> { R1, R2, R3, R4 };
        hold = RandomInt(0, Position.Count - 1);
        Key.transform.position = new Vector3(Position[hold].x, Position[hold].y, Position[hold].z);
        Key.transform.eulerAngles = new Vector3(Rotation[hold].x, Rotation[hold].y, Rotation[hold].z);
    }
}
