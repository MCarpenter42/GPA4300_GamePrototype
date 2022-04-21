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
				null,
				"EmptyIcon",
				false
			),
			// ID 1 - Test key
			new Item
			(
				"Iron Key",
				"A simple iron key.",
				null,
				"Key1",
				false
			),
			// ID 2 - Emerald
			new Item
			(
				"Emerald",
				"A highly polished green gemstone.",
				null,
				"Gem1",
				false
			),
			// ID 3 - Lockpins
			new Item
			(
				"Tough needles",
				"Might be able to be used as lockpicks.",
				null,
				"lockpins",
				true
			),
			// ID 4 - Note A
			new Item
			(
				"Handwritten Note",
				"A cryptic note written in spidery handwriting.",
				"< Click to read >",
				"PaperSheet",
				false
			),
		};
    }
}

public class Item
{
	#region [ PARAMETERS ]

	public readonly string name;
	public readonly string description;
	public readonly string moreInfo;
	public readonly string iconPath;

	public readonly bool breakWhenUsed;

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public Item(string name, string description, string moreInfo, string iconName, bool breakWhenUsed)
    {
		this.name = name;
		this.description = description;
		this.moreInfo = moreInfo;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = breakWhenUsed;
	}
}
