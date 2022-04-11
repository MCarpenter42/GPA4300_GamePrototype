using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public GameObject floor1;
    public GameObject floor2;
    public GameObject floor3;
    public GameObject walls1;
    public GameObject walls2;
    public Vector3 playzone;
    public Vector3 awayzone;

    int hold;

    // Inital floor spawn when the game starts randomly selecting between three rooms
    void Start()
    {
        GameObject[] floors = {floor1, floor2, floor3};
        hold = Random.Range(0, 3);
        floors[hold].transform.position = new Vector3(playzone.x, playzone.y, playzone.z);

        if (hold == 1)
        {
            walls2.transform.position = new Vector3(playzone.x, playzone.y, playzone.z);
        }
        else
        {
            walls1.transform.position = new Vector3(playzone.x, playzone.y, playzone.z);
        }
    }
    void Update()
    {
        GameObject[] floors = { floor1, floor2, floor3 };
        if (Input.GetKeyDown(KeyCode.Space))
        {
            floors[hold].transform.position = new Vector3(awayzone.x, awayzone.y, awayzone.z);

            hold = Random.Range(0, 3);
            floors[hold].transform.position = new Vector3(playzone.x, playzone.y, playzone.z);

            //swiches walls to an altrnative when hold is 1 (cannon room)
            //the walls are set to a cube allowing for the play and away zones to work for the walls
            if (hold == 1)
            {
                walls1.transform.position = new Vector3(awayzone.x, awayzone.y, awayzone.z);
                walls2.transform.position = new Vector3(playzone.x, playzone.y, playzone.z);
            }
            else
            {
                walls1.transform.position = new Vector3(playzone.x, playzone.y, playzone.z);
                walls2.transform.position = new Vector3(awayzone.x, awayzone.y, awayzone.z);
            }
        }
    }
}
