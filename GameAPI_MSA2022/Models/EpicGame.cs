using System.ComponentModel.DataAnnotations;

namespace GameAPI_MSA2022.Models;


public class EpicGame
{
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public bool IsFree { get; set; }
    public string? Genre {get; set;}
}