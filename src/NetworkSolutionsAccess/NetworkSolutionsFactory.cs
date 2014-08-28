using NetworkSolutionsAccess.Models.Configuration;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsFactory
	{
		INetworkSolutionsProductsService CreateProductsService( NetworkSolutionsConfig config );
		INetworkSolutionsOrdersService CreateOrdersService( NetworkSolutionsConfig config );
		INetworkSolutionsAuthService CreateAuthService();
	}

	public class NetworkSolutionsFactory: INetworkSolutionsFactory
	{
		private readonly NetworkSolutionsAppConfig _appConfig;

		public NetworkSolutionsFactory( string applicationName, string certificate )
		{
			this._appConfig = new NetworkSolutionsAppConfig( applicationName, certificate );
		}

		public INetworkSolutionsProductsService CreateProductsService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsProductsService( this._appConfig, config );
		}

		public INetworkSolutionsOrdersService CreateOrdersService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsOrdersService( this._appConfig, config );
		}

		public INetworkSolutionsAuthService CreateAuthService()
		{
			return new NetworkSolutionsAuthService( this._appConfig );
		}
	}
}