using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
			var result = this.GetOrdersBase( null );
			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync()
		{
			var result = await this.GetOrdersBaseAsync( null );
			return result;
		}

		public IEnumerable< OrderType > GetOrders( DateTime startDateUtc, DateTime endDateUtc )
		{
			var filter = this.GetOrdersFilter( startDateUtc, endDateUtc );
			var result = this.GetOrdersBase( filter );
			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc )
		{
			var filter = this.GetOrdersFilter( startDateUtc, endDateUtc );
			var result = await this.GetOrdersBaseAsync( filter );
			return result;
		}

		public IEnumerable< OrderType > GetOrdersExceptReceived( IEnumerable< string > orderNumbers )
		{
			var filter = this.GetOrdersExceptReceivedFilter( orderNumbers );
			var result = this.GetOrdersBase( filter );
			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersExceptReceivedAsync( IEnumerable< string > orderNumbers )
		{
			var filter = this.GetOrdersExceptReceivedFilter( orderNumbers );
			var result = await this.GetOrdersBaseAsync( filter );
			return result;
		}

		private IEnumerable< OrderType > GetOrdersBase( FilterType[] filter )
		{
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

		private async Task< IEnumerable< OrderType > > GetOrdersBaseAsync( FilterType[] filter )
		{
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

		private FilterType[] GetOrdersFilter( DateTime startDateUtc, DateTime endDateUtc )
		{
			var startDate = new DateTimeOffset( startDateUtc, TimeSpan.Zero );
			var endDate = new DateTimeOffset( endDateUtc, TimeSpan.Zero );
			return new[]
			{
				new FilterType
				{
					Field = "CreateDate",
					Operator = OperatorCodeType.Between,
					OperatorSpecified = true,
					ValueList = new[] { startDate.ToString( this._culture ), endDate.ToString( this._culture ) }
				},
				new FilterType
				{
					Field = "OrderNumber",
					Operator = OperatorCodeType.GreaterThan,
					OperatorSpecified = true,
					ValueList = new[] { 0.ToString( this._culture ) }
				}
			};
		}

		private FilterType[] GetOrdersExceptReceivedFilter( IEnumerable< string > orderNumbers )
		{
			return new[]
			{
				new FilterType
				{
					Field = "Status.OrderStatusId",
					Operator = OperatorCodeType.NotEqual,
					OperatorSpecified = true,
					ValueList = new[] { "1" }
				},
				new FilterType
				{
					Field = "OrderNumber",
					Operator = OperatorCodeType.In,
					OperatorSpecified = true,
					ValueList = orderNumbers.ToArray()
				}
			};
		}
	}
}