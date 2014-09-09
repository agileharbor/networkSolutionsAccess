using System;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Services
{
	internal class WebRequestServices
	{
		private const int PageSize = 500;
		private const decimal Version = 7.9M;

		public TResponse GetPage< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, TResponse > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : ReadBaseRequestType
		{
			this.UpdatePageRequest( request );
			var result = this.Get( func, credentials, request );
			return result;
		}

		public async Task< TResponse > GetPageAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : ReadBaseRequestType
		{
			this.UpdatePageRequest( request );
			var result = await this.GetAsync( func, credentials, request );
			return result;
		}

		public TResponse Get< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, TResponse > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );
			var result = ActionPolicies.Get.Get( () => func( credentials, request ) );
			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}

		public async Task< TResponse > GetAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );
			var result = await ActionPolicies.GetAsync.Get( () => func( credentials, request ) );
			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}

		public TResponse Submit< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, TResponse > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );
			var result = ActionPolicies.Submit.Get( () => func( credentials, request ) );
			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}

		public async Task< TResponse > SubmitAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			NetworkSolutionsLogger.TraceRequest( func.Method.Name, credentials, request );
			var result = await ActionPolicies.SubmitAsync.Get( () => func( credentials, request ) );
			NetworkSolutionsLogger.TraceResponse( func.Method.Name, credentials, result );

			return result;
		}

		private void UpdateRequest< T >( T request ) where T : BaseRequestType
		{
			request.Version = Version;
			request.VersionSpecified = true;
		}

		private void UpdatePageRequest< T >( T request ) where T : ReadBaseRequestType
		{
			request.DetailSize = SizeCodeType.Large;
			request.DetailSizeSpecified = true;
			if( request.PageRequest == null )
				request.PageRequest = new PaginationType();
			request.PageRequest.Size = PageSize;
			request.PageRequest.SizeSpecified = true;
			if( request.PageRequest.Page == 0 )
				request.PageRequest.Page = 1;
			request.PageRequest.PageSpecified = true;
		}
	}
}