namespace LancheTCE_Back.models
{
  public class UsuarioPOSTDTO
  {
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public string? Perfil { get; set; }
    public EnderecoDTO? Endereco { get; set; }
  }
}
