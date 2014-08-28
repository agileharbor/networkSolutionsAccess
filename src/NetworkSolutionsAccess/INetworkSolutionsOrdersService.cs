using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsOrdersService
	{
		IEnumerable< OrderType > GetOrders();
		Task< IEnumerable< OrderType > > GetOrdersAsync();
	}
}