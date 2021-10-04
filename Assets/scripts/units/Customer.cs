using UnityEngine;

public class Customer : TrashCan
{
    //REMOVE SERIALIZEFIELD ONLY FOR TESTING
    [SerializeField]
    private ItemType drinkOrder;
    [SerializeField]
    private SpriteRenderer drinkOrderSprite;

    protected override void Start() 
    {
        drinkOrderSprite.sprite = Globals.GetItemInfo(drinkOrder).sprite;
    }

    public override void StoreItem (ref Item item)
    {
        if (item.GetItemType() == drinkOrder)
        {
            UpdateUI();
            base.StoreItem(ref item);
        }
    }


}