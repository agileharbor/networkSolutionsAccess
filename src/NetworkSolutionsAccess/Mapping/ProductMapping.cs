using System.Collections.Generic;
using System.Linq;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Mapping
{
	internal static class ProductMapping
	{
		public static IEnumerable< NetworkSolutionsProduct > ToNsProducts( this IEnumerable< ProductType > products )
		{
			if( products == null )
				return new List< NetworkSolutionsProduct >();
			return products.Select( p => p.ToNsProduct() );
		}

		public static NetworkSolutionsProduct ToNsProduct( this ProductType product )
		{
			return new NetworkSolutionsProduct
			{
			};
		}
	}
}