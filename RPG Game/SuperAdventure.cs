public class SuperAdventure
{
	public Monster CurrentMonster { get; set; }
	public Player CurrentPlayer { get; set; }
	public SuperAdventure(Monster currentMonster, Player currentPlayer)
	{
		CurrentMonster = currentMonster;
		CurrentPlayer = currentPlayer;
	}
}