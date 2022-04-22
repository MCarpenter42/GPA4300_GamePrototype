using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : CoreFunctionality
{
	#region [ PARAMETERS ]
	
    [SerializeField] ItemNames itemID;

    private Player player;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }
	
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    private void GetComponents()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Pickup(Interaction interact)
    {
        bool pickedUp = player.Inventory.AddItem((int)itemID);
        if (pickedUp)
        {
            interact.SetEnabled(false);
            gameObject.SetActive(false);
        }
    }
}
