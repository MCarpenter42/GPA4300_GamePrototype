using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : CoreFunctionality
{
    #region [ PARAMETERS ]

    private int[] items = new int[30];

    public Player player;
    public InvenFrame invenFrame;
    public HUD hud;

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
        invenFrame.UpdateIcons();
        if (hud != null)
        {
            string notifText = itemDB.items[id].name + " added to inventory.";
            player.PlayClip(AudioSources.item, player.clipItemPickup);
            hud.Notification(notifText);
        }
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
    
    public int[] CheckForItem(int id, bool returnSlot)
    {
        int isPresent = 0;
        int slot = -1;
        for (int i = 0; i < items.Length; i++)
        {
            int itemID = items[i];
            if (itemID == id)
            {
                isPresent = 1;
                slot = i;
            }
        }

        int[] output = new int[] { isPresent, slot };

        return output;
    }

    public void SwitchSlots(int slot1_pos, int slot2_pos)
    {
        int slot1_id = items[slot1_pos];
        int slot2_id = items[slot2_pos];
        items[slot1_pos] = slot2_id;
        items[slot2_pos] = slot1_id;
        invenFrame.UpdateIcons();
    }

    public int GetItemID(int index)
    {
        return this.items[index];
    }

    public Item GetItemData(int index)
    {
        int itemID = this.items[index];
        return itemDB.items[this.items[index]];
    }

    public void SetSlot(int index, int value)
    {
        items[index] = value;
        invenFrame.UpdateIcons();
    }
}
