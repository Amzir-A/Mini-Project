public class Quest(int id, string name, string description)
{
    public int ID { get; set; } = id;
    public string Description { get; set; } = description;
    public string Name { get; set; } = name;
    public bool IsCompleted = false;
    public bool RewardCollected = false;

	public void SetCompleted()
	{
		IsCompleted = true;
	}
}