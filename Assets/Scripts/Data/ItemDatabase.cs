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
				"Tough Needles",
				"Might be able to be used as lockpicks.",
				"lockpins",
				true
			),
			// ID 4 - Note A
			new Item
			(
				"Handwritten Note",
				"A cryptic note written in spidery handwriting.",
				"< Click to read >",
				false,
				Color.blue,
				0,
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
	public readonly bool moreInfoItalic;
	public readonly Color moreInfoColour;

	public readonly int actionOnClick;

	public readonly string iconPath;

	public readonly bool breakWhenUsed;

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

	public Item(string name, string description, string iconName)
    {
		this.name = name;
		this.description = description;
		this.moreInfo = null;
		this.moreInfoItalic = false;
		this.moreInfoColour = Color.black;
		this.actionOnClick = -1;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = false;
	}
	
	public Item(string name, string description, string iconName, bool breakWhenUsed)
    {
		this.name = name;
		this.description = description;
		this.moreInfo = null;
		this.moreInfoItalic = false;
		this.moreInfoColour = Color.black;
		this.actionOnClick = -1;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = breakWhenUsed;
	}

	public Item(string name, string description, string moreInfo, bool moreInfoItalic, Color moreInfoColour, string iconName, bool breakWhenUsed)
    {
		this.name = name;
		this.description = description;
		this.moreInfo = moreInfo;
		this.moreInfoItalic = moreInfoItalic;
		this.moreInfoColour = moreInfoColour;
		this.actionOnClick = -1;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = breakWhenUsed;
	}

	public Item(string name, string description, string moreInfo, bool moreInfoItalic, Color moreInfoColour, int actionOnClick, string iconName, bool breakWhenUsed)
    {
		this.name = name;
		this.description = description;
		this.moreInfo = moreInfo;
		this.moreInfoItalic = moreInfoItalic;
		this.moreInfoColour = moreInfoColour;
		this.actionOnClick = actionOnClick;
		this.iconPath = "Images/Sprites/InventoryIcons/" + iconName;
		this.breakWhenUsed = breakWhenUsed;
	}
}
