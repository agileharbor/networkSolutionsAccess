using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsOrdersService: INetworkSolutionsOrdersService
	{
		private readonly SecurityCredentialType _credentials;
		private readonly NetSolEcomServiceSoapClient _client;
		private readonly WebRequestServices _webRequestServices;
		private readonly CultureInfo _culture = new CultureInfo( "en-US" );

		public NetworkSolutionsOrdersService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this._client = new NetSolEcomServiceSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< OrderType > GetOrders()
		{
			var result = new List< OrderType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Page = i } };
				var response = this._webRequestServices.GetPage( this._client.ReadOrder, this._credentials, request );
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
				var response = await this._webRequestServices.GetPageAsync( this._client.ReadOrderAsync, this._credentials, request );
				if( response.ReadOrderResponse1.OrderList != null )
					result.AddRange( response.ReadOrderResponse1.OrderList );
				if( !response.ReadOrderResponse1.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public IEnumerable< OrderType > GetOrders( DateTimeOffset startDateUtc, DateTimeOffset endDateUtc )
		{
			var filter = this.GetOrdersFilter( startDateUtc, endDateUtc );
			var result = new List< OrderType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Page = i }, FilterList = filter };
				var response = this._webRequestServices.GetPage( this._client.ReadOrder, this._credentials, request );
				if( response.OrderList != null )
					result.AddRange( response.OrderList );
				if( !response.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync( DateTimeOffset startDateUtc, DateTimeOffset endDateUtc )
		{
			var filter = this.GetOrdersFilter( startDateUtc, endDateUtc );
			var result = new List< OrderType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Page = i }, FilterList = filter };
				var response = await this._webRequestServices.GetPageAsync( this._client.ReadOrderAsync, this._credentials, request );
				if( response.ReadOrderResponse1.OrderList != null )
					result.AddRange( response.ReadOrderResponse1.OrderList );
				if( !response.ReadOrderResponse1.PageResponse.HasMore )
					break;
			}

			return result;
		}

		private FilterType[] GetOrdersFilter( DateTimeOffset startDateUtc, DateTimeOffset endDateUtc )
		{
			return new[]
			{
				new FilterType
				{
					Field = "CreateDate",
					Operator = OperatorCodeType.Between,
					OperatorSpecified = true,
					ValueList = new[] { startDateUtc.ToString( this._culture ), endDateUtc.ToString( this._culture ) }
				}
			};
		}
	}
}