using CuttingEdge.Conditions;

namespace NetworkSolutionsAccess.Models.Configuration
{
	public sealed class NetworkSolutionsConfig
	{
		public string ApplicationName{ get; private set; }
		public string Certificate{ get; private set; }
		public string UserToken{ get; private set; }

		public NetworkSolutionsConfig( string applicationName, string certificate, string userToken )
		{
			Condition.Requires( applicationName, "applicationName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( certificate, "certificate" ).IsNotNullOrWhiteSpace();
			Condition.Requires( userToken, "userToken" ).IsNotNullOrWhiteSpace();

			this.ApplicationName = applicationName;
			this.Certificate = certificate;
			this.UserToken = userToken;
		}
	}
}