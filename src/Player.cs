class Player
{
    public Room CurrentRoom { get; set; }
    public int health = 100;
    private int damageTaken;
    private int damageHealed;
    public bool isAlive;

    public Player()
    {
        CurrentRoom = null;
        health = 100;
        damageTaken = 25;
        damageHealed = 10;
    }


    public void damagePlayer()
    {
        health -= damageTaken;
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
}
