using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class InvenFrame : UI
{
    #region [ PARAMETERS ]

    private FrameHandler frameHandler;

    private List<Image> slots = new List<Image>();
    private List<Button> slotBtns = new List<Button>();

    private GameObject tooltip;
    private TextMeshProUGUI[] tooltipText;

    public List<UnityEvent> clickEvents = new List<UnityEvent>();

    private Player player;

    private bool invenVis;

	#endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        frameHandler = gameObject.AddComponent<FrameHandler>();
        frameHandler.SetValues(false);
        frameHandler.onShow = OnShow;
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
        frameHandler.GetComponents(gameObject);
        frameHandler.onShow = OnShow;

        GameObject frame = frameHandler.frames[0];
        GameObject slotsContainer = new GameObject();

        for (int i = 0; i < frame.transform.childCount; i++)
        {
            GameObject target = frame.transform.GetChild(i).gameObject;
            if (target.CompareTag("Slots"))
            {
                slotsContainer = target;
            }
            if (target.CompareTag("Tooltip"))
            {
                tooltip = target;
                Transform frameTransform = tooltip.transform.GetChild(0);
                tooltipText = new TextMeshProUGUI[3];
                for (int j = 0; j < frameTransform.childCount; j++)
                {
                    GameObject child = frameTransform.GetChild(j).gameObject;
                    if (child.CompareTag("Name"))
                    {
                        tooltipText[0] = child.GetComponent<TextMeshProUGUI>();
                    }
                    else if (child.CompareTag("Description"))
                    {
                        tooltipText[1] = child.GetComponent<TextMeshProUGUI>();
                    }
                    else if (child.CompareTag("Additional"))
                    {
                        tooltipText[2] = child.GetComponent<TextMeshProUGUI>();
                    }
                }
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
                    slotBtn.gameObject.GetComponent<InvenSlot>().index = i;
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
                    CheckForClickEvent(n);
                }
            );
        }
    }

    public void UpdateIcons()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            string spritePath = player.Inventory.GetItemData(i).iconPath;
            slots[i].sprite = Resources.Load<Sprite>(spritePath);
        }
    }

    public void ShowInven(bool show)
    {
        frameHandler.Show(show);
    }

    private void OnShow(bool show)
    {
        if (show)
        {
            LockCursor(false);
            UpdateIcons();
        }
        else
        {
            LockCursor(true);
            tooltip.SetActive(false);
        }
        invenVis = show;
    }

    public void ShowTooltip(int index)
    {
        if (player.Inventory.GetItemID(index) > 0)
        {
            Vector3 newPos = slotBtns[index].transform.position;
            tooltip.SetActive(true);
            tooltip.transform.position = newPos;
            UpdateTooltip(index);
        }
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }

    private void UpdateTooltip(int index)
    {
        string name = player.Inventory.GetItemData(index).name;
        string desc = player.Inventory.GetItemData(index).description;
        string mInf = player.Inventory.GetItemData(index).moreInfo;

        tooltipText[0].text = name;
        tooltipText[1].text = desc;
        tooltipText[2].text = mInf;

        RectTransform ttFrameRect = tooltip.transform.GetChild(0).GetComponent<RectTransform>();
        if (mInf == null)
        {
            ttFrameRect.sizeDelta = new Vector2(404.0f, 144.0f);
        }
        else
        {
            ttFrameRect.sizeDelta = new Vector2(404.0f, 176.0f);

            Item itemData = player.Inventory.GetItemData(index);
            FontStyles style = FontStyles.Normal;
            if (itemData.moreInfoItalic)
            {
                style = FontStyles.Italic;
            }
            tooltipText[2].fontStyle = style;
            tooltipText[2].color = itemData.moreInfoColour;
        }
    }

    private void CheckForClickEvent(int index)
    {
        int eventID = player.Inventory.GetItemData(index).actionOnClick;
        if (eventID > -1)
        {
            OnClickEvent(eventID);
        }
    }

    public void OnClickEvent(int n)
    {
        if (InBounds(n, clickEvents))
        {
            clickEvents[n].Invoke();
        }
    }
}
