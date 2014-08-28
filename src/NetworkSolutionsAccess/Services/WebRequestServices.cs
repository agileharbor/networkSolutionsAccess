using System;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Services
{
	internal class WebRequestServices
	{
		public TResponse Get< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, TResponse > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );

			var result = ActionPolicies.Get.Get( () => func( credentials, request ) );

			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}

		public async Task< TResponse > GetAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );

			var result = await ActionPolicies.GetAsync.Get( () => func( credentials, request ) );

			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}
	}
}