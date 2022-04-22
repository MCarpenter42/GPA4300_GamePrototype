using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : CoreFunctionality
{
    public List<GameObject> floor;
    public List<GameObject> key;
    public List<Vector3> keyzone1;
    public List<Vector3> keyzone2;
    public List<Vector3> keyzone3;
    public List<Vector3> keyrot1;
    public List<Vector3> keyrot2;
    public List<Vector3> keyrot3;
    public Vector3 playzone;

    int roomhold;
    int keyhold;

    // [I] Inital floor spawn when the game starts randomly selecting between three rooms
    void Start()
    {
        // hold = Random.Range(0, 3);
        roomhold = RandomInt(0, floor.Count - 1);
        // Changed how the value is selected, as Untiy's "Random.Range" behaves slightly differently to C#'sa default random number generation
        floor[roomhold].transform.position = new Vector3(playzone.x, playzone.y, playzone.z);

        keyhold = RandomInt(0, key.Count - 1);
        switch (keyhold)
        {
            case 0:
                key[roomhold].transform.position = new Vector3(keyzone1[roomhold].x, keyzone1[roomhold].y, keyzone1[roomhold].z);
                key[roomhold].transform.eulerAngles = new Vector3(keyrot1[roomhold].x, keyrot1[roomhold].y, keyrot1[roomhold].z);
                break;
            case 1:
                key[roomhold].transform.position = new Vector3(keyzone2[roomhold].x, keyzone2[roomhold].y, keyzone2[roomhold].z);
                key[roomhold].transform.eulerAngles = new Vector3(keyrot2[roomhold].x, keyrot2[roomhold].y, keyrot2[roomhold].z);
                break;
            case 2:
                key[roomhold].transform.position = new Vector3(keyzone3[roomhold].x, keyzone3[roomhold].y, keyzone3[roomhold].z);
                key[roomhold].transform.eulerAngles = new Vector3(keyrot3[roomhold].x, keyrot3[roomhold].y, keyrot3[roomhold].z);
                break;
            case 3:
                key[roomhold].transform.position = new Vector3(keyzone3[roomhold].x, keyzone3[roomhold].y, keyzone3[roomhold].z);
                key[roomhold].transform.eulerAngles = new Vector3(keyrot3[roomhold].x, keyrot3[roomhold].y, keyrot3[roomhold].z);
                break;   
        }
    }
}

