using System;
using System.Collections.Generic;
using System.Data;

namespace AutenticarconToken.Models;

public partial class Usuario
{
    public int id_usuario { get; set; }

    public string email { get; set; } = null!;

    public string contraseña { get; set; } = null!;


}
