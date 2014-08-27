using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Order;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsOrdersService
	{
		IEnumerable< NetworkSolutionsOrder > GetOrders();
		Task< IEnumerable< NetworkSolutionsOrder > > GetOrdersAsync();
	}
}