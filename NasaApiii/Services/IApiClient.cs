using System.Threading.Tasks;

namespace NasaApiii.Services
{
    public interface IApiClient
    {
        Task<string?> GetStringAsync(string url);
    }
}
