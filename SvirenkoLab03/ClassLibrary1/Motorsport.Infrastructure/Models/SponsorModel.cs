public class SponsorModel
{
    public int Id { get; set; }
    public string CompanyName { get; set; }
    public ICollection<CarModel> Cars { get; set; } = new List<CarModel>();
}