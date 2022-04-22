using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This version of the interaction handler script prevents interaction with it
// unless the player has an item with the correct ID in their inventory.

public class LockedInteract : Interaction
{
    #region [ PARAMETERS ]

    public bool isLocked { get; private set; }
    [SerializeField] int unlockedIcon;

    [Header("Key item")]
    [SerializeField] ItemNames keyItemID;
    private int invenSlot;
    private List<int> keysTried = new List<int>();

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public LockedInteract()
    {
        this.isEnabled = true;
        this.isLocked = true;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    // This overrides the default to prevent the interaction from
    // triggering until unlocked.
    public override bool InteractEvent()
    {
        if (interactEvent == null)
        {
            return false;
        }
        else
        {
            if (isLocked)
            {
                // When the player *does* have the correct key item, it
                // unlocks and changes the icon on first keypress, then
                // triggers normally after that.
                int[] hasKey = player.Inventory.CheckForItem((int)keyItemID, true);
                if (ToBool(hasKey[0]))
                {
                    isLocked = false;
                    SetIcon(unlockedIcon);
                    CheckKeyBreak(hasKey[1]);
                }
                else
                {
                    string notifText = "You don't have the right key...";
                    hud.Notification(notifText);
                }
            }
            else
            {
                interactEvent.Invoke();
            }
            return true;
        }
    }

    public void SetLockState(bool lockState)
    {
        isLocked = lockState;
    }

    // If they key item's data says that it's meant to break when
    // used, this makes sure that happens.
    private void CheckKeyBreak(int index)
    {
        if (itemDB.items[(int)keyItemID].breakWhenUsed)
        {
            player.Inventory.SetSlot(index, 0);
            string notifText = itemDB.items[(int)keyItemID].name + " broke!";
            hud.Notification(notifText);
        }
    }
}
