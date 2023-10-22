using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Desafio_Balta.Models;

public partial class Ibge
{
   
    public string Id { get; set; } = null!;

    public string? State { get; set; }

    public string? City { get; set; }
}
