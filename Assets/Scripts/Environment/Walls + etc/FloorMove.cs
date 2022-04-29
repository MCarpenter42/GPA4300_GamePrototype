using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloorMove : CoreFunctionality
{
    public List<GameObject> floor;
    public List<GameObject> key;
    public List<GameObject> jpos;
    public List<GameObject> cpos;
    public List<GameObject> bpos;
    public List<TextMeshProUGUI> hintText;
    public Vector3 playzone;

    int roomhold;
    int keyhold;

    // [I] Inital floor spawn when the game starts randomly selecting between three rooms
    void Start()
    {
        // hold = Random.Range(0, 3);
        roomhold = RandomInt(0, floor.Count - 1);
        // Changed how the value is selected, as Untiy's "Random.Range" behaves slightly differently to C#'s default random number generation
        floor[roomhold].transform.position = new Vector3(playzone.x, playzone.y, playzone.z);

        switch (roomhold)
        {
            case 0:
                keyhold = RandomInt(0, jpos.Count - 1);
                key[roomhold].transform.position = jpos[keyhold].transform.position;
                key[roomhold].transform.eulerAngles = jpos[keyhold].transform.eulerAngles;
                break;
            case 1:
                keyhold = RandomInt(0, cpos.Count - 1);
                key[roomhold].transform.position = cpos[keyhold].transform.position;
                key[roomhold].transform.eulerAngles = cpos[keyhold].transform.eulerAngles;
                break;
            case 2:
                keyhold = RandomInt(0, bpos.Count - 1);
                key[roomhold].transform.position = bpos[keyhold].transform.position;
                key[roomhold].transform.eulerAngles = bpos[keyhold].transform.eulerAngles;
                break;
        }

        SetHintText();
    }

    private void SetHintText()
    {
        switch (roomhold)
        {
            case 0:
                hintText[roomhold].text = jpos[keyhold].GetComponent<KeyLocation>().GetHintText();
                break;
            case 1:
                hintText[roomhold].text = cpos[keyhold].GetComponent<KeyLocation>().GetHintText();
                break;
            case 2:
                hintText[roomhold].text = bpos[keyhold].GetComponent<KeyLocation>().GetHintText();
                break;
        }
    }
}

