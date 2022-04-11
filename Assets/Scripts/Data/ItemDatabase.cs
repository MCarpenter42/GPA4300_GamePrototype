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
				"EmptyIcon",
				false
			),
			// ID 1 - Test key
			new Item
			(
				"Iron Key",
				"A simple iron key.",
				"Key1",
				false
			),
			// ID 2 - Emerald
			new Item
			(
				"Emerald",
				"A highly polished green gemstone.",
				"Gem1",
				false
			),
			// ID 3 - Lockpins
			new Item
			(
				"Lockpins",
				"Useful to lockpick the cell door",
				"lockpins",
				true
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

	public readonly bool breakWhenUsed;

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public Item(string name, string description, string iconName, bool breakWhenUsed)
    {
		this.name = name;
		this.description = description;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = breakWhenUsed;
	}
}
