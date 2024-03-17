class Enemy
{
    public Room CurrentRoom { get; set; }
    public int Health = 100;
    public int armor = 0;
    
    public int damage = 0;
    public string Name = "";
    public string enemyTitle = "";
    private int damageTaken;
    private int damageHealed;
    public bool isAlive;
    public Inventory backPack;
    public Player player;

    public Enemy(int armor,string title, Player p)
    {
        CurrentRoom = null;
        Health = 100;
        Name = "";
        armor = 0;
        enemyTitle = title;

        damageTaken = 5;
        damageHealed = 10;
        player = p;
        backPack = new Inventory(25); 
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