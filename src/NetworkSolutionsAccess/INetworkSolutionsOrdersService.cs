using System.Collections.Generic;
using NetworkSolutionsAccess.Models.Order;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsOrdersService
	{
		IEnumerable< NetworkSolutionsOrder > GetOrders();
	}
}