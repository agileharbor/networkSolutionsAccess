using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsAuthService: NetworkSolutionsBaseService, INetworkSolutionsAuthService
	{
		public NetworkSolutionsAuthService( NetworkSolutionsAppConfig appConfig ): base( appConfig )
		{
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
			var request = new GetUserTokenRequestType { UserToken = new UserTokenType { UserKey = userKey } };
			var response = this._webRequestServices.Get( this._client.GetUserToken, this._credentials, request );
			return response.UserToken;
		}

		public async Task< UserTokenType > GetUserTokenAsync( string userKey )
		{
			var request = new GetUserTokenRequestType { UserToken = new UserTokenType { UserKey = userKey } };
			var response = await this._webRequestServices.GetAsync( this._client.GetUserTokenAsync, this._credentials, request );
			return response.GetUserTokenResponse1.UserToken;
		}
	}
}