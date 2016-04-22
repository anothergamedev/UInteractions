using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public List<Slot> Slots = new List<Slot>();
    public CanvasGroup InventoryGroup;
    public Slot DragSlot;

    private void Awake()
    {
        Transform SlotsParent = InventoryGroup.transform.FindChild("Slots");
        for (int i = 0; i < SlotsParent.childCount; i++)
        {
            Slots.Add(SlotsParent.GetChild(i).GetComponent<Slot>());
        }
    }

    public void AddItem(Item newItemData)
    {
        for (int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].IsEmpty())
            {
                Slots[i].ItemStack.Push(newItemData);
                Slots[i].ChangeSprites(newItemData.NormalSprite, newItemData.HighlightedSprite);
                break;
            }
            else if(Slots[i].IsAvailable() && Slots[i].ItemStack.Peek().Name == newItemData.Name)
            {
                Slots[i].ItemStack.Push(newItemData);
                Slots[i].ChangeItemAmountText();
                break;
            }
        }
    }
    
    public void StartItemDrag(Slot proceedingSlot)
    {
        if(!proceedingSlot.IsEmpty())
        {
            proceedingSlot.GetComponent<Image>().color = Color.gray;
            proceedingSlot.GetComponent<Button>().interactable = false;

            DragSlot.gameObject.SetActive(true);
            DragSlot = proceedingSlot;

            DragSlot.transform.position = proceedingSlot.transform.position;
        }
    }
}