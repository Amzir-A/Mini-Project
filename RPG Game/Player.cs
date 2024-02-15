using System.Security.Cryptography.X509Certificates;

public class Player
{
	public string Name { get; set; }
	public Location? CurrentLocation { get; set; }
	public Weapon? CurrentWeapon { get; set; }
	public int MaximumHitPoints { get; set; }
	public int CurrentHitPoints { get; set; }

	public int Gold = 0;


	public Player(string name, Location? currentLocation, Weapon? currentWeapon, int maximumHitPoints, int currentHitPoints)
	{
		Name = name;
		CurrentLocation = currentLocation;
		CurrentWeapon = currentWeapon;
		CurrentHitPoints = currentHitPoints;
		MaximumHitPoints = maximumHitPoints;
	}
	public void Attack(Monster target)
	{
		int damageDealt = CurrentWeapon.MaximumDamage;
		Console.WriteLine($"{Name} attacks {target.Name} with {CurrentWeapon.Name} for {damageDealt} damage!");
		target.TakeDamage(damageDealt);
	}

	public void DrinkPotion()
	{
		// Method implementation
	}
}