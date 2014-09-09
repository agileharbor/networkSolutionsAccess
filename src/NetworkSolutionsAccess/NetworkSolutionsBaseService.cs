using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public abstract class NetworkSolutionsBaseService
	{
		protected const int PageSize = 300;
		protected const decimal Version = 20.0M;
		protected readonly SecurityCredentialType Credentials;
		protected readonly NetSolEcomServiceSoapClient Client;
		internal readonly WebRequestServices WebRequestServices;

		protected NetworkSolutionsBaseService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this.Credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this.Client = new NetSolEcomServiceSoapClient();
			this.WebRequestServices = new WebRequestServices();
		}
	}
}