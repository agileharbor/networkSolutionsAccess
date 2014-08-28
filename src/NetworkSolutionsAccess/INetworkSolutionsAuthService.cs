using System.Threading.Tasks;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public interface INetworkSolutionsAuthService
	{
		UserKeyType GetUserKey();
		Task< UserKeyType > GetUserKeyAsync();
		UserTokenType GetUserToken( string userKey );
		Task< UserTokenType > GetUserTokenAsync( string userKey );
	}
}