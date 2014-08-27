using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Mapping;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsAuthService: INetworkSolutionsAuthService
	{
		private readonly SecurityCredentialType _credentials;
		private readonly NetSolEcomServiceSoapClient _client;

		public NetworkSolutionsAuthService( NetworkSolutionsConfig config )
		{
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = config.ToSecurityCredentialType();
			this._client = new NetSolEcomServiceSoapClient();
		}

		public string GetUserKey()
		{
			var request = new GetUserKeyRequestType() { };
			var userKey = this._client.GetUserKey( this._credentials, request );
			return userKey.UserKey != null ? userKey.UserKey.UserKey : null;
		}

		public string GetUserToken()
		{
			var request = new GetUserTokenRequestType() { };
			var userToken = this._client.GetUserToken( this._credentials, request );
			return userToken.UserToken != null ? userToken.UserToken.UserToken : null;
		}
	}
}