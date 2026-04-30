using ClassLibrary1;

public class CarModel : VehicleModel
{
    public int TopSpeed { get; set; }

    // Зв'язок 1:Багатьох (Одна команда - багато машин)
    public int TeamId { get; set; }
    public TeamModel Team { get; set; }

    // Зв'язок 1:1 (Одна машина - один двигун)
    public EngineModel Engine { get; set; }

    // Зв'язок Багато:Багатьох (Машина має багато спонсорів, спонсор - багато машин)
    public ICollection<SponsorModel> Sponsors { get; set; } = new List<SponsorModel>();
}