using UnityEngine;

public class Customer : TrashCan
{
    //REMOVE SERIALIZEFIELD ONLY FOR TESTING
    [SerializeField]
    private ItemType drinkOrder;

    public override void StoreItem (ref Item item)
    {
        if (item.GetItemType() == drinkOrder)
            base.StoreItem(ref item);
    }
}