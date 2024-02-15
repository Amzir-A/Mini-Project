public class Quest
{
	public int ID { get; set; }
	public string Description { get; set; }
	public string Name { get; set; }
	public Quest(int id, string name, string description)
	{
		ID = id;
		Name = name;
		Description = description;
	}
}