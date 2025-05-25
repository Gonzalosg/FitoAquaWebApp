using System.Text.Json.Serialization;

public class UsuarioObraDto
{
    public int UsuarioId { get; set; }
    public int ObraId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? NombreUsuario { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? NombreObra { get; set; }
}
