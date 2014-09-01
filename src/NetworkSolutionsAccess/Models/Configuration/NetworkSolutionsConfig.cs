using CuttingEdge.Conditions;

namespace NetworkSolutionsAccess.Models.Configuration
{
	public sealed class NetworkSolutionsConfig
	{
		public string UserKey{ get; private set; }

		public NetworkSolutionsConfig( string userKey )
		{
			Condition.Requires( userKey, "userKey" ).IsNotNullOrWhiteSpace();

			this.UserKey = userKey;
		}
	}
}