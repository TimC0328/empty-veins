using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory;
    GameObject slots;

    public int currentSlot = 0;
    public Item selection;
    public Item combine = null;

    public int option = 0;

    GameObject itemMenu;

    Transform cursor;

    public enum Modes {Inventory, Item, Menu}
    Modes mode = Modes.Inventory;

    public Text description;

    Typewriter typewriter;

    #region Singleton
    public static InventoryUI instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the InventoryUI found");
        }
        instance = this;
    }
    #endregion

    void Start()
    {
        inventory = Inventory.instance;


        typewriter = GameObject.Find("Canvas").GetComponent<Typewriter>();

        cursor = this.transform.GetChild(0);

        slots = this.transform.GetChild(1).gameObject;

        itemMenu = this.transform.GetChild(2).gameObject;

        description = this.transform.GetChild(3).gameObject.GetComponent<Text>();


        itemMenu.SetActive(false);


        mode = Modes.Inventory;
    }

    void Update()
    {
        HandleInput();
        cursor.position = slots.transform.GetChild(currentSlot).transform.position;
    }

    public void UpdateUI()
    {
        GameObject slot, slotChild;
        int count = 0;

        for (int i = 0; i < inventory.items.Count; i++)
        {
            slot = slots.transform.GetChild(i).gameObject;
            slot.SetActive(true);

            slotChild = slot.transform.GetChild(0).gameObject;
            slotChild.SetActive(true);

            if(inventory.items[i].icon != null)
                slotChild.GetComponent<Image>().sprite = inventory.items[i].icon;
            if (inventory.items[i].quantity == -1)
                slot.transform.GetChild(1).gameObject.GetComponent<Text>().text = "";
            else
                slot.transform.GetChild(1).gameObject.GetComponent<Text>().text = ""+inventory.items[i].quantity;


            count++;
        }


        for (int i = count; i < 10; i++)
        {
            slot = slots.transform.GetChild(i).gameObject;
            slot.SetActive(false);
        }

        if (combine == null)
            description.text = "";


    }

    void HandleInput()
    {
        if (mode == Modes.Item)
        {
            HandleItemMenu();
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentSlot <= 1)
                return;

            currentSlot -= 2;
            return;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentSlot >= 8)
                return;

            if (inventory.items.Count < (currentSlot + 3))
                return;

            currentSlot += 2;
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentSlot % 2 == 0)
                return;

            currentSlot -= 1;
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentSlot % 2 > 0)
                return;

            if (inventory.items.Count < currentSlot + 2)
                return;

            currentSlot += 1;
            return;
        }


        if (Input.GetKeyDown("enter") || Input.GetKeyDown("space"))
        {
            selection = inventory.items[currentSlot];
            if(combine != null)
            {
                if (inventory.items.IndexOf(combine) == currentSlot)
                {
                    description.text = "";
                    combine = null;
                    return;
                }

                if (inventory.Combine(combine, selection))
                {
                    typewriter.TypewriterEffect(description, ("Combined."), null);
                    currentSlot = 0;
                }
                else
                    typewriter.TypewriterEffect(description, ("Cannot combine: " + combine.name + " with " + selection.name), null);

                UpdateUI();
                combine = null;
                return;
            }

            description.text = selection.name;

            mode = Modes.Item;

            itemMenu.SetActive(true);
            itemMenu.transform.position = new Vector3(slots.transform.GetChild(currentSlot).position.x - 133.5f, slots.transform.GetChild(currentSlot).position.y - 33f, 1f);

            option = 0;
            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = Color.white;

        }

    }

    void HandleItemMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (option == 0)
                return;

            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = new Color(0.55f, 0f, 0f, 1f);
            option--;
            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = Color.white;

        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (option == 2)
                return;

            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = new Color(0.55f, 0f, 0f, 255f);
            option++;
            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = Color.white;
        }

        if (Input.GetKeyDown("enter") || Input.GetKeyDown("space"))
        {
            switch(option)
            {
                case 0:
                    if (selection.itemType != Item.ItemType.Equip)
                        typewriter.TypewriterEffect(description, ("Using: " + selection.name), null);
                    else
                        typewriter.TypewriterEffect(description, ("Equipping: " + selection.name), null);
                    break;
                case 1:
                    typewriter.TypewriterEffect(description, selection.description, null);
                    break;
                case 2:
                    typewriter.TypewriterEffect(description, ("Combine " + selection.name + " with?"), null);
                    combine = selection;
                    break;
            }
            itemMenu.transform.GetChild(0).GetChild(option).gameObject.GetComponent<Text>().color = new Color(0.55f, 0f, 0f, 255f);
            itemMenu.SetActive(false);
            mode = Modes.Inventory;

        }
    }
}

