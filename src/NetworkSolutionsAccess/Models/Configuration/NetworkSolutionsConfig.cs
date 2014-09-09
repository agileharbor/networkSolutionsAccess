using CuttingEdge.Conditions;

namespace NetworkSolutionsAccess.Models.Configuration
{
	public sealed class NetworkSolutionsConfig
	{
		public string UserToken{ get; private set; }

		public NetworkSolutionsConfig( string userToken )
		{
			Condition.Requires( userToken, "userToken" ).IsNotNullOrWhiteSpace();

			this.UserToken = userToken;
		}
	}
}