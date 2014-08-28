using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsProductsService: NetworkSolutionsBaseService, INetworkSolutionsProductsService
	{
		public NetworkSolutionsProductsService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config ): base( appConfig, config )
		{
		}

		public IEnumerable< ProductType > GetProducts()
		{
			var request = new ReadProductRequestType();
			var response = this._webRequestServices.Get( this._client.ReadProduct, this._credentials, request );
			return response.ProductList;
		}

		public async Task< IEnumerable< ProductType > > GetProductsAsync()
		{
			var request = new ReadProductRequestType();
			var response = await this._webRequestServices.GetAsync( this._client.ReadProductAsync, this._credentials, request );
			return response.ReadProductResponse1.ProductList;
		}
	}
}