using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsProductsService
	{
		IEnumerable< ProductType > GetProducts();
		Task< IEnumerable< ProductType > > GetProductsAsync();
	}
}