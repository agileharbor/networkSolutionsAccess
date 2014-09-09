using System.Collections.Generic;
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
			var result = new List< OrderType >();
			for( var i = 0; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Size = PageSize, Page = i }, Version = Version, DetailSize = SizeCodeType.Large };
				var response = this.WebRequestServices.Get( this.Client.ReadOrder, this.Credentials, request );
				if( response.OrderList == null || response.OrderList.Length == 0 )
					break;
				result.AddRange( response.OrderList );
			}

			return result;
		}

		public async Task< IEnumerable< OrderType > > GetOrdersAsync()
		{
			var result = new List< OrderType >();
			for( var i = 0; i < 99999; i++ )
			{
				var request = new ReadOrderRequestType { PageRequest = new PaginationType { Size = PageSize, Page = i }, Version = Version, DetailSize = SizeCodeType.Large };
				var response = await this.WebRequestServices.GetAsync( this.Client.ReadOrderAsync, this.Credentials, request );
				if( response.ReadOrderResponse1.OrderList == null || response.ReadOrderResponse1.OrderList.Length == 0 )
					break;
				result.AddRange( response.ReadOrderResponse1.OrderList );
			}

			return result;
		}
	}
}