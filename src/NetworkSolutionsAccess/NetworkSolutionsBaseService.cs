using System;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public abstract class NetworkSolutionsBaseService
	{
		protected const int PageSize = 300;
		protected const decimal Version = 20.0M;
		private readonly NetworkSolutionsConfig Config;
		private readonly SecurityCredentialType Credentials;
		protected readonly NetSolEcomServiceSoapClient Client;
		internal readonly WebRequestServices WebRequestServices;
		protected readonly NetworkSolutionsAuthService AuthService;
		protected readonly CacheManager CacheManager;
		protected readonly bool IsUsedCache;

		protected NetworkSolutionsBaseService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config, CacheManager cacheManager = null )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this.Config = config;
			this.CacheManager = cacheManager;
			this.IsUsedCache = cacheManager != null;
			this.Credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate };
			this.Client = new NetSolEcomServiceSoapClient();
			this.WebRequestServices = new WebRequestServices();
			this.AuthService = new NetworkSolutionsAuthService( appConfig );
		}

		protected virtual SecurityCredentialType GetCredentials()
		{
			if( !this.IsUsedCache )
			{
				this.UpdateCredentials();
				return this.Credentials;
			}

			var userToken = this.CacheManager.Get< UserTokenType >( this.Config, "UserToken" );
			if( userToken != null && userToken.Expiration > DateTime.UtcNow.AddMinutes( 3 ) )
			{
				this.Credentials.UserToken = userToken.UserToken;
				return this.Credentials;
			}

			userToken = this.UpdateCredentials();
			if( userToken != null )
				this.CacheManager.AddOrUpdate( userToken, this.Config, "UserToken" );
			return this.Credentials;
		}

		private UserTokenType UpdateCredentials()
		{
			var token = this.AuthService.GetUserToken( this.Config.UserKey );
			this.Credentials.UserToken = token != null ? token.UserToken : null;
			return token;
		}
	}
}