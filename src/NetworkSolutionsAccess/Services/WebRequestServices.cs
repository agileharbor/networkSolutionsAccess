using System;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Services
{
	internal class WebRequestServices
	{
		private const int PageSize = 250;
		private const decimal Version = 7.9M;
		private readonly JavaScriptSerializer Serializer = new JavaScriptSerializer();

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

			this.LogRequest( func.Method.Name, credentials, request );
			var result = ActionPolicies.Get.Get( () => func( credentials, request ) );
			this.LogResponse( func.Method.Name, credentials, result );

			return result;
		}

		public async Task< TResponse > GetAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			this.LogRequest( func.Method.Name, credentials, request );
			var result = await ActionPolicies.GetAsync.Get( () => func( credentials, request ) );
			this.LogResponse( func.Method.Name, credentials, result );

			return result;
		}

		public TResponse Submit< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, TResponse > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			this.LogRequest( func.Method.Name, credentials, request );
			var result = ActionPolicies.Submit.Get( () => func( credentials, request ) );
			this.LogResponse( func.Method.Name, credentials, result );

			return result;
		}

		public async Task< TResponse > SubmitAsync< TCredentials, TRequest, TResponse >( Func< TCredentials, TRequest, Task< TResponse > > func, TCredentials credentials, TRequest request )
			where TCredentials : SecurityCredentialType
			where TRequest : BaseRequestType
		{
			this.UpdateRequest( request );

			this.LogRequest( func.Method.Name, credentials, request );
			var result = await ActionPolicies.SubmitAsync.Get( () => func( credentials, request ) );
			this.LogResponse( func.Method.Name, credentials, result );

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

		private void LogRequest< T >( string methodName, SecurityCredentialType credentials, T obj )
		{
			var json = this.Serializer.Serialize( obj );
			var logstr = string.Format( "Request for {0}\tApplication:{1}\tUserToken:{2}\nData: {3}", methodName, credentials.Application, credentials.UserToken, json );
			NetworkSolutionsLogger.Log.Trace( logstr );
		}

		private void LogResponse< T >( string methodName, SecurityCredentialType credentials, T obj )
		{
			var json = this.Serializer.Serialize( obj );
			if( json.Contains( "\"Status\":1" ) )
			{
				var logStr = string.Format( "Success response for {0}\tApplication:{1}\tUserToken:{2}\nData: {3}", methodName, credentials.Application, credentials.UserToken, json );
				NetworkSolutionsLogger.Log.Trace( logStr );
			}
			else
			{
				var logStr = string.Format( "Failed response for {0}\tApplication:{1}\tUserToken:{2}\nData: {3}", methodName, credentials.Application, credentials.UserToken, json );
				NetworkSolutionsLogger.Log.Error( logStr );
			}
		}
	}
}