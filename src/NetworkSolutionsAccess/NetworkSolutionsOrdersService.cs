using System;
using System.Collections.Generic;
using System.Linq;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Mapping;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Order;
using NetworkSolutionsAccess.NetworkSolutionsService;
using ServiceStack;

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
			var orders = ActionPolicies.Get.Get( () => this._client.ReadOrder( this._credentials, orderRequest ) );
			orders.ToJson();

			return orders.OrderList.ToNsOrders().ToList();
		}

		public TResult Get<TResult>(Func<TResult> action)
		{
			var result = ActionPolicies.Get.Get(action);

			return result;
		}
	}
}