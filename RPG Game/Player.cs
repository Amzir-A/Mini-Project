using System.Security.Cryptography.X509Certificates;

public class Player
{
	public int CurrentHitPoints { get; set; }
	public Location CurrentLocation { get; set; }
	public Weapon CurrentWeapon { get; set; }
	public int MaximumHitPoints { get; set; }
	public string Name { get; set; }

	public Player(int currentHitPoints, Location currentLocation, Weapon currentWeapon, int maximumHitPoints, string name)
	{
		CurrentHitPoints = currentHitPoints;
		CurrentLocation = currentLocation;
		CurrentWeapon = currentWeapon;
		MaximumHitPoints = maximumHitPoints;
		Name = name;
	}
}