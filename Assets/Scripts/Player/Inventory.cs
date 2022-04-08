using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : CoreFunctionality
{
    #region [ PARAMETERS ]

    private int[] items = new int[30];

    public InvenFrame invenFrame;

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
        Debug.Log("Item of ID " + id + " (\"" + itemDB.items[id].name + "\") added to inventory slot " + i + ".");
        invenFrame.UpdateIcons();
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
        invenFrame.UpdateIcons();
    }

    public string[] GetItemData(int index)
    {
        int itemID = this.items[index];
        if (itemID < itemDB.items.Count)
        {
            Item targetItem = itemDB.items[this.items[index]];
            //Debug.Log("Data for slot " + index + " -\nItem ID: " + itemID);
            return new string[] { targetItem.name, targetItem.description, targetItem.iconPath };
        }
        else
        {
            return new string[] { "<INVALID_ID>", "", "Images/Sprites/InventoryIcons/EmptyIcon" };
        }
    }
}
