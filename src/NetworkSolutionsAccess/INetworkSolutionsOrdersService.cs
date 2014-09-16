using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsOrdersService
	{
		IEnumerable< OrderType > GetOrders();
		Task< IEnumerable< OrderType > > GetOrdersAsync();

		IEnumerable< OrderType > GetOrders( DateTimeOffset startDateUtc, DateTimeOffset endDateUtc );
		Task< IEnumerable< OrderType > > GetOrdersAsync( DateTimeOffset startDateUtc, DateTimeOffset endDateUtc );

		IEnumerable< OrderType > GetOrders( IEnumerable< string > orderNumbers );
		Task< IEnumerable< OrderType > > GetOrdersAsync( IEnumerable< string > orderNumbers );
	}
}