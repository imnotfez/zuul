class Player
{
    public Room CurrentRoom { get; set; }
    public int health = 100;
    private int damageTaken;

    public int playerdealtDMG;

    private int devKillValue;
    private int damageHealed;
    public bool isAlive;
    public Inventory backPack;

    public Player()
    {
        CurrentRoom = null;
        health = 100;
        damageTaken = 5;
        damageHealed = 10;
        devKillValue = 100;
        backPack = new Inventory(25); 
    }

    
    public void damagePlayer()
    {
        health -= damageTaken;
    }

     public void devdamagePlayer()
    {
        health -= devKillValue;
    }
    public void CheckAliveStatus()
    {
        if  (health > 1)
        {
            isAlive = true;
        }
        
        if (health <= 0)
        {
            isAlive = false;
        }
    }

    public void ShowPlayerHealth()
    {
        Console.WriteLine($"Player Health: {health}");
    }

    public void CheckBackpackWeight()
    {
        int FreeWeight = backPack.FreeWeight();  
        Console.WriteLine($"Currently you have: {FreeWeight} space");
    }

    public bool TakeFromChest(string itemName)
{

    if (CurrentRoom != null && CurrentRoom.Chest != null)
    {
 
        Item item = CurrentRoom.Chest.Get(itemName);

        if (item != null)
        {

            if (backPack.Put(itemName, item))
            {
                Console.WriteLine($"You took {itemName} from the chest.");
                return true;
            }
            else
            {

                CurrentRoom.Chest.Put(itemName, item);
                Console.WriteLine($"You cannot carry {itemName}. It remains in the chest.");
            }
        }
        else
        {
            Console.WriteLine($"There is no {itemName} in the chest.");
        }
    }
    else
    {
        Console.WriteLine("There is no chest in the room.");
    }

    return false;
}


public bool DropToChest(string itemName)
{

    if (CurrentRoom != null && CurrentRoom.Chest != null)
    {
        Item item = backPack.Get(itemName);

        if (item != null)
        {
            if (CurrentRoom.Chest.Put(itemName, item))
            {
                Console.WriteLine($"You dropped {itemName} into the chest.");
                return true;
            }
            else
            {
                backPack.Put(itemName, item);
                Console.WriteLine($"The chest is full. You cannot drop {itemName}.");
            }
        }
        else
        {
            Console.WriteLine($"You don't have {itemName} in your backpack.");
        }
    }
    else
    {
        Console.WriteLine("There is no chest in the room.");
    }

    return false;
}

}