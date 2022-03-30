using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockedInteract : Interaction
{
    #region [ PARAMETERS ]

    public bool isLocked { get; private set; }
    [SerializeField] int unlockedIcon;

    [SerializeField] int keyItemID;
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
                bool hasKey = player.Inventory.CheckForItem(keyItemID);
                if (hasKey)
                {
                    //Debug.Log("Unlocked!");
                    isLocked = false;
                    interactEvent.Invoke();
                }
                else
                {
                    //Debug.Log("You don't have the right key...");
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
}
