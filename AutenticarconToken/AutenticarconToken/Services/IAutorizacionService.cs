using AutenticarconToken.Models.Customs;

namespace AutenticarconToken.Services
{
    public interface IAutorizacionService
    {
        Task<AutorizacionResponse> DevolverToken(AutorizacionRequest autorizacion);
    }
}
