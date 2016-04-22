using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public Stack<Item> ItemStack = new Stack<Item>();

    public Sprite NormalSlotSprite;
    public Sprite HighlightedSlotSprite;

    public bool IsAvailable()
    {
        if (ItemStack.Count < ItemStack.Peek().MaxPerSlot)
            return true;
        else
            return false;
    }

    public bool IsEmpty()
    {
        if (ItemStack.Count == 0)
            return true;
        else
            return false;
    }

    public void ChangeSprites(Sprite NormalSprite, Sprite HighlightedSprite)
    {
        SpriteState state = new SpriteState();
        state.highlightedSprite = HighlightedSprite;
        state.pressedSprite = NormalSprite;
        GetComponent<Button>().spriteState = state;

        Image image = GetComponent<Image>();
        image.sprite = NormalSprite;
    }

    public void ChangeItemAmountText()
    {
        Text text = GetComponentInChildren<Text>();
        text.text = (ItemStack.Count > 1) ? ItemStack.Count.ToString() : string.Empty;
    }

    private void DropItem()
    {
        GameObject Droped = (GameObject)Instantiate(ItemStack.Peek().Prefab, new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z + 2), Quaternion.identity);
        Droped.GetComponent<Rigidbody>().useGravity = true;
        Droped.name = ItemStack.Peek().Prefab.name;

        if (ItemStack.Count > 1)
        {
            ItemStack.Pop();
            ChangeItemAmountText();
        }
        else
        {
            ItemStack.Pop();
            ChangeSprites(NormalSlotSprite, HighlightedSlotSprite);
            ChangeItemAmountText();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            DropItem();
        }
    }
}