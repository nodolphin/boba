using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : Storage
{
    public override void StoreItem(ref Item item)
    {
        item.Reset();
    }
}
