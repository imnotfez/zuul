class Inventory
{
    // fields
    private int maxWeight;
    internal Dictionary<string, Item> items;

    // constructor
    public Inventory(int maxWeight)
    {
        this.maxWeight = maxWeight;
        this.items = new Dictionary<string, Item>();
    }

    
    // methods
    public bool Put(string itemName, Item item)
    {
        // Check if the item is null
        if (item == null)
        {
            return false; // Cannot add a null item
        }

        // Check if the item already exists in the inventory
        if (items.ContainsKey(itemName))
        {
            return false; // Item with the same name already exists
        }

        // Check if adding the item exceeds the maximum weight
        if (CalculateTotalWeight() + item.Weight > maxWeight)
        {
            return false; // Adding the item exceeds the maximum weight
        }

        // Add the item to the inventory
        items.Add(itemName, item);
        return true;
    }

    public Item Get(string itemName)
    {
        // Check if the item exists in the inventory
        if (items.TryGetValue(itemName, out Item item))
        {
            // Remove the item from the inventory
            items.Remove(itemName);
            return item;
        }

        // Item not found in the inventory
        return null;
    }

    public Item useItem(string itemName)
    {
        if(items.TryGetValue(itemName, out Item item))
        {
            // remove item
            return item;
        }

        return null;
    }

    
    public int TotalWeight()
    {
        int total = 0;

        // Loop through the items and add all the weights
        foreach (var item in items.Values)
        {
            total += item.Weight;
        }

        return total;
    }

    public int FreeWeight()
    {
        // Compare MaxWeight and TotalWeight()
        return maxWeight - CalculateTotalWeight();
    }

    private int CalculateTotalWeight()
    {
        // Calculate the total weight of all items in the inventory
        int totalWeight = 0;
        foreach (var item in items.Values)
        {
            totalWeight += item.Weight;
        }
        return totalWeight;
    }
}
