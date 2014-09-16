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

		IEnumerable< OrderType > GetOrders( DateTime startDateUtc, DateTime endDateUtc );
		Task< IEnumerable< OrderType > > GetOrdersAsync( DateTime startDateUtc, DateTime endDateUtc );

		IEnumerable< OrderType > GetOrders( IEnumerable< string > orderNumbers );
		Task< IEnumerable< OrderType > > GetOrdersAsync( IEnumerable< string > orderNumbers );
	}
}