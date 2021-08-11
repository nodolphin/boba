using UnityEngine;

public class Storage : MonoBehaviour
{
    [Header("Storage class")]
    
    //questionable naming
    public Item storedItem;
    [SerializeField] private sbyte maxCount = 3;

    public Item uiItem;

    [Header("Temperature")]
    [SerializeField] private float temperatureChangeRate;
    [SerializeField] private float temperatureToApproach;

    [Header("Carried object")]
    [SerializeField] private GameObject carriedObject;
    [SerializeField] private Vector2 offset;

    private const int CARRIED_OBJECT_SIZE = 3;

    protected virtual void Start()
    {
        UpdateUI();
    }

    public void WithdrawItem(ref Item item)
    {
        storedItem.Withdraw(ref item, 1);
    }

    public virtual void StoreItem(ref Item item)
    {
        if (!(storedItem.count == 1 && storedItem.Combine(ref item)))
            storedItem.Store(ref item, maxCount);
    }

    protected virtual void FixedUpdate()
    {
        if (uiItem.itemType != storedItem.itemType || uiItem.count != storedItem.count)
        {
            uiItem.itemType = storedItem.itemType;
            uiItem.count = storedItem.count;
            UpdateUI();
        }
        storedItem.AddTemperature(temperatureChangeRate * Time.deltaTime, temperatureToApproach);
    }

    //CALCULATES WHERE WE START CREATING UI STUFF
    protected Vector2 CalculateStart(int amount)
    {
        //RENAME
        float isEven = amount % 2 == 0? 0.5f : 0f; 
        //RENAME
        float distance = ( (amount) / 2 - isEven) * CARRIED_OBJECT_SIZE;
        return -new Vector3(distance, 0, 0);
    }

    //THIS FUNCTION IS AWFULLY SPECIFIC
    //IF THERE IS A FUNCTION THAT IS SIMILAR, IT WILL BE WISE TO MAKE IT BETTER
    //MY CODE IS FUCKING DUMB RIGHT NOW
    protected void UpdateUI()
    {
        DestroyChildren();

        Vector2 startPosition = CalculateStart(storedItem.count);
        for (int i = 0; i < storedItem.count; i++)
        {
            float xPosition = startPosition.x + (i * CARRIED_OBJECT_SIZE);

            Vector2 instantiatePosition = (Vector2)transform.position + new Vector2(xPosition, 0) + offset;

            GameObject instantiatedObject = Instantiate(carriedObject, instantiatePosition, Quaternion.identity);
            
            SpriteRenderer spriteRenderer = instantiatedObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Globals.GetItemInfo(storedItem.itemType).sprite;

            instantiatedObject.transform.SetParent(gameObject.transform);
        }
    }

    //MIGHT PUT IT IN A PARENT CLASS
    protected void DestroyChildren()
    {
        foreach(Transform child in gameObject.transform)
        {
            Destroy(child.gameObject);
        }
    }
}