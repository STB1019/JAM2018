using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LostItemReaction : MonoBehaviour
{
    public Item item;

    private Inventory inventory;

    protected override void SpecificInit()
    {
        inventory = FindObjectOfType<Inventory>();
    }

    protected override void ImmediateReaction()
    {
        inventory.RemoveItem(item);
    }
}
