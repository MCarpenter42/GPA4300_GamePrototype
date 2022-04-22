using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class stores the information about the various items the player
// can pick up, as the player's actual inventory is just an integer array.

public class ItemDatabase
{
	#region [ PARAMETERS ]

	public List<Item> items = new List<Item>
	{
		// ID 0 - Empty item
		new Item
		(
			null,
			null,
			"EmptyIcon"
		),
		// ID 1 - Basic key
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
		// ID 5 - Note B
		new Item
		(
			"Handwritten Note",
			"A cryptic note written in spidery handwriting.",
			"< Click to read >",
			false,
			Color.blue,
			1,
			"PaperSheet",
			false
		),
		// ID 6 - Note C
		new Item
		(
			"Handwritten Note",
			"A cryptic note written in spidery handwriting.",
			"< Click to read >",
			false,
			Color.blue,
			2,
			"PaperSheet",
			false
		),
		// ID 7 - Basic key
		new Item
		(
			"Brass Key",
			"A simple brass key.",
			"Key2"
		),
	};

	#endregion

	/* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

}

// This class handles information storage for individual items

public class Item
{
	#region [ PARAMETERS ]

	// Basic display information - all items will share this
	public readonly string name;
	public readonly string description;

	// Some items need an extra bit of text with slightly different formatting
	public readonly string moreInfo;
	public readonly bool moreInfoItalic;
	public readonly Color moreInfoColour;

	// Some items need to trigger a function when clicked on in the inventory
	public readonly int actionOnClick;

	// Filepath for the item's inventory icon
	// This is re-created in the constructor from the directory path the icons
	// are stored in, and the actual name of the file, which is passed as an
	// argument when the constructor is called.
	public readonly string iconPath;

	// Whether the item should be removed from the inventory when used
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
