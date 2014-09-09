using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsOrdersService: INetworkSolutionsOrdersService
	{
		protected readonly SecurityCredentialType Credentials;
		protected readonly NetSolEcomServiceSoapClient Client;
		internal readonly WebRequestServices WebRequestServices;

		public NetworkSolutionsOrdersService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this.Credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this.Client = new NetSolEcomServiceSoapClient();
			this.WebRequestServices = new WebRequestServices();
		}

		public IEnumerable< OrderType > GetOrders()
		{
			var result = new List< OrderType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Page = i } };
				var response = this.WebRequestServices.GetPage( this.Client.ReadOrder, this.Credentials, request );
				if( response.OrderList != null )
					result.AddRange( response.OrderList );
				if( !response.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync()
		{
			var result = new List< OrderType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Page = i } };
				var response = await this.WebRequestServices.GetPageAsync( this.Client.ReadOrderAsync, this.Credentials, request );
				if( response.ReadOrderResponse1.OrderList != null )
					result.AddRange( response.ReadOrderResponse1.OrderList );
				if( !response.ReadOrderResponse1.PageResponse.HasMore )
					break;
			}

			return result;
		}
	}
}