using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsOrdersService: NetworkSolutionsBaseService, INetworkSolutionsOrdersService
	{
		public NetworkSolutionsOrdersService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config ): base( appConfig, config )
		{
		}

		public IEnumerable< OrderType > GetOrders()
		{
			var request = new ReadOrderRequestType();
			var response = this._webRequestServices.Get( this._client.ReadOrder, this._credentials, request );
			return response.OrderList != null ? response.OrderList.ToList() : new List< OrderType >();
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync()
		{
			var request = new ReadOrderRequestType();
			var response = await this._webRequestServices.GetAsync( this._client.ReadOrderAsync, this._credentials, request );
			return response.ReadOrderResponse1.OrderList != null ? response.ReadOrderResponse1.OrderList.ToList() : new List< OrderType >();
		}
	}
}