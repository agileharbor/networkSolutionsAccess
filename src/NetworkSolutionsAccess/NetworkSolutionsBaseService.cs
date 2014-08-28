using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public abstract class NetworkSolutionsBaseService
	{
		protected readonly SecurityCredentialType _credentials;
		protected readonly NetSolEcomServiceSoapClient _client;
		internal readonly WebRequestServices _webRequestServices;

		protected NetworkSolutionsBaseService( NetworkSolutionsAppConfig appConfig )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();

			this._credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate };
			this._client = new NetSolEcomServiceSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		protected NetworkSolutionsBaseService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this._client = new NetSolEcomServiceSoapClient();
			this._webRequestServices = new WebRequestServices();
		}
	}
}