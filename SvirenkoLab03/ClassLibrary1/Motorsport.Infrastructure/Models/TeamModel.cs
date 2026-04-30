public class TeamModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<CarModel> Cars { get; set; } = new List<CarModel>();
}