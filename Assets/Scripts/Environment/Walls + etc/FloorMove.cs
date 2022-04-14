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

    // [M] If you make all the parts of a room the children of an empty GameObject, you can move
    // the entire thing together, rather than moving the walls and floor separately - unless you
    // *wanted* to move them separately, in which case, you can still use a list, just have a
    // list for each component.

    // [M] Want to know the beauty of this? When you make a list visible in the inspector, you
    // can also add/remove list elements from there! That way,  you don't need to account for
    // adding rooms later on directly in the code, as that will be handled automatically!

    // [M] Also, advantage of putting them in a list is that, rather than having to use if/else
    // to handle the room, you can just use "listName[hold]" and it'll pick automatically! Be
    // sure to convert from float to integer first, though, as "Random.Range(a, b)" returns a
    // float value.

    // [M] Random.Range(a, b) is actually functionality from Unity that overrides base C# -
    // https://docs.unity3d.com/ScriptReference/Random.Range.html

    // [ SUGGESTED ]
    /*
    
    [SerializeField] List<GameObject> rooms;

    */

    int hold;

    // Inital floor spawn when the game starts randomly selecting between three rooms
    void Start()
    {
        GameObject[] floors = {floor1, floor2, floor3};
        // [M] If you switch to using lists, you can auto-generate the max value of the range!
        // Make sure to make it "listName.Count - 1", though, else you may hit a fringe case where
        // it tries to access a value at an index outside the list range
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

    // [M] May want to change this now! We don't want the player jumping to trigger the randomisation.
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

    // [M] My personal recommendation is to have a dedicated funtion to handle the randomisation, and
    // then just call it when relevant.

    // [ SUGGESTED ]
        // (i.e. in Awake or Start)
    /*
    
    private void RandomRoom()
    {

    }

    */

}
