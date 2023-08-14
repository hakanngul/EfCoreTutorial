namespace EfCoreTutorial.Data.Models;

public class StudentAddress
{
    // add properties in StudentAddress class
    public int Id { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string FullAddress { get; set; }
    public string Country { get; set; }
    public virtual Student Student { get; set; }
}