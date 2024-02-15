public class Weapon
{
	public int ID { get; set; }
	public string Name { get; set; }
	public int MaximumDamage { get; set; }

	public Weapon(int id, string name, int maximumDamage)
	{
		ID = id;
		Name = name;
		MaximumDamage = maximumDamage;
	}
}