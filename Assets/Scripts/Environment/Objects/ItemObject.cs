using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemObject : CoreFunctionality
{
	#region [ PARAMETERS ]
	
    [SerializeField] int itemID = -1;

    private Player player;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Start()
    {
        GetPlayer();
    }
	
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    private void GetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Pickup(Interaction interact)
    {
        bool pickedUp = player.Inventory.AddItem(itemID);
        if (pickedUp)
        {
            interact.SetEnabled(false);
            gameObject.SetActive(false);
        }
    }
}
