using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Mapping;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Order;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsOrdersService: INetworkSolutionsOrdersService
	{
		private readonly SecurityCredentialType _credentials;
		private readonly NetSolEcomServiceSoapClient _client;

		public NetworkSolutionsOrdersService( NetworkSolutionsConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = config.ToSecurityCredentialType();
			this._client = new NetSolEcomServiceSoapClient();
		}

		public IEnumerable< NetworkSolutionsOrder > GetOrders()
		{
			var orderRequest = new ReadOrderRequestType() { };

			NetworkSolutionsLogger.TraceRequest( "GetOrders", this._credentials, orderRequest );
			var orders = ActionPolicies.Get.Get( () => this._client.ReadOrder( this._credentials, orderRequest ) );
			NetworkSolutionsLogger.TraceResponse( "GetOrders", this._credentials, orders );

			return orders.OrderList.ToNsOrders().ToList();
		}

		public async Task< IEnumerable< NetworkSolutionsOrder > > GetOrdersAsync()
		{
			var orderRequest = new ReadOrderRequestType() { };

			NetworkSolutionsLogger.TraceRequest( "GetOrdersAsync", this._credentials, orderRequest );
			var orders = await ActionPolicies.GetAsync.Get( () => this._client.ReadOrderAsync( this._credentials, orderRequest ) );
			NetworkSolutionsLogger.TraceResponse( "GetOrdersAsync", this._credentials, orders.ReadOrderResponse1 );

			return orders.ReadOrderResponse1.OrderList.ToNsOrders().ToList();
		}
	}
}