using System;
using System.Runtime.Caching;
using NetworkSolutionsAccess.Models.Configuration;

namespace NetworkSolutionsAccess.Misc
{
	public class CacheManager
	{
		private readonly ObjectCache _cache;
		private readonly NetworkSolutionsAppConfig _appConfig;
		private readonly TimeSpan _slidingExpiration = new TimeSpan( 2, 0, 0, 0 );

		public CacheManager( NetworkSolutionsAppConfig appConfig )
		{
			this._cache = MemoryCache.Default;
			this._appConfig = appConfig;
		}

		public T Get< T >( NetworkSolutionsConfig config, string id )
		{
			return ( T )this._cache[ this.GetId( config, id ) ];
		}

		public void AddOrUpdate< T >( T obj, NetworkSolutionsConfig config, string id )
		{
			this.AddOrUpdate< T >( obj, config, id, this._slidingExpiration );
		}

		public void AddOrUpdate< T >( T obj, NetworkSolutionsConfig config, string id, TimeSpan slidingExpiration )
		{
			id = this.GetId( config, id );
			if( this._cache.Contains( id ) )
				this._cache.Remove( id );
			var policy = new CacheItemPolicy { SlidingExpiration = slidingExpiration };
			this._cache.Set( id, obj, policy );
		}

		private string GetId( NetworkSolutionsConfig config, string id )
		{
			return string.Format( "NetworkSolutionsAccess_{0}_{1}_{2}", this._appConfig.ApplicationName, config.UserKey, id );
		}
	}
}