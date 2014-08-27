using System.Collections.Generic;
using System.Linq;
using NetworkSolutionsAccess.Models.Order;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Mapping
{
	internal static class OrderMapping
	{
		public static IEnumerable< NetworkSolutionsOrder > ToNsOrders( this IEnumerable< OrderType > orders )
		{
			if( orders == null )
				return new List< NetworkSolutionsOrder >();
			return orders.Select( p => p.ToNsOrder() );
		}

		public static NetworkSolutionsOrder ToNsOrder( this OrderType order )
		{
			return new NetworkSolutionsOrder
			{
			};
		}
	}
}