using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenFrame : UI
{
    #region [ PARAMETERS ]

    private GameObject frame;
    private List<Image> slots = new List<Image>();
    private List<Button> slotBtns = new List<Button>();

    private Player player;

    private bool invenVis;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
        SetButtonFunctions();
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
            Button slotBtn = null;
            for (int j = 0; j < slot.transform.childCount; j++)
            {
                if (slot.transform.GetChild(j).gameObject.CompareTag("Icon"))
                {
                    slotImg = slot.transform.GetChild(j).gameObject.GetComponent<Image>();
                }
                else if (slot.transform.GetChild(j).gameObject.CompareTag("Button"))
                {
                    slotBtn = slot.transform.GetChild(j).gameObject.GetComponent<Button>();
                }
            }
            slots.Add(slotImg);
            slotBtns.Add(slotBtn);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void SetButtonFunctions()
    {
        for (int i = 0; i < slotBtns.Count; i++)
        {
            Button btn = slotBtns[i];
            int n = i;
            btn.onClick.AddListener(
                delegate{
                    Debug.Log(player.Inventory.GetItemData(i)[0]);
                }
            );
        }
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

    public void TestDubug(string text)
    {
        Debug.Log(text);
    }
}
