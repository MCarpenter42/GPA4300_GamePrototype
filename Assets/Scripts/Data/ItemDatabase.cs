using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase
{
	#region [ PARAMETERS ]
	
	public Item[] items { get; private set; }

	public Item nullItem = new Item
	(
		null,
		null,
		"EmptyIcon"
	);

	public Item key1 = new Item
	(
		"Iron Key",
		"A simple iron key.",
		"Key1"
	);

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public void CompileDatabase()
    {
		items = new Item[]
		{
			nullItem,
			key1
		};
    }
}

public class Item
{
	#region [ PARAMETERS ]

	public readonly string name;
	public readonly string description;
	public readonly string iconPath;

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public Item(string name, string description, string iconName)
    {
		this.name = name;
		this.description = description;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
	}
}
