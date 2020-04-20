using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerifyCategories : MonoBehaviour
{
    UIInventory mainInventory;
    UIInventory supporting;
    UIInventory refuting;
    UIInventory irrelevant;

    Image worldMapBtn;

    int attempts = 0;
    public static bool itemsInMainInventory = true;

    public void SubmitSlot()
    {
        if(mainInventory.invSlots.Count == 0)
        {
            itemsInMainInventory = false;
        }

        // Loop to check if there are any items in the main inventory
        for(int i = 0; i < mainInventory.invSlots.Count; i++)
        {
            InvSlot invSlot = mainInventory.invSlots[i];

            if (invSlot.item != null)
            {
                itemsInMainInventory = true;
                break;
            }

            if(i == mainInventory.invSlots.Count - 1)
            {
                itemsInMainInventory = false;
            }
        }

        if(attempts != 3)
        {
            // If there are items in the main inventory, make player move items to submit canvas before checking items
            if (itemsInMainInventory == true)
            {
                ModManager.instance.sendMsgWithBtn("There are still items in the inventory. Please move them to the Evidence slots.");
            }
            else
            {
                // Check if all items are in correct panel
                if (CheckCategories(supporting, 1) == false || CheckCategories(refuting, 0) == false || CheckCategories(irrelevant, 2) == false)
                {
                    attempts++;

                    

                    if (attempts == 3)
                    {
                        ClearInventory(mainInventory);
                        ClearInventory(supporting);
                        ClearInventory(refuting);
                        ClearInventory(irrelevant);
                        worldMapBtn.color = new Color(1, 1, 1, 1);
                        ModManager.instance.flag = true;
                        ModManager.instance.sendMsgWithBtn("You've exhausted all your attempts. Please rewatch the video");
                    }
                    else
                        ModManager.instance.sendMsgWithBtn("Sorry, that is incorrect.\nNumber of Attempts left: " + (3 - attempts));
                }
                else
                {
                    ModManager.instance.sendMsgWithBtn("Congratulations, you've passed this level! Continue by clicking the World Map button");
                    worldMapBtn.color = new Color(1, 1, 1, 1);

                    ClearInventory(mainInventory);
                    ClearInventory(supporting);
                    ClearInventory(refuting);
                    ClearInventory(irrelevant);

                    ModManager.instance.flag = true;
                    ModManager.instance.postStart();
                }
            }
        }
        else
        {
            ModManager.instance.sendMsgWithBtn("You've exhausted all your attempts. Please rewatch the video");
        }
       
    }


    public bool CheckCategories(UIInventory inventory, int category)
    {
        foreach (InvSlot invSlot in inventory.invSlots)
        {
            
            if (invSlot.item != null)
            {
                Debug.Log(invSlot.item.category);
                if (invSlot.item.category != category)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void ClearInventory(UIInventory inventory)
    {
        foreach (InvSlot invSlot in inventory.invSlots)
        {
            invSlot.item = null;
        }
    }

    private void Start()
    {
        mainInventory = GameObject.Find("/Inventory Canvas/Inventory 2").GetComponent<UIInventory>();
        supporting = GameObject.Find("/Submit Canvas/Supporting Panel").GetComponent<UIInventory>();
        refuting = GameObject.Find("/Submit Canvas/Refuting Panel").GetComponent<UIInventory>();
        irrelevant = GameObject.Find("/Submit Canvas/Irrelevant Panel").GetComponent<UIInventory>();

        worldMapBtn = GameObject.Find("/Canvas/Buttons/World Map Button").GetComponent<Image>();
        
    }

}
