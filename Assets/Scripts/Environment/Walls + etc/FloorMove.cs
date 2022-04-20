using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : CoreFunctionality
{
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;
    public Vector3 playzone;

    int hold;

    // [I] Inital floor spawn when the game starts randomly selecting between three rooms
    void Start()
    {
        List<GameObject> floors = new List<GameObject> {floor1, floor2, floor3};
        // hold = Random.Range(0, 3);
        hold = RandomInt(0, floors.Count - 1);
        // Changed how the value is selected, as Untiy's "Random.Range" behaves slightly differently to C#'sa default random number generation
        floors[hold].transform.position = new Vector3(playzone.x, playzone.y, playzone.z);
    }
}

