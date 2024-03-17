class Enemy
{
    public Room CurrentRoom { get; set; }
    public int Health = 100;
    public int armor = 0;

    public int Damage { get; set; }
    
    public int damage = 0;
    public int healPlayer;
    public string Name = "";
    public string enemyTitle = "";
    private int damageTaken;
    private int damageHealed;
    public bool isAlive;
    public Inventory backPack;
    public Player player;
    private List<Item> loot;

    public Enemy(int armor,string title,int dmg,int hlPly, Player p)
    {
        CurrentRoom = null;
        Health = 100;
        Name = "";
        armor = 0;
        Damage = dmg;
        enemyTitle = title;
        damage = dmg;
        healPlayer = hlPly;


        damageTaken = 5;
        damageHealed = 10;
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
        Health -= damage;
    }

    public bool isDead()
    {
        if (Health <= 0)
        {
            // Enemy is dead, remove it
            Console.WriteLine($"{Name} is dead!");
            isAlive = false;

            // Optionally, remove the enemy from the current room's dictionary
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