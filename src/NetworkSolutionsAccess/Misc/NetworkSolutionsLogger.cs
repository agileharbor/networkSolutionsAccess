using Netco.Logging;

namespace NetworkSolutionsAccess.Misc
{
	public static class NetworkSolutionsLogger
	{
		public static ILogger Log{ get; private set; }

		static NetworkSolutionsLogger()
		{
			Log = NetcoLogger.GetLogger( "NetworkSolutionsLogger" );
		}
	}
}