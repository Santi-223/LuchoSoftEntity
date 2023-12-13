namespace AutenticarconToken.Models.Customs
{
    public class AutorizacionResponse
    {
        public string Token { get; set; }
        public bool Resultado { get; set; }
        public string Mensaje { get; set; }

        public string EmailUsuario { get; set; }
    }
}
