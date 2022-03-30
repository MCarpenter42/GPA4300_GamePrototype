using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenFrame : UI
{
    #region [ PARAMETERS ]

    private GameObject frame;
    private List<Image> slots = new List<Image>();

    private Player player;

    private bool invenVis;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }

    void Start()
    {
        UpdateIcons();
        ShowInven(false);
    }
	
    void Update()
    {
        if (Input.GetKeyDown(controls.menu.inventory))
        {
            ShowInven(!invenVis);
        }
    }

    void FixedUpdate()
    {
        
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
	
    private void GetComponents()
    {
        frame = gameObject.transform.GetChild(0).gameObject;

        GameObject slotsContainer = new GameObject();
        for (int i = 0; i < frame.transform.childCount; i++)
        {
            GameObject target = frame.transform.GetChild(i).gameObject;
            if (target.CompareTag("Slots"))
            {
                slotsContainer = target;
            }
        }
        for (int i = 0; i < slotsContainer.transform.childCount; i++)
        {
            GameObject slot = slotsContainer.transform.GetChild(i).gameObject;
            Image slotImg = null;
            for (int j = 0; j < slot.transform.childCount; j++)
            {
                if (slot.transform.GetChild(j).gameObject.GetComponent<Image>() != null)
                {
                    slotImg = slot.transform.GetChild(j).gameObject.GetComponent<Image>();
                }
            }
            slots.Add(slotImg);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            string spritePath = player.Inventory.GetItemData(i)[2];
            slots[i].sprite = Resources.Load<Sprite>(spritePath);
        }
    }

    public void ShowInven(bool show)
    {
        if (show)
        {
            LockCursor(false);
            frame.SetActive(true);
            UpdateIcons();
        }
        else
        {
            LockCursor(true);
            frame.SetActive(false);
        }
        invenVis = show;
    }


}
