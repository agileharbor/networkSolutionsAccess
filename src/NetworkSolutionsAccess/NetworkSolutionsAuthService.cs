using System;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsAuthService: INetworkSolutionsAuthService
	{
		protected readonly SecurityCredentialType _credentials;
		protected readonly NetSolEcomServiceSoapClient _client;
		internal readonly WebRequestServices _webRequestServices;

		public NetworkSolutionsAuthService( NetworkSolutionsAppConfig appConfig )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();

			this._credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate };
			this._client = new NetSolEcomServiceSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public UserKeyType GetUserKey()
		{
			var request = new GetUserKeyRequestType();
			var response = this._webRequestServices.Get( this._client.GetUserKey, this._credentials, request );
			return response.UserKey;
		}

		public async Task< UserKeyType > GetUserKeyAsync()
		{
			var request = new GetUserKeyRequestType();
			var response = await this._webRequestServices.GetAsync( this._client.GetUserKeyAsync, this._credentials, request );
			return response.GetUserKeyResponse1.UserKey;
		}

		public UserTokenType GetUserToken( string userKey )
		{
			try
			{
				var request = new GetUserTokenRequestType { UserToken = new UserTokenType { UserKey = userKey } };
				var response = this._webRequestServices.Get( this._client.GetUserToken, this._credentials, request );
				return response.UserToken;
			}
			catch( Exception )
			{
				return null;
			}
		}

		public async Task< UserTokenType > GetUserTokenAsync( string userKey )
		{
			try
			{
				var request = new GetUserTokenRequestType { UserToken = new UserTokenType { UserKey = userKey } };
				var response = await this._webRequestServices.GetAsync( this._client.GetUserTokenAsync, this._credentials, request );
				return response.GetUserTokenResponse1.UserToken;
			}
			catch( Exception )
			{
				return null;
			}
		}
	}
}