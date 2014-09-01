using NetworkSolutionsAccess.Misc;
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
		private readonly CacheManager CacheManager;

		public NetworkSolutionsFactory( string applicationName, string certificate, bool useCache = false )
		{
			this._appConfig = new NetworkSolutionsAppConfig( applicationName, certificate );
			if( useCache )
				this.CacheManager = new CacheManager( this._appConfig );
		}

		public INetworkSolutionsProductsService CreateProductsService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsProductsService( this._appConfig, config, this.CacheManager );
		}

		public INetworkSolutionsOrdersService CreateOrdersService( NetworkSolutionsConfig config )
		{
			return new NetworkSolutionsOrdersService( this._appConfig, config, this.CacheManager );
		}

		public INetworkSolutionsAuthService CreateAuthService()
		{
			return new NetworkSolutionsAuthService( this._appConfig );
		}
	}
}