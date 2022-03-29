using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : CoreFunctionality
{
    #region [ PARAMETERS ]

    private int[] items = new int[30];

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    public bool AddItem(int id)
    {
        bool itemAdded = false;
        int i;
        for (i = 0; i < items.Length; i++)
        {
            if (items[i] == 0)
            {
                items[i] = id;
                itemAdded = true;
                break;
            }
        }
        Debug.Log("Item \"" + itemDB.items[i].name + "\" added to inventory slot " + i + ".");
        return itemAdded;
    }

    public bool CheckForItem(int id)
    {
        bool isPresent = false;
        foreach (int itemID in items)
        {
            if (itemID == id)
            {
                isPresent = true;
            }
        }
        return isPresent;
    }

    public void SwitchSlots(int slot1_pos, int slot2_pos)
    {
        int slot1_id = items[slot1_pos];
        int slot2_id = items[slot2_pos];
        items[slot1_pos] = slot2_id;
        items[slot2_pos] = slot1_id;
    }
}
