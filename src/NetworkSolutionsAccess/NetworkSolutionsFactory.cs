using NetworkSolutionsAccess.Models.Configuration;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsFactory
	{
		INetworkSolutionsProductsService CreateProductsService( NetworkSolutionsConfig config );
		INetworkSolutionsOrdersService CreateOrdersService( NetworkSolutionsConfig config );
		INetworkSolutionsAuthService CreateAuthService( NetworkSolutionsConfig config );
	}

	public class NetworkSolutionsFactory: INetworkSolutionsFactory
	{
		public INetworkSolutionsProductsService CreateProductsService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsProductsService( config );
		}

		public INetworkSolutionsOrdersService CreateOrdersService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsOrdersService( config );
		}

		public INetworkSolutionsAuthService CreateAuthService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsAuthService( config );
		}
	}
}