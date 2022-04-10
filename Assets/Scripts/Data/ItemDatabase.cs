using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDatabase
{
	#region [ PARAMETERS ]
	
	public List<Item> items { get; private set; }

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public void CompileDatabase()
    {
		items = new List<Item>
		{
			// ID 0 - Empty item
			new Item
			(
				null,
				null,
				"EmptyIcon"
			),
			// ID 1 - Test key
			new Item
			(
				"Iron Key",
				"A simple iron key.",
				"Key1"
			),
			// ID 2 - Emerald
			new Item
			(
				"Emerald",
				"A highly polished green gemstone.",
				"Gem1"
			),
			// ID 3 - Lockpins
			new Item
			(
				"Lockpins",
				"Useful to lockpick the cell door",
				"lockpins"
			 ),
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
