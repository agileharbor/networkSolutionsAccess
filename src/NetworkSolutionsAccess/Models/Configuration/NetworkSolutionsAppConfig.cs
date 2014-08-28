using CuttingEdge.Conditions;

namespace NetworkSolutionsAccess.Models.Configuration
{
	public sealed class NetworkSolutionsAppConfig
	{
		public string ApplicationName{ get; private set; }
		public string Certificate{ get; private set; }

		public NetworkSolutionsAppConfig( string applicationName, string certificate )
		{
			Condition.Requires( applicationName, "applicationName" ).IsNotNullOrWhiteSpace();
			Condition.Requires( certificate, "certificate" ).IsNotNullOrWhiteSpace();

			this.ApplicationName = applicationName;
			this.Certificate = certificate;
		}
	}
}