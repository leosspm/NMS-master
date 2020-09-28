using System.Threading.Tasks;

namespace KERRY.NMS.API.TestApiKey.Authentication
{
    public interface IGetApiKeyQuery
    {
        Task<ApiKey> Execute(string providedApiKey);
    }
}
