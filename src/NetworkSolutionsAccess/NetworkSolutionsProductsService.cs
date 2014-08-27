using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Mapping;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsProductsService: INetworkSolutionsProductsService
	{
		private readonly SecurityCredentialType _credentials;
		private readonly NetSolEcomServiceSoapClient _client;

		public NetworkSolutionsProductsService( NetworkSolutionsConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = config.ToSecurityCredentialType();
			this._client = new NetSolEcomServiceSoapClient();
		}

		public IEnumerable< NetworkSolutionsProduct > GetProducts()
		{
			var prt = new ReadProductRequestType() { };
			var products = this._client.ReadProduct( this._credentials, prt );
			return products.ProductList.ToNsProducts().ToList();
		}
	}
}