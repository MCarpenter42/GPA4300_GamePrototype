using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                int[] hasKey = player.Inventory.CheckForItem((int)keyItemID, true);
                if (ToBool(hasKey[0]))
                {
                    Debug.Log("Unlocked!");
                    isLocked = false;
                    SetIcon(unlockedIcon);
                    //interactEvent.Invoke();
                    CheckKeyBreak(hasKey[1]);
                }
                else
                {
                    Debug.Log("You don't have the right key...");
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

    private void CheckKeyBreak(int index)
    {
        if (itemDB.items[(int)keyItemID].breakWhenUsed)
        {
            player.Inventory.SetSlot(index, 0);
        }
    }
}
