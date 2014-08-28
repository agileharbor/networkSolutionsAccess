using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsProductsService
	{
		IEnumerable< ProductType > GetProducts();
		Task< IEnumerable< ProductType > > GetProductsAsync();
		NetworkSolutionsInventory UpdateInventory( NetworkSolutionsInventory inventory );
		Task< NetworkSolutionsInventory > UpdateInventoryAsync( NetworkSolutionsInventory inventory );
		IEnumerable< NetworkSolutionsInventory > UpdateInventory( IEnumerable< NetworkSolutionsInventory > inventory );
		Task< IEnumerable< NetworkSolutionsInventory > > UpdateInventoryAsync( IEnumerable< NetworkSolutionsInventory > inventory );
	}
}