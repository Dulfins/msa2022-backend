using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Domain_Layer.Models;


public class Game
{
    [Required]
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public bool IsFree { get; set; }
    public string? Genre { get; set; }
}