using System.Collections.Generic;
using NetworkSolutionsAccess.Models.Product;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsProductsService
	{
		IEnumerable< NetworkSolutionsProduct > GetProducts();
	}
}