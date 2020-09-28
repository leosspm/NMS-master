using KERRY.NMS.MODEL.ExternalModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KERRY.NMS.BL.ExternalApi
{
    public interface IApiProduct
    {
        Task<IEnumerable<ProductExternalModel>> GetAllProducts();
    }

    internal class ApiProduct : ExternalApiBase, IApiProduct
    {
        public ApiProduct(HttpClient httpClient) : base(httpClient, "Product") { }

        public async Task<IEnumerable<ProductExternalModel>> GetAllProducts()
        {
            return (await PostAsync<IEnumerable<ProductExternalModel>>("GetProductList"));
        }
    }
}
