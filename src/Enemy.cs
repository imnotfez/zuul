class Enemy
{
    public Room CurrentRoom { get; set; }
    public int Health;
    public int health;
    public int armor;

    public int Damage { get; set; }
    
    public int damage = 0;
    public int healPlayer;
    public string Name = "";
    public string enemyTitle = "";
    public bool isAlive;
    public Inventory backPack;
    public Player player;
    private List<Item> loot;

    public Enemy(int health,int armor,string title,int dmg,int hlPly, Player p)
    {
        CurrentRoom = null;
        Name = "";
        this.health = Health;
        this.armor = armor;
        Health += armor;
        Damage = dmg;
        enemyTitle = title;
        damage = dmg;
        healPlayer = hlPly;
        player = p;
        backPack = new Inventory(25); 
        this.loot = new List<Item>();
    }


    public void AddLoot(Item item)
    {
        loot.Add(item);
    }

    // Method to handle dropping loot upon enemy's death
    public void DropLoot()
    {
        foreach (Item item in loot)
        {
            player.CurrentRoom.Chest.Put(item.Description.ToLower(), item); // Put the item in the room's chest
            Console.WriteLine($"\u001b[31mThe {Name}\u001b[0m dropped \u001b[33m{item.Description}\u001b[0m upon its defeat.");

        }
    }

    public void damageEnemy(int damage)
{
    // First, deduct from armor if armor is greater than 0
    if (armor > 0)
    {
        if (damage >= armor)
        {
            // If damage is greater than or equal to armor, armor goes to 0
            damage -= armor;
            armor = 0;
        }
        else
        {
            // If damage is less than armor, only deduct from armor
            armor -= damage;
            damage = 0; // No remaining damage to health
        }
    }
    
    // Then, if there's any remaining damage after armor deduction, deduct from health
    if (damage > 0)
    {
        Health -= damage;
    }
}

    public bool isDead()
    {
        if (Health <= 0)
        {
            // Enemy is dead, remove it
            Console.WriteLine($"{Name} is dead!");
            isAlive = false;

            if (CurrentRoom != null && CurrentRoom.enemies.ContainsKey(Name))
            {
                CurrentRoom.enemies.Remove(Name);
            }

            // You can add additional logic here for cleanup or other actions before removal
            return true;
        }
        return false;
    }



    public void CheckAliveStatus()
    {
        if  (Health > 1)
        {
            isAlive = true;
        }
        
        if (Health <= 0)
        {
            isDead();
        }
    }

    public void ShowEnemyHealth()
    {
        Console.WriteLine($"Player Health: {Health}");
    }

    

}