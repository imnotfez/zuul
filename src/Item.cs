class Item
{
    public int Weight { get; }
    public string Description { get; }

    public int Damage { get; }


    public Item(int weight, int damage, string description)
    {
        Weight = weight;
        Description = description;
        Damage = damage;

    }
}